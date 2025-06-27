namespace DynamicMapEngine.Models.Internal
{
    public class Error
    {
        public string Code { get; set; }
        public string StackTrace { get; set; }
        public string UserMessage { get; set; }

        public string GetMessage(params object[] args)
        {
            if (args is null || args.Length == 0)
                return UserMessage;
            return string.Format(UserMessage, args);
        }

    }
}
