using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Discharge
    {
        public int DischargeId { get; set; }

        [Required]
        [MaxLength(30)]
        public string DischargeNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DischargeDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string DischargeType { get; set; } = "Normal";

        [MaxLength(2000)]
        public string? DischargeSummary { get; set; }

        [MaxLength(1000)]
        public string? FinalDiagnosis { get; set; }

        [MaxLength(2000)]
        public string? TreatmentSummary { get; set; }

        [MaxLength(1000)]
        public string? DoctorInstructions { get; set; }

        [MaxLength(1000)]
        public string? FollowUpInstructions { get; set; }

        [MaxLength(100)]
        public string? ConditionAtDischarge { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AdmissionId { get; set; }
        public Admission Admission { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}