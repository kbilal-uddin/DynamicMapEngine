using Mapper.Mappers.Reservation.Google;
using GoogleModels = Models.External.Google;

namespace DynamicMapEngine.Tests.Mappers.Reservation.Google
{
    public class FromGoogleReservationMapperTests
    {
        [Fact]
        public void Map_ValidSourceWithGuestAndRoom_ReturnsCorrectTarget()
        {
            // Arrange
            var source = new GoogleModels.Reservation
            {
                Id = 101,
                CheckInDate = new DateTime(2025, 7, 15),
                CheckOutDate = new DateTime(2025, 7, 18),
                GuestInfo = new GoogleModels.Guest
                {
                    Id = 501,
                    Name = "John Doe"
                },
                RoomInfo = new GoogleModels.Room
                {
                    Id = "12",
                    Name = "Deluxe Sea View",
                    Capacity = 1,
                    Description = "A deluxe room with ocean view, king-size bed, and balcony.",
                    Number = "D302"
                }
            };

            var mapper = new FromGoogleReservationMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(101, result.ReservationId);
            Assert.Equal(new DateTime(2025, 7, 15), result.CheckInDate);
            Assert.Equal(new DateTime(2025, 7, 18), result.CheckOutDate);

            Assert.NotNull(result.Guest);
            Assert.Equal(501, result.Guest.GuestId);
            Assert.Equal("John Doe", result.Guest.FullName);

            Assert.NotNull(result.Room);
            Assert.Equal("12", result.Room.RoomID);
            Assert.Equal("Deluxe Sea View", result.Room.RoomName);
            Assert.Equal(1, result.Room.NoOfPersons);
            Assert.Equal("A deluxe room with ocean view, king-size bed, and balcony.", result.Room.RoomDescription);
            Assert.Equal("D302", result.Room.RoomNumber);
        }
    }
}
