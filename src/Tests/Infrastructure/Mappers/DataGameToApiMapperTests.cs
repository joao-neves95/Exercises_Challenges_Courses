
using AutoFixture;

using FluentAssertions;

using GamingApi.WebApi.Core.Extensions;
using GamingApi.WebApi.Infrastructure.Entities;
using GamingApi.WebApi.Infrastructure.Mappers;

namespace GamingApi.WebApi.Tests.Infrastructure.Mappers
{
    public class DataGameToApiMapperTests : BaseTests
    {
        private readonly DataGameToApiMapper _sut = new();

        [Fact]
        public void Map_Passes()
        {
            var input = DataGenerator.Create<DataGame>();

            var result = _sut.Map(input);

            result.Id.Should().Be(input.AppId);
            result.Name.Should().Be(input.Name);
            result.ShortDescription.Should().Be(input.ShortDescription);
            result.Publisher.Should().Be(input.Publisher);
            result.ReleaseDate.Should().Be(input.ReleaseDate);
            result.RequiredAge.Should().Be(input.RequiredAge.GetStartIntNumber());
            result.Genre.Should().Be(input.Genre);
            result.Platforms.Linux.Should().Be(input.Platforms.Linux);
            result.Platforms.Windows.Should().Be(input.Platforms.Windows);
            result.Platforms.Mac.Should().Be(input.Platforms.Mac);

            for (var i = 0; i < result.Categories.Count(); ++i)
            {
                result.Categories.ElementAtOrDefault(i).Should().Be(input.Categories.ElementAtOrDefault(i));
            }
        }
    }
}
