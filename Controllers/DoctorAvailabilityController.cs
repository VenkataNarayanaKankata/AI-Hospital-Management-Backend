using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _service;

        public DoctorAvailabilityController(
            IDoctorAvailabilityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound("Doctor availability not found.");

            return Ok(result);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var result = await _service.GetByDoctorAsync(doctorId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateDoctorAvailabilityDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.DoctorAvailabilityId },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateDoctorAvailabilityDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);

                if (result == null)
                    return NotFound("Doctor availability not found.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound("Doctor availability not found.");

            return Ok("Doctor availability deleted successfully.");
        }
        [HttpGet("leaves")]
        public async Task<IActionResult> GetAllLeaves()
        {
            var result = await _service.GetAllLeavesAsync();

            return Ok(result);
        }

        [HttpGet("leaves/{id}")]
        public async Task<IActionResult> GetLeaveById(int id)
        {
            var result = await _service.GetLeaveByIdAsync(id);

            if (result == null)
                return NotFound("Doctor leave not found.");

            return Ok(result);
        }

        [HttpGet("leaves/doctor/{doctorId}")]
        public async Task<IActionResult> GetLeavesByDoctor(int doctorId)
        {
            var result = await _service.GetLeavesByDoctorAsync(doctorId);

            return Ok(result);
        }

        [HttpPost("leaves")]
        public async Task<IActionResult> CreateLeave(
            CreateDoctorLeaveDto dto)
        {
            try
            {
                var result = await _service.CreateLeaveAsync(dto);

                return CreatedAtAction(
                    nameof(GetLeaveById),
                    new { id = result.DoctorLeaveId },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("leaves/{id}")]
        public async Task<IActionResult> UpdateLeave(
            int id,
            UpdateDoctorLeaveDto dto)
        {
            try
            {
                var result = await _service.UpdateLeaveAsync(id, dto);

                if (result == null)
                    return NotFound("Doctor leave not found.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("leaves/{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var deleted = await _service.DeleteLeaveAsync(id);

            if (!deleted)
                return NotFound("Doctor leave not found.");

            return Ok("Doctor leave deleted successfully.");
        }
    }
}