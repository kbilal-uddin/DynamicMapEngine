using Mapper.Helper;
using Mapper.Interfaces;
using SourceModel = Models.External.Google.Room;
using TargetModel = Models.Internal.Room;

namespace Mapper.Mappers.Room.Google
{
    public class FromGoogleRoomMapper : IObjectMapper<SourceModel, TargetModel>
    {
        public TargetModel Map(SourceModel source)
        {
            #region Validations

            // Dynamic validations: This method validates all properties in the target model
            // marked with [RequiredField] attribute to ensure required data is present.
            // Simply add [RequiredField] on target model properties to enforce validation.

            ValidationHelper.ValidateRequiredProperties(source);

            #endregion

            return new TargetModel
            {
                RoomID = source.Id,
                RoomName = source.Name,
                RoomDescription = source.Description,
                NoOfPersons = source.Capacity,
                RoomNumber = source.Number
            };

        }
    }
}
