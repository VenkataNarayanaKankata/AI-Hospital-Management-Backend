using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly ApplicationDbContext _context;

        public DoctorScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorScheduleResponseDto>> GetAllAsync()
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(ds => ds.Doctor.Department)
                .Select(ds => new DoctorScheduleResponseDto
                {
                    DoctorScheduleId = ds.DoctorScheduleId,
                    DayOfWeek = ds.DayOfWeek,
                    StartTime = ds.StartTime,
                    EndTime = ds.EndTime,
                    BreakStart = ds.BreakStart,
                    BreakEnd = ds.BreakEnd,
                    IsAvailable = ds.IsAvailable,
                    CreatedAt = ds.CreatedAt,

                    DoctorId = ds.DoctorId,
                    DoctorName = ds.Doctor.FullName,

                    BranchId = ds.BranchId,
                    BranchName = ds.Branch.BranchName,

                    DepartmentId = ds.Doctor.DepartmentId,
                    DepartmentName = ds.Doctor.Department.DepartmentName,

                    HospitalId = ds.Branch.HospitalId,
                    HospitalName = ds.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<DoctorScheduleResponseDto>> GetByDoctorIdAsync(
            int doctorId)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Where(ds => ds.DoctorId == doctorId)
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(ds => ds.Doctor.Department)
                .Select(ds => new DoctorScheduleResponseDto
                {
                    DoctorScheduleId = ds.DoctorScheduleId,
                    DayOfWeek = ds.DayOfWeek,
                    StartTime = ds.StartTime,
                    EndTime = ds.EndTime,
                    BreakStart = ds.BreakStart,
                    BreakEnd = ds.BreakEnd,
                    IsAvailable = ds.IsAvailable,
                    CreatedAt = ds.CreatedAt,

                    DoctorId = ds.DoctorId,
                    DoctorName = ds.Doctor.FullName,

                    BranchId = ds.BranchId,
                    BranchName = ds.Branch.BranchName,

                    DepartmentId = ds.Doctor.DepartmentId,
                    DepartmentName = ds.Doctor.Department.DepartmentName,

                    HospitalId = ds.Branch.HospitalId,
                    HospitalName = ds.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<DoctorScheduleResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Where(ds => ds.BranchId == branchId)
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(ds => ds.Doctor.Department)
                .Select(ds => new DoctorScheduleResponseDto
                {
                    DoctorScheduleId = ds.DoctorScheduleId,
                    DayOfWeek = ds.DayOfWeek,
                    StartTime = ds.StartTime,
                    EndTime = ds.EndTime,
                    BreakStart = ds.BreakStart,
                    BreakEnd = ds.BreakEnd,
                    IsAvailable = ds.IsAvailable,
                    CreatedAt = ds.CreatedAt,

                    DoctorId = ds.DoctorId,
                    DoctorName = ds.Doctor.FullName,

                    BranchId = ds.BranchId,
                    BranchName = ds.Branch.BranchName,

                    DepartmentId = ds.Doctor.DepartmentId,
                    DepartmentName = ds.Doctor.Department.DepartmentName,

                    HospitalId = ds.Branch.HospitalId,
                    HospitalName = ds.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<DoctorScheduleResponseDto?> GetByIdAsync(int id)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Where(ds => ds.DoctorScheduleId == id)
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(ds => ds.Doctor.Department)
                .Select(ds => new DoctorScheduleResponseDto
                {
                    DoctorScheduleId = ds.DoctorScheduleId,
                    DayOfWeek = ds.DayOfWeek,
                    StartTime = ds.StartTime,
                    EndTime = ds.EndTime,
                    BreakStart = ds.BreakStart,
                    BreakEnd = ds.BreakEnd,
                    IsAvailable = ds.IsAvailable,
                    CreatedAt = ds.CreatedAt,

                    DoctorId = ds.DoctorId,
                    DoctorName = ds.Doctor.FullName,

                    BranchId = ds.BranchId,
                    BranchName = ds.Branch.BranchName,

                    DepartmentId = ds.Doctor.DepartmentId,
                    DepartmentName = ds.Doctor.Department.DepartmentName,

                    HospitalId = ds.Branch.HospitalId,
                    HospitalName = ds.Branch.Hospital.HospitalName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DoctorScheduleResponseDto?> CreateAsync(
            DoctorScheduleCreateDto dto)
        {
            // 1. Verify Doctor belongs to the selected Branch
            var doctor = await _context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId &&
                    d.BranchId == dto.BranchId);

            if (doctor == null)
            {
                return null;
            }

            // 2. Verify Branch exists
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // 3. Verify Doctor's Department belongs to the same Branch
            if (doctor.Department.BranchId != dto.BranchId)
            {
                return null;
            }

            // 4. Validate working hours
            if (dto.StartTime >= dto.EndTime)
            {
                return null;
            }

            // 5. Validate break times
            if (dto.BreakStart.HasValue || dto.BreakEnd.HasValue)
            {
                if (!dto.BreakStart.HasValue || !dto.BreakEnd.HasValue)
                {
                    return null;
                }

                if (dto.BreakStart.Value >= dto.BreakEnd.Value)
                {
                    return null;
                }

                if (dto.BreakStart.Value < dto.StartTime ||
                    dto.BreakEnd.Value > dto.EndTime)
                {
                    return null;
                }
            }

            // 6. Prevent duplicate schedule for same Doctor + Day
            var scheduleExists = await _context.DoctorSchedules
                .AnyAsync(ds =>
                    ds.DoctorId == dto.DoctorId &&
                    ds.DayOfWeek == dto.DayOfWeek);

            if (scheduleExists)
            {
                return null;
            }

            var schedule = new DoctorSchedule
            {
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                BreakStart = dto.BreakStart,
                BreakEnd = dto.BreakEnd,
                IsAvailable = dto.IsAvailable,
                DoctorId = dto.DoctorId,
                BranchId = dto.BranchId,
                CreatedAt = DateTime.UtcNow
            };

            _context.DoctorSchedules.Add(schedule);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(schedule.DoctorScheduleId);
        }

        public async Task<DoctorScheduleResponseDto?> UpdateAsync(
            int id,
            DoctorScheduleUpdateDto dto)
        {
            var schedule = await _context.DoctorSchedules
                .FirstOrDefaultAsync(ds =>
                    ds.DoctorScheduleId == id);

            if (schedule == null)
            {
                return null;
            }

            // 1. Verify Doctor belongs to the selected Branch
            var doctor = await _context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId &&
                    d.BranchId == dto.BranchId);

            if (doctor == null)
            {
                return null;
            }

            // 2. Verify Doctor's Department belongs to same Branch
            if (doctor.Department.BranchId != dto.BranchId)
            {
                return null;
            }

            // 3. Validate working hours
            if (dto.StartTime >= dto.EndTime)
            {
                return null;
            }

            // 4. Validate break times
            if (dto.BreakStart.HasValue || dto.BreakEnd.HasValue)
            {
                if (!dto.BreakStart.HasValue || !dto.BreakEnd.HasValue)
                {
                    return null;
                }

                if (dto.BreakStart.Value >= dto.BreakEnd.Value)
                {
                    return null;
                }

                if (dto.BreakStart.Value < dto.StartTime ||
                    dto.BreakEnd.Value > dto.EndTime)
                {
                    return null;
                }
            }

            // 5. Prevent duplicate schedule for same Doctor + Day
            var scheduleExists = await _context.DoctorSchedules
                .AnyAsync(ds =>
                    ds.DoctorId == dto.DoctorId &&
                    ds.DayOfWeek == dto.DayOfWeek &&
                    ds.DoctorScheduleId != id);

            if (scheduleExists)
            {
                return null;
            }

            schedule.DayOfWeek = dto.DayOfWeek;
            schedule.StartTime = dto.StartTime;
            schedule.EndTime = dto.EndTime;
            schedule.BreakStart = dto.BreakStart;
            schedule.BreakEnd = dto.BreakEnd;
            schedule.IsAvailable = dto.IsAvailable;
            schedule.DoctorId = dto.DoctorId;
            schedule.BranchId = dto.BranchId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(schedule.DoctorScheduleId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.DoctorSchedules
                .FirstOrDefaultAsync(ds =>
                    ds.DoctorScheduleId == id);

            if (schedule == null)
            {
                return false;
            }

            _context.DoctorSchedules.Remove(schedule);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}