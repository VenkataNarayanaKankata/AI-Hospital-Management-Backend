using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class LaboratoryController : ControllerBase
    {
        private readonly ILaboratoryService _laboratoryService;

        public LaboratoryController(
            ILaboratoryService laboratoryService)
        {
            _laboratoryService = laboratoryService;
        }

        [HttpGet("tests")]
        public async Task<IActionResult> GetAllTests()
        {
            var tests =
                await _laboratoryService.GetAllTestsAsync();

            return Ok(tests);
        }

        [HttpGet("tests/{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test =
                await _laboratoryService.GetTestByIdAsync(id);

            if (test == null)
            {
                return NotFound(new
                {
                    message = "Laboratory test not found."
                });
            }

            return Ok(test);
        }

        [HttpPost("tests")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateTest(
            [FromBody] LaboratoryTestCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test =
                await _laboratoryService.CreateTestAsync(dto);

            if (test == null)
            {
                return BadRequest(new
                {
                    message =
                        "Laboratory test already exists or " +
                        "the test code is already in use."
                });
            }

            return CreatedAtAction(
                nameof(GetTestById),
                new { id = test.LaboratoryTestId },
                test);
        }

        [HttpPut("tests/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateTest(
            int id,
            [FromBody] LaboratoryTestUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test =
                await _laboratoryService
                    .UpdateTestAsync(id, dto);

            if (test == null)
            {
                return BadRequest(new
                {
                    message =
                        "Laboratory test was not found or " +
                        "duplicate test information was provided."
                });
            }

            return Ok(test);
        }

        [HttpDelete("tests/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var result =
                await _laboratoryService.DeleteTestAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Laboratory test cannot be deleted because " +
                        "it does not exist or is already used."
                });
            }

            return Ok(new
            {
                message =
                    "Laboratory test deleted successfully."
            });
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders =
                await _laboratoryService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order =
                await _laboratoryService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Lab order not found."
                });
            }

            return Ok(order);
        }

        [HttpGet("orders/patient/{patientId}")]
        public async Task<IActionResult> GetOrdersByPatient(
            int patientId)
        {
            var orders =
                await _laboratoryService
                    .GetOrdersByPatientIdAsync(patientId);

            return Ok(orders);
        }

        [HttpGet("orders/doctor/{doctorId}")]
        public async Task<IActionResult> GetOrdersByDoctor(
            int doctorId)
        {
            var orders =
                await _laboratoryService
                    .GetOrdersByDoctorIdAsync(doctorId);

            return Ok(orders);
        }

        [HttpGet("orders/appointment/{appointmentId}")]
        public async Task<IActionResult> GetOrdersByAppointment(
            int appointmentId)
        {
            var orders =
                await _laboratoryService
                    .GetOrdersByAppointmentIdAsync(appointmentId);

            return Ok(orders);
        }

        [HttpGet("orders/branch/{branchId}")]
        public async Task<IActionResult> GetOrdersByBranch(
            int branchId)
        {
            var orders =
                await _laboratoryService
                    .GetOrdersByBranchIdAsync(branchId);

            return Ok(orders);
        }

        [HttpPost("orders")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateOrder(
            [FromBody] LabOrderCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order =
                await _laboratoryService.CreateOrderAsync(dto);

            if (order == null)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid laboratory order. Verify the " +
                        "Branch, Patient, Doctor, Appointment " +
                        "and laboratory tests."
                });
            }

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = order.LabOrderId },
                order);
        }

        [HttpPut("orders/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateOrder(
            int id,
            [FromBody] LabOrderUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order =
                await _laboratoryService
                    .UpdateOrderAsync(id, dto);

            if (order == null)
            {
                return BadRequest(new
                {
                    message =
                        "Lab order cannot be updated. Verify the " +
                        "relationships, status and laboratory tests."
                });
            }

            return Ok(order);
        }

        [HttpPost("order-items/{labOrderItemId}/collect-sample")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CollectSample(
            int labOrderItemId)
        {
            var result =
                await _laboratoryService
                    .CollectSampleAsync(labOrderItemId);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Sample cannot be collected for this " +
                        "laboratory order item."
                });
            }

            return Ok(new
            {
                message =
                    "Sample collected successfully."
            });
        }

        [HttpPost("order-items/{labOrderItemId}/result")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddResult(
            int labOrderItemId,
            [FromBody] LabResultCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _laboratoryService
                    .AddResultAsync(
                        labOrderItemId,
                        dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    message =
                        "Result cannot be added. Verify the " +
                        "laboratory order item and sample status."
                });
            }

            return Ok(result);
        }

        [HttpPut("results/{labResultId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateResult(
            int labResultId,
            [FromBody] LabResultUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _laboratoryService
                    .UpdateResultAsync(
                        labResultId,
                        dto);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Lab result not found."
                });
            }

            return Ok(result);
        }

        [HttpDelete("orders/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result =
                await _laboratoryService.DeleteOrderAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Lab order cannot be deleted because " +
                        "it does not exist or already has results."
                });
            }

            return Ok(new
            {
                message =
                    "Lab order deleted successfully."
            });
        }
    }
}