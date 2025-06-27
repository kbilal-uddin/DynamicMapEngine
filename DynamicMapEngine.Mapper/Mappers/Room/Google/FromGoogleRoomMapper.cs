using DynamicMapEngine.Mapper.Helper;
using DynamicMapEngine.Mapper.Interfaces;
using SourceModel = DynamicMapEngine.Models.External.Google.Room;
using TargetModel = DynamicMapEngine.Models.Internal.Room;

namespace DynamicMapEngine.Mapper.Mappers.Room.Google
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

            var result = new TargetModel
            {
                RoomID = source.Id,
                RoomName = source.Name,
                RoomDescription = source.Description,
                NoOfPersons = source.Capacity,
                RoomNumber = source.Number
            };

            ValidationHelper.ValidateRequiredProperties(result);

            return result;
        }
    }
}
