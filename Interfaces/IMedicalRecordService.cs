using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecordResponseDto>> GetAllAsync();

        Task<List<MedicalRecordResponseDto>> GetByPatientIdAsync(
            int patientId);

        Task<List<MedicalRecordResponseDto>> GetByDoctorIdAsync(
            int doctorId);

        Task<List<MedicalRecordResponseDto>> GetByAppointmentIdAsync(
            int appointmentId);

        Task<List<MedicalRecordResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<MedicalRecordResponseDto?> GetByIdAsync(int id);

        Task<MedicalRecordResponseDto?> CreateAsync(
            MedicalRecordCreateDto dto);

        Task<MedicalRecordResponseDto?> UpdateAsync(
            int id,
            MedicalRecordUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}