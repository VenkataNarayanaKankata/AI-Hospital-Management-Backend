using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IAdmissionService
    {
        Task<IEnumerable<AdmissionDto>> GetAllAdmissionsAsync();
        Task<AdmissionDto?> GetAdmissionByIdAsync(int id);
        Task<IEnumerable<AdmissionDto>> GetAdmissionsByPatientAsync(int patientId);
        Task<IEnumerable<AdmissionDto>> GetAdmissionsByBranchAsync(int branchId);
        Task<IEnumerable<AdmissionDto>> GetAdmissionsByStatusAsync(string status);

        Task<AdmissionDto> CreateAdmissionAsync(CreateAdmissionDto dto);
        Task<AdmissionDto?> UpdateAdmissionAsync(int id, UpdateAdmissionDto dto);
        Task<bool> DeleteAdmissionAsync(int id);
    }
}