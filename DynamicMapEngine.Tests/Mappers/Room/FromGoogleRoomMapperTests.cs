using DynamicMapEngine.Mapper.Mappers.Room.Google;
using GoogleModels = DynamicMapEngine.Models.External.Google;

namespace DynamicMapEngine.Tests.Mappers.Room
{
    public class FromGoogleRoomMapperTests
    {
        [Fact]
        public void Map_ValidGoogleRoom_ReturnsInternalRoom()
        {
            // Arrange
            var source = new GoogleModels.Room
            {
                Id = "R123",
                Name = "Deluxe Sea View",
                Description = "A deluxe room with ocean view, king-size bed, and balcony.",
                Capacity = 2,
                Number = "D302"
            };

            var mapper = new FromGoogleRoomMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("R123", result.RoomID);
            Assert.Equal("Deluxe Sea View", result.RoomName);
            Assert.Equal("A deluxe room with ocean view, king-size bed, and balcony.", result.RoomDescription);
            Assert.Equal(2, result.NoOfPersons);
            Assert.Equal("D302", result.RoomNumber);
        }

        [Fact]
        public void Map_MissingRequiredField_ThrowsValidationException()
        {
            // Arrange
            var source = new GoogleModels.Room
            {
                Id = "102",
                Name = "Deluxe Sea View",
                Description = "A deluxe room with ocean view, king-size bed, and balcony.",
                Number = "D302"
            };

            var mapper = new FromGoogleRoomMapper();

            // Act & Assert
            var ex = Assert.ThrowsAny<Exception>(() => mapper.Map(source));

            Assert.Contains("Capacity", ex.Message);
        }
    }
}
