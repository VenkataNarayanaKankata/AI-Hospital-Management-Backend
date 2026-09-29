using HospitalManagement.Api.DTOs;

namespace HospitalManagement.Api.Interfaces
{
    public interface IRoomBedService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<IEnumerable<RoomDto>> GetRoomsByWardAsync(int wardId);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto dto);
        Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto dto);
        Task<bool> DeleteRoomAsync(int id);

        Task<IEnumerable<BedDto>> GetAllBedsAsync();
        Task<BedDto?> GetBedByIdAsync(int id);
        Task<IEnumerable<BedDto>> GetBedsByRoomAsync(int roomId);
        Task<IEnumerable<BedDto>> GetBedsByStatusAsync(string status);
        Task<BedDto> CreateBedAsync(CreateBedDto dto);
        Task<BedDto?> UpdateBedAsync(int id, UpdateBedDto dto);
        Task<bool> DeleteBedAsync(int id);
    }
}