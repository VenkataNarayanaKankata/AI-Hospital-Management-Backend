using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class MedicineBatchCreateDto
    {
        [Required]
        [StringLength(50)]
        public string BatchNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ManufacturingDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Range(0, 1000000)]
        public int Quantity { get; set; }

        [Range(0, 1000000)]
        public decimal PurchasePrice { get; set; }

        [Range(0, 1000000)]
        public decimal SellingPrice { get; set; }

        [Range(0, 1000000)]
        public int ReorderLevel { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }

    public class MedicineBatchUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string BatchNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ManufacturingDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Range(0, 1000000)]
        public int Quantity { get; set; }

        [Range(0, 1000000)]
        public decimal PurchasePrice { get; set; }

        [Range(0, 1000000)]
        public decimal SellingPrice { get; set; }

        [Range(0, 1000000)]
        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; }
    }

    public class MedicineBatchResponseDto
    {
        public int MedicineBatchId { get; set; }

        public string BatchNumber { get; set; } = string.Empty;

        public DateTime ManufacturingDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int MedicineId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;
    }

    public class PharmacySaleItemCreateDto
    {
        [Required]
        public int MedicineBatchId { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }
    }

    public class PharmacySaleCreateDto
    {
        [Required]
        public int BranchId { get; set; }

        public int? PatientId { get; set; }

        public int? PrescriptionId { get; set; }

        [Range(0, 1000000)]
        public decimal DiscountAmount { get; set; }

        [Range(0, 1000000)]
        public decimal TaxAmount { get; set; }

        [StringLength(30)]
        public string? PaymentMethod { get; set; }

        [Required]
        [MinLength(1)]
        public List<PharmacySaleItemCreateDto> Items { get; set; }
            = new List<PharmacySaleItemCreateDto>();
    }

    public class PharmacySaleUpdateDto
    {
        [Range(0, 1000000)]
        public decimal DiscountAmount { get; set; }

        [Range(0, 1000000)]
        public decimal TaxAmount { get; set; }

        [Required]
        [StringLength(30)]
        public string PaymentStatus { get; set; } = "Pending";

        [StringLength(30)]
        public string? PaymentMethod { get; set; }
    }

    public class PharmacySaleItemResponseDto
    {
        public int PharmacySaleItemId { get; set; }

        public int MedicineBatchId { get; set; }

        public string BatchNumber { get; set; } = string.Empty;

        public int MedicineId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class PharmacySaleResponseDto
    {
        public int PharmacySaleId { get; set; }

        public string SaleNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;

        public string? PaymentMethod { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        public int? PatientId { get; set; }

        public string? PatientName { get; set; }

        public int? PrescriptionId { get; set; }

        public List<PharmacySaleItemResponseDto> Items { get; set; }
            = new List<PharmacySaleItemResponseDto>();
    }
}