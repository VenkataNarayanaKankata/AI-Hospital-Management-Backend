using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentResponseDto>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(d => new DepartmentResponseDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Code = d.Code,
                    Description = d.Description,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,
                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,
                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<DepartmentResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => d.BranchId == branchId)
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(d => new DepartmentResponseDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Code = d.Code,
                    Description = d.Description,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,
                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,
                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<DepartmentResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => d.DepartmentId == id)
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(d => new DepartmentResponseDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Code = d.Code,
                    Description = d.Description,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,
                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,
                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DepartmentResponseDto?> CreateAsync(
            DepartmentCreateDto dto)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == dto.BranchId);

            if (branch == null)
            {
                return null;
            }

            var codeExists = await _context.Departments
                .AnyAsync(d =>
                    d.BranchId == dto.BranchId &&
                    d.Code == dto.Code);

            if (codeExists)
            {
                return null;
            }

            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                Code = dto.Code,
                Description = dto.Description,
                BranchId = dto.BranchId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Departments.Add(department);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(department.DepartmentId);
        }

        public async Task<DepartmentResponseDto?> UpdateAsync(
            int id,
            DepartmentUpdateDto dto)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return null;
            }

            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            var codeExists = await _context.Departments
                .AnyAsync(d =>
                    d.BranchId == dto.BranchId &&
                    d.Code == dto.Code &&
                    d.DepartmentId != id);

            if (codeExists)
            {
                return null;
            }

            department.DepartmentName = dto.DepartmentName;
            department.Code = dto.Code;
            department.Description = dto.Description;
            department.BranchId = dto.BranchId;
            department.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(department.DepartmentId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return false;
            }

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}