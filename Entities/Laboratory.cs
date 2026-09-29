using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class LaboratoryTest
    {
        public int LaboratoryTestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TestName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? TestCode { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? SampleType { get; set; }

        [MaxLength(200)]
        public string? NormalRange { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; }

        public decimal TestFee { get; set; }

        public int TurnaroundTimeHours { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<LabOrderItem> LabOrderItems { get; set; }
            = new List<LabOrderItem>();
    }

    public class LabOrder
    {
        public int LabOrderId { get; set; }

        [Required]
        [MaxLength(30)]
        public string LabOrderNumber { get; set; } = string.Empty;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Ordered";

        public DateTime? SampleCollectedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [MaxLength(1000)]
        public string? ClinicalNotes { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; } = null!;

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; } = null!;

        public ICollection<LabOrderItem> LabOrderItems { get; set; }
            = new List<LabOrderItem>();
    }

    public class LabOrderItem
    {
        public int LabOrderItemId { get; set; }

        public int LabOrderId { get; set; }

        public LabOrder LabOrder { get; set; } = null!;

        public int LaboratoryTestId { get; set; }

        public LaboratoryTest LaboratoryTest { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Ordered";

        public DateTime? SampleCollectedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public LabResult? LabResult { get; set; }
    }
    public class LabResult
    {
        public int LabResultId { get; set; }

        public int LabOrderItemId { get; set; }

        public LabOrderItem LabOrderItem { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string ResultValue { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? NormalRange { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; }

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        [MaxLength(150)]
        public string? ResultedBy { get; set; }

        public DateTime ResultedAt { get; set; }

        public bool IsAbnormal { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}