using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LocationResponseDto>> GetAllAsync()
        {
            return await _context.Locations
                .AsNoTracking()
                .Select(l => new LocationResponseDto
                {
                    LocationId = l.LocationId,
                    Country = l.Country,
                    State = l.State,
                    City = l.City,
                    Address = l.Address,
                    PostalCode = l.PostalCode
                })
                .ToListAsync();
        }

        public async Task<LocationResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Locations
                .AsNoTracking()
                .Where(l => l.LocationId == id)
                .Select(l => new LocationResponseDto
                {
                    LocationId = l.LocationId,
                    Country = l.Country,
                    State = l.State,
                    City = l.City,
                    Address = l.Address,
                    PostalCode = l.PostalCode
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LocationResponseDto?> CreateAsync(
            LocationCreateDto dto)
        {
            var location = new Location
            {
                Country = dto.Country,
                State = dto.State,
                City = dto.City,
                Address = dto.Address,
                PostalCode = dto.PostalCode
            };

            _context.Locations.Add(location);

            await _context.SaveChangesAsync();

            return new LocationResponseDto
            {
                LocationId = location.LocationId,
                Country = location.Country,
                State = location.State,
                City = location.City,
                Address = location.Address,
                PostalCode = location.PostalCode
            };
        }

        public async Task<LocationResponseDto?> UpdateAsync(
            int id,
            LocationUpdateDto dto)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == id);

            if (location == null)
            {
                return null;
            }

            location.Country = dto.Country;
            location.State = dto.State;
            location.City = dto.City;
            location.Address = dto.Address;
            location.PostalCode = dto.PostalCode;

            await _context.SaveChangesAsync();

            return new LocationResponseDto
            {
                LocationId = location.LocationId,
                Country = location.Country,
                State = location.State,
                City = location.City,
                Address = location.Address,
                PostalCode = location.PostalCode
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == id);

            if (location == null)
            {
                return false;
            }

            var isUsed = await _context.Branches
                .AnyAsync(b => b.LocationId == id);

            if (isUsed)
            {
                return false;
            }

            _context.Locations.Remove(location);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}