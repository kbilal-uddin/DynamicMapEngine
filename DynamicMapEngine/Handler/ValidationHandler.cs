using DynamicMapEngine.Common.Extensions;
using DynamicMapEngine.Common.Utils;
using DynamicMapEngine.Models.Internal;
using DynamicMapEngine.Mapper.Registry;
using System.Net;
using static DynamicMapEngine.Mapper.Registry.MappingRegistry;

namespace DynamicMapEngine.Handler
{
    public class ValidationHandler
    {
        public void Validate(object data, string sourceType, string targetType)
        {
            #region Null/Empty Checks

            if (string.IsNullOrEmpty(sourceType))
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.InvalidSourceTargetType, UserMessage = ErrorCache.InvalidSourceTargetTypeMessage }, "Source");

            if (string.IsNullOrEmpty(targetType))
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.InvalidSourceTargetType, UserMessage = ErrorCache.InvalidSourceTargetTypeMessage }, "Target");

            #endregion

            #region Supported Type Checks

            if (!IsValidMappingType(sourceType))
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.UnsupportedType, UserMessage = ErrorCache.UnsupportedTypeMessage }, "sourceType", $"{sourceType}");

            if (!IsValidMappingType(targetType))
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.UnsupportedType, UserMessage = ErrorCache.UnsupportedTypeMessage }, "targetType", $"{targetType}");

            #endregion

            string sourceEntity = GetEntityName(sourceType);
            string targetEntity = GetEntityName(targetType);

            if (!sourceEntity.Equals(targetEntity, StringComparison.OrdinalIgnoreCase))
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.InvalidEntityMatch, UserMessage = ErrorCache.InvalidEntityMatchMessage }, $"{sourceEntity}", $"{targetEntity}");

            if (data is null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, new Error { Code = ErrorCache.EmptyRequestBody, UserMessage = ErrorCache.EmptyRequestBodyMessage });
        }

        private static bool IsValidMappingType(string type)
        {
            return Enum.GetValues(typeof(SupportedMappings))
                       .Cast<SupportedMappings>()
                       .Any(e => MappingRegistry.GetDescription(e).Equals(type, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetEntityName(string mappingType)
        {
            var parts = mappingType.Split('.');
            return parts.Length == 2 ? parts[1].ToLowerInvariant() : string.Empty;
        }
    }
}
