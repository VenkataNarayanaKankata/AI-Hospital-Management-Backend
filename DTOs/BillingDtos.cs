using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class InvoiceItemCreateDto
    {
        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ItemType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; } = 1;

        [Range(0, 10000000)]
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceCreateDto
    {
        [Required]
        public DateTime InvoiceDate { get; set; }

        [Range(0, 10000000)]
        public decimal DiscountAmount { get; set; }

        [Range(0, 10000000)]
        public decimal TaxAmount { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [MinLength(1)]
        public List<InvoiceItemCreateDto> Items { get; set; }
            = new List<InvoiceItemCreateDto>();
    }

    public class InvoiceItemUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ItemType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; }

        [Range(0, 10000000)]
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceUpdateDto
    {
        [Required]
        public DateTime InvoiceDate { get; set; }

        [Range(0, 10000000)]
        public decimal DiscountAmount { get; set; }

        [Range(0, 10000000)]
        public decimal TaxAmount { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [MinLength(1)]
        public List<InvoiceItemUpdateDto> Items { get; set; }
            = new List<InvoiceItemUpdateDto>();
    }

    public class PaymentCreateDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(30)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(100)]
        public string? TransactionReference { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }

        public string PaymentNumber { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? TransactionReference { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class InvoiceItemResponseDto
    {
        public int InvoiceItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? ItemType { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

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

        public List<InvoiceItemResponseDto> Items { get; set; }
            = new List<InvoiceItemResponseDto>();

        public List<PaymentResponseDto> Payments { get; set; }
            = new List<PaymentResponseDto>();
    }
}