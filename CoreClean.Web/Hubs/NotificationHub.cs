using CoreClean.Domain.Models;
using CoreClean.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreClean.Web.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public static NotificationHub Instance { get; private set; }

        public NotificationHub() {
            Instance = this;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.GetId().ToString());
            await base.OnConnectedAsync();
        }

    }
}
