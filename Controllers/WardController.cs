using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class WardController : ControllerBase
    {
        private readonly IWardService _wardService;

        public WardController(IWardService wardService)
        {
            _wardService = wardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _wardService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ward = await _wardService.GetByIdAsync(id);

            if (ward == null)
            {
                return NotFound(new
                {
                    message = "Ward not found."
                });
            }

            return Ok(ward);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranch(int branchId)
        {
            return Ok(
                await _wardService.GetByBranchIdAsync(branchId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] WardCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ward = await _wardService.CreateAsync(dto);

            if (ward == null)
            {
                return BadRequest(new
                {
                    message =
                        "Ward could not be created. Check the branch or duplicate ward."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = ward.WardId },
                ward);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] WardUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ward = await _wardService.UpdateAsync(id, dto);

            if (ward == null)
            {
                return BadRequest(new
                {
                    message = "Ward could not be updated."
                });
            }

            return Ok(ward);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _wardService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Ward not found."
                });
            }

            return Ok(new
            {
                message = "Ward deleted successfully."
            });
        }
    }
}