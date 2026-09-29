using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var today = DateTime.Today;

            var totalPatients = await _context.Patients
                .CountAsync();

            var totalDoctors = await _context.Doctors
                .CountAsync();

            var todaysAppointments = await _context.Appointments
                .CountAsync(a => a.AppointmentDate.Date == today);

            var currentAdmissions = await _context.Admissions
                .CountAsync(a => a.Status == "Admitted");

            var availableBeds = await _context.Beds
                .CountAsync(b =>
                    b.Status == "Available" &&
                    b.IsActive);

            var totalRevenue = await _context.Payments
                .Where(p => p.Status == "Completed")
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var pendingLabOrders = await _context.LabOrders
                .CountAsync(l =>
                    l.Status != "Completed" &&
                    l.Status != "Cancelled");

            var pharmacySales = await _context.PharmacySales
                .Where(s => s.PaymentStatus == "Paid")
                .SumAsync(s => (decimal?)s.TotalAmount) ?? 0;

            return new DashboardSummaryDto
            {
                TotalPatients = totalPatients,
                TotalDoctors = totalDoctors,
                TodaysAppointments = todaysAppointments,
                CurrentAdmissions = currentAdmissions,
                AvailableBeds = availableBeds,
                TotalRevenue = totalRevenue,
                PendingLabOrders = pendingLabOrders,
                PharmacySales = pharmacySales
            };
        }
        public async Task<IEnumerable<AppointmentStatusDto>> GetAppointmentStatusAsync()
        {
            return await _context.Appointments
                .GroupBy(a => a.Status)
                .Select(g => new AppointmentStatusDto
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Status)
                .ToListAsync();
        }
    }
}