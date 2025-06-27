using Mapper.Interfaces;
using SourceModel = Models.Internal.Guest;
using TargetModel = Models.External.Google.Guest;

namespace Mapper.Mappers.Guest.Google
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
