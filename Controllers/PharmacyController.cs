using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet("batches")]
        public async Task<IActionResult> GetAllBatches()
        {
            return Ok(await _pharmacyService.GetAllBatchesAsync());
        }

        [HttpGet("batches/{id}")]
        public async Task<IActionResult> GetBatchById(int id)
        {
            var batch = await _pharmacyService.GetBatchByIdAsync(id);

            if (batch == null)
                return NotFound(new { message = "Medicine batch not found." });

            return Ok(batch);
        }

        [HttpGet("batches/medicine/{medicineId}")]
        public async Task<IActionResult> GetBatchesByMedicine(int medicineId)
        {
            return Ok(
                await _pharmacyService.GetBatchesByMedicineIdAsync(medicineId));
        }

        [HttpGet("batches/branch/{branchId}")]
        public async Task<IActionResult> GetBatchesByBranch(int branchId)
        {
            return Ok(
                await _pharmacyService.GetBatchesByBranchIdAsync(branchId));
        }

        [HttpPost("batches")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateBatch(
            [FromBody] MedicineBatchCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var batch = await _pharmacyService.CreateBatchAsync(dto);

            if (batch == null)
            {
                return BadRequest(new
                {
                    message = "Invalid medicine batch or duplicate batch."
                });
            }

            return CreatedAtAction(
                nameof(GetBatchById),
                new { id = batch.MedicineBatchId },
                batch);
        }

        [HttpPut("batches/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateBatch(
            int id,
            [FromBody] MedicineBatchUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var batch = await _pharmacyService.UpdateBatchAsync(id, dto);

            if (batch == null)
            {
                return BadRequest(new
                {
                    message = "Medicine batch could not be updated."
                });
            }

            return Ok(batch);
        }

        [HttpDelete("batches/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var result = await _pharmacyService.DeleteBatchAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Medicine batch cannot be deleted because it " +
                        "does not exist or is already used."
                });
            }

            return Ok(new
            {
                message = "Medicine batch deleted successfully."
            });
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetAllSales()
        {
            return Ok(await _pharmacyService.GetAllSalesAsync());
        }

        [HttpGet("sales/{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _pharmacyService.GetSaleByIdAsync(id);

            if (sale == null)
                return NotFound(new { message = "Pharmacy sale not found." });

            return Ok(sale);
        }

        [HttpGet("sales/patient/{patientId}")]
        public async Task<IActionResult> GetSalesByPatient(int patientId)
        {
            return Ok(
                await _pharmacyService.GetSalesByPatientIdAsync(patientId));
        }

        [HttpGet("sales/branch/{branchId}")]
        public async Task<IActionResult> GetSalesByBranch(int branchId)
        {
            return Ok(
                await _pharmacyService.GetSalesByBranchIdAsync(branchId));
        }

        [HttpPost("sales")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSale(
            [FromBody] PharmacySaleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sale = await _pharmacyService.CreateSaleAsync(dto);

            if (sale == null)
            {
                return BadRequest(new
                {
                    message =
                        "Pharmacy sale could not be created. " +
                        "Check branch, patient, prescription, " +
                        "stock and medicine batches."
                });
            }

            return CreatedAtAction(
                nameof(GetSaleById),
                new { id = sale.PharmacySaleId },
                sale);
        }

        [HttpPut("sales/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateSale(
            int id,
            [FromBody] PharmacySaleUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sale = await _pharmacyService.UpdateSaleAsync(id, dto);

            if (sale == null)
            {
                return BadRequest(new
                {
                    message = "Pharmacy sale could not be updated."
                });
            }

            return Ok(sale);
        }

        [HttpDelete("sales/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var result = await _pharmacyService.DeleteSaleAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Pharmacy sale cannot be deleted because it " +
                        "does not exist or has already been paid."
                });
            }

            return Ok(new
            {
                message = "Pharmacy sale deleted successfully."
            });
        }
    }
}