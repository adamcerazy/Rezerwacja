using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rezerwacja.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string OriginalName { get; set; } = string.Empty;

        [Required]
        public long Size { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; } = null!;
    }
}