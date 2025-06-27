using DynamicMapEngine.Mapper.Helper;
using DynamicMapEngine.Mapper.Interfaces;
using SourceModel = DynamicMapEngine.Models.Internal.Room;
using TargetModel = DynamicMapEngine.Models.External.Google.Room;

namespace DynamicMapEngine.Mapper.Mappers.Room.Google
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

            var result = new TargetModel
            {
                Id = source.RoomID,
                Name = source.RoomName,
                Description = source.RoomDescription,
                Capacity = source.NoOfPersons,
                Number = source.RoomNumber
            };

            ValidationHelper.ValidateRequiredProperties(result);

            return result;
        }
    }
}
