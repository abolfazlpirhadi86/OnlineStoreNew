using Microsoft.AspNetCore.Http;
using System.Text;

namespace OnlineStore.Common.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        public static async Task<string> GetRequestBody(this HttpContext httpContext)
        {
            var requestBody = new StringBuilder();
            httpContext.Request.EnableBuffering();
            var reader = new StreamReader(httpContext.Request.Body);
            requestBody.Append(await reader.ReadToEndAsync());
            httpContext.Request.Body.Position = 0L;
            var result = requestBody.ToString();
            return result;
        }
    }
}
