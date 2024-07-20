using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace InsurTech.APIs.Helpers
{
    public class NotificationHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<NotificationHub> logger;

        public NotificationHub(UserManager<AppUser> userManager , ILogger<NotificationHub> logger)
        {
            _userManager = userManager;
            this.logger = logger;
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = Context.UserIdentifier;
                var token = Context.GetHttpContext().Request.Query["access_token"];
                var type = Context.GetHttpContext().Request.Query["type"];


                var user = await _userManager.FindByIdAsync(userId);

                if (type == "2")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "admin");
                }
                else if (type == "1")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "company");
                }
                else if (type == "0")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "customer");
                }

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in OnConnectedAsync");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var userId = Context.UserIdentifier;
                var user = await _userManager.FindByIdAsync(userId);
                var type = Context.GetHttpContext().Request.Query["type"];

                if (user != null)
                {
                    if (type == "2")
                    {
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "admin");
                    }
                    else if (type == "1")
                    {
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "company");
                    }
                    else if (type == "0")
                    {
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "customer");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in OnDisconnectedAsync");
                throw; // Rethrow the exception to propagate it further if necessary
            }

            await base.OnDisconnectedAsync(exception);
        }


    }

}

