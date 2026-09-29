using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(150)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(200)]
        public string? Qualification { get; set; }

        [MaxLength(150)]
        public string? Specialization { get; set; }

        public int ExperienceYears { get; set; }

        public decimal ConsultationFee { get; set; }

        [MaxLength(500)]
        public string? ProfileImage { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key → Branch
        public int BranchId { get; set; }

        // Navigation Property → Branch
        public Branch Branch { get; set; } = null!;

        // Foreign Key → Department
        public int DepartmentId { get; set; }

        // Navigation Property → Department
        public Department Department { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; }
    = new List<Appointment>();
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    = new List<DoctorSchedule>();
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; }
    = new List<DoctorAvailability>();
        public ICollection<DoctorLeave> DoctorLeaves { get; set; }
    = new List<DoctorLeave>();
    }
    public class DoctorAvailability
    {
        public int DoctorAvailabilityId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }

    public class DoctorLeave
    {
        public int DoctorLeaveId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string LeaveType { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Reason { get; set; }

        public bool IsFullDay { get; set; } = true;

        public bool IsApproved { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; } = null!;
    }
}