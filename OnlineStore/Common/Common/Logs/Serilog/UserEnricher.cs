using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logs.Serilog
{
    public class UserEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
        {
            logEvent.AddPropertyIfAbsent(factory.CreateProperty("UserId", _httpContextAccessor?.HttpContext?.User?.GetClaim("userId") ?? string.Empty));
            logEvent.AddPropertyIfAbsent(factory.CreateProperty("UserName", _httpContextAccessor?.HttpContext?.User?.GetClaim("userName") ?? string.Empty));
            logEvent.AddPropertyIfAbsent(factory.CreateProperty("UserFullName", $"{_httpContextAccessor?.HttpContext?.User?.GetClaim("firstName") ?? string.Empty} {_httpContextAccessor?.HttpContext?.User?.GetClaim("lastName") ?? string.Empty}"));

            var xForwardedFor = _httpContextAccessor?.HttpContext?.Request?.Headers["X-Forwarded-For"];
            var remoteIpAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress;
            var localIpAddress = _httpContextAccessor?.HttpContext?.Connection?.LocalIpAddress;
            var userIpList = $"X-Forwarded-For = {xForwardedFor} , RemoteIpAddress = {remoteIpAddress} , LocalIpAddress = {localIpAddress}";

            logEvent.AddPropertyIfAbsent(factory.CreateProperty("UserIPList", userIpList));
            logEvent.AddPropertyIfAbsent(factory.CreateProperty("UserIP", xForwardedFor));
        }
    }
}
