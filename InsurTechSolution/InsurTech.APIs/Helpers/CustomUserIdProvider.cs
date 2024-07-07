using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace InsurTech.APIs.Helpers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var a = connection.UserIdentifier;
            var b = connection.User;
            var logger = connection.GetHttpContext().RequestServices.GetRequiredService<ILogger<CustomUserIdProvider>>();


            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
