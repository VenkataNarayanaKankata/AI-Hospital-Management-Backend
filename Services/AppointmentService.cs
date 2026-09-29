using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        // Standard consultation slot duration.
        // Since Appointment currently stores only AppointmentTime,
        // every appointment is treated as a 30-minute slot.
        private static readonly TimeSpan AppointmentDuration =
            TimeSpan.FromMinutes(30);

        private static readonly string[] AllowedStatuses =
        {
            "Scheduled",
            "Confirmed",
            "Completed",
            "Cancelled",
            "NoShow"
        };

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentResponseDto>> GetAllAsync()
        {
            return await _context.Appointments
                .AsNoTracking()
                .Include(a => a.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Select(a => new AppointmentResponseDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Status = a.Status,
                    ReasonForVisit = a.ReasonForVisit,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    HospitalId = a.Branch.HospitalId,
                    HospitalName = a.Branch.Hospital.HospitalName,

                    PatientId = a.PatientId,
                    PatientNumber = a.Patient.PatientNumber,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    DepartmentId = a.DepartmentId,
                    DepartmentName = a.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.BranchId == branchId)
                .Include(a => a.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Select(a => new AppointmentResponseDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Status = a.Status,
                    ReasonForVisit = a.ReasonForVisit,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    HospitalId = a.Branch.HospitalId,
                    HospitalName = a.Branch.Hospital.HospitalName,

                    PatientId = a.PatientId,
                    PatientNumber = a.Patient.PatientNumber,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    DepartmentId = a.DepartmentId,
                    DepartmentName = a.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentResponseDto>> GetByPatientIdAsync(
            int patientId)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Select(a => new AppointmentResponseDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Status = a.Status,
                    ReasonForVisit = a.ReasonForVisit,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    HospitalId = a.Branch.HospitalId,
                    HospitalName = a.Branch.Hospital.HospitalName,

                    PatientId = a.PatientId,
                    PatientNumber = a.Patient.PatientNumber,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    DepartmentId = a.DepartmentId,
                    DepartmentName = a.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentResponseDto>> GetByDoctorIdAsync(
            int doctorId)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Select(a => new AppointmentResponseDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Status = a.Status,
                    ReasonForVisit = a.ReasonForVisit,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    HospitalId = a.Branch.HospitalId,
                    HospitalName = a.Branch.Hospital.HospitalName,

                    PatientId = a.PatientId,
                    PatientNumber = a.Patient.PatientNumber,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    DepartmentId = a.DepartmentId,
                    DepartmentName = a.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.AppointmentId == id)
                .Include(a => a.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Select(a => new AppointmentResponseDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Status = a.Status,
                    ReasonForVisit = a.ReasonForVisit,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    HospitalId = a.Branch.HospitalId,
                    HospitalName = a.Branch.Hospital.HospitalName,

                    PatientId = a.PatientId,
                    PatientNumber = a.Patient.PatientNumber,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    DepartmentId = a.DepartmentId,
                    DepartmentName = a.Department.DepartmentName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AppointmentResponseDto?> CreateAsync(
            AppointmentCreateDto dto)
        {
            // 1. Validate Branch
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == dto.BranchId);

            if (branch == null)
            {
                return null;
            }

            // 2. Validate Patient
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == dto.PatientId);

            if (patient == null)
            {
                return null;
            }

            // Patient must belong to selected Branch
            if (patient.BranchId != dto.BranchId)
            {
                return null;
            }

            // 3. Validate Department
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
            {
                return null;
            }

            // Department must belong to selected Branch
            if (department.BranchId != dto.BranchId)
            {
                return null;
            }

            // 4. Validate Doctor
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
            {
                return null;
            }

            // Doctor must belong to selected Branch
            if (doctor.BranchId != dto.BranchId)
            {
                return null;
            }

            // Doctor must belong to selected Department
            if (doctor.DepartmentId != dto.DepartmentId)
            {
                return null;
            }

            // 5. Validate Doctor Schedule
            var scheduleIsValid = await IsDoctorAvailableAsync(
                dto.DoctorId,
                dto.BranchId,
                dto.AppointmentDate,
                dto.AppointmentTime);

            if (!scheduleIsValid)
            {
                return null;
            }

            // 6. Prevent duplicate/overlapping appointment
            var appointmentConflict = await HasAppointmentConflictAsync(
                dto.DoctorId,
                dto.AppointmentDate,
                dto.AppointmentTime);

            if (appointmentConflict)
            {
                return null;
            }

            // 7. Generate Appointment Number
            var lastAppointmentId = await _context.Appointments
                .OrderByDescending(a => a.AppointmentId)
                .Select(a => (int?)a.AppointmentId)
                .FirstOrDefaultAsync();

            var nextAppointmentId = (lastAppointmentId ?? 0) + 1;

            var appointmentNumber =
                $"APT-{nextAppointmentId:D6}";

            // 8. Create Appointment
            var appointment = new Appointment
            {
                AppointmentNumber = appointmentNumber,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,

                Status = "Scheduled",

                ReasonForVisit = dto.ReasonForVisit,
                Notes = dto.Notes,

                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                DepartmentId = dto.DepartmentId,

                CreatedAt = DateTime.UtcNow
            };

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(appointment.AppointmentId);
        }

        public async Task<AppointmentResponseDto?> UpdateAsync(
            int id,
            AppointmentUpdateDto dto)
        {
            // 1. Find appointment
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return null;
            }

            // 2. Validate status
            if (!AllowedStatuses.Contains(dto.Status))
            {
                return null;
            }

            // 3. Validate Branch
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // 4. Validate Patient
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == dto.PatientId);

            if (patient == null)
            {
                return null;
            }

            // Patient must belong to selected Branch
            if (patient.BranchId != dto.BranchId)
            {
                return null;
            }

            // 5. Validate Department
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
            {
                return null;
            }

            // Department must belong to selected Branch
            if (department.BranchId != dto.BranchId)
            {
                return null;
            }

            // 6. Validate Doctor
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
            {
                return null;
            }

            // Doctor must belong to selected Branch
            if (doctor.BranchId != dto.BranchId)
            {
                return null;
            }

            // Doctor must belong to selected Department
            if (doctor.DepartmentId != dto.DepartmentId)
            {
                return null;
            }

            // 7. Validate Doctor Schedule
            var scheduleIsValid = await IsDoctorAvailableAsync(
                dto.DoctorId,
                dto.BranchId,
                dto.AppointmentDate,
                dto.AppointmentTime);

            if (!scheduleIsValid)
            {
                return null;
            }

            // 8. Prevent duplicate/overlapping appointment
            var appointmentConflict = await HasAppointmentConflictAsync(
                dto.DoctorId,
                dto.AppointmentDate,
                dto.AppointmentTime,
                id);

            if (appointmentConflict)
            {
                return null;
            }

            // 9. Update appointment
            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.AppointmentTime = dto.AppointmentTime;
            appointment.Status = dto.Status;

            appointment.ReasonForVisit = dto.ReasonForVisit;
            appointment.Notes = dto.Notes;

            appointment.BranchId = dto.BranchId;
            appointment.PatientId = dto.PatientId;
            appointment.DoctorId = dto.DoctorId;
            appointment.DepartmentId = dto.DepartmentId;

            // AppointmentNumber and CreatedAt remain unchanged.

            await _context.SaveChangesAsync();

            return await GetByIdAsync(appointment.AppointmentId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            return true;
        }

        // ============================================================
        // PRIVATE VALIDATION METHODS
        // ============================================================

        private async Task<bool> IsDoctorAvailableAsync(
    int doctorId,
    int branchId,
    DateTime appointmentDate,
    TimeSpan appointmentTime)
        {
            var appointmentDateTime =
                appointmentDate.Date + appointmentTime;

            if (appointmentDateTime <= DateTime.Now)
            {
                return false;
            }

            var availability = await _context.DoctorAvailabilities
                .AsNoTracking()
                .FirstOrDefaultAsync(da =>
                    da.DoctorId == doctorId &&
                    da.DayOfWeek == appointmentDate.DayOfWeek &&
                    da.IsAvailable);

            if (availability == null)
            {
                return false;
            }

            if (appointmentTime < availability.StartTime)
            {
                return false;
            }

            var appointmentEndTime =
                appointmentTime + AppointmentDuration;

            if (appointmentEndTime > availability.EndTime)
            {
                return false;
            }

            var hasApprovedLeave = await _context.DoctorLeaves
                .AsNoTracking()
                .AnyAsync(dl =>
                    dl.DoctorId == doctorId &&
                    dl.IsApproved &&
                    dl.StartDate.Date <= appointmentDate.Date &&
                    dl.EndDate.Date >= appointmentDate.Date &&
                    (
                        dl.IsFullDay ||
                        (
                            dl.StartDate.Date == appointmentDate.Date &&
                            dl.EndDate.Date == appointmentDate.Date &&
                            dl.StartTime.HasValue &&
                            dl.EndTime.HasValue &&
                            appointmentTime < dl.EndTime.Value &&
                            appointmentEndTime > dl.StartTime.Value
                        )
                    ));

            if (hasApprovedLeave)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> HasAppointmentConflictAsync(
            int doctorId,
            DateTime appointmentDate,
            TimeSpan appointmentTime,
            int? excludeAppointmentId = null)
        {
            var appointmentEndTime =
                appointmentTime + AppointmentDuration;

            var query = _context.Appointments
                .AsNoTracking()
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.AppointmentDate.Date == appointmentDate.Date &&
                    a.Status != "Cancelled" &&
                    a.Status != "NoShow");

            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a =>
                    a.AppointmentId != excludeAppointmentId.Value);
            }

            var existingAppointments = await query
                .Select(a => a.AppointmentTime)
                .ToListAsync();

            foreach (var existingAppointmentTime in existingAppointments)
            {
                var existingAppointmentEnd =
                    existingAppointmentTime + AppointmentDuration;

                var overlaps =
                    appointmentTime < existingAppointmentEnd &&
                    appointmentEndTime > existingAppointmentTime;

                if (overlaps)
                {
                    return true;
                }
            }

            return false;
        }
    }
}