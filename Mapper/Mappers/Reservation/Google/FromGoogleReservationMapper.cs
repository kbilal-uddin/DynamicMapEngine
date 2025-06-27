using Mapper.Helper;
using Mapper.Interfaces;
using SourceModel = Models.External.Google.Reservation;
using TargetModel = Models.Internal.Reservation;

namespace Mapper.Mappers.Reservation.Google
{
    public class FromGoogleReservationMapper : IObjectMapper<SourceModel, TargetModel>
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
                ReservationId = source.Id,
                CheckInDate = source.CheckInDate,
                CheckOutDate = source.CheckOutDate,
                Guest = source.GuestInfo is not null ? new Guest.Google.FromGoogleGuestMapper().Map(source.GuestInfo) : null,
                Room = source.RoomInfo is not null ? new Room.Google.FromGoogleRoomMapper().Map(source.RoomInfo) : null
            };

            ValidationHelper.ValidateRequiredProperties(result);

            return result;

        }
    }
}