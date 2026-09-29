using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(
            IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prescriptions =
                await _prescriptionService.GetAllAsync();

            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prescription =
                await _prescriptionService.GetByIdAsync(id);

            if (prescription == null)
            {
                return NotFound(new
                {
                    message = "Prescription not found."
                });
            }

            return Ok(prescription);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(
            int patientId)
        {
            var prescriptions =
                await _prescriptionService
                    .GetByPatientIdAsync(patientId);

            return Ok(prescriptions);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(
            int doctorId)
        {
            var prescriptions =
                await _prescriptionService
                    .GetByDoctorIdAsync(doctorId);

            return Ok(prescriptions);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(
            int appointmentId)
        {
            var prescriptions =
                await _prescriptionService
                    .GetByAppointmentIdAsync(appointmentId);

            return Ok(prescriptions);
        }

        [HttpGet("medical-record/{medicalRecordId}")]
        public async Task<IActionResult> GetByMedicalRecordId(
            int medicalRecordId)
        {
            var prescriptions =
                await _prescriptionService
                    .GetByMedicalRecordIdAsync(medicalRecordId);

            return Ok(prescriptions);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(
            int branchId)
        {
            var prescriptions =
                await _prescriptionService
                    .GetByBranchIdAsync(branchId);

            return Ok(prescriptions);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] PrescriptionCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prescription =
                await _prescriptionService.CreateAsync(dto);

            if (prescription == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid prescription. Verify that the " +
                        "Branch, Patient, Doctor, Appointment, " +
                        "Medical Record, and Medicines are correctly " +
                        "related."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = prescription.PrescriptionId },
                prescription);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PrescriptionUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prescription =
                await _prescriptionService.UpdateAsync(
                    id,
                    dto);

            if (prescription == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid prescription update. Verify that " +
                        "all related records belong to the same " +
                        "clinical visit."
                });
            }

            return Ok(prescription);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _prescriptionService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Prescription not found."
                });
            }

            return Ok(new
            {
                message =
                    "Prescription deleted successfully."
            });
        }
    }
}