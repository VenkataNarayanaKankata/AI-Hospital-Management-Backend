using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _branchService.GetAllAsync();

            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);

            if (branch == null)
            {
                return NotFound(new
                {
                    message = "Branch not found."
                });
            }

            return Ok(branch);
        }

        [HttpGet("hospital/{hospitalId}")]
        public async Task<IActionResult> GetByHospitalId(int hospitalId)
        {
            var branches = await _branchService
                .GetByHospitalIdAsync(hospitalId);

            return Ok(branches);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] BranchCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _branchService.CreateAsync(dto);

            if (branch == null)
            {
                return BadRequest(new
                {
                    message = "Hospital or location does not exist, or branch code already exists."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = branch.BranchId },
                branch);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] BranchUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _branchService.UpdateAsync(id, dto);

            if (branch == null)
            {
                return BadRequest(new
                {
                    message = "Branch not found, hospital or location does not exist, or branch code already exists."
                });
            }

            return Ok(branch);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _branchService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Branch not found."
                });
            }

            return Ok(new
            {
                message = "Branch deleted successfully."
            });
        }
    }
}