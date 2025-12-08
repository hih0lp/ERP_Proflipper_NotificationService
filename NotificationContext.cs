using ERP_Proflipper_NotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_Proflipper_NotificationService
{
    public class NotificationContext : DbContext
    {
        public NotificationContext () : base ()
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=ERP_USERS_NOTIFICATIONS; Username=admin; Password=Tandem_2025; Encoding=UTF8; Pooling=true");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
