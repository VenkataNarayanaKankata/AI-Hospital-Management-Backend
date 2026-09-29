using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class WardCreateDto
    {
        [Required]
        [StringLength(100)]
        public string WardName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string WardType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 10000)]
        public int TotalBeds { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class WardUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string WardName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string WardType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 10000)]
        public int TotalBeds { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class WardResponseDto
    {
        public int WardId { get; set; }

        public string WardName { get; set; } = string.Empty;

        public string WardType { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int TotalBeds { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}