using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(
            IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments =
                await _appointmentService.GetAllAsync();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment =
                await _appointmentService.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound(new
                {
                    message = "Appointment not found."
                });
            }

            return Ok(appointment);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(
            int branchId)
        {
            var appointments =
                await _appointmentService
                    .GetByBranchIdAsync(branchId);

            return Ok(appointments);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(
            int patientId)
        {
            var appointments =
                await _appointmentService
                    .GetByPatientIdAsync(patientId);

            return Ok(appointments);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(
            int doctorId)
        {
            var appointments =
                await _appointmentService
                    .GetByDoctorIdAsync(doctorId);

            return Ok(appointments);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] AppointmentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment =
                await _appointmentService.CreateAsync(dto);

            if (appointment == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid appointment relationship. " +
                        "Verify that the Branch, Patient, Department, " +
                        "and Doctor exist and belong to the correct branch. " +
                        "The Doctor must also belong to the selected Department."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = appointment.AppointmentId },
                appointment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] AppointmentUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment =
                await _appointmentService
                    .UpdateAsync(id, dto);

            if (appointment == null)
            {
                return BadRequest(new
                {
                    message =
                        "Appointment not found or invalid appointment " +
                        "relationship/status. Verify the Branch, Patient, " +
                        "Department, Doctor, and Status."
                });
            }

            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _appointmentService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Appointment not found."
                });
            }

            return Ok(new
            {
                message = "Appointment deleted successfully."
            });
        }
    }
}