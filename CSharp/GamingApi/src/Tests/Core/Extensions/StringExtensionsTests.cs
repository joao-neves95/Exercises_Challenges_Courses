using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using GamingApi.WebApi.Core.Extensions;
using GamingApi.WebApi.Tests.Mocks;

namespace GamingApi.WebApi.Tests.Core.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("18+", 18)]
        [InlineData("3gbny", 3)]
        [InlineData("x", 0)]
        public void GetStartIntNumber_Should_Parse(string input, int expectedOutput)
        {
            input.GetStartIntNumber().Should().Be(expectedOutput);
        }
    }
}
