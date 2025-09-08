using Microsoft.AspNetCore.SignalR;

namespace ERP_Proflipper_NotificationService.Hubs
{
    public class NotificationsHub : Hub
    {
        //private readonly ILogger<NotificationsHub> logger;
        private static readonly Dictionary<string, string> _userConnections = new();

        public async Task ClientRegister(string userId)
        {
            _userConnections[userId] = Context.ConnectionId;

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

            Console.WriteLine("Something");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (userId is not null)
            {
                _userConnections.Remove(userId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
