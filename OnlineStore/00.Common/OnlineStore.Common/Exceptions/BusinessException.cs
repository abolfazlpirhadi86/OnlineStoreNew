using OnlineStore.Common.Message;

namespace OnlineStore.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public MessageTemplate MessageTemplate { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }


        public BusinessException()
        {

        }
        public BusinessException(string message) : base(message.ToString())
        {
            Message = message;
        }
        public BusinessException(MessageTemplate message) : base(message.ToString())
        {
            MessageTemplate = message;
        }
        public BusinessException(string message, object data) : base(message.ToString())
        {
            Message = message;
            Data = data;
        }
        public BusinessException(MessageTemplate message, object data) : base(message.ToString())
        {
            MessageTemplate = message;
            Data = data;
        }
        public BusinessException(string message, params string[] messageParameters) : base(message.ToString())
        {
            Message = SetParametersToMessage(message.ToString(), messageParameters);
        }
        public BusinessException(MessageTemplate message, params string[] messageParameters) : base(message.ToString())
        {
            MessageTemplate = new MessageTemplate(SetParametersToMessage(message.ToString(), messageParameters), message.GetCode());
        }
        public BusinessException(string message, object data, params string[] messageParameters) : base(message.ToString())
        {
            Message = SetParametersToMessage(message.ToString(), messageParameters);
            Data = data;
        }
        public BusinessException(MessageTemplate message, object data, params string[] messageParameters) : base(message.ToString())
        {
            MessageTemplate = new MessageTemplate(SetParametersToMessage(message.ToString(), messageParameters), message.GetCode());
            Data = data;
        }
        public override string ToString()
        {
            var result = MessageTemplate != null ? MessageTemplate.ToString() : Message;
            return result;
        }
        private static string SetParametersToMessage(string message, params string[] messageParameters)
        {
            var result = message;
            for (int i = 0; i < messageParameters.Length; i++)
            {
                var placeHolder = $"{{{i}}}";
                result = result.Replace(placeHolder, messageParameters[i]);
            }
            return result;
        }
    }
}
