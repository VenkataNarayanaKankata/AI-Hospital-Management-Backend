using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IAdminService
    {
        Task<bool> RegisterAdminAsync(AdminRegisterDto dto);

        Task<AdminLoginResponseDto?> LoginAdminAsync(
            AdminLoginDto dto);
    }
}