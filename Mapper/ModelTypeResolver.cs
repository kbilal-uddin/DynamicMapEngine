using DynamicMapEngine.Common.Extensions;
using DynamicMapEngine.Common.Utils;
using DynamicMapEngine.Models.Internal;
using DynamicMapEngine.Mapper.Interfaces;
using System.Net;
using System.Text.Json;
using static DynamicMapEngine.Mapper.Registry.MappingRegistry;

namespace Mapper
{

    public class ModelTypeResolver : IModelTypeResolver
    {

        private static readonly Dictionary<string, Type> _map = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { SupportedMappings.GoogleRoom.GetDescription(), typeof(DynamicMapEngine.Models.External.Google.Room) },
            { SupportedMappings.GoogleReservation.GetDescription(), typeof(DynamicMapEngine.Models.External.Google.Reservation) },
            { SupportedMappings.GoogleGuest.GetDescription(), typeof(DynamicMapEngine.Models.External.Google.Guest) },
            { SupportedMappings.InternalReservation.GetDescription(), typeof(DynamicMapEngine.Models.Internal.Reservation) },
            { SupportedMappings.InternalRoom.GetDescription(), typeof(DynamicMapEngine.Models.Internal.Room) },
            { SupportedMappings.InternalGuest.GetDescription(), typeof(DynamicMapEngine.Models.Internal.Guest) },
        };

        public Type ResolveType(string key)
        {
            if (_map.TryGetValue(key.ToLowerInvariant(), out var type))
                return type;

            throw new StatusCodeException(HttpStatusCode.InternalServerError, new Error { Code = ErrorCache.NoModelExists, UserMessage = ErrorCache.NoModelExistsMessage }, $"{key}");
        }

        public object CreateInstance(string key, params object[] constructorArgs)
        {
            var type = ResolveType(key);
            return Activator.CreateInstance(type, constructorArgs);
        }

        public object GetSourceObject(object data, Type sourceType)
        {
            try
            {
                var sourceObj = JsonSerializer.Deserialize((JsonElement)data, sourceType, JsonSerializerOptions.Default);

                if (sourceObj is not null)
                    return sourceObj;
                else
                    throw new StatusCodeException(HttpStatusCode.InternalServerError, new Error { Code = ErrorCache.InvalidModelFormat, UserMessage = ErrorCache.InvalidModelFormatMessage }, $"{sourceType.FullName}");
            }
            catch
            {
                throw new StatusCodeException(HttpStatusCode.InternalServerError,
                    new Error
                    {
                        Code = ErrorCache.InvalidPayload, 
                        UserMessage = ErrorCache.InvalidPayloadMessage
                    });
            }
        }
    }
}
