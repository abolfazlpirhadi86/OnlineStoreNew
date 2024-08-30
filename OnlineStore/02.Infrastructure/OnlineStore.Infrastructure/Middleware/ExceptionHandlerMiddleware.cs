using Microsoft.AspNetCore.Http;
using OnlineStore.Common.DTOs.Log;
using OnlineStore.Common.Extensions;
using OnlineStore.Common.Services.Abstractions;
using OnlineStore.Common.Utilities;
using System.Diagnostics;

namespace OnlineStore.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISerializerService _serializerService;
        public ExceptionHandlerMiddleware(RequestDelegate next, ISerializerService serializerService)
        {
            _next = next;
            _serializerService = serializerService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var requestBody = string.Empty;
            Exception? logException = null;
            try
            {
                requestBody = await httpContext.GetRequestBody();
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                logException = exception;

                var response = httpContext.Response;
                var responseModel = ExceptionUtilities.CreateApiResultResponseException(response, exception);
                await response.WriteAsync(_serializerService.Serialize(responseModel));

            }

            stopwatch.Stop();
            LogUtilities.LogRequest(new LogRequestDTO
            {
                HttpContext = httpContext,
                RequestBody = requestBody,
                ElapsedTime = stopwatch.Elapsed,
                Exception = logException
            });
        }
    }
}
