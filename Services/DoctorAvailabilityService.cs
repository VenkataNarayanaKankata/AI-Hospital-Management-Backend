using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly ApplicationDbContext _context;

        public DoctorAvailabilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorAvailabilityDto>> GetAllAsync()
        {
            return await _context.DoctorAvailabilities
                .Include(da => da.Doctor)
                .Select(da => new DoctorAvailabilityDto
                {
                    DoctorAvailabilityId = da.DoctorAvailabilityId,
                    DayOfWeek = da.DayOfWeek,
                    StartTime = da.StartTime,
                    EndTime = da.EndTime,
                    IsAvailable = da.IsAvailable,
                    DoctorId = da.DoctorId,
                    DoctorName = da.Doctor.FullName
                })
                .ToListAsync();
        }

        public async Task<DoctorAvailabilityDto?> GetByIdAsync(int id)
        {
            return await _context.DoctorAvailabilities
                .Include(da => da.Doctor)
                .Where(da => da.DoctorAvailabilityId == id)
                .Select(da => new DoctorAvailabilityDto
                {
                    DoctorAvailabilityId = da.DoctorAvailabilityId,
                    DayOfWeek = da.DayOfWeek,
                    StartTime = da.StartTime,
                    EndTime = da.EndTime,
                    IsAvailable = da.IsAvailable,
                    DoctorId = da.DoctorId,
                    DoctorName = da.Doctor.FullName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DoctorAvailabilityDto>>
            GetByDoctorAsync(int doctorId)
        {
            return await _context.DoctorAvailabilities
                .Include(da => da.Doctor)
                .Where(da => da.DoctorId == doctorId)
                .OrderBy(da => da.DayOfWeek)
                .ThenBy(da => da.StartTime)
                .Select(da => new DoctorAvailabilityDto
                {
                    DoctorAvailabilityId = da.DoctorAvailabilityId,
                    DayOfWeek = da.DayOfWeek,
                    StartTime = da.StartTime,
                    EndTime = da.EndTime,
                    IsAvailable = da.IsAvailable,
                    DoctorId = da.DoctorId,
                    DoctorName = da.Doctor.FullName
                })
                .ToListAsync();
        }

        public async Task<DoctorAvailabilityDto> CreateAsync(
            CreateDoctorAvailabilityDto dto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            if (!doctor.IsActive)
                throw new InvalidOperationException(
                    "Cannot create availability for an inactive doctor.");

            if (dto.StartTime >= dto.EndTime)
                throw new ArgumentException(
                    "Start time must be earlier than end time.");

            var overlapping = await _context.DoctorAvailabilities
                .AnyAsync(da =>
                    da.DoctorId == dto.DoctorId &&
                    da.DayOfWeek == dto.DayOfWeek &&
                    dto.StartTime < da.EndTime &&
                    dto.EndTime > da.StartTime);

            if (overlapping)
                throw new InvalidOperationException(
                    "The availability period overlaps with an existing schedule.");

            var availability = new DoctorAvailability
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsAvailable = dto.IsAvailable
            };

            _context.DoctorAvailabilities.Add(availability);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(availability.DoctorAvailabilityId)
                ?? throw new InvalidOperationException(
                    "Unable to retrieve created availability.");
        }

        public async Task<DoctorAvailabilityDto?> UpdateAsync(
            int id,
            UpdateDoctorAvailabilityDto dto)
        {
            var availability = await _context.DoctorAvailabilities
                .FirstOrDefaultAsync(da =>
                    da.DoctorAvailabilityId == id);

            if (availability == null)
                return null;

            if (dto.StartTime >= dto.EndTime)
                throw new ArgumentException(
                    "Start time must be earlier than end time.");

            var overlapping = await _context.DoctorAvailabilities
                .AnyAsync(da =>
                    da.DoctorAvailabilityId != id &&
                    da.DoctorId == availability.DoctorId &&
                    da.DayOfWeek == dto.DayOfWeek &&
                    dto.StartTime < da.EndTime &&
                    dto.EndTime > da.StartTime);

            if (overlapping)
                throw new InvalidOperationException(
                    "The availability period overlaps with an existing schedule.");

            availability.DayOfWeek = dto.DayOfWeek;
            availability.StartTime = dto.StartTime;
            availability.EndTime = dto.EndTime;
            availability.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var availability = await _context.DoctorAvailabilities
                .FirstOrDefaultAsync(da =>
                    da.DoctorAvailabilityId == id);

            if (availability == null)
                return false;

            _context.DoctorAvailabilities.Remove(availability);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<DoctorLeaveDto>> GetAllLeavesAsync()
        {
            return await _context.DoctorLeaves
                .Include(dl => dl.Doctor)
                .OrderBy(dl => dl.StartDate)
                .ThenBy(dl => dl.StartTime)
                .Select(dl => new DoctorLeaveDto
                {
                    DoctorLeaveId = dl.DoctorLeaveId,
                    StartDate = dl.StartDate,
                    EndDate = dl.EndDate,
                    StartTime = dl.StartTime,
                    EndTime = dl.EndTime,
                    LeaveType = dl.LeaveType,
                    Reason = dl.Reason,
                    IsFullDay = dl.IsFullDay,
                    IsApproved = dl.IsApproved,
                    DoctorId = dl.DoctorId,
                    DoctorName = dl.Doctor.FullName
                })
                .ToListAsync();
        }

        public async Task<DoctorLeaveDto?> GetLeaveByIdAsync(int id)
        {
            return await _context.DoctorLeaves
                .Include(dl => dl.Doctor)
                .Where(dl => dl.DoctorLeaveId == id)
                .Select(dl => new DoctorLeaveDto
                {
                    DoctorLeaveId = dl.DoctorLeaveId,
                    StartDate = dl.StartDate,
                    EndDate = dl.EndDate,
                    StartTime = dl.StartTime,
                    EndTime = dl.EndTime,
                    LeaveType = dl.LeaveType,
                    Reason = dl.Reason,
                    IsFullDay = dl.IsFullDay,
                    IsApproved = dl.IsApproved,
                    DoctorId = dl.DoctorId,
                    DoctorName = dl.Doctor.FullName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DoctorLeaveDto>>
            GetLeavesByDoctorAsync(int doctorId)
        {
            return await _context.DoctorLeaves
                .Include(dl => dl.Doctor)
                .Where(dl => dl.DoctorId == doctorId)
                .OrderBy(dl => dl.StartDate)
                .ThenBy(dl => dl.StartTime)
                .Select(dl => new DoctorLeaveDto
                {
                    DoctorLeaveId = dl.DoctorLeaveId,
                    StartDate = dl.StartDate,
                    EndDate = dl.EndDate,
                    StartTime = dl.StartTime,
                    EndTime = dl.EndTime,
                    LeaveType = dl.LeaveType,
                    Reason = dl.Reason,
                    IsFullDay = dl.IsFullDay,
                    IsApproved = dl.IsApproved,
                    DoctorId = dl.DoctorId,
                    DoctorName = dl.Doctor.FullName
                })
                .ToListAsync();
        }

        public async Task<DoctorLeaveDto> CreateLeaveAsync(
            CreateDoctorLeaveDto dto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            if (!doctor.IsActive)
                throw new InvalidOperationException(
                    "Cannot create leave for an inactive doctor.");

            if (dto.StartDate.Date > dto.EndDate.Date)
                throw new ArgumentException(
                    "Start date cannot be after end date.");

            if (string.IsNullOrWhiteSpace(dto.LeaveType))
                throw new ArgumentException(
                    "Leave type is required.");

            if (!dto.IsFullDay)
            {
                if (!dto.StartTime.HasValue ||
                    !dto.EndTime.HasValue)
                {
                    throw new ArgumentException(
                        "Start time and end time are required for partial-day leave.");
                }

                if (dto.StartTime.Value >= dto.EndTime.Value)
                {
                    throw new ArgumentException(
                        "Start time must be earlier than end time.");
                }

                if (dto.StartDate.Date != dto.EndDate.Date)
                {
                    throw new ArgumentException(
                        "Partial-day leave must be for a single date.");
                }
            }

            var existingLeaves = await _context.DoctorLeaves
                .Where(dl =>
                    dl.DoctorId == dto.DoctorId &&
                    dl.IsApproved &&
                    dl.StartDate.Date <= dto.EndDate.Date &&
                    dl.EndDate.Date >= dto.StartDate.Date)
                .ToListAsync();

            foreach (var existingLeave in existingLeaves)
            {
                if (existingLeave.IsFullDay || dto.IsFullDay)
                {
                    throw new InvalidOperationException(
                        "The leave period overlaps with an existing full-day leave.");
                }

                if (dto.StartTime!.Value < existingLeave.EndTime!.Value &&
                    dto.EndTime!.Value > existingLeave.StartTime!.Value)
                {
                    throw new InvalidOperationException(
                        "The leave period overlaps with an existing leave.");
                }
            }

            var leave = new DoctorLeave
            {
                DoctorId = dto.DoctorId,
                StartDate = dto.StartDate.Date,
                EndDate = dto.EndDate.Date,
                StartTime = dto.IsFullDay ? null : dto.StartTime,
                EndTime = dto.IsFullDay ? null : dto.EndTime,
                LeaveType = dto.LeaveType,
                Reason = dto.Reason,
                IsFullDay = dto.IsFullDay,
                IsApproved = dto.IsApproved
            };

            _context.DoctorLeaves.Add(leave);

            await _context.SaveChangesAsync();

            return await GetLeaveByIdAsync(leave.DoctorLeaveId)
                ?? throw new InvalidOperationException(
                    "Unable to retrieve created doctor leave.");
        }

        public async Task<DoctorLeaveDto?> UpdateLeaveAsync(
            int id,
            UpdateDoctorLeaveDto dto)
        {
            var leave = await _context.DoctorLeaves
                .FirstOrDefaultAsync(dl =>
                    dl.DoctorLeaveId == id);

            if (leave == null)
                return null;

            if (dto.StartDate.Date > dto.EndDate.Date)
                throw new ArgumentException(
                    "Start date cannot be after end date.");

            if (string.IsNullOrWhiteSpace(dto.LeaveType))
                throw new ArgumentException(
                    "Leave type is required.");

            if (!dto.IsFullDay)
            {
                if (!dto.StartTime.HasValue ||
                    !dto.EndTime.HasValue)
                {
                    throw new ArgumentException(
                        "Start time and end time are required for partial-day leave.");
                }

                if (dto.StartTime.Value >= dto.EndTime.Value)
                {
                    throw new ArgumentException(
                        "Start time must be earlier than end time.");
                }

                if (dto.StartDate.Date != dto.EndDate.Date)
                {
                    throw new ArgumentException(
                        "Partial-day leave must be for a single date.");
                }
            }

            var existingLeaves = await _context.DoctorLeaves
                .Where(dl =>
                    dl.DoctorLeaveId != id &&
                    dl.DoctorId == leave.DoctorId &&
                    dl.IsApproved &&
                    dl.StartDate.Date <= dto.EndDate.Date &&
                    dl.EndDate.Date >= dto.StartDate.Date)
                .ToListAsync();

            foreach (var existingLeave in existingLeaves)
            {
                if (existingLeave.IsFullDay || dto.IsFullDay)
                {
                    throw new InvalidOperationException(
                        "The leave period overlaps with an existing full-day leave.");
                }

                if (dto.StartTime!.Value < existingLeave.EndTime!.Value &&
                    dto.EndTime!.Value > existingLeave.StartTime!.Value)
                {
                    throw new InvalidOperationException(
                        "The leave period overlaps with an existing leave.");
                }
            }

            leave.StartDate = dto.StartDate.Date;
            leave.EndDate = dto.EndDate.Date;
            leave.StartTime = dto.IsFullDay ? null : dto.StartTime;
            leave.EndTime = dto.IsFullDay ? null : dto.EndTime;
            leave.LeaveType = dto.LeaveType;
            leave.Reason = dto.Reason;
            leave.IsFullDay = dto.IsFullDay;
            leave.IsApproved = dto.IsApproved;

            await _context.SaveChangesAsync();

            return await GetLeaveByIdAsync(id);
        }

        public async Task<bool> DeleteLeaveAsync(int id)
        {
            var leave = await _context.DoctorLeaves
                .FirstOrDefaultAsync(dl =>
                    dl.DoctorLeaveId == id);

            if (leave == null)
                return false;

            _context.DoctorLeaves.Remove(leave);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}