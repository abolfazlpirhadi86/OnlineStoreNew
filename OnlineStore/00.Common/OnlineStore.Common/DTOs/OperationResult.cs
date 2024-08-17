namespace OnlineStore.Common.DTOs
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public OperationResult Success(string message = "")
        {
            IsSuccess = true;
            Message = message;
            return this;
        }

        public OperationResult Fail(string message = "")
        {
            IsSuccess = false;
            Message = message;
            return this;
        }

    }
}
