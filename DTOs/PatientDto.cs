using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class PatientCreateDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(10)]
        public string? BloodGroup { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(150)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [StringLength(15)]
        public string? EmergencyContactNumber { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class PatientUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(10)]
        public string? BloodGroup { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(150)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [StringLength(15)]
        public string? EmergencyContactNumber { get; set; }

        [Required]
        public int BranchId { get; set; }

        public bool IsActive { get; set; }
    }

    public class PatientResponseDto
    {
        public int PatientId { get; set; }

        public string PatientNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string? BloodGroup { get; set; }

        public string MobileNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        // Branch information
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        // Hospital information
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}