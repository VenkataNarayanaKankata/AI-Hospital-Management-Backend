using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class DoctorScheduleCreateDto
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public TimeSpan? BreakStart { get; set; }

        public TimeSpan? BreakEnd { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class DoctorScheduleUpdateDto
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public TimeSpan? BreakStart { get; set; }

        public TimeSpan? BreakEnd { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class DoctorScheduleResponseDto
    {
        public int DoctorScheduleId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan? BreakStart { get; set; }

        public TimeSpan? BreakEnd { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedAt { get; set; }

        // Doctor
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        // Branch
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        // Department
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        // Hospital
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }
}