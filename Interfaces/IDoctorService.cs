using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorResponseDto>> GetAllAsync();

        Task<List<DoctorResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<List<DoctorResponseDto>> GetByDepartmentIdAsync(
            int departmentId);

        Task<DoctorResponseDto?> GetByIdAsync(int id);

        Task<DoctorResponseDto?> CreateAsync(
            DoctorCreateDto dto);

        Task<DoctorResponseDto?> UpdateAsync(
            int id,
            DoctorUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}