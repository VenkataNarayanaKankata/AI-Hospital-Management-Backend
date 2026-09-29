using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using HospitalManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _staffService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);

            if (staff == null)
            {
                return NotFound(new
                {
                    message = "Staff member not found."
                });
            }

            return Ok(staff);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranch(int branchId)
        {
            return Ok(
                await _staffService.GetByBranchIdAsync(branchId));
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(
            int departmentId)
        {
            return Ok(
                await _staffService.GetByDepartmentIdAsync(
                    departmentId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] StaffCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = await _staffService.CreateAsync(dto);

            if (staff == null)
            {
                return BadRequest(new
                {
                    message =
                        "Staff could not be created. Check branch, department and dates."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = staff.StaffId },
                staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] StaffUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = await _staffService.UpdateAsync(
                id,
                dto);

            if (staff == null)
            {
                return BadRequest(new
                {
                    message =
                        "Staff could not be updated."
                });
            }

            return Ok(staff);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _staffService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Staff member not found."
                });
            }

            return Ok(new
            {
                message = "Staff member deleted successfully."
            });
        }
    }
}