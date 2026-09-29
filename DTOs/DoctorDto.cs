using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class DoctorCreateDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? MobileNumber { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string? Qualification { get; set; }

        [StringLength(150)]
        public string? Specialization { get; set; }

        [Range(0, 70)]
        public int ExperienceYears { get; set; }

        [Range(0, 1000000)]
        public decimal ConsultationFee { get; set; }

        [StringLength(500)]
        public string? ProfileImage { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class DoctorUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? MobileNumber { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string? Qualification { get; set; }

        [StringLength(150)]
        public string? Specialization { get; set; }

        [Range(0, 70)]
        public int ExperienceYears { get; set; }

        [Range(0, 1000000)]
        public decimal ConsultationFee { get; set; }

        [StringLength(500)]
        public string? ProfileImage { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }
    }

    public class DoctorResponseDto
    {
        public int DoctorId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Qualification { get; set; }

        public string? Specialization { get; set; }

        public int ExperienceYears { get; set; }

        public decimal ConsultationFee { get; set; }

        public string? ProfileImage { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Branch information
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        // Department information
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        // Hospital information
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}