using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class BranchService : IBranchService
    {
        private readonly ApplicationDbContext _context;

        public BranchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchResponseDto>> GetAllAsync()
        {
            return await _context.Branches
                .AsNoTracking()
                .Include(b => b.Hospital)
                .Include(b => b.Location)
                .Select(b => new BranchResponseDto
                {
                    BranchId = b.BranchId,
                    BranchName = b.BranchName,
                    Code = b.Code,
                    Email = b.Email,
                    Phone = b.Phone,
                    IsActive = b.IsActive,
                    CreatedAt = b.CreatedAt,
                    HospitalId = b.HospitalId,
                    HospitalName = b.Hospital.HospitalName,
                    LocationId = b.LocationId,
                    Location = new LocationResponseDto
                    {
                        LocationId = b.Location.LocationId,
                        Country = b.Location.Country,
                        State = b.Location.State,
                        City = b.Location.City,
                        Address = b.Location.Address,
                        PostalCode = b.Location.PostalCode
                    }
                })
                .ToListAsync();
        }

        public async Task<List<BranchResponseDto>> GetByHospitalIdAsync(
            int hospitalId)
        {
            return await _context.Branches
                .AsNoTracking()
                .Where(b => b.HospitalId == hospitalId)
                .Include(b => b.Hospital)
                .Include(b => b.Location)
                .Select(b => new BranchResponseDto
                {
                    BranchId = b.BranchId,
                    BranchName = b.BranchName,
                    Code = b.Code,
                    Email = b.Email,
                    Phone = b.Phone,
                    IsActive = b.IsActive,
                    CreatedAt = b.CreatedAt,
                    HospitalId = b.HospitalId,
                    HospitalName = b.Hospital.HospitalName,
                    LocationId = b.LocationId,
                    Location = new LocationResponseDto
                    {
                        LocationId = b.Location.LocationId,
                        Country = b.Location.Country,
                        State = b.Location.State,
                        City = b.Location.City,
                        Address = b.Location.Address,
                        PostalCode = b.Location.PostalCode
                    }
                })
                .ToListAsync();
        }

        public async Task<BranchResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Branches
                .AsNoTracking()
                .Where(b => b.BranchId == id)
                .Include(b => b.Hospital)
                .Include(b => b.Location)
                .Select(b => new BranchResponseDto
                {
                    BranchId = b.BranchId,
                    BranchName = b.BranchName,
                    Code = b.Code,
                    Email = b.Email,
                    Phone = b.Phone,
                    IsActive = b.IsActive,
                    CreatedAt = b.CreatedAt,
                    HospitalId = b.HospitalId,
                    HospitalName = b.Hospital.HospitalName,
                    LocationId = b.LocationId,
                    Location = new LocationResponseDto
                    {
                        LocationId = b.Location.LocationId,
                        Country = b.Location.Country,
                        State = b.Location.State,
                        City = b.Location.City,
                        Address = b.Location.Address,
                        PostalCode = b.Location.PostalCode
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<BranchResponseDto?> CreateAsync(
            BranchCreateDto dto)
        {
            var hospitalExists = await _context.Hospitals
                .AnyAsync(h => h.HospitalId == dto.HospitalId);

            if (!hospitalExists)
            {
                return null;
            }

            var locationExists = await _context.Locations
                .AnyAsync(l => l.LocationId == dto.LocationId);

            if (!locationExists)
            {
                return null;
            }

            var codeExists = await _context.Branches
                .AnyAsync(b => b.Code == dto.Code);

            if (codeExists)
            {
                return null;
            }

            var branch = new Branch
            {
                BranchName = dto.BranchName,
                Code = dto.Code,
                Email = dto.Email,
                Phone = dto.Phone,
                HospitalId = dto.HospitalId,
                LocationId = dto.LocationId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Branches.Add(branch);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(branch.BranchId);
        }

        public async Task<BranchResponseDto?> UpdateAsync(
            int id,
            BranchUpdateDto dto)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == id);

            if (branch == null)
            {
                return null;
            }

            var hospitalExists = await _context.Hospitals
                .AnyAsync(h => h.HospitalId == dto.HospitalId);

            if (!hospitalExists)
            {
                return null;
            }

            var locationExists = await _context.Locations
                .AnyAsync(l => l.LocationId == dto.LocationId);

            if (!locationExists)
            {
                return null;
            }

            var codeExists = await _context.Branches
                .AnyAsync(b =>
                    b.Code == dto.Code &&
                    b.BranchId != id);

            if (codeExists)
            {
                return null;
            }

            branch.BranchName = dto.BranchName;
            branch.Code = dto.Code;
            branch.Email = dto.Email;
            branch.Phone = dto.Phone;
            branch.HospitalId = dto.HospitalId;
            branch.LocationId = dto.LocationId;
            branch.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(branch.BranchId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == id);

            if (branch == null)
            {
                return false;
            }

            _context.Branches.Remove(branch);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}