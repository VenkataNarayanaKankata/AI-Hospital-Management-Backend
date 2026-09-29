using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllAsync();

        Task<List<DepartmentResponseDto>> GetByBranchIdAsync(int branchId);

        Task<DepartmentResponseDto?> GetByIdAsync(int id);

        Task<DepartmentResponseDto?> CreateAsync(
            DepartmentCreateDto dto);

        Task<DepartmentResponseDto?> UpdateAsync(
            int id,
            DepartmentUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}