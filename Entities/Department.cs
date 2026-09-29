using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(150)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BranchId { get; set; }

        public Branch Branch { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; }
    = new List<Appointment>();
    }
}