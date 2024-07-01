using System;
using System.Net.Sockets;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Utilities
{
    public static class GeneralUtilities
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip + "/" + host.HostName;
            return string.Empty;
        }
        public static Enums.Environment GetEnvironment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var result = (Enums.Environment)Enum.Parse(typeof(Enums.Environment), environment);
            return result;
        }
        public static byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var result = ms.ToArray();
            return result;
        }
        public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
        {
            var result = userClaimsPrincipal.Claims.FirstOrDefault((Claim x) => x.Type == claimType)?.Value;
            return result;
        }
    }
}
