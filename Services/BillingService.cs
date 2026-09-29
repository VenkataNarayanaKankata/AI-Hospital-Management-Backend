using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class BillingService : IBillingService
    {
        private readonly ApplicationDbContext _context;

        public BillingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceResponseDto>> GetAllAsync()
        {
            return await BuildQuery()
                .AsNoTracking()
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<List<InvoiceResponseDto>> GetByPatientIdAsync(
            int patientId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(i => i.PatientId == patientId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<List<InvoiceResponseDto>> GetByDoctorIdAsync(
            int doctorId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(i => i.DoctorId == doctorId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<List<InvoiceResponseDto>> GetByAppointmentIdAsync(
            int appointmentId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(i => i.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task<List<InvoiceResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(i => i.BranchId == branchId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<InvoiceResponseDto?> GetByIdAsync(int id)
        {
            return await BuildQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<InvoiceResponseDto?> CreateAsync(
            InvoiceCreateDto dto)
        {
            var validationResult =
                await ValidateInvoiceRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId);

            if (!validationResult)
            {
                return null;
            }

            if (dto.DiscountAmount > dto.TaxAmount + 10000000)
            {
                return null;
            }

            var lastInvoiceId =
                await _context.Invoices
                    .OrderByDescending(i => i.InvoiceId)
                    .Select(i => (int?)i.InvoiceId)
                    .FirstOrDefaultAsync();

            var nextInvoiceId =
                (lastInvoiceId ?? 0) + 1;

            var invoiceNumber =
                $"INV-{nextInvoiceId:D6}";

            var invoice = new Invoice
            {
                InvoiceNumber = invoiceNumber,
                InvoiceDate = dto.InvoiceDate,
                Status = "Unpaid",
                DiscountAmount = dto.DiscountAmount,
                TaxAmount = dto.TaxAmount,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,

                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                AppointmentId = dto.AppointmentId,
                DoctorId = dto.DoctorId
            };

            decimal subTotal = 0;

            foreach (var item in dto.Items)
            {
                var totalAmount =
                    item.Quantity * item.UnitPrice;

                subTotal += totalAmount;

                invoice.InvoiceItems.Add(
                    new InvoiceItem
                    {
                        ItemName = item.ItemName,
                        ItemType = item.ItemType,
                        Description = item.Description,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalAmount = totalAmount
                    });
            }

            var total =
                subTotal
                - dto.DiscountAmount
                + dto.TaxAmount;

            if (total < 0)
            {
                return null;
            }

            invoice.SubTotal = subTotal;
            invoice.TotalAmount = total;
            invoice.PaidAmount = 0;
            invoice.BalanceAmount = total;

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(invoice.InvoiceId);
        }

        public async Task<InvoiceResponseDto?> UpdateAsync(
            int id,
            InvoiceUpdateDto dto)
        {
            var invoice =
                await _context.Invoices
                    .Include(i => i.InvoiceItems)
                    .Include(i => i.Payments)
                    .FirstOrDefaultAsync(i =>
                        i.InvoiceId == id);

            if (invoice == null)
            {
                return null;
            }

            var validationResult =
                await ValidateInvoiceRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId);

            if (!validationResult)
            {
                return null;
            }

            var existingPaidAmount =
                invoice.Payments
                    .Where(p => p.Status == "Completed")
                    .Sum(p => p.Amount);

            var subTotal =
                dto.Items.Sum(i =>
                    i.Quantity * i.UnitPrice);

            var total =
                subTotal
                - dto.DiscountAmount
                + dto.TaxAmount;

            if (total < 0 ||
                existingPaidAmount > total)
            {
                return null;
            }

            invoice.InvoiceDate =
                dto.InvoiceDate;

            invoice.DiscountAmount =
                dto.DiscountAmount;

            invoice.TaxAmount =
                dto.TaxAmount;

            invoice.Notes =
                dto.Notes;

            invoice.BranchId =
                dto.BranchId;

            invoice.PatientId =
                dto.PatientId;

            invoice.AppointmentId =
                dto.AppointmentId;

            invoice.DoctorId =
                dto.DoctorId;

            invoice.SubTotal =
                subTotal;

            invoice.TotalAmount =
                total;

            invoice.PaidAmount =
                existingPaidAmount;

            invoice.BalanceAmount =
                total - existingPaidAmount;

            invoice.Status =
                CalculateInvoiceStatus(
                    total,
                    existingPaidAmount);

            _context.InvoiceItems.RemoveRange(
                invoice.InvoiceItems);

            invoice.InvoiceItems.Clear();

            foreach (var item in dto.Items)
            {
                invoice.InvoiceItems.Add(
                    new InvoiceItem
                    {
                        InvoiceId =
                            invoice.InvoiceId,

                        ItemName =
                            item.ItemName,

                        ItemType =
                            item.ItemType,

                        Description =
                            item.Description,

                        Quantity =
                            item.Quantity,

                        UnitPrice =
                            item.UnitPrice,

                        TotalAmount =
                            item.Quantity *
                            item.UnitPrice
                    });
            }

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<PaymentResponseDto?> AddPaymentAsync(
            int invoiceId,
            PaymentCreateDto dto)
        {
            var invoice =
                await _context.Invoices
                    .Include(i => i.Payments)
                    .FirstOrDefaultAsync(i =>
                        i.InvoiceId == invoiceId);

            if (invoice == null)
            {
                return null;
            }

            if (dto.Amount <= 0)
            {
                return null;
            }

            if (dto.Amount > invoice.BalanceAmount)
            {
                return null;
            }

            var lastPaymentId =
                await _context.Payments
                    .OrderByDescending(p => p.PaymentId)
                    .Select(p => (int?)p.PaymentId)
                    .FirstOrDefaultAsync();

            var nextPaymentId =
                (lastPaymentId ?? 0) + 1;

            var paymentNumber =
                $"PAY-{nextPaymentId:D6}";

            var payment = new Payment
            {
                PaymentNumber =
                    paymentNumber,

                PaymentDate =
                    dto.PaymentDate,

                Amount =
                    dto.Amount,

                PaymentMethod =
                    dto.PaymentMethod,

                Status =
                    "Completed",

                TransactionReference =
                    dto.TransactionReference,

                Notes =
                    dto.Notes,

                CreatedAt =
                    DateTime.UtcNow,

                InvoiceId =
                    invoiceId
            };

            _context.Payments.Add(payment);

            invoice.PaidAmount += dto.Amount;

            invoice.BalanceAmount =
                invoice.TotalAmount -
                invoice.PaidAmount;

            invoice.Status =
                CalculateInvoiceStatus(
                    invoice.TotalAmount,
                    invoice.PaidAmount);

            await _context.SaveChangesAsync();

            return new PaymentResponseDto
            {
                PaymentId =
                    payment.PaymentId,

                PaymentNumber =
                    payment.PaymentNumber,

                PaymentDate =
                    payment.PaymentDate,

                Amount =
                    payment.Amount,

                PaymentMethod =
                    payment.PaymentMethod,

                Status =
                    payment.Status,

                TransactionReference =
                    payment.TransactionReference,

                Notes =
                    payment.Notes,

                CreatedAt =
                    payment.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice =
                await _context.Invoices
                    .FirstOrDefaultAsync(i =>
                        i.InvoiceId == id);

            if (invoice == null)
            {
                return false;
            }

            var hasCompletedPayments =
                await _context.Payments
                    .AnyAsync(p =>
                        p.InvoiceId == id &&
                        p.Status == "Completed");

            if (hasCompletedPayments)
            {
                return false;
            }

            _context.Invoices.Remove(invoice);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> ValidateInvoiceRelationshipsAsync(
            int branchId,
            int patientId,
            int doctorId,
            int appointmentId)
        {
            var branchExists =
                await _context.Branches
                    .AnyAsync(b =>
                        b.BranchId == branchId);

            if (!branchExists)
            {
                return false;
            }

            var patient =
                await _context.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                        p.PatientId == patientId);

            if (patient == null ||
                patient.BranchId != branchId)
            {
                return false;
            }

            var doctor =
                await _context.Doctors
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d =>
                        d.DoctorId == doctorId);

            if (doctor == null ||
                doctor.BranchId != branchId)
            {
                return false;
            }

            var appointment =
                await _context.Appointments
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

        private static string CalculateInvoiceStatus(
            decimal totalAmount,
            decimal paidAmount)
        {
            if (paidAmount <= 0)
            {
                return "Unpaid";
            }

            if (paidAmount >= totalAmount)
            {
                return "Paid";
            }

            return "PartiallyPaid";
        }

        private IQueryable<InvoiceResponseDto> BuildQuery()
        {
            return _context.Invoices
                .Select(i => new InvoiceResponseDto
                {
                    InvoiceId =
                        i.InvoiceId,

                    InvoiceNumber =
                        i.InvoiceNumber,

                    InvoiceDate =
                        i.InvoiceDate,

                    Status =
                        i.Status,

                    SubTotal =
                        i.SubTotal,

                    DiscountAmount =
                        i.DiscountAmount,

                    TaxAmount =
                        i.TaxAmount,

                    TotalAmount =
                        i.TotalAmount,

                    PaidAmount =
                        i.PaidAmount,

                    BalanceAmount =
                        i.BalanceAmount,

                    Notes =
                        i.Notes,

                    CreatedAt =
                        i.CreatedAt,

                    BranchId =
                        i.BranchId,

                    BranchName =
                        i.Branch.BranchName,

                    HospitalId =
                        i.Branch.HospitalId,

                    HospitalName =
                        i.Branch.Hospital.HospitalName,

                    PatientId =
                        i.PatientId,

                    PatientNumber =
                        i.Patient.PatientNumber,

                    PatientName =
                        i.Patient.FullName,

                    DoctorId =
                        i.DoctorId,

                    DoctorName =
                        i.Doctor.FullName,

                    AppointmentId =
                        i.AppointmentId,

                    AppointmentNumber =
                        i.Appointment.AppointmentNumber,

                    Items =
                        i.InvoiceItems
                            .Select(ii =>
                                new InvoiceItemResponseDto
                                {
                                    InvoiceItemId =
                                        ii.InvoiceItemId,

                                    ItemName =
                                        ii.ItemName,

                                    ItemType =
                                        ii.ItemType,

                                    Description =
                                        ii.Description,

                                    Quantity =
                                        ii.Quantity,

                                    UnitPrice =
                                        ii.UnitPrice,

                                    TotalAmount =
                                        ii.TotalAmount
                                })
                            .ToList(),

                    Payments =
                        i.Payments
                            .Select(p =>
                                new PaymentResponseDto
                                {
                                    PaymentId =
                                        p.PaymentId,

                                    PaymentNumber =
                                        p.PaymentNumber,

                                    PaymentDate =
                                        p.PaymentDate,

                                    Amount =
                                        p.Amount,

                                    PaymentMethod =
                                        p.PaymentMethod,

                                    Status =
                                        p.Status,

                                    TransactionReference =
                                        p.TransactionReference,

                                    Notes =
                                        p.Notes,

                                    CreatedAt =
                                        p.CreatedAt
                                })
                            .ToList()
                });
        }
    }
}