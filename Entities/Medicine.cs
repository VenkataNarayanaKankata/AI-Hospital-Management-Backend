using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }

        [Required]
        [MaxLength(150)]
        public string MedicineName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? GenericName { get; set; }

        [MaxLength(100)]
        public string? BrandName { get; set; }

        [MaxLength(100)]
        public string? DosageForm { get; set; }

        [MaxLength(50)]
        public string? Strength { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; }
            = new List<PrescriptionItem>();
        public ICollection<MedicineBatch> MedicineBatches { get; set; }
    = new List<MedicineBatch>();
    }
}