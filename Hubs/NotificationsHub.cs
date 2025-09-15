using ERP_Proflipper_NotificationService.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Concurrent;

namespace ERP_Proflipper_NotificationService.Hubs
{
    public class NotificationsHub : Hub
    {
        //private readonly ILogger<NotificationsHub> logger;
        private static readonly ConcurrentDictionary<string, string> _userConnections = new();
        private readonly NotificationService _notificationService;

        public NotificationsHub(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task ClientRegister(string userLogin)
        {
            _userConnections[userLogin] = Context.ConnectionId;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userLogin}");

            await _notificationService.SendPendingNotificationAsync(userLogin);
        }

        //public override async Task OnConnectedAsync()
        //{
        //    await base.OnConnectedAsync();
        //}

        public static bool IsUserOnline(string userLogin)
        {
            return _userConnections.ContainsKey(userLogin);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userLogin = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (userLogin is not null)
            {
                _userConnections.TryRemove(userLogin, out _);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userLogin}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
