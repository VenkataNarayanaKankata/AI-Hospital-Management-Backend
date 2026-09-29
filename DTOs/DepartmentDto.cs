using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class DepartmentCreateDto
    {
        [Required]
        [StringLength(150)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class DepartmentUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int BranchId { get; set; }

        public bool IsActive { get; set; }
    }

    public class DepartmentResponseDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}