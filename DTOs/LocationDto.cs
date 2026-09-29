using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class LocationCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }
    }

    public class LocationUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }
    }

    public class LocationResponseDto
    {
        public int LocationId { get; set; }

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PostalCode { get; set; }
    }
}