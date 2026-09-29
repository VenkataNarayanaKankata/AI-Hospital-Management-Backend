using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task<List<DoctorScheduleResponseDto>> GetAllAsync();

        Task<List<DoctorScheduleResponseDto>> GetByDoctorIdAsync(
            int doctorId);

        Task<List<DoctorScheduleResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<DoctorScheduleResponseDto?> GetByIdAsync(int id);

        Task<DoctorScheduleResponseDto?> CreateAsync(
            DoctorScheduleCreateDto dto);

        Task<DoctorScheduleResponseDto?> UpdateAsync(
            int id,
            DoctorScheduleUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}