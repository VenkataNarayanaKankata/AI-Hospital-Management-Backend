using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _scheduleService;

        public DoctorScheduleController(
            IDoctorScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _scheduleService.GetAllAsync();

            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);

            if (schedule == null)
            {
                return NotFound(new
                {
                    message = "Doctor schedule not found."
                });
            }

            return Ok(schedule);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(int doctorId)
        {
            var schedules =
                await _scheduleService.GetByDoctorIdAsync(doctorId);

            return Ok(schedules);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(int branchId)
        {
            var schedules =
                await _scheduleService.GetByBranchIdAsync(branchId);

            return Ok(schedules);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] DoctorScheduleCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedule =
                await _scheduleService.CreateAsync(dto);

            if (schedule == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid schedule. Verify that the Doctor " +
                        "belongs to the selected Branch, the Doctor's " +
                        "Department belongs to the same Branch, working " +
                        "hours are valid, break times are valid, and the " +
                        "Doctor does not already have a schedule for this day."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = schedule.DoctorScheduleId },
                schedule);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] DoctorScheduleUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedule =
                await _scheduleService.UpdateAsync(id, dto);

            if (schedule == null)
            {
                return BadRequest(new
                {
                    message =
                        "Schedule not found or invalid schedule. " +
                        "Verify Doctor, Branch, Department relationship, " +
                        "working hours, break times, and duplicate day."
                });
            }

            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _scheduleService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Doctor schedule not found."
                });
            }

            return Ok(new
            {
                message = "Doctor schedule deleted successfully."
            });
        }
    }
}