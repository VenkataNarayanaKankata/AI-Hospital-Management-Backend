using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        [MaxLength(30)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string RoomType { get; set; } = string.Empty;

        public int FloorNumber { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int WardId { get; set; }

        public Ward Ward { get; set; } = null!;

        public ICollection<Bed> Beds { get; set; }
            = new List<Bed>();
    }

    public class Bed
    {
        public int BedId { get; set; }

        [Required]
        [MaxLength(30)]
        public string BedNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BedType { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Available";

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RoomId { get; set; }

        public Room Room { get; set; } = null!;
    }
}