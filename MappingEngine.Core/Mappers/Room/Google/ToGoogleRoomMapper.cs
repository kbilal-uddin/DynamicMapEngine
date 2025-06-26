using Mapper.Helper;
using Mapper.Interfaces;
using SourceModel = Models.Internal.Room;
using TargetModel = Models.External.Google.Room;

namespace Mapper.Mappers.Room.Google
{
    public class ToGoogleRoomMapper : IObjectMapper<SourceModel, TargetModel>
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
                Id = source.RoomID,
                Name = source.RoomName,
                Description = source.RoomDescription,
                Capacity = source.NoOfPersons,
                Number = source.RoomNumber
            };
        }
    }
}
