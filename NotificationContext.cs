using ERP_Proflipper_NotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_Proflipper_NotificationService
{
    public class NotificationContext : DbContext
    {
        public NotificationContext () : base ()
        {
            Database.EnsureCreated();
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=195.54.178.243; Port=27031; Database=ERP_USERS_NOTIFICATIONS; Username=admin; Password=Tandem_2025; Encoding=UTF8; Pooling=true");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
