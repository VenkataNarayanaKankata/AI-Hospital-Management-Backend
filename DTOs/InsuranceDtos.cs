namespace HospitalManagement.Api.DTOs
{
    public class InsuranceProviderDto
    {
        public int InsuranceProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ProviderCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateInsuranceProviderDto
    {
        public string ProviderName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ProviderCode { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateInsuranceProviderDto
    {
        public string ProviderName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ProviderCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class PatientInsuranceDto
    {
        public int PatientInsuranceId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string? MemberId { get; set; }
        public string? PolicyHolderName { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public decimal SumInsured { get; set; }
        public decimal AvailableCoverage { get; set; }
        public string Status { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int InsuranceProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
    }

    public class CreatePatientInsuranceDto
    {
        public string PolicyNumber { get; set; } = string.Empty;
        public string? MemberId { get; set; }
        public string? PolicyHolderName { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public decimal SumInsured { get; set; }
        public decimal AvailableCoverage { get; set; }
        public string Status { get; set; } = "Active";

        public int PatientId { get; set; }
        public int InsuranceProviderId { get; set; }
    }

    public class UpdatePatientInsuranceDto
    {
        public string? MemberId { get; set; }
        public string? PolicyHolderName { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public decimal SumInsured { get; set; }
        public decimal AvailableCoverage { get; set; }
        public string Status { get; set; } = "Active";
    }

    public class InsuranceClaimDto
    {
        public int InsuranceClaimId { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal RejectedAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SettledDate { get; set; }

        public int PatientInsuranceId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int AdmissionId { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
    }

    public class CreateInsuranceClaimDto
    {
        public string ClaimNumber { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string? Remarks { get; set; }

        public int PatientInsuranceId { get; set; }
        public int AdmissionId { get; set; }
    }

    public class UpdateInsuranceClaimDto
    {
        public decimal ApprovedAmount { get; set; }
        public decimal RejectedAmount { get; set; }
        public string Status { get; set; } = "Submitted";
        public string? Remarks { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SettledDate { get; set; }
    }
}