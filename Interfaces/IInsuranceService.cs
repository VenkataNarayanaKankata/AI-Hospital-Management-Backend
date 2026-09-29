using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IInsuranceService
    {
        Task<IEnumerable<InsuranceProviderDto>> GetAllProvidersAsync();
        Task<InsuranceProviderDto?> GetProviderByIdAsync(int id);
        Task<InsuranceProviderDto> CreateProviderAsync(CreateInsuranceProviderDto dto);
        Task<InsuranceProviderDto?> UpdateProviderAsync(int id, UpdateInsuranceProviderDto dto);
        Task<bool> DeleteProviderAsync(int id);

        Task<IEnumerable<PatientInsuranceDto>> GetAllPatientInsurancesAsync();
        Task<PatientInsuranceDto?> GetPatientInsuranceByIdAsync(int id);
        Task<IEnumerable<PatientInsuranceDto>> GetPatientInsurancesByPatientAsync(int patientId);
        Task<PatientInsuranceDto> CreatePatientInsuranceAsync(CreatePatientInsuranceDto dto);
        Task<PatientInsuranceDto?> UpdatePatientInsuranceAsync(int id, UpdatePatientInsuranceDto dto);
        Task<bool> DeletePatientInsuranceAsync(int id);

        Task<IEnumerable<InsuranceClaimDto>> GetAllClaimsAsync();
        Task<InsuranceClaimDto?> GetClaimByIdAsync(int id);
        Task<IEnumerable<InsuranceClaimDto>> GetClaimsByPatientInsuranceAsync(int patientInsuranceId);
        Task<IEnumerable<InsuranceClaimDto>> GetClaimsByAdmissionAsync(int admissionId);
        Task<InsuranceClaimDto> CreateClaimAsync(CreateInsuranceClaimDto dto);
        Task<InsuranceClaimDto?> UpdateClaimAsync(int id, UpdateInsuranceClaimDto dto);
        Task<bool> DeleteClaimAsync(int id);
    }
}