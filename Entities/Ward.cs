using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Ward
    {
        public int WardId { get; set; }

        [Required]
        [MaxLength(100)]
        public string WardName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string WardType { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public int TotalBeds { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;
    }
}