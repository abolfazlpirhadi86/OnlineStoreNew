using Microsoft.AspNetCore.Http;

namespace OnlineStore.Common.DTOs.Log
{
    public class LogRequestDTO : BaseLogDTO
    {
        public HttpContext HttpContext { get; set; }
        public string RequestBody { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public System.Exception Exception { get; set; }
    }
}
