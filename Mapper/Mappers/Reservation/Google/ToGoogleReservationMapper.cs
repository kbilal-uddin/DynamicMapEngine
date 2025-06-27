using Mapper.Helper;
using Mapper.Interfaces;
using SourceModel = Models.Internal.Reservation;
using TargetModel = Models.External.Google.Reservation;

namespace Mapper.Mappers.Reservation.Google
{
    public class ToGoogleReservationMapper : IObjectMapper<SourceModel, TargetModel>
    {
        public TargetModel Map(SourceModel source)
        {
            #region Validations

            // Dynamic validations: This method validates all properties in the target model
            // marked with [RequiredField] attribute to ensure required data is present.
            // Simply add [RequiredField] on target model properties to enforce validation.

            ValidationHelper.ValidateRequiredProperties(source);

            #endregion
            var result = new TargetModel
            {
                Id = source.ReservationId,
                CheckInDate = source.CheckInDate,
                CheckOutDate = source.CheckOutDate,
                GuestInfo = source.Guest is not null ? new Guest.Google.ToGoogleGuestMapper().Map(source.Guest) : null,
                RoomInfo = source.Room is not null ? new Room.Google.ToGoogleRoomMapper().Map(source.Room) : null
            };

            ValidationHelper.ValidateRequiredProperties(result);

            return result;
        }
    }
}
