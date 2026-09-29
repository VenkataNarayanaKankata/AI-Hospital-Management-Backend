using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Admission
    {
        public int AdmissionId { get; set; }

        [Required]
        [MaxLength(30)]
        public string AdmissionNumber { get; set; } = string.Empty;

        [Required]
        public DateTime AdmissionDate { get; set; }

        public DateTime? ExpectedDischargeDate { get; set; }

        public DateTime? ActualDischargeDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string AdmissionType { get; set; } = "Emergency";

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Admitted";

        [MaxLength(1000)]
        public string? ReasonForAdmission { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public decimal? AdvanceAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public int WardId { get; set; }
        public Ward Ward { get; set; } = null!;

        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public int BedId { get; set; }
        public Bed Bed { get; set; } = null!;
    }
}