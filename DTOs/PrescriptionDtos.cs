using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class PrescriptionItemCreateDto
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosage { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Frequency { get; set; } = string.Empty;

        [Range(1, 3650)]
        public int DurationDays { get; set; }

        [StringLength(50)]
        public string? Route { get; set; }

        [StringLength(500)]
        public string? Instructions { get; set; }
    }

    public class PrescriptionCreateDto
    {
        [Required]
        public DateTime PrescriptionDate { get; set; }

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
        public int MedicalRecordId { get; set; }

        [Required]
        [MinLength(1)]
        public List<PrescriptionItemCreateDto> Items { get; set; }
            = new List<PrescriptionItemCreateDto>();
    }

    public class PrescriptionItemUpdateDto
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosage { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Frequency { get; set; } = string.Empty;

        [Range(1, 3650)]
        public int DurationDays { get; set; }

        [StringLength(50)]
        public string? Route { get; set; }

        [StringLength(500)]
        public string? Instructions { get; set; }
    }

    public class PrescriptionUpdateDto
    {
        [Required]
        public DateTime PrescriptionDate { get; set; }

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
        public int MedicalRecordId { get; set; }

        [Required]
        [MinLength(1)]
        public List<PrescriptionItemUpdateDto> Items { get; set; }
            = new List<PrescriptionItemUpdateDto>();
    }

    public class PrescriptionItemResponseDto
    {
        public int PrescriptionItemId { get; set; }

        public int MedicineId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public string? GenericName { get; set; }

        public string? BrandName { get; set; }

        public string? Strength { get; set; }

        public string? DosageForm { get; set; }

        public string Dosage { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public int DurationDays { get; set; }

        public string? Route { get; set; }

        public string? Instructions { get; set; }
    }

    public class PrescriptionResponseDto
    {
        public int PrescriptionId { get; set; }

        public string PrescriptionNumber { get; set; } = string.Empty;

        public DateTime PrescriptionDate { get; set; }

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

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public int AppointmentId { get; set; }

        public string AppointmentNumber { get; set; } = string.Empty;

        public int MedicalRecordId { get; set; }

        public string MedicalRecordNumber { get; set; } = string.Empty;

        public List<PrescriptionItemResponseDto> Items { get; set; }
            = new List<PrescriptionItemResponseDto>();
    }
}