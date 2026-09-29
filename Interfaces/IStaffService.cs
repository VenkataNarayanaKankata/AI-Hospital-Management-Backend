using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IStaffService
    {
        Task<List<StaffResponseDto>> GetAllAsync();

        Task<List<StaffResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<List<StaffResponseDto>> GetByDepartmentIdAsync(
            int departmentId);

        Task<StaffResponseDto?> GetByIdAsync(int id);

        Task<StaffResponseDto?> CreateAsync(
            StaffCreateDto dto);

        Task<StaffResponseDto?> UpdateAsync(
            int id,
            StaffUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}