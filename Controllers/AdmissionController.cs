using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _service;

        public AdmissionController(IAdmissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmissions()
        {
            var admissions = await _service.GetAllAdmissionsAsync();
            return Ok(admissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmissionById(int id)
        {
            var admission = await _service.GetAdmissionByIdAsync(id);

            if (admission == null)
                return NotFound("Admission not found.");

            return Ok(admission);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAdmissionsByPatient(int patientId)
        {
            var admissions =
                await _service.GetAdmissionsByPatientAsync(patientId);

            return Ok(admissions);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetAdmissionsByBranch(int branchId)
        {
            var admissions =
                await _service.GetAdmissionsByBranchAsync(branchId);

            return Ok(admissions);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetAdmissionsByStatus(string status)
        {
            var admissions =
                await _service.GetAdmissionsByStatusAsync(status);

            return Ok(admissions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmission(
            CreateAdmissionDto dto)
        {
            try
            {
                var admission =
                    await _service.CreateAdmissionAsync(dto);

                return CreatedAtAction(
                    nameof(GetAdmissionById),
                    new { id = admission.AdmissionId },
                    admission);
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
        public async Task<IActionResult> UpdateAdmission(
            int id,
            UpdateAdmissionDto dto)
        {
            try
            {
                var admission =
                    await _service.UpdateAdmissionAsync(id, dto);

                if (admission == null)
                    return NotFound("Admission not found.");

                return Ok(admission);
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
        public async Task<IActionResult> DeleteAdmission(int id)
        {
            try
            {
                var result =
                    await _service.DeleteAdmissionAsync(id);

                if (!result)
                    return NotFound("Admission not found.");

                return Ok("Admission deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}