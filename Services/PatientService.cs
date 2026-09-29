using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientResponseDto>> GetAllAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(p => new PatientResponseDto
                {
                    PatientId = p.PatientId,
                    PatientNumber = p.PatientNumber,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    BloodGroup = p.BloodGroup,
                    MobileNumber = p.MobileNumber,
                    Email = p.Email,
                    Address = p.Address,
                    EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactNumber = p.EmergencyContactNumber,
                    RegistrationDate = p.RegistrationDate,
                    IsActive = p.IsActive,

                    BranchId = p.BranchId,
                    BranchName = p.Branch.BranchName,

                    HospitalId = p.Branch.HospitalId,
                    HospitalName = p.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<PatientResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p => p.BranchId == branchId)
                .Include(p => p.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(p => new PatientResponseDto
                {
                    PatientId = p.PatientId,
                    PatientNumber = p.PatientNumber,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    BloodGroup = p.BloodGroup,
                    MobileNumber = p.MobileNumber,
                    Email = p.Email,
                    Address = p.Address,
                    EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactNumber = p.EmergencyContactNumber,
                    RegistrationDate = p.RegistrationDate,
                    IsActive = p.IsActive,

                    BranchId = p.BranchId,
                    BranchName = p.Branch.BranchName,

                    HospitalId = p.Branch.HospitalId,
                    HospitalName = p.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<PatientResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p => p.PatientId == id)
                .Include(p => p.Branch)
                .ThenInclude(b => b.Hospital)
                .Select(p => new PatientResponseDto
                {
                    PatientId = p.PatientId,
                    PatientNumber = p.PatientNumber,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    BloodGroup = p.BloodGroup,
                    MobileNumber = p.MobileNumber,
                    Email = p.Email,
                    Address = p.Address,
                    EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactNumber = p.EmergencyContactNumber,
                    RegistrationDate = p.RegistrationDate,
                    IsActive = p.IsActive,

                    BranchId = p.BranchId,
                    BranchName = p.Branch.BranchName,

                    HospitalId = p.Branch.HospitalId,
                    HospitalName = p.Branch.Hospital.HospitalName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PatientResponseDto?> CreateAsync(
            PatientCreateDto dto)
        {
            // 1. Check whether the selected Branch exists
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // 2. Generate the next Patient Number
            var lastPatientId = await _context.Patients
                .OrderByDescending(p => p.PatientId)
                .Select(p => (int?)p.PatientId)
                .FirstOrDefaultAsync();

            var nextPatientId = (lastPatientId ?? 0) + 1;

            var patientNumber = $"PAT-{nextPatientId:D6}";

            // 3. Create Patient
            var patient = new Patient
            {
                PatientNumber = patientNumber,
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                BloodGroup = dto.BloodGroup,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                Address = dto.Address,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactNumber = dto.EmergencyContactNumber,

                BranchId = dto.BranchId,

                RegistrationDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(patient.PatientId);
        }

        public async Task<PatientResponseDto?> UpdateAsync(
            int id,
            PatientUpdateDto dto)
        {
            // 1. Find Patient
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return null;
            }

            // 2. Check Branch
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // 3. Update Patient details
            patient.FullName = dto.FullName;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.BloodGroup = dto.BloodGroup;
            patient.MobileNumber = dto.MobileNumber;
            patient.Email = dto.Email;
            patient.Address = dto.Address;
            patient.EmergencyContactName = dto.EmergencyContactName;
            patient.EmergencyContactNumber = dto.EmergencyContactNumber;

            patient.BranchId = dto.BranchId;

            patient.IsActive = dto.IsActive;

            // PatientNumber and RegistrationDate are NOT changed.

            await _context.SaveChangesAsync();

            return await GetByIdAsync(patient.PatientId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}