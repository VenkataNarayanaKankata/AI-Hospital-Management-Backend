using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        [MaxLength(30)]
        public string AppointmentNumber { get; set; } = string.Empty;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Scheduled";

        [MaxLength(500)]
        public string? ReasonForVisit { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key → Branch
        public int BranchId { get; set; }

        // Navigation Property → Branch
        public Branch Branch { get; set; } = null!;

        // Foreign Key → Patient
        public int PatientId { get; set; }

        // Navigation Property → Patient
        public Patient Patient { get; set; } = null!;

        // Foreign Key → Doctor
        public int DoctorId { get; set; }

        // Navigation Property → Doctor
        public Doctor Doctor { get; set; } = null!;

        // Foreign Key → Department
        public int DepartmentId { get; set; }

        // Navigation Property → Department
        public Department Department { get; set; } = null!;
    }
}