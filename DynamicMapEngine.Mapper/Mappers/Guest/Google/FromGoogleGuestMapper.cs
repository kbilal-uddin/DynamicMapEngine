using DynamicMapEngine.Mapper.Interfaces;
using SourceModel = DynamicMapEngine.Models.External.Google.Guest;
using TargetModel = DynamicMapEngine.Models.Internal.Guest;

namespace DynamicMapEngine.Mapper.Mappers.Guest.Google
{
    public class FromGoogleGuestMapper : IObjectMapper<SourceModel, TargetModel>
    {
        public TargetModel Map(SourceModel source)
        {
            return new TargetModel
            {
                GuestId = source.Id,
                FullName = source.Name
            };
        }
    }
}