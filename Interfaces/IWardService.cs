using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IWardService
    {
        Task<List<WardResponseDto>> GetAllAsync();

        Task<List<WardResponseDto>> GetByBranchIdAsync(int branchId);

        Task<WardResponseDto?> GetByIdAsync(int id);

        Task<WardResponseDto?> CreateAsync(WardCreateDto dto);

        Task<WardResponseDto?> UpdateAsync(
            int id,
            WardUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}