using System.Diagnostics;
using System.Text.Json;
using ERP_Proflipper_NotificationService.Filters;
using ERP_Proflipper_NotificationService.Hubs;
using ERP_Proflipper_NotificationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ERP_Proflipper_NotificationService.Services;

namespace ERP_Proflipper_NotificationService.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly NotificationService _notificationService;
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly NotificationContext _db = new();

        public NotificationController(ILogger<NotificationController> logger, IHubContext<NotificationsHub> hubContext, NotificationService notificationService)
        {
            _logger = logger;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        [HttpPost]
        [ServiceKeyAuthAttribute]
        [Route("/not")]
        public async Task<ActionResult> Broadcast([FromBody] Broadcast request)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", request.Message);
            return Ok(new { Status = "Broadcast sent"});
        }

        [HttpPost("user/{userLogin}")]
        [ServiceKeyAuthAttribute]
        public async Task<IActionResult> SendToUser(string userLogin, [FromBody]Notification request)//‰ÓÊ‰‡Ú¸Òˇ Â„ÓËÍ‡
        {
            //await _hubContext.Clients.Groups($"user_{userLogin}").SendAsync("ReceiveNotification", request, request.CreatedAt);
            //return Ok(new { Status = $"Notification sent to {userLogin}"});
            Console.WriteLine("Á¿œ–Œ— ”—œ≈ÿ≈Õ");

            await _notificationService.SendNotificationsAsync(userLogin, request);
            Console.WriteLine("—ŒŒ¡Ÿ≈Õ»ﬂ Œ“œ–¿¬À≈Õ€");

            return Ok();    
        }

        [HttpGet("user/{userLogin}")]
        public async Task<JsonResult> GetNotifications(string userLogin)
        {
            return Json(_db.Notifications
                            .Where(x => x.UserLogin == userLogin)
                            .Where(x => x.IsSent)
                            .OrderBy(x => x.CreatedAt)
                            .ToList());
        }
    }
}
