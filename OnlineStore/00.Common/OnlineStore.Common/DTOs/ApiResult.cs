using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.Common.Message;

namespace OnlineStore.Common.DTOs
{
    public class ApiResult
    {
        #region Variables
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
        #endregion
        #region Constructor
        public ApiResult()
        {
            IsSuccess = true;
            Message = Messages.HttpCode_OK.ToString();
            Code = Messages.HttpCode_OK.GetCode();
        }
        public ApiResult(object model)
        {
            IsSuccess = true;
            Message = Messages.HttpCode_OK.ToString();
            Code = Messages.HttpCode_OK.GetCode();
            Data = model;
        }
        public ApiResult(object model, bool isSuccess = false, int code = 0)
        {
            IsSuccess = isSuccess;
            Code = code;
            Data = model;
        }
        public ApiResult(MessageTemplate message, bool isSuccess = false, object data = null)
        {
            IsSuccess = isSuccess;
            Message = message.ToString();
            Code = message.GetCode() != 0 ? message.GetCode() : (isSuccess ? Messages.HttpCode_OK.GetCode() : Messages.HttpCode_BadRequest.GetCode());
            Data = data;
        }
        public ApiResult(string message, bool isSuccess = false, object data = null, int code = 0)
        {
            IsSuccess = isSuccess;
            Message = message;
            Code = code;
            Data = data;
        }
        #endregion
        #region Implicit Operators

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult();
        }

        public static implicit operator ApiResult(OkObjectResult result)
        {
            return new ApiResult(result.Value);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(Messages.HttpCode_BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            return new ApiResult(result.Value, isSuccess: false, Messages.HttpCode_BadRequest.GetCode());
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(Messages.HttpCode_NotFound);
        }

        public static implicit operator ApiResult(UnauthorizedResult result)
        {
            return new ApiResult(Messages.HttpCode_Unauthorized);
        }
        #endregion
        public override string ToString()
        {
            return Message;
        }
    }

    public class ApiResult<T> 
    {
        #region Constructor        
        public ApiResult()
        {
            IsSuccess = true;
            Message = Messages.HttpCode_OK.ToString();
            Code = Messages.HttpCode_OK.GetCode();
        }

        public ApiResult(T model)
        {
            IsSuccess = true;
            Message = Messages.HttpCode_OK.ToString();
            Code = Messages.HttpCode_OK.GetCode();
            Data = model;
        }

        public ApiResult(T data = default, bool isSuccess = false, int code = 0)
        {
            IsSuccess = isSuccess;
            Data = data;
            Code = code;
        }

        public ApiResult(MessageTemplate message, bool isSuccess = false, T data = default)
        {
            IsSuccess = isSuccess;
            Message = message.ToString();
            Code = message.GetCode() != 0 ? message.GetCode() : (isSuccess ? Messages.HttpCode_OK.GetCode() : Messages.HttpCode_BadRequest.GetCode());
            Data = data;
        }

        public ApiResult(string message, bool isSuccess = false, T data = default, int code = 0)
        {
            IsSuccess = isSuccess;
            Message = message;
            Code = code;
            Data = data;
        }
        #endregion
        #region Variables
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
        #endregion
        #region Implicit Operators

        public static implicit operator ApiResult<T>(OkResult result)
        {
            return new ApiResult<T>();
        }

        public static implicit operator ApiResult<T>(OkObjectResult result)
        {
            return new ApiResult<T>((T)result.Value);
        }

        public static implicit operator ApiResult<T>(BadRequestResult result)
        {
            return new ApiResult<T>(Messages.HttpCode_BadRequest);
        }

        public static implicit operator ApiResult<T>(BadRequestObjectResult result)
        {
            return new ApiResult<T>((T)result.Value, false, Messages.HttpCode_BadRequest.GetCode());
        }

        public static implicit operator ApiResult<T>(ContentResult result)
        {
            return new ApiResult<T>(result.Content);
        }

        public static implicit operator ApiResult<T>(NotFoundResult result)
        {
            return new ApiResult<T>(Messages.HttpCode_NotFound);
        }

        public static implicit operator ApiResult<T>(UnauthorizedResult result)
        {
            return new ApiResult<T>(Messages.HttpCode_Unauthorized);
        }
        #endregion
        public override string ToString()
        {
            return Message;
        }
    }
}
