using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;

        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffResponseDto>> GetAllAsync()
        {
            return await BuildQuery()
                .AsNoTracking()
                .OrderBy(s => s.FullName)
                .ToListAsync();
        }

        public async Task<List<StaffResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(s => s.BranchId == branchId)
                .OrderBy(s => s.FullName)
                .ToListAsync();
        }

        public async Task<List<StaffResponseDto>> GetByDepartmentIdAsync(
            int departmentId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(s => s.DepartmentId == departmentId)
                .OrderBy(s => s.FullName)
                .ToListAsync();
        }

        public async Task<StaffResponseDto?> GetByIdAsync(int id)
        {
            return await BuildQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StaffId == id);
        }

        public async Task<StaffResponseDto?> CreateAsync(
            StaffCreateDto dto)
        {
            if (dto.DateOfBirth.Date >= DateTime.UtcNow.Date)
            {
                return null;
            }

            if (dto.JoiningDate.Date > DateTime.UtcNow.Date)
            {
                return null;
            }

            var branch = await _context.Branches
                .AsNoTracking()
                .FirstOrDefaultAsync(b =>
                    b.BranchId == dto.BranchId);

            if (branch == null)
            {
                return null;
            }

            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d =>
                    d.DepartmentId == dto.DepartmentId);

            if (department == null)
            {
                return null;
            }

            var staff = new Staff
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                Address = dto.Address,
                Qualification = dto.Qualification,
                Designation = dto.Designation,
                JoiningDate = dto.JoiningDate,
                Salary = dto.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                BranchId = dto.BranchId,
                DepartmentId = dto.DepartmentId
            };

            _context.Staff.Add(staff);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(staff.StaffId);
        }

        public async Task<StaffResponseDto?> UpdateAsync(
            int id,
            StaffUpdateDto dto)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s =>
                    s.StaffId == id);

            if (staff == null)
            {
                return null;
            }

            if (dto.DateOfBirth.Date >= DateTime.UtcNow.Date)
            {
                return null;
            }

            if (dto.JoiningDate.Date > DateTime.UtcNow.Date)
            {
                return null;
            }

            var branchExists = await _context.Branches
                .AnyAsync(b =>
                    b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            var departmentExists = await _context.Departments
                .AnyAsync(d =>
                    d.DepartmentId == dto.DepartmentId);

            if (!departmentExists)
            {
                return null;
            }

            staff.FullName = dto.FullName;
            staff.Gender = dto.Gender;
            staff.DateOfBirth = dto.DateOfBirth;
            staff.MobileNumber = dto.MobileNumber;
            staff.Email = dto.Email;
            staff.Address = dto.Address;
            staff.Qualification = dto.Qualification;
            staff.Designation = dto.Designation;
            staff.JoiningDate = dto.JoiningDate;
            staff.Salary = dto.Salary;
            staff.IsActive = dto.IsActive;
            staff.BranchId = dto.BranchId;
            staff.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s =>
                    s.StaffId == id);

            if (staff == null)
            {
                return false;
            }

            _context.Staff.Remove(staff);

            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<StaffResponseDto> BuildQuery()
        {
            return _context.Staff
                .Select(s => new StaffResponseDto
                {
                    StaffId = s.StaffId,
                    FullName = s.FullName,
                    Gender = s.Gender,
                    DateOfBirth = s.DateOfBirth,
                    MobileNumber = s.MobileNumber,
                    Email = s.Email,
                    Address = s.Address,
                    Qualification = s.Qualification,
                    Designation = s.Designation,
                    JoiningDate = s.JoiningDate,
                    Salary = s.Salary,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,

                    BranchId = s.BranchId,
                    BranchName = s.Branch.BranchName,

                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.Department.DepartmentName,

                    HospitalId = s.Branch.HospitalId,
                    HospitalName = s.Branch.Hospital.HospitalName
                });
        }
    }
}