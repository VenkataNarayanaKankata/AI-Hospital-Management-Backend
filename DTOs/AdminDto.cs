using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.DTOs
{
    public class AdminRegisterDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }
    }


    public class AdminLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }


    public class AdminResponseDto
    {
        public int AdminId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }
    }


    public class AdminLoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public AdminResponseDto Admin { get; set; } = new();
    }
}