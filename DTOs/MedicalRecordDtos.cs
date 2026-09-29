using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class MedicalRecordCreateDto
    {
        [Required]
        public DateTime VisitDate { get; set; }

        [StringLength(500)]
        public string? ChiefComplaint { get; set; }

        [StringLength(1000)]
        public string? Symptoms { get; set; }

        [StringLength(1000)]
        public string? Diagnosis { get; set; }

        [StringLength(1000)]
        public string? Treatment { get; set; }

        [StringLength(2000)]
        public string? ClinicalNotes { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }


    public class MedicalRecordUpdateDto
    {
        [Required]
        public DateTime VisitDate { get; set; }

        [StringLength(500)]
        public string? ChiefComplaint { get; set; }

        [StringLength(1000)]
        public string? Symptoms { get; set; }

        [StringLength(1000)]
        public string? Diagnosis { get; set; }

        [StringLength(1000)]
        public string? Treatment { get; set; }

        [StringLength(2000)]
        public string? ClinicalNotes { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }


    public class MedicalRecordResponseDto
    {
        public int MedicalRecordId { get; set; }

        public string RecordNumber { get; set; } = string.Empty;

        public DateTime VisitDate { get; set; }

        public string? ChiefComplaint { get; set; }

        public string? Symptoms { get; set; }

        public string? Diagnosis { get; set; }

        public string? Treatment { get; set; }

        public string? ClinicalNotes { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public DateTime CreatedAt { get; set; }


        // Appointment information
        public int AppointmentId { get; set; }

        public string AppointmentNumber { get; set; } = string.Empty;


        // Patient information
        public int PatientId { get; set; }

        public string PatientNumber { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;


        // Doctor information
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;


        // Branch information
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;


        // Hospital information
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;


        // Department information
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
    }
}