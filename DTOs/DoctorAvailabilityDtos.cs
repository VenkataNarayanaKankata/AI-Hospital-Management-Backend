namespace HospitalManagement.Api.DTOs
{
    public class DoctorAvailabilityDto
    {
        public int DoctorAvailabilityId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;
    }

    public class CreateDoctorAvailabilityDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int DoctorId { get; set; }
    }

    public class UpdateDoctorAvailabilityDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; }
    }
    public class DoctorLeaveDto
    {
        public int DoctorLeaveId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string LeaveType { get; set; } = string.Empty;

        public string? Reason { get; set; }

        public bool IsFullDay { get; set; }

        public bool IsApproved { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;
    }

    public class CreateDoctorLeaveDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string LeaveType { get; set; } = string.Empty;

        public string? Reason { get; set; }

        public bool IsFullDay { get; set; } = true;

        public bool IsApproved { get; set; } = true;

        public int DoctorId { get; set; }
    }

    public class UpdateDoctorLeaveDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string LeaveType { get; set; } = string.Empty;

        public string? Reason { get; set; }

        public bool IsFullDay { get; set; }

        public bool IsApproved { get; set; }
    }
}