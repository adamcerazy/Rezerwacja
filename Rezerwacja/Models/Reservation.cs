using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rezerwacja.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        // Navigation property for attachments (będzie dodane później)
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}