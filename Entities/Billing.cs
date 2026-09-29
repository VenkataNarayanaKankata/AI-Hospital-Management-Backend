using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required]
        [MaxLength(30)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Unpaid";

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; } = null!;

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; } = null!;

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
            = new List<InvoiceItem>();

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ItemType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; } = 1;

        [Range(0, 10000000)]
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }
    }
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        [MaxLength(30)]
        public string PaymentNumber { get; set; } = string.Empty;

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(30)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Completed";

        [MaxLength(100)]
        public string? TransactionReference { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; } = null!;
    }

}
