using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class WardService : IWardService
    {
        private readonly ApplicationDbContext _context;

        public WardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WardResponseDto>> GetAllAsync()
        {
            return await BuildQuery()
                .AsNoTracking()
                .OrderBy(w => w.WardName)
                .ToListAsync();
        }

        public async Task<List<WardResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(w => w.BranchId == branchId)
                .OrderBy(w => w.WardName)
                .ToListAsync();
        }

        public async Task<WardResponseDto?> GetByIdAsync(int id)
        {
            return await BuildQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.WardId == id);
        }

        public async Task<WardResponseDto?> CreateAsync(
            WardCreateDto dto)
        {
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            var wardExists = await _context.Wards
                .AnyAsync(w =>
                    w.BranchId == dto.BranchId &&
                    w.WardName == dto.WardName);

            if (wardExists)
            {
                return null;
            }

            var ward = new Ward
            {
                WardName = dto.WardName,
                WardType = dto.WardType,
                Description = dto.Description,
                TotalBeds = dto.TotalBeds,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                BranchId = dto.BranchId
            };

            _context.Wards.Add(ward);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(ward.WardId);
        }

        public async Task<WardResponseDto?> UpdateAsync(
            int id,
            WardUpdateDto dto)
        {
            var ward = await _context.Wards
                .FirstOrDefaultAsync(w => w.WardId == id);

            if (ward == null)
            {
                return null;
            }

            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            var duplicateWard = await _context.Wards
                .AnyAsync(w =>
                    w.WardId != id &&
                    w.BranchId == dto.BranchId &&
                    w.WardName == dto.WardName);

            if (duplicateWard)
            {
                return null;
            }

            ward.WardName = dto.WardName;
            ward.WardType = dto.WardType;
            ward.Description = dto.Description;
            ward.TotalBeds = dto.TotalBeds;
            ward.IsActive = dto.IsActive;
            ward.BranchId = dto.BranchId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ward = await _context.Wards
                .FirstOrDefaultAsync(w => w.WardId == id);

            if (ward == null)
            {
                return false;
            }

            _context.Wards.Remove(ward);

            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<WardResponseDto> BuildQuery()
        {
            return _context.Wards
                .Select(w => new WardResponseDto
                {
                    WardId = w.WardId,
                    WardName = w.WardName,
                    WardType = w.WardType,
                    Description = w.Description,
                    TotalBeds = w.TotalBeds,
                    IsActive = w.IsActive,
                    CreatedAt = w.CreatedAt,

                    BranchId = w.BranchId,
                    BranchName = w.Branch.BranchName,

                    HospitalId = w.Branch.HospitalId,
                    HospitalName = w.Branch.Hospital.HospitalName
                });
        }
    }
}