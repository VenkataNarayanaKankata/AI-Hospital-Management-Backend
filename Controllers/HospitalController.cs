using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hospitals = await _hospitalService.GetAllAsync();

            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);

            if (hospital == null)
            {
                return NotFound(new
                {
                    message = "Hospital not found."
                });
            }

            return Ok(hospital);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] HospitalCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospital = await _hospitalService.CreateAsync(dto);

            if (hospital == null)
            {
                return BadRequest(new
                {
                    message = "Hospital code already exists."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = hospital.HospitalId },
                hospital);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] HospitalUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospital = await _hospitalService.UpdateAsync(id, dto);

            if (hospital == null)
            {
                return BadRequest(new
                {
                    message = "Hospital not found or code already exists."
                });
            }

            return Ok(hospital);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hospitalService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Hospital not found or it has branches."
                });
            }

            return Ok(new
            {
                message = "Hospital deleted successfully."
            });
        }
    }
}