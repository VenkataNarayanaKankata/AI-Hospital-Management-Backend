using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Hospital
    {
        public int HospitalId { get; set; }

        [Required]
        [MaxLength(150)]
        public string HospitalName { get; set; } = string.Empty;

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

        // One Hospital can have many Branches
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}