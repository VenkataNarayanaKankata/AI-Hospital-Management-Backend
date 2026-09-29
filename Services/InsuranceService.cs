using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDbContext _context;

        public InsuranceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InsuranceProviderDto>> GetAllProvidersAsync()
        {
            return await _context.InsuranceProviders
                .Select(p => new InsuranceProviderDto
                {
                    InsuranceProviderId = p.InsuranceProviderId,
                    ProviderName = p.ProviderName,
                    ContactPerson = p.ContactPerson,
                    ContactNumber = p.ContactNumber,
                    Email = p.Email,
                    Address = p.Address,
                    ProviderCode = p.ProviderCode,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        public async Task<InsuranceProviderDto?> GetProviderByIdAsync(int id)
        {
            return await _context.InsuranceProviders
                .Where(p => p.InsuranceProviderId == id)
                .Select(p => new InsuranceProviderDto
                {
                    InsuranceProviderId = p.InsuranceProviderId,
                    ProviderName = p.ProviderName,
                    ContactPerson = p.ContactPerson,
                    ContactNumber = p.ContactNumber,
                    Email = p.Email,
                    Address = p.Address,
                    ProviderCode = p.ProviderCode,
                    IsActive = p.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InsuranceProviderDto> CreateProviderAsync(
            CreateInsuranceProviderDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProviderName))
                throw new ArgumentException("Provider name is required.");

            var exists = await _context.InsuranceProviders
                .AnyAsync(p => p.ProviderName == dto.ProviderName);

            if (exists)
                throw new ArgumentException(
                    "Insurance provider already exists.");

            if (!string.IsNullOrWhiteSpace(dto.ProviderCode))
            {
                var codeExists = await _context.InsuranceProviders
                    .AnyAsync(p => p.ProviderCode == dto.ProviderCode);

                if (codeExists)
                    throw new ArgumentException(
                        "Provider code already exists.");
            }

            var provider = new InsuranceProvider
            {
                ProviderName = dto.ProviderName,
                ContactPerson = dto.ContactPerson,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                Address = dto.Address,
                ProviderCode = dto.ProviderCode,
                IsActive = dto.IsActive
            };

            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            return new InsuranceProviderDto
            {
                InsuranceProviderId = provider.InsuranceProviderId,
                ProviderName = provider.ProviderName,
                ContactPerson = provider.ContactPerson,
                ContactNumber = provider.ContactNumber,
                Email = provider.Email,
                Address = provider.Address,
                ProviderCode = provider.ProviderCode,
                IsActive = provider.IsActive
            };
        }

        public async Task<InsuranceProviderDto?> UpdateProviderAsync(
            int id,
            UpdateInsuranceProviderDto dto)
        {
            var provider = await _context.InsuranceProviders
                .FirstOrDefaultAsync(p => p.InsuranceProviderId == id);

            if (provider == null)
                return null;

            var nameExists = await _context.InsuranceProviders
                .AnyAsync(p =>
                    p.InsuranceProviderId != id &&
                    p.ProviderName == dto.ProviderName);

            if (nameExists)
                throw new ArgumentException(
                    "Insurance provider already exists.");

            if (!string.IsNullOrWhiteSpace(dto.ProviderCode))
            {
                var codeExists = await _context.InsuranceProviders
                    .AnyAsync(p =>
                        p.InsuranceProviderId != id &&
                        p.ProviderCode == dto.ProviderCode);

                if (codeExists)
                    throw new ArgumentException(
                        "Provider code already exists.");
            }

            provider.ProviderName = dto.ProviderName;
            provider.ContactPerson = dto.ContactPerson;
            provider.ContactNumber = dto.ContactNumber;
            provider.Email = dto.Email;
            provider.Address = dto.Address;
            provider.ProviderCode = dto.ProviderCode;
            provider.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return new InsuranceProviderDto
            {
                InsuranceProviderId = provider.InsuranceProviderId,
                ProviderName = provider.ProviderName,
                ContactPerson = provider.ContactPerson,
                ContactNumber = provider.ContactNumber,
                Email = provider.Email,
                Address = provider.Address,
                ProviderCode = provider.ProviderCode,
                IsActive = provider.IsActive
            };
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            var provider = await _context.InsuranceProviders
                .Include(p => p.PatientInsurances)
                .FirstOrDefaultAsync(p =>
                    p.InsuranceProviderId == id);

            if (provider == null)
                return false;

            if (provider.PatientInsurances.Any())
                throw new InvalidOperationException(
                    "Insurance provider cannot be deleted because it has patient insurance policies.");

            _context.InsuranceProviders.Remove(provider);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PatientInsuranceDto>>
            GetAllPatientInsurancesAsync()
        {
            return await _context.PatientInsurances
                .Include(pi => pi.Patient)
                .Include(pi => pi.InsuranceProvider)
                .Select(pi => new PatientInsuranceDto
                {
                    PatientInsuranceId = pi.PatientInsuranceId,
                    PolicyNumber = pi.PolicyNumber,
                    MemberId = pi.MemberId,
                    PolicyHolderName = pi.PolicyHolderName,
                    PolicyStartDate = pi.PolicyStartDate,
                    PolicyEndDate = pi.PolicyEndDate,
                    SumInsured = pi.SumInsured,
                    AvailableCoverage = pi.AvailableCoverage,
                    Status = pi.Status,

                    PatientId = pi.PatientId,
                    PatientName = pi.Patient.FullName,

                    InsuranceProviderId = pi.InsuranceProviderId,
                    ProviderName = pi.InsuranceProvider.ProviderName
                })
                .ToListAsync();
        }

        public async Task<PatientInsuranceDto?>
            GetPatientInsuranceByIdAsync(int id)
        {
            return await _context.PatientInsurances
                .Include(pi => pi.Patient)
                .Include(pi => pi.InsuranceProvider)
                .Where(pi => pi.PatientInsuranceId == id)
                .Select(pi => new PatientInsuranceDto
                {
                    PatientInsuranceId = pi.PatientInsuranceId,
                    PolicyNumber = pi.PolicyNumber,
                    MemberId = pi.MemberId,
                    PolicyHolderName = pi.PolicyHolderName,
                    PolicyStartDate = pi.PolicyStartDate,
                    PolicyEndDate = pi.PolicyEndDate,
                    SumInsured = pi.SumInsured,
                    AvailableCoverage = pi.AvailableCoverage,
                    Status = pi.Status,

                    PatientId = pi.PatientId,
                    PatientName = pi.Patient.FullName,

                    InsuranceProviderId = pi.InsuranceProviderId,
                    ProviderName = pi.InsuranceProvider.ProviderName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PatientInsuranceDto>>
            GetPatientInsurancesByPatientAsync(int patientId)
        {
            return await _context.PatientInsurances
                .Include(pi => pi.Patient)
                .Include(pi => pi.InsuranceProvider)
                .Where(pi => pi.PatientId == patientId)
                .Select(pi => new PatientInsuranceDto
                {
                    PatientInsuranceId = pi.PatientInsuranceId,
                    PolicyNumber = pi.PolicyNumber,
                    MemberId = pi.MemberId,
                    PolicyHolderName = pi.PolicyHolderName,
                    PolicyStartDate = pi.PolicyStartDate,
                    PolicyEndDate = pi.PolicyEndDate,
                    SumInsured = pi.SumInsured,
                    AvailableCoverage = pi.AvailableCoverage,
                    Status = pi.Status,

                    PatientId = pi.PatientId,
                    PatientName = pi.Patient.FullName,

                    InsuranceProviderId = pi.InsuranceProviderId,
                    ProviderName = pi.InsuranceProvider.ProviderName
                })
                .ToListAsync();
        }

        public async Task<PatientInsuranceDto>
            CreatePatientInsuranceAsync(
                CreatePatientInsuranceDto dto)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p =>
                    p.PatientId == dto.PatientId);

            if (patient == null)
                throw new ArgumentException("Patient not found.");

            var provider = await _context.InsuranceProviders
                .FirstOrDefaultAsync(p =>
                    p.InsuranceProviderId ==
                    dto.InsuranceProviderId);

            if (provider == null)
                throw new ArgumentException(
                    "Insurance provider not found.");

            if (!provider.IsActive)
                throw new InvalidOperationException(
                    "Insurance provider is inactive.");

            if (dto.PolicyEndDate < dto.PolicyStartDate)
                throw new ArgumentException(
                    "Policy end date cannot be before policy start date.");

            if (dto.SumInsured < 0)
                throw new ArgumentException(
                    "Sum insured cannot be negative.");

            if (dto.AvailableCoverage < 0 ||
                dto.AvailableCoverage > dto.SumInsured)
                throw new ArgumentException(
                    "Available coverage must be between zero and the sum insured.");

            var policyExists = await _context.PatientInsurances
                .AnyAsync(pi =>
                    pi.PolicyNumber == dto.PolicyNumber);

            if (policyExists)
                throw new ArgumentException(
                    "Policy number already exists.");

            var insurance = new PatientInsurance
            {
                PolicyNumber = dto.PolicyNumber,
                MemberId = dto.MemberId,
                PolicyHolderName = dto.PolicyHolderName,
                PolicyStartDate = dto.PolicyStartDate,
                PolicyEndDate = dto.PolicyEndDate,
                SumInsured = dto.SumInsured,
                AvailableCoverage = dto.AvailableCoverage,
                Status = dto.Status,
                PatientId = dto.PatientId,
                InsuranceProviderId = dto.InsuranceProviderId
            };

            _context.PatientInsurances.Add(insurance);
            await _context.SaveChangesAsync();

            return new PatientInsuranceDto
            {
                PatientInsuranceId = insurance.PatientInsuranceId,
                PolicyNumber = insurance.PolicyNumber,
                MemberId = insurance.MemberId,
                PolicyHolderName = insurance.PolicyHolderName,
                PolicyStartDate = insurance.PolicyStartDate,
                PolicyEndDate = insurance.PolicyEndDate,
                SumInsured = insurance.SumInsured,
                AvailableCoverage = insurance.AvailableCoverage,
                Status = insurance.Status,

                PatientId = patient.PatientId,
                PatientName = patient.FullName,

                InsuranceProviderId = provider.InsuranceProviderId,
                ProviderName = provider.ProviderName
            };
        }

        public async Task<PatientInsuranceDto?>
            UpdatePatientInsuranceAsync(
                int id,
                UpdatePatientInsuranceDto dto)
        {
            var insurance = await _context.PatientInsurances
                .Include(pi => pi.Patient)
                .Include(pi => pi.InsuranceProvider)
                .FirstOrDefaultAsync(pi =>
                    pi.PatientInsuranceId == id);

            if (insurance == null)
                return null;

            if (dto.PolicyEndDate < dto.PolicyStartDate)
                throw new ArgumentException(
                    "Policy end date cannot be before policy start date.");

            if (dto.SumInsured < 0)
                throw new ArgumentException(
                    "Sum insured cannot be negative.");

            if (dto.AvailableCoverage < 0 ||
                dto.AvailableCoverage > dto.SumInsured)
                throw new ArgumentException(
                    "Available coverage must be between zero and the sum insured.");

            insurance.MemberId = dto.MemberId;
            insurance.PolicyHolderName = dto.PolicyHolderName;
            insurance.PolicyStartDate = dto.PolicyStartDate;
            insurance.PolicyEndDate = dto.PolicyEndDate;
            insurance.SumInsured = dto.SumInsured;
            insurance.AvailableCoverage = dto.AvailableCoverage;
            insurance.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new PatientInsuranceDto
            {
                PatientInsuranceId = insurance.PatientInsuranceId,
                PolicyNumber = insurance.PolicyNumber,
                MemberId = insurance.MemberId,
                PolicyHolderName = insurance.PolicyHolderName,
                PolicyStartDate = insurance.PolicyStartDate,
                PolicyEndDate = insurance.PolicyEndDate,
                SumInsured = insurance.SumInsured,
                AvailableCoverage = insurance.AvailableCoverage,
                Status = insurance.Status,

                PatientId = insurance.PatientId,
                PatientName = insurance.Patient.FullName,

                InsuranceProviderId =
                    insurance.InsuranceProviderId,
                ProviderName =
                    insurance.InsuranceProvider.ProviderName
            };
        }

        public async Task<bool> DeletePatientInsuranceAsync(int id)
        {
            var insurance = await _context.PatientInsurances
                .Include(pi => pi.InsuranceClaims)
                .FirstOrDefaultAsync(pi =>
                    pi.PatientInsuranceId == id);

            if (insurance == null)
                return false;

            if (insurance.InsuranceClaims.Any())
                throw new InvalidOperationException(
                    "Insurance policy cannot be deleted because it has claims.");

            _context.PatientInsurances.Remove(insurance);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<InsuranceClaimDto>>
            GetAllClaimsAsync()
        {
            return await _context.InsuranceClaims
                .Include(c => c.PatientInsurance)
                .Include(c => c.Admission)
                .ThenInclude(a => a.Patient)
                .Select(c => new InsuranceClaimDto
                {
                    InsuranceClaimId = c.InsuranceClaimId,
                    ClaimNumber = c.ClaimNumber,
                    ClaimDate = c.ClaimDate,
                    ClaimAmount = c.ClaimAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    Status = c.Status,
                    Remarks = c.Remarks,
                    ApprovedDate = c.ApprovedDate,
                    SettledDate = c.SettledDate,

                    PatientInsuranceId = c.PatientInsuranceId,
                    PolicyNumber = c.PatientInsurance.PolicyNumber,

                    PatientId = c.Admission.PatientId,
                    PatientName = c.Admission.Patient.FullName,

                    AdmissionId = c.AdmissionId,
                    AdmissionNumber = c.Admission.AdmissionNumber
                })
                .ToListAsync();
        }

        public async Task<InsuranceClaimDto?> GetClaimByIdAsync(int id)
        {
            return await _context.InsuranceClaims
                .Include(c => c.PatientInsurance)
                .Include(c => c.Admission)
                .ThenInclude(a => a.Patient)
                .Where(c => c.InsuranceClaimId == id)
                .Select(c => new InsuranceClaimDto
                {
                    InsuranceClaimId = c.InsuranceClaimId,
                    ClaimNumber = c.ClaimNumber,
                    ClaimDate = c.ClaimDate,
                    ClaimAmount = c.ClaimAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    Status = c.Status,
                    Remarks = c.Remarks,
                    ApprovedDate = c.ApprovedDate,
                    SettledDate = c.SettledDate,

                    PatientInsuranceId = c.PatientInsuranceId,
                    PolicyNumber = c.PatientInsurance.PolicyNumber,

                    PatientId = c.Admission.PatientId,
                    PatientName = c.Admission.Patient.FullName,

                    AdmissionId = c.AdmissionId,
                    AdmissionNumber = c.Admission.AdmissionNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InsuranceClaimDto>>
            GetClaimsByPatientInsuranceAsync(
                int patientInsuranceId)
        {
            return await _context.InsuranceClaims
                .Include(c => c.PatientInsurance)
                .Include(c => c.Admission)
                .ThenInclude(a => a.Patient)
                .Where(c =>
                    c.PatientInsuranceId == patientInsuranceId)
                .Select(c => new InsuranceClaimDto
                {
                    InsuranceClaimId = c.InsuranceClaimId,
                    ClaimNumber = c.ClaimNumber,
                    ClaimDate = c.ClaimDate,
                    ClaimAmount = c.ClaimAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    Status = c.Status,
                    Remarks = c.Remarks,
                    ApprovedDate = c.ApprovedDate,
                    SettledDate = c.SettledDate,

                    PatientInsuranceId = c.PatientInsuranceId,
                    PolicyNumber = c.PatientInsurance.PolicyNumber,

                    PatientId = c.Admission.PatientId,
                    PatientName = c.Admission.Patient.FullName,

                    AdmissionId = c.AdmissionId,
                    AdmissionNumber = c.Admission.AdmissionNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<InsuranceClaimDto>>
            GetClaimsByAdmissionAsync(int admissionId)
        {
            return await _context.InsuranceClaims
                .Include(c => c.PatientInsurance)
                .Include(c => c.Admission)
                .ThenInclude(a => a.Patient)
                .Where(c => c.AdmissionId == admissionId)
                .Select(c => new InsuranceClaimDto
                {
                    InsuranceClaimId = c.InsuranceClaimId,
                    ClaimNumber = c.ClaimNumber,
                    ClaimDate = c.ClaimDate,
                    ClaimAmount = c.ClaimAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    Status = c.Status,
                    Remarks = c.Remarks,
                    ApprovedDate = c.ApprovedDate,
                    SettledDate = c.SettledDate,

                    PatientInsuranceId = c.PatientInsuranceId,
                    PolicyNumber = c.PatientInsurance.PolicyNumber,

                    PatientId = c.Admission.PatientId,
                    PatientName = c.Admission.Patient.FullName,

                    AdmissionId = c.AdmissionId,
                    AdmissionNumber = c.Admission.AdmissionNumber
                })
                .ToListAsync();
        }

        public async Task<InsuranceClaimDto> CreateClaimAsync(
            CreateInsuranceClaimDto dto)
        {
            var insurance = await _context.PatientInsurances
                .FirstOrDefaultAsync(pi =>
                    pi.PatientInsuranceId ==
                    dto.PatientInsuranceId);

            if (insurance == null)
                throw new ArgumentException(
                    "Patient insurance policy not found.");

            var admission = await _context.Admissions
                .FirstOrDefaultAsync(a =>
                    a.AdmissionId == dto.AdmissionId);

            if (admission == null)
                throw new ArgumentException(
                    "Admission not found.");

            if (admission.PatientId != insurance.PatientId)
                throw new ArgumentException(
                    "Insurance policy does not belong to the admitted patient.");

            if (insurance.Status != "Active")
                throw new InvalidOperationException(
                    "Insurance policy is not active.");

            if (DateTime.UtcNow.Date < insurance.PolicyStartDate.Date ||
                DateTime.UtcNow.Date > insurance.PolicyEndDate.Date)
                throw new InvalidOperationException(
                    "Insurance policy is not currently valid.");

            if (dto.ClaimAmount <= 0)
                throw new ArgumentException(
                    "Claim amount must be greater than zero.");

            if (dto.ClaimAmount > insurance.AvailableCoverage)
                throw new InvalidOperationException(
                    "Claim amount exceeds available insurance coverage.");

            var claimExists = await _context.InsuranceClaims
                .AnyAsync(c =>
                    c.ClaimNumber == dto.ClaimNumber);

            if (claimExists)
                throw new ArgumentException(
                    "Claim number already exists.");

            var admissionClaimExists = await _context.InsuranceClaims
                .AnyAsync(c =>
                    c.AdmissionId == dto.AdmissionId &&
                    c.PatientInsuranceId ==
                    dto.PatientInsuranceId);

            if (admissionClaimExists)
                throw new InvalidOperationException(
                    "A claim already exists for this admission and insurance policy.");

            var claim = new InsuranceClaim
            {
                ClaimNumber = dto.ClaimNumber,
                ClaimDate = dto.ClaimDate,
                ClaimAmount = dto.ClaimAmount,
                ApprovedAmount = 0,
                RejectedAmount = 0,
                Status = "Submitted",
                Remarks = dto.Remarks,
                PatientInsuranceId =
                    dto.PatientInsuranceId,
                AdmissionId = dto.AdmissionId
            };

            _context.InsuranceClaims.Add(claim);

            await _context.SaveChangesAsync();

            return await GetClaimByIdAsync(claim.InsuranceClaimId)
                   ?? throw new InvalidOperationException(
                       "Unable to retrieve created insurance claim.");
        }

        public async Task<InsuranceClaimDto?>
            UpdateClaimAsync(
                int id,
                UpdateInsuranceClaimDto dto)
        {
            var claim = await _context.InsuranceClaims
                .Include(c => c.PatientInsurance)
                .Include(c => c.Admission)
                .ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(c =>
                    c.InsuranceClaimId == id);

            if (claim == null)
                return null;

            var validStatuses = new[]
            {
                "Submitted",
                "Approved",
                "PartiallyApproved",
                "Rejected",
                "Settled"
            };

            if (!validStatuses.Contains(dto.Status))
                throw new ArgumentException(
                    "Invalid claim status.");

            if (dto.ApprovedAmount < 0 ||
                dto.RejectedAmount < 0)
                throw new ArgumentException(
                    "Approved and rejected amounts cannot be negative.");

            if (dto.ApprovedAmount + dto.RejectedAmount >
                claim.ClaimAmount)
                throw new ArgumentException(
                    "Approved and rejected amounts cannot exceed the claim amount.");

            if (dto.Status == "Approved" &&
                dto.ApprovedAmount <= 0)
                throw new ArgumentException(
                    "Approved amount must be greater than zero.");

            if (dto.Status == "PartiallyApproved")
            {
                if (dto.ApprovedAmount <= 0 ||
                    dto.RejectedAmount <= 0)
                    throw new ArgumentException(
                        "Partially approved claims must have both approved and rejected amounts.");
            }

            if (dto.Status == "Rejected" &&
                dto.RejectedAmount <= 0)
                throw new ArgumentException(
                    "Rejected amount must be greater than zero.");

            if (dto.Status == "Settled" &&
                dto.ApprovedAmount <= 0)
                throw new ArgumentException(
                    "A claim must have an approved amount before settlement.");

            var previousApprovedAmount =
                claim.ApprovedAmount;

            var newApprovedAmount =
                dto.ApprovedAmount;

            var coverageDifference =
                newApprovedAmount - previousApprovedAmount;

            if (coverageDifference > 0)
            {
                if (coverageDifference >
                    claim.PatientInsurance.AvailableCoverage)
                    throw new InvalidOperationException(
                        "Approved amount exceeds available insurance coverage.");

                claim.PatientInsurance.AvailableCoverage -=
                    coverageDifference;
            }
            else if (coverageDifference < 0)
            {
                claim.PatientInsurance.AvailableCoverage +=
                    Math.Abs(coverageDifference);

                if (claim.PatientInsurance.AvailableCoverage >
                    claim.PatientInsurance.SumInsured)
                {
                    claim.PatientInsurance.AvailableCoverage =
                        claim.PatientInsurance.SumInsured;
                }
            }

            claim.ApprovedAmount = dto.ApprovedAmount;
            claim.RejectedAmount = dto.RejectedAmount;
            claim.Status = dto.Status;
            claim.Remarks = dto.Remarks;

            if (dto.Status == "Approved" ||
                dto.Status == "PartiallyApproved")
            {
                claim.ApprovedDate =
                    dto.ApprovedDate ?? DateTime.UtcNow;
            }
            else
            {
                claim.ApprovedDate =
                    dto.ApprovedDate;
            }

            if (dto.Status == "Settled")
            {
                claim.SettledDate =
                    dto.SettledDate ?? DateTime.UtcNow;
            }
            else
            {
                claim.SettledDate =
                    dto.SettledDate;
            }

            await _context.SaveChangesAsync();

            return await GetClaimByIdAsync(id);
        }

        public async Task<bool> DeleteClaimAsync(int id)
        {
            var claim = await _context.InsuranceClaims
                .FirstOrDefaultAsync(c =>
                    c.InsuranceClaimId == id);

            if (claim == null)
                return false;

            if (claim.Status != "Submitted")
                throw new InvalidOperationException(
                    "Only submitted claims can be deleted.");

            _context.InsuranceClaims.Remove(claim);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}