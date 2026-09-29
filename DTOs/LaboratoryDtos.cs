using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class LaboratoryTestCreateDto
    {
        [Required]
        [StringLength(100)]
        public string TestName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? TestCode { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? SampleType { get; set; }

        [StringLength(200)]
        public string? NormalRange { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [Range(0, 1000000)]
        public decimal TestFee { get; set; }

        [Range(0, 10000)]
        public int TurnaroundTimeHours { get; set; }
    }

    public class LaboratoryTestUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string TestName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? TestCode { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? SampleType { get; set; }

        [StringLength(200)]
        public string? NormalRange { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [Range(0, 1000000)]
        public decimal TestFee { get; set; }

        [Range(0, 10000)]
        public int TurnaroundTimeHours { get; set; }

        public bool IsActive { get; set; }
    }

    public class LaboratoryTestResponseDto
    {
        public int LaboratoryTestId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public string? TestCode { get; set; }

        public string? Description { get; set; }

        public string? SampleType { get; set; }

        public string? NormalRange { get; set; }

        public string? Unit { get; set; }

        public decimal TestFee { get; set; }

        public int TurnaroundTimeHours { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class LabOrderItemCreateDto
    {
        [Required]
        public int LaboratoryTestId { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public class LabOrderCreateDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(1000)]
        public string? ClinicalNotes { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [MinLength(1)]
        public List<LabOrderItemCreateDto> Items { get; set; }
            = new List<LabOrderItemCreateDto>();
    }

    public class LabOrderUpdateDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Ordered";

        [StringLength(1000)]
        public string? ClinicalNotes { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [MinLength(1)]
        public List<LabOrderItemCreateDto> Items { get; set; }
            = new List<LabOrderItemCreateDto>();
    }

    public class LabResultCreateDto
    {
        [Required]
        [StringLength(1000)]
        public string ResultValue { get; set; } = string.Empty;

        [StringLength(200)]
        public string? NormalRange { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        [StringLength(150)]
        public string? ResultedBy { get; set; }

        public DateTime ResultedAt { get; set; }

        public bool IsAbnormal { get; set; }
    }

    public class LabResultUpdateDto
    {
        [Required]
        [StringLength(1000)]
        public string ResultValue { get; set; } = string.Empty;

        [StringLength(200)]
        public string? NormalRange { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        [StringLength(150)]
        public string? ResultedBy { get; set; }

        public DateTime ResultedAt { get; set; }

        public bool IsAbnormal { get; set; }
    }

    public class LabOrderItemResponseDto
    {
        public int LabOrderItemId { get; set; }

        public int LaboratoryTestId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public string? TestCode { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? SampleCollectedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? Notes { get; set; }

        public LabResultResponseDto? Result { get; set; }
    }

    public class LabResultResponseDto
    {
        public int LabResultId { get; set; }

        public string ResultValue { get; set; } = string.Empty;

        public string? NormalRange { get; set; }

        public string? Unit { get; set; }

        public string? Remarks { get; set; }

        public string? ResultedBy { get; set; }

        public DateTime ResultedAt { get; set; }

        public bool IsAbnormal { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class LabOrderResponseDto
    {
        public int LabOrderId { get; set; }

        public string LabOrderNumber { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? SampleCollectedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? ClinicalNotes { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        public int PatientId { get; set; }

        public string PatientNumber { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public int AppointmentId { get; set; }

        public string AppointmentNumber { get; set; } = string.Empty;

        public List<LabOrderItemResponseDto> Items { get; set; }
            = new List<LabOrderItemResponseDto>();
    }
}