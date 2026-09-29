using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DischargeController : ControllerBase
    {
        private readonly IDischargeService _service;

        public DischargeController(IDischargeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDischarges()
        {
            var discharges = await _service.GetAllDischargesAsync();
            return Ok(discharges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDischargeById(int id)
        {
            var discharge = await _service.GetDischargeByIdAsync(id);

            if (discharge == null)
                return NotFound("Discharge not found.");

            return Ok(discharge);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetDischargesByPatient(int patientId)
        {
            var discharges =
                await _service.GetDischargesByPatientAsync(patientId);

            return Ok(discharges);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDischargesByDoctor(int doctorId)
        {
            var discharges =
                await _service.GetDischargesByDoctorAsync(doctorId);

            return Ok(discharges);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDischarge(
            CreateDischargeDto dto)
        {
            try
            {
                var discharge =
                    await _service.CreateDischargeAsync(dto);

                return CreatedAtAction(
                    nameof(GetDischargeById),
                    new { id = discharge.DischargeId },
                    discharge);
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
        public async Task<IActionResult> UpdateDischarge(
            int id,
            UpdateDischargeDto dto)
        {
            try
            {
                var discharge =
                    await _service.UpdateDischargeAsync(id, dto);

                if (discharge == null)
                    return NotFound("Discharge not found.");

                return Ok(discharge);
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
        public async Task<IActionResult> DeleteDischarge(int id)
        {
            try
            {
                var result =
                    await _service.DeleteDischargeAsync(id);

                if (!result)
                    return NotFound("Discharge not found.");

                return Ok("Discharge deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}