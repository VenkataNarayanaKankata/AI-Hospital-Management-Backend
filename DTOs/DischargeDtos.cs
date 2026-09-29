namespace HospitalManagement.Api.DTOs
{
    public class DischargeDto
    {
        public int DischargeId { get; set; }
        public string DischargeNumber { get; set; } = string.Empty;
        public DateTime DischargeDate { get; set; }
        public string DischargeType { get; set; } = string.Empty;
        public string? DischargeSummary { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? TreatmentSummary { get; set; }
        public string? DoctorInstructions { get; set; }
        public string? FollowUpInstructions { get; set; }
        public string? ConditionAtDischarge { get; set; }

        public int AdmissionId { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public int BedId { get; set; }
        public string BedNumber { get; set; } = string.Empty;

        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
    }

    public class CreateDischargeDto
    {
        public string DischargeNumber { get; set; } = string.Empty;
        public DateTime DischargeDate { get; set; }
        public string DischargeType { get; set; } = "Normal";
        public string? DischargeSummary { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? TreatmentSummary { get; set; }
        public string? DoctorInstructions { get; set; }
        public string? FollowUpInstructions { get; set; }
        public string? ConditionAtDischarge { get; set; }

        public int AdmissionId { get; set; }
        public int DoctorId { get; set; }
    }

    public class UpdateDischargeDto
    {
        public DateTime DischargeDate { get; set; }
        public string DischargeType { get; set; } = "Normal";
        public string? DischargeSummary { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? TreatmentSummary { get; set; }
        public string? DoctorInstructions { get; set; }
        public string? FollowUpInstructions { get; set; }
        public string? ConditionAtDischarge { get; set; }
        public int DoctorId { get; set; }
    }
}