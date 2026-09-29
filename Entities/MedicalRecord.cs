using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }

        [Required]
        [MaxLength(30)]
        public string RecordNumber { get; set; } = string.Empty;

        [Required]
        public DateTime VisitDate { get; set; }

        [MaxLength(500)]
        public string? ChiefComplaint { get; set; }

        [MaxLength(1000)]
        public string? Symptoms { get; set; }

        [MaxLength(1000)]
        public string? Diagnosis { get; set; }

        [MaxLength(1000)]
        public string? Treatment { get; set; }

        [MaxLength(2000)]
        public string? ClinicalNotes { get; set; }

        public DateTime? FollowUpDate { get; set; }

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
    }
}