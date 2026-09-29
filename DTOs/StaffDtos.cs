using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class StaffCreateDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? Qualification { get; set; }

        [Required]
        [StringLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Range(0, 100000000)]
        public decimal Salary { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class StaffUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? Qualification { get; set; }

        [Required]
        [StringLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Range(0, 100000000)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class StaffResponseDto
    {
        public int StaffId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string MobileNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Qualification { get; set; }

        public string Designation { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}