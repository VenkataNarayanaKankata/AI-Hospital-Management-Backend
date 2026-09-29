using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IPharmacyService
    {
        Task<List<MedicineBatchResponseDto>> GetAllBatchesAsync();

        Task<List<MedicineBatchResponseDto>> GetBatchesByMedicineIdAsync(
            int medicineId);

        Task<List<MedicineBatchResponseDto>> GetBatchesByBranchIdAsync(
            int branchId);

        Task<MedicineBatchResponseDto?> GetBatchByIdAsync(int id);

        Task<MedicineBatchResponseDto?> CreateBatchAsync(
            MedicineBatchCreateDto dto);

        Task<MedicineBatchResponseDto?> UpdateBatchAsync(
            int id,
            MedicineBatchUpdateDto dto);

        Task<bool> DeleteBatchAsync(int id);

        Task<List<PharmacySaleResponseDto>> GetAllSalesAsync();

        Task<List<PharmacySaleResponseDto>> GetSalesByPatientIdAsync(
            int patientId);

        Task<List<PharmacySaleResponseDto>> GetSalesByBranchIdAsync(
            int branchId);

        Task<PharmacySaleResponseDto?> GetSaleByIdAsync(int id);

        Task<PharmacySaleResponseDto?> CreateSaleAsync(
            PharmacySaleCreateDto dto);

        Task<PharmacySaleResponseDto?> UpdateSaleAsync(
            int id,
            PharmacySaleUpdateDto dto);

        Task<bool> DeleteSaleAsync(int id);
    }
}