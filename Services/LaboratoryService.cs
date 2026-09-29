using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class LaboratoryService : ILaboratoryService
    {
        private readonly ApplicationDbContext _context;

        public LaboratoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LaboratoryTestResponseDto>> GetAllTestsAsync()
        {
            return await _context.LaboratoryTests
                .AsNoTracking()
                .OrderBy(t => t.TestName)
                .Select(t => new LaboratoryTestResponseDto
                {
                    LaboratoryTestId = t.LaboratoryTestId,
                    TestName = t.TestName,
                    TestCode = t.TestCode,
                    Description = t.Description,
                    SampleType = t.SampleType,
                    NormalRange = t.NormalRange,
                    Unit = t.Unit,
                    TestFee = t.TestFee,
                    TurnaroundTimeHours = t.TurnaroundTimeHours,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<LaboratoryTestResponseDto?> GetTestByIdAsync(int id)
        {
            return await _context.LaboratoryTests
                .AsNoTracking()
                .Where(t => t.LaboratoryTestId == id)
                .Select(t => new LaboratoryTestResponseDto
                {
                    LaboratoryTestId = t.LaboratoryTestId,
                    TestName = t.TestName,
                    TestCode = t.TestCode,
                    Description = t.Description,
                    SampleType = t.SampleType,
                    NormalRange = t.NormalRange,
                    Unit = t.Unit,
                    TestFee = t.TestFee,
                    TurnaroundTimeHours = t.TurnaroundTimeHours,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LaboratoryTestResponseDto?> CreateTestAsync(
            LaboratoryTestCreateDto dto)
        {
            if (await _context.LaboratoryTests
                .AnyAsync(t =>
                    t.TestName.ToLower() == dto.TestName.ToLower()))
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.TestCode) &&
                await _context.LaboratoryTests
                    .AnyAsync(t =>
                        t.TestCode != null &&
                        t.TestCode.ToLower() == dto.TestCode.ToLower()))
            {
                return null;
            }

            var test = new LaboratoryTest
            {
                TestName = dto.TestName,
                TestCode = dto.TestCode,
                Description = dto.Description,
                SampleType = dto.SampleType,
                NormalRange = dto.NormalRange,
                Unit = dto.Unit,
                TestFee = dto.TestFee,
                TurnaroundTimeHours = dto.TurnaroundTimeHours,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.LaboratoryTests.Add(test);

            await _context.SaveChangesAsync();

            return await GetTestByIdAsync(test.LaboratoryTestId);
        }

        public async Task<LaboratoryTestResponseDto?> UpdateTestAsync(
            int id,
            LaboratoryTestUpdateDto dto)
        {
            var test = await _context.LaboratoryTests
                .FirstOrDefaultAsync(t =>
                    t.LaboratoryTestId == id);

            if (test == null)
            {
                return null;
            }

            if (await _context.LaboratoryTests
                .AnyAsync(t =>
                    t.LaboratoryTestId != id &&
                    t.TestName.ToLower() == dto.TestName.ToLower()))
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.TestCode) &&
                await _context.LaboratoryTests
                    .AnyAsync(t =>
                        t.LaboratoryTestId != id &&
                        t.TestCode != null &&
                        t.TestCode.ToLower() == dto.TestCode.ToLower()))
            {
                return null;
            }

            test.TestName = dto.TestName;
            test.TestCode = dto.TestCode;
            test.Description = dto.Description;
            test.SampleType = dto.SampleType;
            test.NormalRange = dto.NormalRange;
            test.Unit = dto.Unit;
            test.TestFee = dto.TestFee;
            test.TurnaroundTimeHours = dto.TurnaroundTimeHours;
            test.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return await GetTestByIdAsync(id);
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            var test = await _context.LaboratoryTests
                .FirstOrDefaultAsync(t =>
                    t.LaboratoryTestId == id);

            if (test == null)
            {
                return false;
            }

            var isUsed = await _context.LabOrderItems
                .AnyAsync(i =>
                    i.LaboratoryTestId == id);

            if (isUsed)
            {
                return false;
            }

            _context.LaboratoryTests.Remove(test);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<LabOrderResponseDto>> GetAllOrdersAsync()
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<LabOrderResponseDto>> GetOrdersByPatientIdAsync(
            int patientId)
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .Where(o => o.PatientId == patientId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<LabOrderResponseDto>> GetOrdersByDoctorIdAsync(
            int doctorId)
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .Where(o => o.DoctorId == doctorId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<LabOrderResponseDto>> GetOrdersByAppointmentIdAsync(
            int appointmentId)
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .Where(o => o.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task<List<LabOrderResponseDto>> GetOrdersByBranchIdAsync(
            int branchId)
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .Where(o => o.BranchId == branchId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<LabOrderResponseDto?> GetOrderByIdAsync(int id)
        {
            return await BuildOrderQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(o =>
                    o.LabOrderId == id);
        }

        public async Task<LabOrderResponseDto?> CreateOrderAsync(
            LabOrderCreateDto dto)
        {
            var validRelationship =
                await ValidateOrderRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId);

            if (!validRelationship)
            {
                return null;
            }

            if (dto.Items.Count == 0)
            {
                return null;
            }

            var testIds = dto.Items
                .Select(i => i.LaboratoryTestId)
                .Distinct()
                .ToList();

            if (testIds.Count != dto.Items.Count)
            {
                return null;
            }

            var tests = await _context.LaboratoryTests
                .Where(t =>
                    testIds.Contains(t.LaboratoryTestId) &&
                    t.IsActive)
                .ToListAsync();

            if (tests.Count != testIds.Count)
            {
                return null;
            }

            var lastOrderId = await _context.LabOrders
                .OrderByDescending(o => o.LabOrderId)
                .Select(o => (int?)o.LabOrderId)
                .FirstOrDefaultAsync();

            var orderNumber =
                $"LAB-{((lastOrderId ?? 0) + 1):D6}";

            var order = new LabOrder
            {
                LabOrderNumber = orderNumber,
                OrderDate = dto.OrderDate,
                Status = "Ordered",
                ClinicalNotes = dto.ClinicalNotes,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId
            };

            foreach (var item in dto.Items)
            {
                order.LabOrderItems.Add(new LabOrderItem
                {
                    LaboratoryTestId = item.LaboratoryTestId,
                    Status = "Ordered",
                    Notes = item.Notes
                });
            }

            _context.LabOrders.Add(order);

            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.LabOrderId);
        }

        public async Task<LabOrderResponseDto?> UpdateOrderAsync(
            int id,
            LabOrderUpdateDto dto)
        {
            var order = await _context.LabOrders
                .Include(o => o.LabOrderItems)
                .ThenInclude(i => i.LabResult)
                .FirstOrDefaultAsync(o =>
                    o.LabOrderId == id);

            if (order == null)
            {
                return null;
            }

            if (!IsValidOrderStatus(dto.Status))
            {
                return null;
            }

            var validRelationship =
                await ValidateOrderRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId);

            if (!validRelationship)
            {
                return null;
            }

            var testIds = dto.Items
                .Select(i => i.LaboratoryTestId)
                .Distinct()
                .ToList();

            if (testIds.Count != dto.Items.Count)
            {
                return null;
            }

            var tests = await _context.LaboratoryTests
                .Where(t =>
                    testIds.Contains(t.LaboratoryTestId) &&
                    t.IsActive)
                .ToListAsync();

            if (tests.Count != testIds.Count)
            {
                return null;
            }

            var hasResults = order.LabOrderItems
                .Any(i => i.LabResult != null);

            if (hasResults)
            {
                return null;
            }

            order.OrderDate = dto.OrderDate;
            order.Status = dto.Status;
            order.ClinicalNotes = dto.ClinicalNotes;
            order.Notes = dto.Notes;
            order.BranchId = dto.BranchId;
            order.PatientId = dto.PatientId;
            order.DoctorId = dto.DoctorId;
            order.AppointmentId = dto.AppointmentId;

            _context.LabOrderItems.RemoveRange(
                order.LabOrderItems);

            order.LabOrderItems.Clear();

            foreach (var item in dto.Items)
            {
                order.LabOrderItems.Add(new LabOrderItem
                {
                    LabOrderId = order.LabOrderId,
                    LaboratoryTestId = item.LaboratoryTestId,
                    Status = dto.Status == "Ordered"
                        ? "Ordered"
                        : "Processing",
                    Notes = item.Notes
                });
            }

            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(id);
        }

        public async Task<bool> CollectSampleAsync(
            int labOrderItemId)
        {
            var item = await _context.LabOrderItems
                .Include(i => i.LabOrder)
                .FirstOrDefaultAsync(i =>
                    i.LabOrderItemId == labOrderItemId);

            if (item == null)
            {
                return false;
            }

            if (item.Status != "Ordered")
            {
                return false;
            }

            item.Status = "SampleCollected";
            item.SampleCollectedAt = DateTime.UtcNow;

            var orderItems = await _context.LabOrderItems
                .Where(i =>
                    i.LabOrderId == item.LabOrderId)
                .ToListAsync();

            if (orderItems.All(i =>
                i.Status == "SampleCollected" ||
                i.Status == "Processing" ||
                i.Status == "Completed"))
            {
                item.LabOrder.Status = "SampleCollected";
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LabResultResponseDto?> AddResultAsync(
            int labOrderItemId,
            LabResultCreateDto dto)
        {
            var item = await _context.LabOrderItems
                .Include(i => i.LabResult)
                .Include(i => i.LabOrder)
                .FirstOrDefaultAsync(i =>
                    i.LabOrderItemId == labOrderItemId);

            if (item == null)
            {
                return null;
            }

            if (item.LabResult != null)
            {
                return null;
            }

            if (item.Status != "SampleCollected" &&
                item.Status != "Processing")
            {
                return null;
            }

            var result = new LabResult
            {
                LabOrderItemId = labOrderItemId,
                ResultValue = dto.ResultValue,
                NormalRange = dto.NormalRange,
                Unit = dto.Unit,
                Remarks = dto.Remarks,
                ResultedBy = dto.ResultedBy,
                ResultedAt = dto.ResultedAt,
                IsAbnormal = dto.IsAbnormal,
                CreatedAt = DateTime.UtcNow
            };

            item.Status = "Completed";
            item.CompletedAt = DateTime.UtcNow;

            _context.LabResults.Add(result);

            var orderItems = await _context.LabOrderItems
                .Where(i =>
                    i.LabOrderId == item.LabOrderId)
                .ToListAsync();

            if (orderItems.All(i =>
                i.Status == "Completed"))
            {
                item.LabOrder.Status = "Completed";
                item.LabOrder.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                item.LabOrder.Status = "Processing";
            }

            await _context.SaveChangesAsync();

            return new LabResultResponseDto
            {
                LabResultId = result.LabResultId,
                ResultValue = result.ResultValue,
                NormalRange = result.NormalRange,
                Unit = result.Unit,
                Remarks = result.Remarks,
                ResultedBy = result.ResultedBy,
                ResultedAt = result.ResultedAt,
                IsAbnormal = result.IsAbnormal,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<LabResultResponseDto?> UpdateResultAsync(
            int labResultId,
            LabResultUpdateDto dto)
        {
            var result = await _context.LabResults
                .FirstOrDefaultAsync(r =>
                    r.LabResultId == labResultId);

            if (result == null)
            {
                return null;
            }

            result.ResultValue = dto.ResultValue;
            result.NormalRange = dto.NormalRange;
            result.Unit = dto.Unit;
            result.Remarks = dto.Remarks;
            result.ResultedBy = dto.ResultedBy;
            result.ResultedAt = dto.ResultedAt;
            result.IsAbnormal = dto.IsAbnormal;

            await _context.SaveChangesAsync();

            return new LabResultResponseDto
            {
                LabResultId = result.LabResultId,
                ResultValue = result.ResultValue,
                NormalRange = result.NormalRange,
                Unit = result.Unit,
                Remarks = result.Remarks,
                ResultedBy = result.ResultedBy,
                ResultedAt = result.ResultedAt,
                IsAbnormal = result.IsAbnormal,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.LabOrders
                .Include(o => o.LabOrderItems)
                .ThenInclude(i => i.LabResult)
                .FirstOrDefaultAsync(o =>
                    o.LabOrderId == id);

            if (order == null)
            {
                return false;
            }

            var hasResults = order.LabOrderItems
                .Any(i => i.LabResult != null);

            if (hasResults)
            {
                return false;
            }

            _context.LabOrders.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> ValidateOrderRelationshipsAsync(
            int branchId,
            int patientId,
            int doctorId,
            int appointmentId)
        {
            var branchExists = await _context.Branches
                .AnyAsync(b =>
                    b.BranchId == branchId);

            if (!branchExists)
            {
                return false;
            }

            var patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.PatientId == patientId);

            if (patient == null ||
                patient.BranchId != branchId)
            {
                return false;
            }

            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == doctorId);

            if (doctor == null ||
                doctor.BranchId != branchId)
            {
                return false;
            }

            var appointment = await _context.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return false;
            }

            if (appointment.BranchId != branchId ||
                appointment.PatientId != patientId ||
                appointment.DoctorId != doctorId)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidOrderStatus(string status)
        {
            return status == "Ordered" ||
                   status == "SampleCollected" ||
                   status == "Processing" ||
                   status == "Completed" ||
                   status == "Cancelled";
        }

        private IQueryable<LabOrderResponseDto> BuildOrderQuery()
        {
            return _context.LabOrders
                .Select(o => new LabOrderResponseDto
                {
                    LabOrderId = o.LabOrderId,
                    LabOrderNumber = o.LabOrderNumber,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    SampleCollectedAt = o.SampleCollectedAt,
                    CompletedAt = o.CompletedAt,
                    ClinicalNotes = o.ClinicalNotes,
                    Notes = o.Notes,
                    CreatedAt = o.CreatedAt,

                    BranchId = o.BranchId,
                    BranchName = o.Branch.BranchName,

                    HospitalId = o.Branch.HospitalId,
                    HospitalName = o.Branch.Hospital.HospitalName,

                    PatientId = o.PatientId,
                    PatientNumber = o.Patient.PatientNumber,
                    PatientName = o.Patient.FullName,

                    DoctorId = o.DoctorId,
                    DoctorName = o.Doctor.FullName,

                    AppointmentId = o.AppointmentId,
                    AppointmentNumber = o.Appointment.AppointmentNumber,

                    Items = o.LabOrderItems
                        .Select(i => new LabOrderItemResponseDto
                        {
                            LabOrderItemId = i.LabOrderItemId,

                            LaboratoryTestId =
                                i.LaboratoryTestId,

                            TestName =
                                i.LaboratoryTest.TestName,

                            TestCode =
                                i.LaboratoryTest.TestCode,

                            Status =
                                i.Status,

                            SampleCollectedAt =
                                i.SampleCollectedAt,

                            CompletedAt =
                                i.CompletedAt,

                            Notes =
                                i.Notes,

                            Result =
                                i.LabResult == null
                                    ? null
                                    : new LabResultResponseDto
                                    {
                                        LabResultId =
                                            i.LabResult.LabResultId,

                                        ResultValue =
                                            i.LabResult.ResultValue,

                                        NormalRange =
                                            i.LabResult.NormalRange,

                                        Unit =
                                            i.LabResult.Unit,

                                        Remarks =
                                            i.LabResult.Remarks,

                                        ResultedBy =
                                            i.LabResult.ResultedBy,

                                        ResultedAt =
                                            i.LabResult.ResultedAt,

                                        IsAbnormal =
                                            i.LabResult.IsAbnormal,

                                        CreatedAt =
                                            i.LabResult.CreatedAt
                                    }
                        })
                        .ToList()
                });
        }
    }
}