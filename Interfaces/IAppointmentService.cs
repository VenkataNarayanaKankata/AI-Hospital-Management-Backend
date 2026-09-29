using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDto>> GetAllAsync();

        Task<List<AppointmentResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<List<AppointmentResponseDto>> GetByPatientIdAsync(
            int patientId);

        Task<List<AppointmentResponseDto>> GetByDoctorIdAsync(
            int doctorId);

        Task<AppointmentResponseDto?> GetByIdAsync(int id);

        Task<AppointmentResponseDto?> CreateAsync(
            AppointmentCreateDto dto);

        Task<AppointmentResponseDto?> UpdateAsync(
            int id,
            AppointmentUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}