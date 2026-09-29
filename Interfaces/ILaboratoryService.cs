using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface ILaboratoryService
    {
        Task<List<LaboratoryTestResponseDto>> GetAllTestsAsync();

        Task<LaboratoryTestResponseDto?> GetTestByIdAsync(int id);

        Task<LaboratoryTestResponseDto?> CreateTestAsync(
            LaboratoryTestCreateDto dto);

        Task<LaboratoryTestResponseDto?> UpdateTestAsync(
            int id,
            LaboratoryTestUpdateDto dto);

        Task<bool> DeleteTestAsync(int id);

        Task<List<LabOrderResponseDto>> GetAllOrdersAsync();

        Task<List<LabOrderResponseDto>> GetOrdersByPatientIdAsync(
            int patientId);

        Task<List<LabOrderResponseDto>> GetOrdersByDoctorIdAsync(
            int doctorId);

        Task<List<LabOrderResponseDto>> GetOrdersByAppointmentIdAsync(
            int appointmentId);

        Task<List<LabOrderResponseDto>> GetOrdersByBranchIdAsync(
            int branchId);

        Task<LabOrderResponseDto?> GetOrderByIdAsync(int id);

        Task<LabOrderResponseDto?> CreateOrderAsync(
            LabOrderCreateDto dto);

        Task<LabOrderResponseDto?> UpdateOrderAsync(
            int id,
            LabOrderUpdateDto dto);

        Task<bool> CollectSampleAsync(int labOrderItemId);

        Task<LabResultResponseDto?> AddResultAsync(
            int labOrderItemId,
            LabResultCreateDto dto);

        Task<LabResultResponseDto?> UpdateResultAsync(
            int labResultId,
            LabResultUpdateDto dto);

        Task<bool> DeleteOrderAsync(int id);
    }
}