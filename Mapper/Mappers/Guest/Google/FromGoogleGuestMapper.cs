using Mapper.Interfaces;
using SourceModel = Models.External.Google.Guest;
using TargetModel = Models.Internal.Guest;

namespace Mapper.Mappers.Guest.Google
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