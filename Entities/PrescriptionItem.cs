using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; }

        // Foreign Key → Prescription
        public int PrescriptionId { get; set; }

        // Navigation Property → Prescription
        public Prescription Prescription { get; set; } = null!;


        // Foreign Key → Medicine
        public int MedicineId { get; set; }

        // Navigation Property → Medicine
        public Medicine Medicine { get; set; } = null!;


        [Required]
        [MaxLength(100)]
        public string Dosage { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Frequency { get; set; } = string.Empty;

        [Required]
        public int DurationDays { get; set; }

        [MaxLength(50)]
        public string? Route { get; set; }

        [MaxLength(500)]
        public string? Instructions { get; set; }
    }
}