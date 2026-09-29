using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class MedicineBatch
    {
        public int MedicineBatchId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BatchNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ManufacturingDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; } = null!;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;
    }

    public class PharmacySale
    {
        public int PharmacySaleId { get; set; }

        [Required]
        [MaxLength(30)]
        public string SaleNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(30)]
        public string PaymentStatus { get; set; } = "Pending";

        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;

        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int? PrescriptionId { get; set; }

        public Prescription? Prescription { get; set; }

        public ICollection<PharmacySaleItem> Items { get; set; }
            = new List<PharmacySaleItem>();
    }

    public class PharmacySaleItem
    {
        public int PharmacySaleItemId { get; set; }

        public int PharmacySaleId { get; set; }

        public PharmacySale PharmacySale { get; set; } = null!;

        public int MedicineBatchId { get; set; }

        public MedicineBatch MedicineBatch { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}