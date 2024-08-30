using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using OnlineStore.Common.DTOs.Log;
using OnlineStore.Common.Exceptions;
using OnlineStore.Common.Services.Abstractions;
using Serilog;
using Serilog.Context;
using mainSerilog = Serilog;

namespace OnlineStore.Common.Utilities
{
    public static class LogUtilities
    {
        private static ILoggerFactory _loggerFactory;
        private static ISerializerService _serializerService;
        public static void ConfigureDI(ILoggerFactory loggerFactory, ISerializerService serializerService)
        {
            _loggerFactory = loggerFactory;
            _serializerService = serializerService;
        }
        public static void LogRequest(LogRequestDTO model)
        {
            LogContext.PushProperty("SourceContext", "TataLogRequest");
            LogContext.PushProperty("Url", $"{model.HttpContext.Request.Method} {model.HttpContext.Request.Path}{model.HttpContext.Request.QueryString}");
            LogContext.PushProperty("HttpStatusCode", model.HttpContext.Response.StatusCode);
            LogContext.PushProperty("ElapsedTime", model.ElapsedTime);
            if (!string.IsNullOrWhiteSpace(model.RequestBody))
            {
                int num = 10240;
                if (model.RequestBody.Length > num)
                {
                    model.RequestBody = model.RequestBody.Substring(0, num - 1);
                }

                LogContext.PushProperty("RequestBody", model.RequestBody);
            }

            if (model.Exception == null)
            {
                Log.Information(string.Empty);
            }
            else if (model.Exception is BusinessException || (model.Exception.GetType() == typeof(AggregateException) && model.Exception.InnerException != null && model.Exception.InnerException is BusinessException))
            {
                BusinessException ex = ((model.Exception is BusinessException) ? model.Exception : model.Exception.InnerException) as BusinessException;
                LogContext.PushProperty("BusinessExceptionMessage", ex.ToString());
                if (ex.Data != null)
                {
                    LogContext.PushProperty("BusinessExceptionData", ex.Data);
                }

                Log.Warning(string.Empty);
            }
            else
            {
                if (model.Exception.InnerException != null)
                {
                    LogContext.PushProperty("InnerExceptionMessage", model.Exception.InnerException.Message);
                }
                Log.Error(model.Exception, string.Empty);
            }
        }
        
    }
}
