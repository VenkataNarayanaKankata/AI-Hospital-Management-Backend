using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly ApplicationDbContext _context;

        public HospitalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HospitalResponseDto>> GetAllAsync()
        {
            return await _context.Hospitals
                .AsNoTracking()
                .Select(h => new HospitalResponseDto
                {
                    HospitalId = h.HospitalId,
                    HospitalName = h.HospitalName,
                    Code = h.Code,
                    Email = h.Email,
                    Phone = h.Phone,
                    Address = h.Address,
                    IsActive = h.IsActive,
                    CreatedAt = h.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<HospitalResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Hospitals
                .AsNoTracking()
                .Where(h => h.HospitalId == id)
                .Select(h => new HospitalResponseDto
                {
                    HospitalId = h.HospitalId,
                    HospitalName = h.HospitalName,
                    Code = h.Code,
                    Email = h.Email,
                    Phone = h.Phone,
                    Address = h.Address,
                    IsActive = h.IsActive,
                    CreatedAt = h.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<HospitalResponseDto?> CreateAsync(
            HospitalCreateDto dto)
        {
            var codeExists = await _context.Hospitals
                .AnyAsync(h => h.Code == dto.Code);

            if (codeExists)
            {
                return null;
            }

            var hospital = new Hospital
            {
                HospitalName = dto.HospitalName,
                Code = dto.Code,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Hospitals.Add(hospital);

            await _context.SaveChangesAsync();

            return new HospitalResponseDto
            {
                HospitalId = hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                Code = hospital.Code,
                Email = hospital.Email,
                Phone = hospital.Phone,
                Address = hospital.Address,
                IsActive = hospital.IsActive,
                CreatedAt = hospital.CreatedAt
            };
        }

        public async Task<HospitalResponseDto?> UpdateAsync(
            int id,
            HospitalUpdateDto dto)
        {
            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(h => h.HospitalId == id);

            if (hospital == null)
            {
                return null;
            }

            var codeExists = await _context.Hospitals
                .AnyAsync(h =>
                    h.Code == dto.Code &&
                    h.HospitalId != id);

            if (codeExists)
            {
                return null;
            }

            hospital.HospitalName = dto.HospitalName;
            hospital.Code = dto.Code;
            hospital.Email = dto.Email;
            hospital.Phone = dto.Phone;
            hospital.Address = dto.Address;
            hospital.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return new HospitalResponseDto
            {
                HospitalId = hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                Code = hospital.Code,
                Email = hospital.Email,
                Phone = hospital.Phone,
                Address = hospital.Address,
                IsActive = hospital.IsActive,
                CreatedAt = hospital.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(h => h.HospitalId == id);

            if (hospital == null)
            {
                return false;
            }

            var hasBranches = await _context.Branches
                .AnyAsync(b => b.HospitalId == id);

            if (hasBranches)
            {
                return false;
            }

            _context.Hospitals.Remove(hospital);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}