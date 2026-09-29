using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientResponseDto>> GetAllAsync();

        Task<List<PatientResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<PatientResponseDto?> GetByIdAsync(int id);

        Task<PatientResponseDto?> CreateAsync(
            PatientCreateDto dto);

        Task<PatientResponseDto?> UpdateAsync(
            int id,
            PatientUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}