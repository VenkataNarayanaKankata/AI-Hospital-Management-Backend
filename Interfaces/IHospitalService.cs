using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IHospitalService
    {
        Task<List<HospitalResponseDto>> GetAllAsync();

        Task<HospitalResponseDto?> GetByIdAsync(int id);

        Task<HospitalResponseDto?> CreateAsync(HospitalCreateDto dto);

        Task<HospitalResponseDto?> UpdateAsync(
            int id,
            HospitalUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}