namespace HospitalManagement.Api.DTOs
{
    public class AdmissionDto
    {
        public int AdmissionId { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public DateTime? ExpectedDischargeDate { get; set; }
        public DateTime? ActualDischargeDate { get; set; }
        public string AdmissionType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ReasonForAdmission { get; set; }
        public string? Notes { get; set; }
        public decimal? AdvanceAmount { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public int? AppointmentId { get; set; }

        public int WardId { get; set; }
        public string WardName { get; set; } = string.Empty;

        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;

        public int BedId { get; set; }
        public string BedNumber { get; set; } = string.Empty;
    }

    public class CreateAdmissionDto
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public DateTime? ExpectedDischargeDate { get; set; }
        public string AdmissionType { get; set; } = "Regular";
        public string? ReasonForAdmission { get; set; }
        public string? Notes { get; set; }
        public decimal? AdvanceAmount { get; set; }

        public int BranchId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public int WardId { get; set; }
        public int RoomId { get; set; }
        public int BedId { get; set; }
    }

    public class UpdateAdmissionDto
    {
        public DateTime? ExpectedDischargeDate { get; set; }
        public string AdmissionType { get; set; } = "Regular";
        public string Status { get; set; } = "Admitted";
        public string? ReasonForAdmission { get; set; }
        public string? Notes { get; set; }
        public decimal? AdvanceAmount { get; set; }

        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
    }
}