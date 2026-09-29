using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        [Required]
        [MaxLength(30)]
        public string PrescriptionNumber { get; set; } = string.Empty;

        [Required]
        public DateTime PrescriptionDate { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Foreign Key → Branch
        public int BranchId { get; set; }

        // Navigation Property → Branch
        public Branch Branch { get; set; } = null!;


        // Foreign Key → Patient
        public int PatientId { get; set; }

        // Navigation Property → Patient
        public Patient Patient { get; set; } = null!;


        // Foreign Key → Doctor
        public int DoctorId { get; set; }

        // Navigation Property → Doctor
        public Doctor Doctor { get; set; } = null!;


        // Foreign Key → Appointment
        public int AppointmentId { get; set; }

        // Navigation Property → Appointment
        public Appointment Appointment { get; set; } = null!;


        // Foreign Key → MedicalRecord
        public int MedicalRecordId { get; set; }

        // Navigation Property → MedicalRecord
        public MedicalRecord MedicalRecord { get; set; } = null!;


        // Prescription Items
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; }
            = new List<PrescriptionItem>();
    }
}