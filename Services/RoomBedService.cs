using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class RoomBedService : IRoomBedService
    {
        private readonly ApplicationDbContext _context;

        public RoomBedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            return await _context.Rooms
                .Include(r => r.Ward)
                .Include(r => r.Beds)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType,
                    FloorNumber = r.FloorNumber,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    WardId = r.WardId,
                    WardName = r.Ward.WardName,
                    BedCount = r.Beds.Count
                })
                .ToListAsync();
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.Ward)
                .Include(r => r.Beds)
                .Where(r => r.RoomId == id)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType,
                    FloorNumber = r.FloorNumber,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    WardId = r.WardId,
                    WardName = r.Ward.WardName,
                    BedCount = r.Beds.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByWardAsync(int wardId)
        {
            return await _context.Rooms
                .Include(r => r.Ward)
                .Include(r => r.Beds)
                .Where(r => r.WardId == wardId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType,
                    FloorNumber = r.FloorNumber,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    WardId = r.WardId,
                    WardName = r.Ward.WardName,
                    BedCount = r.Beds.Count
                })
                .ToListAsync();
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto dto)
        {
            var ward = await _context.Wards
                .FirstOrDefaultAsync(w => w.WardId == dto.WardId);

            if (ward == null)
                throw new ArgumentException("Ward not found.");

            var roomExists = await _context.Rooms
                .AnyAsync(r =>
                    r.WardId == dto.WardId &&
                    r.RoomNumber == dto.RoomNumber);

            if (roomExists)
                throw new ArgumentException("Room number already exists in this ward.");

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                FloorNumber = dto.FloorNumber,
                Description = dto.Description,
                IsActive = dto.IsActive,
                WardId = dto.WardId
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new RoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                FloorNumber = room.FloorNumber,
                Description = room.Description,
                IsActive = room.IsActive,
                WardId = room.WardId,
                WardName = ward.WardName,
                BedCount = 0
            };
        }

        public async Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto dto)
        {
            var room = await _context.Rooms
                .Include(r => r.Ward)
                .Include(r => r.Beds)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return null;

            var ward = await _context.Wards
                .FirstOrDefaultAsync(w => w.WardId == dto.WardId);

            if (ward == null)
                throw new ArgumentException("Ward not found.");

            var roomExists = await _context.Rooms
                .AnyAsync(r =>
                    r.RoomId != id &&
                    r.WardId == dto.WardId &&
                    r.RoomNumber == dto.RoomNumber);

            if (roomExists)
                throw new ArgumentException("Room number already exists in this ward.");

            room.RoomNumber = dto.RoomNumber;
            room.RoomType = dto.RoomType;
            room.FloorNumber = dto.FloorNumber;
            room.Description = dto.Description;
            room.IsActive = dto.IsActive;
            room.WardId = dto.WardId;

            await _context.SaveChangesAsync();

            return new RoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                FloorNumber = room.FloorNumber,
                Description = room.Description,
                IsActive = room.IsActive,
                WardId = room.WardId,
                WardName = ward.WardName,
                BedCount = room.Beds.Count
            };
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Beds)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return false;

            if (room.Beds.Any())
                throw new InvalidOperationException(
                    "Room cannot be deleted because it contains beds.");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BedDto>> GetAllBedsAsync()
        {
            return await _context.Beds
                .Include(b => b.Room)
                .Select(b => new BedDto
                {
                    BedId = b.BedId,
                    BedNumber = b.BedNumber,
                    BedType = b.BedType,
                    Status = b.Status,
                    Description = b.Description,
                    IsActive = b.IsActive,
                    RoomId = b.RoomId,
                    RoomNumber = b.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<BedDto?> GetBedByIdAsync(int id)
        {
            return await _context.Beds
                .Include(b => b.Room)
                .Where(b => b.BedId == id)
                .Select(b => new BedDto
                {
                    BedId = b.BedId,
                    BedNumber = b.BedNumber,
                    BedType = b.BedType,
                    Status = b.Status,
                    Description = b.Description,
                    IsActive = b.IsActive,
                    RoomId = b.RoomId,
                    RoomNumber = b.Room.RoomNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BedDto>> GetBedsByRoomAsync(int roomId)
        {
            return await _context.Beds
                .Include(b => b.Room)
                .Where(b => b.RoomId == roomId)
                .Select(b => new BedDto
                {
                    BedId = b.BedId,
                    BedNumber = b.BedNumber,
                    BedType = b.BedType,
                    Status = b.Status,
                    Description = b.Description,
                    IsActive = b.IsActive,
                    RoomId = b.RoomId,
                    RoomNumber = b.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BedDto>> GetBedsByStatusAsync(string status)
        {
            return await _context.Beds
                .Include(b => b.Room)
                .Where(b => b.Status == status)
                .Select(b => new BedDto
                {
                    BedId = b.BedId,
                    BedNumber = b.BedNumber,
                    BedType = b.BedType,
                    Status = b.Status,
                    Description = b.Description,
                    IsActive = b.IsActive,
                    RoomId = b.RoomId,
                    RoomNumber = b.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<BedDto> CreateBedAsync(CreateBedDto dto)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == dto.RoomId);

            if (room == null)
                throw new ArgumentException("Room not found.");

            var validStatuses = new[]
            {
                "Available",
                "Occupied",
                "Reserved",
                "Maintenance"
            };

            if (!validStatuses.Contains(dto.Status))
                throw new ArgumentException(
                    "Invalid bed status.");

            var bedExists = await _context.Beds
                .AnyAsync(b =>
                    b.RoomId == dto.RoomId &&
                    b.BedNumber == dto.BedNumber);

            if (bedExists)
                throw new ArgumentException(
                    "Bed number already exists in this room.");

            var bed = new Bed
            {
                BedNumber = dto.BedNumber,
                BedType = dto.BedType,
                Status = dto.Status,
                Description = dto.Description,
                IsActive = dto.IsActive,
                RoomId = dto.RoomId
            };

            _context.Beds.Add(bed);
            await _context.SaveChangesAsync();

            return new BedDto
            {
                BedId = bed.BedId,
                BedNumber = bed.BedNumber,
                BedType = bed.BedType,
                Status = bed.Status,
                Description = bed.Description,
                IsActive = bed.IsActive,
                RoomId = bed.RoomId,
                RoomNumber = room.RoomNumber
            };
        }

        public async Task<BedDto?> UpdateBedAsync(int id, UpdateBedDto dto)
        {
            var bed = await _context.Beds
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BedId == id);

            if (bed == null)
                return null;

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == dto.RoomId);

            if (room == null)
                throw new ArgumentException("Room not found.");

            var validStatuses = new[]
            {
                "Available",
                "Occupied",
                "Reserved",
                "Maintenance"
            };

            if (!validStatuses.Contains(dto.Status))
                throw new ArgumentException(
                    "Invalid bed status.");

            var bedExists = await _context.Beds
                .AnyAsync(b =>
                    b.BedId != id &&
                    b.RoomId == dto.RoomId &&
                    b.BedNumber == dto.BedNumber);

            if (bedExists)
                throw new ArgumentException(
                    "Bed number already exists in this room.");

            bed.BedNumber = dto.BedNumber;
            bed.BedType = dto.BedType;
            bed.Status = dto.Status;
            bed.Description = dto.Description;
            bed.IsActive = dto.IsActive;
            bed.RoomId = dto.RoomId;

            await _context.SaveChangesAsync();

            return new BedDto
            {
                BedId = bed.BedId,
                BedNumber = bed.BedNumber,
                BedType = bed.BedType,
                Status = bed.Status,
                Description = bed.Description,
                IsActive = bed.IsActive,
                RoomId = bed.RoomId,
                RoomNumber = room.RoomNumber
            };
        }

        public async Task<bool> DeleteBedAsync(int id)
        {
            var bed = await _context.Beds
                .FirstOrDefaultAsync(b => b.BedId == id);

            if (bed == null)
                return false;

            _context.Beds.Remove(bed);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}