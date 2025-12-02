namespace ERP_Proflipper_NotificationService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserLogin { get; set; }
        public string? RedirectUri { get; set; }
        public bool IsSent { get; set; } = false;
    }
}
