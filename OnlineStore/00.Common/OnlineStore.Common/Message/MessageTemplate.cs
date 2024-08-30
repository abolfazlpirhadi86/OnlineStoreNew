namespace OnlineStore.Common.Message
{
    public class MessageTemplate
    {
        private readonly string _message;
        private readonly int _code;

        public MessageTemplate(string text, int code)
        {
            _message = text;
            _code = code;
        }
        public override string ToString()
        {
            return _message;
        }
        public int GetCode()
        {
            return _code;
        }
    }
}
