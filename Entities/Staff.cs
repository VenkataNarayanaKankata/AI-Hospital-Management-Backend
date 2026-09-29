using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Qualification { get; set; }

        [Required]
        [MaxLength(100)]
        public string Designation { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
    }
}