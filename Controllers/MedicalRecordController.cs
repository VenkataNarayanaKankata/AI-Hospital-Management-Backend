using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(
            IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _medicalRecordService.GetAllAsync();

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record =
                await _medicalRecordService.GetByIdAsync(id);

            if (record == null)
            {
                return NotFound(new
                {
                    message = "Medical record not found."
                });
            }

            return Ok(record);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(
            int patientId)
        {
            var records =
                await _medicalRecordService
                    .GetByPatientIdAsync(patientId);

            return Ok(records);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(
            int doctorId)
        {
            var records =
                await _medicalRecordService
                    .GetByDoctorIdAsync(doctorId);

            return Ok(records);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(
            int appointmentId)
        {
            var records =
                await _medicalRecordService
                    .GetByAppointmentIdAsync(appointmentId);

            return Ok(records);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(
            int branchId)
        {
            var records =
                await _medicalRecordService
                    .GetByBranchIdAsync(branchId);

            return Ok(records);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] MedicalRecordCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record =
                await _medicalRecordService.CreateAsync(dto);

            if (record == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid medical record. Verify that the " +
                        "Appointment, Patient, Doctor, and Branch " +
                        "belong to the same clinical visit, the " +
                        "visit date matches the appointment date, " +
                        "and the appointment does not already have " +
                        "a medical record."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = record.MedicalRecordId },
                record);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] MedicalRecordUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record =
                await _medicalRecordService
                    .UpdateAsync(id, dto);

            if (record == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid medical record update. Verify that " +
                        "the Appointment, Patient, Doctor, and Branch " +
                        "belong to the same clinical visit and that " +
                        "the visit date matches the appointment date."
                });
            }

            return Ok(record);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _medicalRecordService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Medical record not found."
                });
            }

            return Ok(new
            {
                message =
                    "Medical record deleted successfully."
            });
        }
    }
}