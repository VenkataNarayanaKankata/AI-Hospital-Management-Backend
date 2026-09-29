using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly ApplicationDbContext _context;

        public PharmacyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicineBatchResponseDto>> GetAllBatchesAsync()
        {
            return await BuildBatchQuery()
                .AsNoTracking()
                .OrderBy(b => b.MedicineName)
                .ThenBy(b => b.ExpiryDate)
                .ToListAsync();
        }

        public async Task<List<MedicineBatchResponseDto>> GetBatchesByMedicineIdAsync(
            int medicineId)
        {
            return await BuildBatchQuery()
                .AsNoTracking()
                .Where(b => b.MedicineId == medicineId)
                .OrderBy(b => b.ExpiryDate)
                .ToListAsync();
        }

        public async Task<List<MedicineBatchResponseDto>> GetBatchesByBranchIdAsync(
            int branchId)
        {
            return await BuildBatchQuery()
                .AsNoTracking()
                .Where(b => b.BranchId == branchId)
                .OrderBy(b => b.ExpiryDate)
                .ToListAsync();
        }

        public async Task<MedicineBatchResponseDto?> GetBatchByIdAsync(int id)
        {
            return await BuildBatchQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.MedicineBatchId == id);
        }

        public async Task<MedicineBatchResponseDto?> CreateBatchAsync(
            MedicineBatchCreateDto dto)
        {
            if (dto.ExpiryDate <= dto.ManufacturingDate)
            {
                return null;
            }

            var medicineExists = await _context.Medicines
                .AnyAsync(m => m.MedicineId == dto.MedicineId);

            if (!medicineExists)
            {
                return null;
            }

            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            var duplicateBatch = await _context.MedicineBatches
                .AnyAsync(b =>
                    b.MedicineId == dto.MedicineId &&
                    b.BranchId == dto.BranchId &&
                    b.BatchNumber.ToLower() == dto.BatchNumber.ToLower());

            if (duplicateBatch)
            {
                return null;
            }

            if (dto.SellingPrice < dto.PurchasePrice)
            {
                return null;
            }

            var batch = new MedicineBatch
            {
                BatchNumber = dto.BatchNumber,
                ManufacturingDate = dto.ManufacturingDate,
                ExpiryDate = dto.ExpiryDate,
                Quantity = dto.Quantity,
                PurchasePrice = dto.PurchasePrice,
                SellingPrice = dto.SellingPrice,
                ReorderLevel = dto.ReorderLevel,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                MedicineId = dto.MedicineId,
                BranchId = dto.BranchId
            };

            _context.MedicineBatches.Add(batch);

            await _context.SaveChangesAsync();

            return await GetBatchByIdAsync(batch.MedicineBatchId);
        }

        public async Task<MedicineBatchResponseDto?> UpdateBatchAsync(
            int id,
            MedicineBatchUpdateDto dto)
        {
            var batch = await _context.MedicineBatches
                .FirstOrDefaultAsync(b =>
                    b.MedicineBatchId == id);

            if (batch == null)
            {
                return null;
            }

            if (dto.ExpiryDate <= dto.ManufacturingDate)
            {
                return null;
            }

            if (dto.SellingPrice < dto.PurchasePrice)
            {
                return null;
            }

            var duplicateBatch = await _context.MedicineBatches
                .AnyAsync(b =>
                    b.MedicineBatchId != id &&
                    b.MedicineId == batch.MedicineId &&
                    b.BranchId == batch.BranchId &&
                    b.BatchNumber.ToLower() ==
                        dto.BatchNumber.ToLower());

            if (duplicateBatch)
            {
                return null;
            }

            batch.BatchNumber = dto.BatchNumber;
            batch.ManufacturingDate = dto.ManufacturingDate;
            batch.ExpiryDate = dto.ExpiryDate;
            batch.Quantity = dto.Quantity;
            batch.PurchasePrice = dto.PurchasePrice;
            batch.SellingPrice = dto.SellingPrice;
            batch.ReorderLevel = dto.ReorderLevel;
            batch.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return await GetBatchByIdAsync(id);
        }

        public async Task<bool> DeleteBatchAsync(int id)
        {
            var batch = await _context.MedicineBatches
                .FirstOrDefaultAsync(b =>
                    b.MedicineBatchId == id);

            if (batch == null)
            {
                return false;
            }

            var isUsed = await _context.PharmacySaleItems
                .AnyAsync(i =>
                    i.MedicineBatchId == id);

            if (isUsed)
            {
                return false;
            }

            _context.MedicineBatches.Remove(batch);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PharmacySaleResponseDto>> GetAllSalesAsync()
        {
            return await BuildSaleQuery()
                .AsNoTracking()
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<List<PharmacySaleResponseDto>> GetSalesByPatientIdAsync(
            int patientId)
        {
            return await BuildSaleQuery()
                .AsNoTracking()
                .Where(s => s.PatientId == patientId)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<List<PharmacySaleResponseDto>> GetSalesByBranchIdAsync(
            int branchId)
        {
            return await BuildSaleQuery()
                .AsNoTracking()
                .Where(s => s.BranchId == branchId)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<PharmacySaleResponseDto?> GetSaleByIdAsync(int id)
        {
            return await BuildSaleQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(s =>
                    s.PharmacySaleId == id);
        }

        public async Task<PharmacySaleResponseDto?> CreateSaleAsync(
            PharmacySaleCreateDto dto)
        {
            if (dto.Items.Count == 0)
            {
                return null;
            }

            if (dto.DiscountAmount < 0 ||
                dto.TaxAmount < 0)
            {
                return null;
            }

            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            if (dto.PatientId.HasValue)
            {
                var patient = await _context.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                        p.PatientId == dto.PatientId.Value);

                if (patient == null ||
                    patient.BranchId != dto.BranchId)
                {
                    return null;
                }
            }

            Prescription? prescription = null;

            if (dto.PrescriptionId.HasValue)
            {
                prescription = await _context.Prescriptions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                        p.PrescriptionId ==
                        dto.PrescriptionId.Value);

                if (prescription == null)
                {
                    return null;
                }

                if (prescription.PatientId != dto.PatientId)
                {
                    return null;
                }
            }

            var batchIds = dto.Items
                .Select(i => i.MedicineBatchId)
                .Distinct()
                .ToList();

            if (batchIds.Count != dto.Items.Count)
            {
                return null;
            }

            var batches = await _context.MedicineBatches
                .Where(b =>
                    batchIds.Contains(b.MedicineBatchId) &&
                    b.BranchId == dto.BranchId &&
                    b.IsActive)
                .ToListAsync();

            if (batches.Count != batchIds.Count)
            {
                return null;
            }

            var currentDate = DateTime.UtcNow.Date;

            if (batches.Any(b => b.ExpiryDate.Date < currentDate))
            {
                return null;
            }

            foreach (var item in dto.Items)
            {
                var batch = batches.First(b =>
                    b.MedicineBatchId == item.MedicineBatchId);

                if (batch.Quantity < item.Quantity)
                {
                    return null;
                }
            }

            var lastSaleId = await _context.PharmacySales
                .OrderByDescending(s => s.PharmacySaleId)
                .Select(s => (int?)s.PharmacySaleId)
                .FirstOrDefaultAsync();

            var saleNumber =
                $"SALE-{((lastSaleId ?? 0) + 1):D6}";

            var sale = new PharmacySale
            {
                SaleNumber = saleNumber,
                SaleDate = DateTime.UtcNow,
                DiscountAmount = dto.DiscountAmount,
                TaxAmount = dto.TaxAmount,
                PaymentStatus = "Pending",
                PaymentMethod = dto.PaymentMethod,
                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                PrescriptionId = dto.PrescriptionId
            };

            decimal subtotal = 0;

            foreach (var item in dto.Items)
            {
                var batch = batches.First(b =>
                    b.MedicineBatchId == item.MedicineBatchId);

                var unitPrice = batch.SellingPrice;
                var totalPrice = unitPrice * item.Quantity;

                subtotal += totalPrice;

                batch.Quantity -= item.Quantity;

                sale.Items.Add(new PharmacySaleItem
                {
                    MedicineBatchId = item.MedicineBatchId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice
                });
            }

            if (dto.DiscountAmount > subtotal)
            {
                return null;
            }

            sale.SubTotal = subtotal;
            sale.TotalAmount =
                subtotal -
                dto.DiscountAmount +
                dto.TaxAmount;

            _context.PharmacySales.Add(sale);

            await _context.SaveChangesAsync();

            return await GetSaleByIdAsync(
                sale.PharmacySaleId);
        }

        public async Task<PharmacySaleResponseDto?> UpdateSaleAsync(
            int id,
            PharmacySaleUpdateDto dto)
        {
            var sale = await _context.PharmacySales
                .FirstOrDefaultAsync(s =>
                    s.PharmacySaleId == id);

            if (sale == null)
            {
                return null;
            }

            if (!IsValidPaymentStatus(
                    dto.PaymentStatus))
            {
                return null;
            }

            if (dto.DiscountAmount < 0 ||
                dto.TaxAmount < 0)
            {
                return null;
            }

            if (dto.DiscountAmount > sale.SubTotal)
            {
                return null;
            }

            sale.DiscountAmount = dto.DiscountAmount;
            sale.TaxAmount = dto.TaxAmount;
            sale.PaymentStatus = dto.PaymentStatus;
            sale.PaymentMethod = dto.PaymentMethod;

            sale.TotalAmount =
                sale.SubTotal -
                dto.DiscountAmount +
                dto.TaxAmount;

            await _context.SaveChangesAsync();

            return await GetSaleByIdAsync(id);
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.PharmacySales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s =>
                    s.PharmacySaleId == id);

            if (sale == null)
            {
                return false;
            }

            if (sale.PaymentStatus == "Paid")
            {
                return false;
            }

            var batches = await _context.MedicineBatches
                .Where(b =>
                    sale.Items
                        .Select(i => i.MedicineBatchId)
                        .Contains(b.MedicineBatchId))
                .ToListAsync();

            foreach (var item in sale.Items)
            {
                var batch = batches.FirstOrDefault(b =>
                    b.MedicineBatchId ==
                    item.MedicineBatchId);

                if (batch != null)
                {
                    batch.Quantity += item.Quantity;
                }
            }

            _context.PharmacySales.Remove(sale);

            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<MedicineBatchResponseDto> BuildBatchQuery()
        {
            return _context.MedicineBatches
                .Select(b => new MedicineBatchResponseDto
                {
                    MedicineBatchId = b.MedicineBatchId,
                    BatchNumber = b.BatchNumber,
                    ManufacturingDate = b.ManufacturingDate,
                    ExpiryDate = b.ExpiryDate,
                    Quantity = b.Quantity,
                    PurchasePrice = b.PurchasePrice,
                    SellingPrice = b.SellingPrice,
                    ReorderLevel = b.ReorderLevel,
                    IsActive = b.IsActive,
                    CreatedAt = b.CreatedAt,

                    MedicineId = b.MedicineId,
                    MedicineName = b.Medicine.MedicineName,

                    BranchId = b.BranchId,
                    BranchName = b.Branch.BranchName,

                    HospitalId = b.Branch.HospitalId,
                    HospitalName = b.Branch.Hospital.HospitalName
                });
        }

        private IQueryable<PharmacySaleResponseDto> BuildSaleQuery()
        {
            return _context.PharmacySales
                .Select(s => new PharmacySaleResponseDto
                {
                    PharmacySaleId = s.PharmacySaleId,
                    SaleNumber = s.SaleNumber,
                    SaleDate = s.SaleDate,
                    SubTotal = s.SubTotal,
                    DiscountAmount = s.DiscountAmount,
                    TaxAmount = s.TaxAmount,
                    TotalAmount = s.TotalAmount,
                    PaymentStatus = s.PaymentStatus,
                    PaymentMethod = s.PaymentMethod,

                    BranchId = s.BranchId,
                    BranchName = s.Branch.BranchName,

                    HospitalId = s.Branch.HospitalId,
                    HospitalName = s.Branch.Hospital.HospitalName,

                    PatientId = s.PatientId,
                    PatientName = s.Patient != null
                        ? s.Patient.FullName
                        : null,

                    PrescriptionId = s.PrescriptionId,

                    Items = s.Items
                        .Select(i => new PharmacySaleItemResponseDto
                        {
                            PharmacySaleItemId =
                                i.PharmacySaleItemId,

                            MedicineBatchId =
                                i.MedicineBatchId,

                            BatchNumber =
                                i.MedicineBatch.BatchNumber,

                            MedicineId =
                                i.MedicineBatch.MedicineId,

                            MedicineName =
                                i.MedicineBatch.Medicine.MedicineName,

                            Quantity =
                                i.Quantity,

                            UnitPrice =
                                i.UnitPrice,

                            TotalPrice =
                                i.TotalPrice
                        })
                        .ToList()
                });
        }

        private static bool IsValidPaymentStatus(
            string status)
        {
            return status == "Pending" ||
                   status == "Paid" ||
                   status == "PartiallyPaid" ||
                   status == "Cancelled";
        }
    }
}