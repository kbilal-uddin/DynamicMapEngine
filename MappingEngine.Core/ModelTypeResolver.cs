using Common.Extensions;
using Common.Utils;
using DynamicMapEngine.Models.Internal;
using System.Net;
using System.Text.Json;
using static Mapper.Registry.MappingRegistry;

namespace Mapper
{

    public static class ModelTypeResolver
    {

        private static readonly Dictionary<string, Type> _map = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { SupportedMappings.GoogleRoom.GetDescription(), typeof(Models.External.Google.Room) },
            { SupportedMappings.GoogleReservation.GetDescription(), typeof(Models.External.Google.Reservation) },
            { SupportedMappings.GoogleGuest.GetDescription(), typeof(Models.External.Google.Guest) },
            { SupportedMappings.InternalReservation.GetDescription(), typeof(Models.Internal.Reservation) },
            { SupportedMappings.InternalRoom.GetDescription(), typeof(Models.Internal.Room) },
            { SupportedMappings.InternalGuest.GetDescription(), typeof(Models.Internal.Guest) },
        };

        public static Type ResolveType(string key)
        {
            if (_map.TryGetValue(key.ToLowerInvariant(), out var type))
                return type;

            throw new StatusCodeException(HttpStatusCode.InternalServerError, new Error { Code = ErrorCache.NoModelExists, UserMessage = ErrorCache.NoModelExistsMessage }, $"{key}");
        }

        public static object CreateInstance(string key, params object[] constructorArgs)
        {
            var type = ResolveType(key);
            return Activator.CreateInstance(type, constructorArgs);
        }

        public static object GetSourceObject(object data, Type sourceType)
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
