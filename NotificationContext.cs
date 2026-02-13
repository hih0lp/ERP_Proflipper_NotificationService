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
            optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("DB_HOST")}; Port={Environment.GetEnvironmentVariable("DB_PORT")}; Database=ERP_USERS; Username={Environment.GetEnvironmentVariable("DB_USER")}; Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}; Encoding=UTF8; Pooling=true");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
