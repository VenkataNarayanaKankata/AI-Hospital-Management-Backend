using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IDischargeService
    {
        Task<IEnumerable<DischargeDto>> GetAllDischargesAsync();
        Task<DischargeDto?> GetDischargeByIdAsync(int id);
        Task<IEnumerable<DischargeDto>> GetDischargesByPatientAsync(int patientId);
        Task<IEnumerable<DischargeDto>> GetDischargesByDoctorAsync(int doctorId);

        Task<DischargeDto> CreateDischargeAsync(CreateDischargeDto dto);
        Task<DischargeDto?> UpdateDischargeAsync(int id, UpdateDischargeDto dto);
        Task<bool> DeleteDischargeAsync(int id);
    }
}