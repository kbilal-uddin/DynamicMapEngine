
namespace DynamicMapEngine.Common.Utils
{
    public static class ErrorCache
    {
        public static string InvalidSourceTargetType = "1000001";
        public static string InvalidSourceTargetTypeMessage = "{0} type cannot be null, must provide the {0}.";

        public static string UnsupportedType = "1000002";
        public static string UnsupportedTypeMessage = "Unsupported {0}: {1}";

        public static string InvalidEntityMatch = "1000003";
        public static string InvalidEntityMatchMessage = "Entity mismatch: cannot map from '{0}' to '{1}'.";

        public static string EmptyRequestBody = "1000004";
        public static string EmptyRequestBodyMessage = "Request body cannot be null, must provide the request.";

        public static string InvalidPayload = "1000005";
        public static string InvalidPayloadMessage = "Invalid request payload. Please check the input format.";

        public static string NoMatchingMapper = "1000006";
        public static string NoMatchingMapperMessage = "No matching mapper found for Source Type '{0}' and Target Type '{1}'. Please ensure the appropriate mapper exists.";

        public static string RequiredField = "1000007";
        public static string RequiredFieldMessage = "Invalid input: {0} is required and cannot be null or empty.";

        public static string InvalidModelFormat = "1000008";
        public static string InvalidModelFormatMessage = "Unable to convert provided data to {0}";

        public static string NoModelExists = "1000009";
        public static string NoModelExistsMessage = "Model does not exists: {0}";



    }
}
