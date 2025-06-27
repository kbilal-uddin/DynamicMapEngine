using Common.Extensions;
using Mapper.Mappers.Reservation.Google;
using InternalModels = Models.Internal;

namespace DynamicMapEngine.Tests.Mappers.Reservation.Google
{
    public class ToGoogleReservationMapperTests
    {
        [Fact]
        public void Map_ValidInternalReservation_ReturnsMappedGoogleReservation()
        {
            // Arrange
            var source = new InternalModels.Reservation
            {
                ReservationId = 101,
                CheckInDate = new DateTime(2025, 7, 15),
                CheckOutDate = new DateTime(2025, 7, 18),
                Guest = new InternalModels.Guest
                {
                    GuestId = 501,
                    FullName = "John Doe"
                },
                Room = new InternalModels.Room
                {
                    RoomID = "12",
                    RoomName = "Deluxe Sea View",
                    NoOfPersons = 1,
                    RoomDescription = "A deluxe room with ocean view, king-size bed, and balcony.",
                    RoomNumber = "D302"
                }
            };

            var mapper = new ToGoogleReservationMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(101, result.Id);
            Assert.Equal(new DateTime(2025, 7, 15), result.CheckInDate);
            Assert.Equal(new DateTime(2025, 7, 18), result.CheckOutDate);

            Assert.NotNull(result.GuestInfo);
            Assert.Equal(501, result.GuestInfo.Id);
            Assert.Equal("John Doe", result.GuestInfo.Name);

            Assert.NotNull(result.RoomInfo);
            Assert.Equal("12", result.RoomInfo.Id);
            Assert.Equal("Deluxe Sea View", result.RoomInfo.Name);
            Assert.Equal(1, result.RoomInfo.Capacity);
            Assert.Equal("A deluxe room with ocean view, king-size bed, and balcony.", result.RoomInfo.Description);
            Assert.Equal("D302", result.RoomInfo.Number);
        }

        [Fact]
        public void Map_MissingRoomId_ThrowsValidationException()
        {
            // Arrange
            var source = new InternalModels.Reservation
            {
                ReservationId = 101,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                Guest = new InternalModels.Guest
                {
                    GuestId = 501,
                    FullName = "John Doe"
                },
                Room = new InternalModels.Room
                {
                    RoomID = null,
                    RoomName = "Deluxe Sea View"
                }
            };

            var mapper = new ToGoogleReservationMapper();

            // Act & Assert
            var ex = Assert.Throws<StatusCodeException>(() => mapper.Map(source));

            Assert.Contains("RoomID", ex.Message);
        }
    }
}
