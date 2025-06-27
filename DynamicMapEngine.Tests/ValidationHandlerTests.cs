using DynamicMapEngine.Common.Extensions;
using DynamicMapEngine.Common.Utils;
using DynamicMapEngine.Handler;
using System.Net;

namespace DynamicMapEngine.Tests
{
    public class ValidationHandlerTests
    {
        private readonly ValidationHandler _handler = new();

        [Theory]
        [InlineData(null, "internal.room")]
        [InlineData("", "internal.room")]
        [InlineData("internal.room", null)]
        [InlineData("internal.room", "")]
        public void Validate_ShouldThrow_WhenSourceOrTargetTypeIsNullOrEmpty(string sourceType, string targetType)
        {
            var exception = Assert.Throws<StatusCodeException>(() =>
                _handler.Validate(new { }, sourceType, targetType));

            Assert.Equal((int)HttpStatusCode.BadRequest, ((StatusCodeException)exception).StatusCode);
            Assert.Equal(ErrorCache.InvalidSourceTargetType, ((StatusCodeException)exception).Code);
        }

        [Theory]
        [InlineData("unknown.source", "internal.room")]
        [InlineData("google.room", "random.target")]
        public void Validate_ShouldThrow_WhenUnsupportedMappingTypes(string sourceType, string targetType)
        {
            var exception = Assert.Throws<StatusCodeException>(() =>
                _handler.Validate(new { }, sourceType, targetType));

            Assert.Equal((int)HttpStatusCode.BadRequest, ((StatusCodeException)exception).StatusCode);
            Assert.Equal(ErrorCache.UnsupportedType, ((StatusCodeException)exception).Code);
        }

        [Fact]
        public void Validate_ShouldThrow_WhenSourceAndTargetEntitiesDoNotMatch()
        {
            var exception = Assert.Throws<StatusCodeException>(() =>
                _handler.Validate(new { }, "google.room", "internal.guest"));

            Assert.Equal((int)HttpStatusCode.BadRequest, ((StatusCodeException)exception).StatusCode);
            Assert.Equal(ErrorCache.InvalidEntityMatch, ((StatusCodeException)exception).Code);
        }

        [Fact]
        public void Validate_ShouldThrow_WhenDataIsNull()
        {
            var exception = Assert.Throws<StatusCodeException>(() =>
                _handler.Validate(null, "google.room", "internal.room"));

            Assert.Equal((int)HttpStatusCode.BadRequest, ((StatusCodeException)exception).StatusCode);
            Assert.Equal(ErrorCache.EmptyRequestBody, ((StatusCodeException)exception).Code);
        }

        [Fact]
        public void Validate_ShouldPass_ForValidInput()
        {
            var data = new { };
            var sourceType = "google.room";
            var targetType = "internal.room";

            var exception = Record.Exception(() => _handler.Validate(data, sourceType, targetType));

            Assert.Null(exception);
        }
    }
}
