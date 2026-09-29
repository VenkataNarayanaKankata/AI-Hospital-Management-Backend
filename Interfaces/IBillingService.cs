using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IBillingService
    {
        Task<List<InvoiceResponseDto>> GetAllAsync();

        Task<List<InvoiceResponseDto>> GetByPatientIdAsync(
            int patientId);

        Task<List<InvoiceResponseDto>> GetByDoctorIdAsync(
            int doctorId);

        Task<List<InvoiceResponseDto>> GetByAppointmentIdAsync(
            int appointmentId);

        Task<List<InvoiceResponseDto>> GetByBranchIdAsync(
            int branchId);

        Task<InvoiceResponseDto?> GetByIdAsync(int id);

        Task<InvoiceResponseDto?> CreateAsync(
            InvoiceCreateDto dto);

        Task<InvoiceResponseDto?> UpdateAsync(
            int id,
            InvoiceUpdateDto dto);

        Task<PaymentResponseDto?> AddPaymentAsync(
            int invoiceId,
            PaymentCreateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}