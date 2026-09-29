using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet("providers")]
        public async Task<IActionResult> GetAllProviders()
        {
            var providers =
                await _insuranceService.GetAllProvidersAsync();

            return Ok(providers);
        }

        [HttpGet("providers/{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider =
                await _insuranceService.GetProviderByIdAsync(id);

            if (provider == null)
                return NotFound("Insurance provider not found.");

            return Ok(provider);
        }

        [HttpPost("providers")]
        public async Task<IActionResult> CreateProvider(
            CreateInsuranceProviderDto dto)
        {
            try
            {
                var provider =
                    await _insuranceService.CreateProviderAsync(dto);

                return CreatedAtAction(
                    nameof(GetProviderById),
                    new { id = provider.InsuranceProviderId },
                    provider);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("providers/{id}")]
        public async Task<IActionResult> UpdateProvider(
            int id,
            UpdateInsuranceProviderDto dto)
        {
            try
            {
                var provider =
                    await _insuranceService.UpdateProviderAsync(id, dto);

                if (provider == null)
                    return NotFound("Insurance provider not found.");

                return Ok(provider);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("providers/{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                var deleted =
                    await _insuranceService.DeleteProviderAsync(id);

                if (!deleted)
                    return NotFound("Insurance provider not found.");

                return Ok("Insurance provider deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("patient-insurances")]
        public async Task<IActionResult> GetAllPatientInsurances()
        {
            var insurances =
                await _insuranceService.GetAllPatientInsurancesAsync();

            return Ok(insurances);
        }

        [HttpGet("patient-insurances/{id}")]
        public async Task<IActionResult> GetPatientInsuranceById(int id)
        {
            var insurance =
                await _insuranceService
                    .GetPatientInsuranceByIdAsync(id);

            if (insurance == null)
                return NotFound("Patient insurance not found.");

            return Ok(insurance);
        }

        [HttpGet("patient-insurances/patient/{patientId}")]
        public async Task<IActionResult> GetPatientInsurancesByPatient(
            int patientId)
        {
            var insurances =
                await _insuranceService
                    .GetPatientInsurancesByPatientAsync(patientId);

            return Ok(insurances);
        }

        [HttpPost("patient-insurances")]
        public async Task<IActionResult> CreatePatientInsurance(
            CreatePatientInsuranceDto dto)
        {
            try
            {
                var insurance =
                    await _insuranceService
                        .CreatePatientInsuranceAsync(dto);

                return CreatedAtAction(
                    nameof(GetPatientInsuranceById),
                    new { id = insurance.PatientInsuranceId },
                    insurance);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("patient-insurances/{id}")]
        public async Task<IActionResult> UpdatePatientInsurance(
            int id,
            UpdatePatientInsuranceDto dto)
        {
            try
            {
                var insurance =
                    await _insuranceService
                        .UpdatePatientInsuranceAsync(id, dto);

                if (insurance == null)
                    return NotFound("Patient insurance not found.");

                return Ok(insurance);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("patient-insurances/{id}")]
        public async Task<IActionResult> DeletePatientInsurance(int id)
        {
            try
            {
                var deleted =
                    await _insuranceService
                        .DeletePatientInsuranceAsync(id);

                if (!deleted)
                    return NotFound("Patient insurance not found.");

                return Ok("Patient insurance deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims =
                await _insuranceService.GetAllClaimsAsync();

            return Ok(claims);
        }

        [HttpGet("claims/{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim =
                await _insuranceService.GetClaimByIdAsync(id);

            if (claim == null)
                return NotFound("Insurance claim not found.");

            return Ok(claim);
        }

        [HttpGet("claims/patient-insurance/{patientInsuranceId}")]
        public async Task<IActionResult> GetClaimsByPatientInsurance(
            int patientInsuranceId)
        {
            var claims =
                await _insuranceService
                    .GetClaimsByPatientInsuranceAsync(
                        patientInsuranceId);

            return Ok(claims);
        }

        [HttpGet("claims/admission/{admissionId}")]
        public async Task<IActionResult> GetClaimsByAdmission(
            int admissionId)
        {
            var claims =
                await _insuranceService
                    .GetClaimsByAdmissionAsync(admissionId);

            return Ok(claims);
        }

        [HttpPost("claims")]
        public async Task<IActionResult> CreateClaim(
            CreateInsuranceClaimDto dto)
        {
            try
            {
                var claim =
                    await _insuranceService.CreateClaimAsync(dto);

                return CreatedAtAction(
                    nameof(GetClaimById),
                    new { id = claim.InsuranceClaimId },
                    claim);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("claims/{id}")]
        public async Task<IActionResult> UpdateClaim(
            int id,
            UpdateInsuranceClaimDto dto)
        {
            try
            {
                var claim =
                    await _insuranceService.UpdateClaimAsync(id, dto);

                if (claim == null)
                    return NotFound("Insurance claim not found.");

                return Ok(claim);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("claims/{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            try
            {
                var deleted =
                    await _insuranceService.DeleteClaimAsync(id);

                if (!deleted)
                    return NotFound("Insurance claim not found.");

                return Ok("Insurance claim deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}