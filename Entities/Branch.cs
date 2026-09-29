using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Branch
    {
        public int BranchId { get; set; }

        [Required]
        [MaxLength(150)]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key → Hospital
        public int HospitalId { get; set; }

        // Navigation Property
        public Hospital Hospital { get; set; } = null!;
        public ICollection<Department> Departments { get; set; }
            = new List<Department>();

        public ICollection<Patient> Patients { get; set; }
            = new List<Patient>();
        public ICollection<Appointment> Appointments { get; set; }
    = new List<Appointment>();
        public int LocationId { get; set; }

        public Location Location { get; set; } = null!;
        public ICollection<MedicineBatch> MedicineBatches { get; set; }
    = new List<MedicineBatch>();

        public ICollection<PharmacySale> PharmacySales { get; set; }
            = new List<PharmacySale>();
    }
}