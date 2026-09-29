using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IPrescriptionService
    {
        Task<List<PrescriptionResponseDto>> GetAllAsync();

        Task<List<PrescriptionResponseDto>> GetByPatientIdAsync(
            int patientId);

        Task<List<PrescriptionResponseDto>> GetByDoctorIdAsync(
            int doctorId);

        Task<List<PrescriptionResponseDto>> GetByAppointmentIdAsync(
            int appointmentId);

        Task<List<PrescriptionResponseDto>> GetByMedicalRecordIdAsync(
            int medicalRecordId);

        Task<List<PrescriptionResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<PrescriptionResponseDto?> GetByIdAsync(int id);

        Task<PrescriptionResponseDto?> CreateAsync(
            PrescriptionCreateDto dto);

        Task<PrescriptionResponseDto?> UpdateAsync(
            int id,
            PrescriptionUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}