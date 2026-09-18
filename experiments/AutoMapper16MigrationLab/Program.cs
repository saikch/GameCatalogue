using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;

Console.WriteLine("AutoMapper 16.2.0 migration lab");
Console.WriteLine("--------------------------------");

// Representative OperationsPortal models.
var comment = new Comment { Id = 101, UserId = Guid.NewGuid(), Text = "hello" };
var rule = new QueryRule { Id = 7, Name = "Active" };

// A. Minimal migration: existing manual MapperConfiguration pattern,
// adapted to AutoMapper 16 by supplying ILoggerFactory.
var manualConfiguration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Comment, CommentDto>()
        .ForMember(d => d.CommentId, o => o.MapFrom(s => s.Id))
        .ForMember(d => d.EditedUserGuid, o => o.MapFrom(s => s.UserId));

    cfg.CreateMap<QueryRule, QueryRuleModel>();
}, NullLoggerFactory.Instance);

manualConfiguration.AssertConfigurationIsValid();
var manualMapper = manualConfiguration.CreateMapper();

AssertCommentMapping(manualMapper, comment);
AssertQueryRuleMapping(manualMapper, rule);
Console.WriteLine("PASS: manual MapperConfiguration + ILoggerFactory");

// B. Future-safe design: one DI-owned AutoMapper configuration/profile
// shared by controllers and handlers through IMapper.
var services = new ServiceCollection();
services.AddSingleton<ILoggerFactory>(NullLoggerFactory.Instance);
services.AddAutoMapper(cfg =>
{
    // Intentionally no real license secret in this public test repository.
}, typeof(OperationsPortalProfile));

using var provider = services.BuildServiceProvider();
var mapper = provider.GetRequiredService<IMapper>();

mapper.ConfigurationProvider.AssertConfigurationIsValid();
AssertCommentMapping(mapper, comment);
AssertQueryRuleMapping(mapper, rule);
Console.WriteLine("PASS: centralized Profile + DI IMapper");

// C. AddAutoMapper may create IMapper instances per resolution, but they should
// reuse the same configuration provider. The expensive configuration should
// not be rebuilt by each controller/handler.
var mapperAgain = provider.GetRequiredService<IMapper>();
if (!ReferenceEquals(mapper.ConfigurationProvider, mapperAgain.ConfigurationProvider))
{
    throw new InvalidOperationException("Expected DI-resolved IMapper instances to share one configuration provider.");
}
Console.WriteLine("PASS: DI-resolved IMapper instances share one configuration provider");

// D. Lightweight comparative timing. This is not a production benchmark;
// it only demonstrates the cost difference between reusing configuration
// and repeatedly constructing MapperConfiguration.
const int mapIterations = 100_000;
var stopwatch = Stopwatch.StartNew();
for (var i = 0; i < mapIterations; i++)
{
    _ = mapper.Map<CommentDto>(comment);
}
stopwatch.Stop();
Console.WriteLine($"Centralized IMapper: {mapIterations:N0} mappings in {stopwatch.ElapsedMilliseconds} ms");

const int configurationIterations = 500;
stopwatch.Restart();
for (var i = 0; i < configurationIterations; i++)
{
    var configuration = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Comment, CommentDto>()
            .ForMember(d => d.CommentId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.EditedUserGuid, o => o.MapFrom(s => s.UserId));
    }, NullLoggerFactory.Instance);

    var localMapper = configuration.CreateMapper();
    _ = localMapper.Map<CommentDto>(comment);
}
stopwatch.Stop();
Console.WriteLine($"Repeated configuration: {configurationIterations:N0} configure+map operations in {stopwatch.ElapsedMilliseconds} ms");

Console.WriteLine("--------------------------------");
Console.WriteLine("ALL TESTS PASSED");

static void AssertCommentMapping(IMapper mapper, Comment source)
{
    var result = mapper.Map<CommentDto>(source);

    if (result.CommentId != source.Id)
        throw new InvalidOperationException("Comment.Id -> CommentDto.CommentId mapping failed.");

    if (result.EditedUserGuid != source.UserId)
        throw new InvalidOperationException("Comment.UserId -> CommentDto.EditedUserGuid mapping failed.");

    if (result.Text != source.Text)
        throw new InvalidOperationException("Comment.Text mapping failed.");
}

static void AssertQueryRuleMapping(IMapper mapper, QueryRule source)
{
    var result = mapper.Map<QueryRuleModel>(source);

    if (result.Id != source.Id || result.Name != source.Name)
        throw new InvalidOperationException("QueryRule -> QueryRuleModel mapping failed.");
}

public sealed class OperationsPortalProfile : Profile
{
    public OperationsPortalProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(d => d.CommentId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.EditedUserGuid, o => o.MapFrom(s => s.UserId));

        CreateMap<QueryRule, QueryRuleModel>();
    }
}

public sealed class Comment
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
}

public sealed class CommentDto
{
    public int CommentId { get; set; }
    public Guid EditedUserGuid { get; set; }
    public string Text { get; set; } = string.Empty;
}

public sealed class QueryRule
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class QueryRuleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
