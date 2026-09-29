using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _dashboardService.GetSummaryAsync();

            return Ok(summary);
        }
        [HttpGet("appointment-status")]
        public async Task<IActionResult> GetAppointmentStatus()
        {
            var result = await _dashboardService.GetAppointmentStatusAsync();

            return Ok(result);
        }
    }
}