using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Entities
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}