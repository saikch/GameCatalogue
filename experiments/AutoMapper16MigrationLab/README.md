# AutoMapper 16.2.0 Migration Lab

This is a **representative** .NET 8 experiment based on the AutoMapper patterns observed in OperationsPortal. It does not contain LifeLabs source code.

It validates two approaches:

1. **Minimal migration** — retain manual `MapperConfiguration` and supply an `ILoggerFactory`.
2. **Centralized/future-safe design** — put mappings in a `Profile`, register AutoMapper once through DI, and inject/reuse `IMapper`.

Representative mappings tested:

- `Comment.Id -> CommentDto.CommentId`
- `Comment.UserId -> CommentDto.EditedUserGuid`
- `QueryRule -> QueryRuleModel`

The program calls `AssertConfigurationIsValid()`, verifies mapping behavior, verifies that DI-resolved `IMapper` instances share one configuration provider, and prints a small illustrative timing comparison between reusing one configured mapper and repeatedly constructing `MapperConfiguration`.

## Observed results

GitHub Actions on .NET 8 completed successfully with AutoMapper 16.2.0:

- Restore: passed
- Build: passed with 0 warnings and 0 errors
- Manual `MapperConfiguration` + `ILoggerFactory`: passed
- Centralized `Profile` + DI `IMapper`: passed
- Shared DI configuration provider: passed
- 100,000 mappings using the centralized mapper: 41 ms in this run
- 500 repeated configure+map operations: 664 ms in this run

The timing output is **not a production benchmark** and the two loops perform different amounts of setup work. It demonstrates that repeatedly building mapping configuration has meaningful overhead compared with reusing already-built configuration.

## Dependency finding

An initial experiment explicitly pinned `Microsoft.Extensions.Logging.Abstractions` 8.0.2. Restore failed with `NU1605` because AutoMapper 16.2.0 requires `Microsoft.Extensions.Logging.Abstractions >= 10.0.0`. Removing the old direct pin allowed AutoMapper to resolve its required dependency and the .NET 8 build/run succeeded.

The actual OperationsPortal solution must therefore be checked for any direct 8.x pin of `Microsoft.Extensions.Logging.Abstractions` (or another dependency constraint that forces it lower) before concluding that the package-only upgrade is clean.

The actual OperationsPortal solution still needs its own restore/build/test validation before any refactor is accepted.
