using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }
        public DbSet<CarrierReport> CarrierReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarrierConfiguration>()
                .Property(x => x.CarrierCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(x => x.OrderCarrierCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CarrierConfiguration>()
                .HasOne(x => x.Carrier)
                .WithMany(c => c.CarrierConfigurations)
                .HasForeignKey(x => x.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Carrier)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CarrierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}