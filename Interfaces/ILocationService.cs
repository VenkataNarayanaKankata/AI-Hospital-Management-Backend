using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface ILocationService
    {
        Task<List<LocationResponseDto>> GetAllAsync();

        Task<LocationResponseDto?> GetByIdAsync(int id);

        Task<LocationResponseDto?> CreateAsync(
            LocationCreateDto dto);

        Task<LocationResponseDto?> UpdateAsync(
            int id,
            LocationUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}