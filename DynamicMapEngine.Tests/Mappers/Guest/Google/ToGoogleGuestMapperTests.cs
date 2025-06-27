using Mapper.Mappers.Guest.Google;
using InternalModels = Models.Internal;

namespace DynamicMapEngine.Tests.Mappers.Guest.Google
{
    public class ToGoogleGuestMapperTests
    {
        [Fact]
        public void Map_ValidInternalGuest_ReturnsGoogleGuest()
        {
            // Arrange
            var source = new InternalModels.Guest
            {
                GuestId = 501,
                FullName = "John Doe"
            };

            var mapper = new ToGoogleGuestMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(501, result.Id);
            Assert.Equal("John Doe", result.Name);
        }
    }
}
