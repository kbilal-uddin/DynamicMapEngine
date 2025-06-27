using DynamicMapEngine.Models.Internal;
using System.Net;

namespace DynamicMapEngine.Common.Extensions
{
    public class StatusCodeException : Exception
    {
        public int StatusCode { get; }

        public string Code { get; }

        public string UserMessage { get; }

        public StatusCodeException(HttpStatusCode statusCode, string code, string userMessage)
            : base(userMessage)
        {
            StatusCode = (int)statusCode;
            Code = code;
            UserMessage = userMessage;
        }

        public StatusCodeException(HttpStatusCode statusCode, Error error, params object[] formatArgs)
        : base(error.GetMessage(formatArgs))
        {
            StatusCode = (int)statusCode;
            Code = error.Code;
            UserMessage = error.GetMessage(formatArgs);
        }
    }
}
