using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rezerwacja.Models;

namespace Rezerwacja.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Konfiguracja relacji Room -> Reservations
            builder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja relacji Reservation -> Attachments
            builder.Entity<Attachment>()
                .HasOne(a => a.Reservation)
                .WithMany(r => r.Attachments)
                .HasForeignKey(a => a.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja indeksów dla wydajności
            builder.Entity<Reservation>()
                .HasIndex(r => new { r.RoomId, r.StartAt, r.EndAt })
                .HasDatabaseName("IX_Reservation_Room_DateTime");

            builder.Entity<Reservation>()
                .HasIndex(r => r.Status)
                .HasDatabaseName("IX_Reservation_Status");

            builder.Entity<Room>()
                .HasIndex(r => r.IsActive)
                .HasDatabaseName("IX_Room_IsActive");

            // Konfiguracja precyzji dla DateTime
            builder.Entity<Reservation>()
                .Property(r => r.StartAt)
                .HasColumnType("datetime2");

            builder.Entity<Reservation>()
                .Property(r => r.EndAt)
                .HasColumnType("datetime2");

            builder.Entity<Reservation>()
                .Property(r => r.CreatedAt)
                .HasColumnType("datetime2");

            builder.Entity<Reservation>()
                .Property(r => r.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Entity<Attachment>()
                .Property(a => a.UploadedAt)
                .HasColumnType("datetime2");
        }
    }
}
