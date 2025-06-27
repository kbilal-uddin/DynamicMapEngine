using DynamicMapEngine.Mapper.Mappers.Guest.Google;
using GoogleModels = DynamicMapEngine.Models.External.Google;

namespace DynamicMapEngine.Tests.Mappers.Guest.Google
{
    public class FromGoogleGuestMapperTests
    {
        [Fact]
        public void Map_ValidGoogleGuest_ReturnsInternalGuest()
        {
            // Arrange
            var source = new GoogleModels.Guest
            {
                Id = 501,
                Name = "John Doe"
            };

            var mapper = new FromGoogleGuestMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(501, result.GuestId);
            Assert.Equal("John Doe", result.FullName);
        }
    }
}
