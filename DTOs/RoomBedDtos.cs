namespace HospitalManagement.Api.DTOs
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; } = string.Empty;
        public int BedCount { get; set; }
    }

    public class CreateRoomDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int WardId { get; set; }
    }

    public class UpdateRoomDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int WardId { get; set; }
    }

    public class BedDto
    {
        public int BedId { get; set; }
        public string BedNumber { get; set; } = string.Empty;
        public string BedType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
    }

    public class CreateBedDto
    {
        public string BedNumber { get; set; } = string.Empty;
        public string BedType { get; set; } = string.Empty;
        public string Status { get; set; } = "Available";
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int RoomId { get; set; }
    }

    public class UpdateBedDto
    {
        public string BedNumber { get; set; } = string.Empty;
        public string BedType { get; set; } = string.Empty;
        public string Status { get; set; } = "Available";
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
    }
}