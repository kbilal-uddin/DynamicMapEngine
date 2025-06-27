using DynamicMapEngine.Mapper.Interfaces;
using SourceModel = DynamicMapEngine.Models.Internal.Guest;
using TargetModel = DynamicMapEngine.Models.External.Google.Guest;

namespace DynamicMapEngine.Mapper.Mappers.Guest.Google
{
    public class ToGoogleGuestMapper : IObjectMapper<SourceModel, TargetModel>
    {
        public TargetModel Map(SourceModel source)
        {
            return new TargetModel
            {
                Id = source.GuestId,
                Name = source.FullName
            };
        }
    }
}
