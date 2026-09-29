using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class BranchCreateDto
    {
        [Required]
        [StringLength(150)]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public int LocationId { get; set; }
    }


    public class BranchUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public int LocationId { get; set; }

        public bool IsActive { get; set; }
    }


    public class BranchResponseDto
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }


        // Hospital relationship
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;


        // Location relationship
        public int LocationId { get; set; }

        public LocationResponseDto? Location { get; set; }
    }
}