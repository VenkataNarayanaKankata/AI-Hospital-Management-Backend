using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        Task<IEnumerable<DoctorAvailabilityDto>> GetAllAsync();

        Task<DoctorAvailabilityDto?> GetByIdAsync(int id);

        Task<IEnumerable<DoctorAvailabilityDto>>
            GetByDoctorAsync(int doctorId);

        Task<DoctorAvailabilityDto> CreateAsync(
            CreateDoctorAvailabilityDto dto);

        Task<DoctorAvailabilityDto?> UpdateAsync(
            int id,
            UpdateDoctorAvailabilityDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<DoctorLeaveDto>> GetAllLeavesAsync();

        Task<DoctorLeaveDto?> GetLeaveByIdAsync(int id);

        Task<IEnumerable<DoctorLeaveDto>>
            GetLeavesByDoctorAsync(int doctorId);

        Task<DoctorLeaveDto> CreateLeaveAsync(
            CreateDoctorLeaveDto dto);

        Task<DoctorLeaveDto?> UpdateLeaveAsync(
            int id,
            UpdateDoctorLeaveDto dto);

        Task<bool> DeleteLeaveAsync(int id);
    }
}