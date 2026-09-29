using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class InsuranceProvider
    {
        public int InsuranceProviderId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProviderName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(20)]
        public string? ContactNumber { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? ProviderCode { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PatientInsurance> PatientInsurances { get; set; }
            = new List<PatientInsurance>();
    }

    public class PatientInsurance
    {
        public int PatientInsuranceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MemberId { get; set; }

        [MaxLength(100)]
        public string? PolicyHolderName { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public DateTime PolicyEndDate { get; set; }

        public decimal SumInsured { get; set; }

        public decimal AvailableCoverage { get; set; }

        [MaxLength(30)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int InsuranceProviderId { get; set; }
        public InsuranceProvider InsuranceProvider { get; set; } = null!;

        public ICollection<InsuranceClaim> InsuranceClaims { get; set; }
            = new List<InsuranceClaim>();
    }

    public class InsuranceClaim
    {
        public int InsuranceClaimId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClaimNumber { get; set; } = string.Empty;

        public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

        public decimal ClaimAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        public decimal RejectedAmount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Submitted";

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? SettledDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientInsuranceId { get; set; }
        public PatientInsurance PatientInsurance { get; set; } = null!;

        public int AdmissionId { get; set; }
        public Admission Admission { get; set; } = null!;
    }
}