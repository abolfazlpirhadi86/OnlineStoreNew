namespace Common.Results
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public OperationResult() 
        {
            IsSuccess = false;
        }

        public OperationResult Success(string message= Messages.Message.RegisterSuccess)
        {
            IsSuccess = true;
            Message = message;
            return this;
        }

        public OperationResult Fail(string message)
        {
            IsSuccess = false;
            Message = message;
            return this;
        }
    }
}
