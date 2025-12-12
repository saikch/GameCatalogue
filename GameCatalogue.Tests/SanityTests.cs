using Xunit;
using FluentAssertions;

public class SanityTests
{
    [Fact]
    public void TestRunner_Works()
    {
        true.Should().BeTrue();
    }
}
