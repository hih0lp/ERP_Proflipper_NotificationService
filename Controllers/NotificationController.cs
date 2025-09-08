using System.Diagnostics;
using System.Text.Json;
using ERP_Proflipper_NotificationService.Filters;
using ERP_Proflipper_NotificationService.Hubs;
using ERP_Proflipper_NotificationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace ERP_Proflipper_NotificationService.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationController(ILogger<NotificationController> logger, IHubContext<NotificationsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost]
        [ServiceKeyAuthAttribute]
        public async Task<ActionResult> Broadcast([FromBody] Broadcast request)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", request.Message);
            return Ok(new { Status = "Broadcast sent"});
        }

        [HttpPost("user/{userId}")]
        [ServiceKeyAuthAttribute]
        public async Task<IActionResult> SendToUser(string userId, [FromBody] UserRequest request)
        {
            await _hubContext.Clients.Groups($"user_{userId}").SendAsync("ReceiveNotification", request.Message, request.CreatedAt);
            return Ok(new { Status = $"Notification sent to {userId}"});
        }

        
    }
}
