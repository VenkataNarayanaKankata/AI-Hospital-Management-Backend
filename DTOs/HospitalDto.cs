using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class HospitalCreateDto
    {
        [Required]
        [StringLength(150)]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }
    }

    public class HospitalUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }

    public class HospitalResponseDto
    {
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class DashboardSummaryDto
    {
        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TodaysAppointments { get; set; }
        public int CurrentAdmissions { get; set; }
        public int AvailableBeds { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingLabOrders { get; set; }
        public decimal PharmacySales { get; set; }
    }
    public class AppointmentStatusDto
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}