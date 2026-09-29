using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IBranchService
    {
        Task<List<BranchResponseDto>> GetAllAsync();

        Task<List<BranchResponseDto>> GetByHospitalIdAsync(
            int hospitalId);

        Task<BranchResponseDto?> GetByIdAsync(int id);

        Task<BranchResponseDto?> CreateAsync(
            BranchCreateDto dto);

        Task<BranchResponseDto?> UpdateAsync(
            int id,
            BranchUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}