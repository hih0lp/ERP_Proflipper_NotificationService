using Microsoft.AspNetCore.SignalR;

namespace ERP_Proflipper_NotificationService.Hubs
{
    public class NotificationsHub : Hub
    {
        //private readonly ILogger<NotificationsHub> logger;
        private static readonly Dictionary<string, string> _userConnections = new();

        public async Task ClientRegister(string userLogin)
        {
            _userConnections[userLogin] = Context.ConnectionId;

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userLogin}");

            Console.WriteLine("Something");
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userLogin = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (userLogin is not null)
            {
                _userConnections.Remove(userLogin);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userLogin}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
