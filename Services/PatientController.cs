using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound(new
                {
                    message = "Patient not found."
                });
            }

            return Ok(patient);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(int branchId)
        {
            var patients = await _patientService
                .GetByBranchIdAsync(branchId);

            return Ok(patients);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] PatientCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _patientService.CreateAsync(dto);

            if (patient == null)
            {
                return BadRequest(new
                {
                    message = "Branch does not exist."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = patient.PatientId },
                patient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PatientUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _patientService
                .UpdateAsync(id, dto);

            if (patient == null)
            {
                return BadRequest(new
                {
                    message = "Patient not found or branch does not exist."
                });
            }

            return Ok(patient);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Patient not found."
                });
            }

            return Ok(new
            {
                message = "Patient deleted successfully."
            });
        }
    }
}