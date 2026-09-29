using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(
            IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices =
                await _billingService.GetAllAsync();

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice =
                await _billingService.GetByIdAsync(id);

            if (invoice == null)
            {
                return NotFound(new
                {
                    message = "Invoice not found."
                });
            }

            return Ok(invoice);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(
            int patientId)
        {
            var invoices =
                await _billingService
                    .GetByPatientIdAsync(patientId);

            return Ok(invoices);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(
            int doctorId)
        {
            var invoices =
                await _billingService
                    .GetByDoctorIdAsync(doctorId);

            return Ok(invoices);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(
            int appointmentId)
        {
            var invoices =
                await _billingService
                    .GetByAppointmentIdAsync(appointmentId);

            return Ok(invoices);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(
            int branchId)
        {
            var invoices =
                await _billingService
                    .GetByBranchIdAsync(branchId);

            return Ok(invoices);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(
            [FromBody] InvoiceCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice =
                await _billingService.CreateAsync(dto);

            if (invoice == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid invoice. Verify the Branch, " +
                        "Patient, Doctor, Appointment and billing " +
                        "details."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = invoice.InvoiceId },
                invoice);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] InvoiceUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice =
                await _billingService.UpdateAsync(
                    id,
                    dto);

            if (invoice == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid invoice update. Verify the " +
                        "relationships and financial amounts."
                });
            }

            return Ok(invoice);
        }

        [HttpPost("{invoiceId}/payments")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddPayment(
            int invoiceId,
            [FromBody] PaymentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment =
                await _billingService.AddPaymentAsync(
                    invoiceId,
                    dto);

            if (payment == null)
            {
                return BadRequest(new
                {
                    message =
                        "Payment could not be processed. " +
                        "Verify the invoice and payment amount."
                });
            }

            return Ok(payment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _billingService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Invoice cannot be deleted because it " +
                        "does not exist or has completed payments."
                });
            }

            return Ok(new
            {
                message =
                    "Invoice deleted successfully."
            });
        }
    }
}