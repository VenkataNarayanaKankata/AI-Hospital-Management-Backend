using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class AppointmentCreateDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [StringLength(500)]
        public string? ReasonForVisit { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class AppointmentUpdateDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ReasonForVisit { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public string AppointmentNumber { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public TimeSpan AppointmentTime { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? ReasonForVisit { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        // Branch
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        // Hospital
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        // Patient
        public int PatientId { get; set; }

        public string PatientNumber { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        // Doctor
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        // Department
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
    }
}
