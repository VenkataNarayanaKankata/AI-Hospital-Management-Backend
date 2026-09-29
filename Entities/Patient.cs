using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        [MaxLength(30)]
        public string PatientNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? BloodGroup { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(150)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? EmergencyContactNumber { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Foreign Key → Branch
        public int BranchId { get; set; }

        // Navigation Property → Branch
        public Branch Branch { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; }
    = new List<Appointment>();
        public ICollection<PharmacySale> PharmacySales { get; set; }
    = new List<PharmacySale>();
    }
}