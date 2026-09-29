using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class DoctorSchedule
    {
        public int DoctorScheduleId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public TimeSpan? BreakStart { get; set; }

        public TimeSpan? BreakEnd { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key → Doctor
        public int DoctorId { get; set; }

        // Navigation Property → Doctor
        public Doctor Doctor { get; set; } = null!;

        // Foreign Key → Branch
        public int BranchId { get; set; }

        // Navigation Property → Branch
        public Branch Branch { get; set; } = null!;
    }
}