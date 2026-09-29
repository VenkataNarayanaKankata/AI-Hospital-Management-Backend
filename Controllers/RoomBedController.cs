using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class RoomBedController : ControllerBase
    {
        private readonly IRoomBedService _service;

        public RoomBedController(IRoomBedService service)
        {
            _service = service;
        }

        [HttpGet("rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _service.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("rooms/{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _service.GetRoomByIdAsync(id);

            if (room == null)
                return NotFound("Room not found.");

            return Ok(room);
        }

        [HttpGet("rooms/ward/{wardId}")]
        public async Task<IActionResult> GetRoomsByWard(int wardId)
        {
            var rooms = await _service.GetRoomsByWardAsync(wardId);
            return Ok(rooms);
        }

        [HttpPost("rooms")]
        public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
        {
            try
            {
                var room = await _service.CreateRoomAsync(dto);
                return CreatedAtAction(
                    nameof(GetRoomById),
                    new { id = room.RoomId },
                    room);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("rooms/{id}")]
        public async Task<IActionResult> UpdateRoom(
            int id,
            UpdateRoomDto dto)
        {
            try
            {
                var room = await _service.UpdateRoomAsync(id, dto);

                if (room == null)
                    return NotFound("Room not found.");

                return Ok(room);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("rooms/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var result = await _service.DeleteRoomAsync(id);

                if (!result)
                    return NotFound("Room not found.");

                return Ok("Room deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("beds")]
        public async Task<IActionResult> GetAllBeds()
        {
            var beds = await _service.GetAllBedsAsync();
            return Ok(beds);
        }

        [HttpGet("beds/{id}")]
        public async Task<IActionResult> GetBedById(int id)
        {
            var bed = await _service.GetBedByIdAsync(id);

            if (bed == null)
                return NotFound("Bed not found.");

            return Ok(bed);
        }

        [HttpGet("beds/room/{roomId}")]
        public async Task<IActionResult> GetBedsByRoom(int roomId)
        {
            var beds = await _service.GetBedsByRoomAsync(roomId);
            return Ok(beds);
        }

        [HttpGet("beds/status/{status}")]
        public async Task<IActionResult> GetBedsByStatus(string status)
        {
            var beds = await _service.GetBedsByStatusAsync(status);
            return Ok(beds);
        }

        [HttpPost("beds")]
        public async Task<IActionResult> CreateBed(CreateBedDto dto)
        {
            try
            {
                var bed = await _service.CreateBedAsync(dto);
                return CreatedAtAction(
                    nameof(GetBedById),
                    new { id = bed.BedId },
                    bed);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("beds/{id}")]
        public async Task<IActionResult> UpdateBed(
            int id,
            UpdateBedDto dto)
        {
            try
            {
                var bed = await _service.UpdateBedAsync(id, dto);

                if (bed == null)
                    return NotFound("Bed not found.");

                return Ok(bed);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("beds/{id}")]
        public async Task<IActionResult> DeleteBed(int id)
        {
            var result = await _service.DeleteBedAsync(id);

            if (!result)
                return NotFound("Bed not found.");

            return Ok("Bed deleted successfully.");
        }
    }
}