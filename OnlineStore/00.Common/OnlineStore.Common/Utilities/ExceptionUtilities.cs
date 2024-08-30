using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.DTOs;
using OnlineStore.Common.Exceptions;
using OnlineStore.Common.Message;
using OnlineStore.Common.Services.Abstractions;
using System.Net;

namespace OnlineStore.Common.Utilities
{
    public static class ExceptionUtilities
    {
        private static ISerializerService? _serializerService;
        public static void ConfigureDI(ISerializerService serializerService)
        {
            _serializerService = serializerService;
        }

        public static ApiResult CreateApiResultResponseException(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            ApiResult responseModel;
            switch (exception)
            {
                case BusinessException e:
                    responseModel = e.MessageTemplate != null ?
                    new ApiResult(e.MessageTemplate, data: e.Data) :
                        new ApiResult(e.Message, code: HttpStatusCode.BadRequest.GetHashCode(), data: e.Data);
                    response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                    break;
                default:
                    var exceptioType = exception.GetType();
                    if (exceptioType == typeof(DbUpdateException))
                    {
                        var exceptionMessage = exception.GetBaseException().Message;
                        if (exceptionMessage.Contains("with the REFERENCE constraint"))
                        {
                            responseModel = new ApiResult(Messages.RecordHasReference);
                            response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                        }
                        else if (exceptionMessage.Contains("with the FOREIGN KEY constraint"))
                        {
                            responseModel = new ApiResult(Messages.ForeignKeyConstraint);
                            response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                        }
                        else if (exceptionMessage.Contains("The duplicate key value is"))
                        {
                            responseModel = new ApiResult(Messages.UniqueIndex);
                            response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                        }
                        else
                        {
                            responseModel = new ApiResult(Messages.HttpCode_InternalServerError);
                            response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                        }
                    }
                    else if (exceptioType == typeof(DbUpdateConcurrencyException))
                    {
                        responseModel = new ApiResult(Messages.DbUpdateConcurrencyException);
                        response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                    }
                    else if (exception.InnerException?.GetType() == typeof(BusinessException))
                    {
                        var e = (BusinessException)exception.InnerException;
                        responseModel = e.MessageTemplate != null ?
                        new ApiResult(e.MessageTemplate, data: e.Data) :
                        new ApiResult(e.Message, code: HttpStatusCode.BadRequest.GetHashCode(), data: e.Data);
                        response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                    }
                    else
                    {
                        responseModel = new ApiResult(Messages.HttpCode_InternalServerError);
                        response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    }
                    break;
            }
            return responseModel;
        }
        public static string GetExceptionDetail(Exception exception)
        {
            var exceptionModel = new
            {
                exception.Message,
                exception.StackTrace,
                InnerExceptionMessage = exception.InnerException?.Message
            };

            var result = _serializerService.Serialize(exceptionModel);
            return result;
        }
    }
}
