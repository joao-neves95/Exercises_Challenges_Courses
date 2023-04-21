
using FluentAssertions;

using GamingApi.WebApi.Tests.Mocks;

using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Tests.Core.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ThrowIfNull_ShouldThrow_IfNullClass()
        {
            MockClass mockClass = null!;

            var action = () => mockClass.ThrowIfNull();

            action.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(MockClass));
        }

        [Fact]
        public void ThrowIfNull_ShouldNotThrow_IfNotNullClass()
        {
            MockClass mockClass = new();

            var action = () => mockClass.ThrowIfNull();

            action.Should().NotThrow();
        }
    }
}
