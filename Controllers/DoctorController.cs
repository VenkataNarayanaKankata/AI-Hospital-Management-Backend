using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);

            if (doctor == null)
            {
                return NotFound(new
                {
                    message = "Doctor not found."
                });
            }

            return Ok(doctor);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(int branchId)
        {
            var doctors = await _doctorService
                .GetByBranchIdAsync(branchId);

            return Ok(doctors);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentId(int departmentId)
        {
            var doctors = await _doctorService
                .GetByDepartmentIdAsync(departmentId);

            return Ok(doctors);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] DoctorCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _doctorService.CreateAsync(dto);

            if (doctor == null)
            {
                return BadRequest(new
                {
                    message = "Branch does not exist, department does not belong to the selected branch, or registration number already exists."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = doctor.DoctorId },
                doctor);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] DoctorUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _doctorService
                .UpdateAsync(id, dto);

            if (doctor == null)
            {
                return BadRequest(new
                {
                    message = "Doctor not found, branch does not exist, department does not belong to the selected branch, or registration number already exists."
                });
            }

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Doctor not found."
                });
            }

            return Ok(new
            {
                message = "Doctor deleted successfully."
            });
        }
    }
}