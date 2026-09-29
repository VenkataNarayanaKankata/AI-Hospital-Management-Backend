using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorResponseDto>> GetAllAsync()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Include(d => d.Department)
                .Select(d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    RegistrationNumber = d.RegistrationNumber,
                    Email = d.Email,
                    MobileNumber = d.MobileNumber,
                    Gender = d.Gender,
                    DateOfBirth = d.DateOfBirth,
                    Qualification = d.Qualification,
                    Specialization = d.Specialization,
                    ExperienceYears = d.ExperienceYears,
                    ConsultationFee = d.ConsultationFee,
                    ProfileImage = d.ProfileImage,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,

                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,

                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.DepartmentName,

                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<DoctorResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.BranchId == branchId)
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Include(d => d.Department)
                .Select(d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    RegistrationNumber = d.RegistrationNumber,
                    Email = d.Email,
                    MobileNumber = d.MobileNumber,
                    Gender = d.Gender,
                    DateOfBirth = d.DateOfBirth,
                    Qualification = d.Qualification,
                    Specialization = d.Specialization,
                    ExperienceYears = d.ExperienceYears,
                    ConsultationFee = d.ConsultationFee,
                    ProfileImage = d.ProfileImage,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,

                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,

                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.DepartmentName,

                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<List<DoctorResponseDto>> GetByDepartmentIdAsync(
            int departmentId)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.DepartmentId == departmentId)
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Include(d => d.Department)
                .Select(d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    RegistrationNumber = d.RegistrationNumber,
                    Email = d.Email,
                    MobileNumber = d.MobileNumber,
                    Gender = d.Gender,
                    DateOfBirth = d.DateOfBirth,
                    Qualification = d.Qualification,
                    Specialization = d.Specialization,
                    ExperienceYears = d.ExperienceYears,
                    ConsultationFee = d.ConsultationFee,
                    ProfileImage = d.ProfileImage,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,

                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,

                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.DepartmentName,

                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .ToListAsync();
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.DoctorId == id)
                .Include(d => d.Branch)
                .ThenInclude(b => b.Hospital)
                .Include(d => d.Department)
                .Select(d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    RegistrationNumber = d.RegistrationNumber,
                    Email = d.Email,
                    MobileNumber = d.MobileNumber,
                    Gender = d.Gender,
                    DateOfBirth = d.DateOfBirth,
                    Qualification = d.Qualification,
                    Specialization = d.Specialization,
                    ExperienceYears = d.ExperienceYears,
                    ConsultationFee = d.ConsultationFee,
                    ProfileImage = d.ProfileImage,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,

                    BranchId = d.BranchId,
                    BranchName = d.Branch.BranchName,

                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.DepartmentName,

                    HospitalId = d.Branch.HospitalId,
                    HospitalName = d.Branch.Hospital.HospitalName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DoctorResponseDto?> CreateAsync(
            DoctorCreateDto dto)
        {
            // Check Branch
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == dto.BranchId);

            if (branch == null)
            {
                return null;
            }

            // Check Department and make sure it belongs to the same Branch
            var department = await _context.Departments
                .FirstOrDefaultAsync(d =>
                    d.DepartmentId == dto.DepartmentId &&
                    d.BranchId == dto.BranchId);

            if (department == null)
            {
                return null;
            }

            // Registration number must be unique
            var registrationExists = await _context.Doctors
                .AnyAsync(d =>
                    d.RegistrationNumber == dto.RegistrationNumber);

            if (registrationExists)
            {
                return null;
            }

            var doctor = new Doctor
            {
                FullName = dto.FullName,
                RegistrationNumber = dto.RegistrationNumber,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Qualification = dto.Qualification,
                Specialization = dto.Specialization,
                ExperienceYears = dto.ExperienceYears,
                ConsultationFee = dto.ConsultationFee,
                ProfileImage = dto.ProfileImage,

                BranchId = dto.BranchId,
                DepartmentId = dto.DepartmentId,

                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(doctor.DoctorId);
        }

        public async Task<DoctorResponseDto?> UpdateAsync(
            int id,
            DoctorUpdateDto dto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
            {
                return null;
            }

            // Check Branch
            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // Check Department belongs to selected Branch
            var departmentExists = await _context.Departments
                .AnyAsync(d =>
                    d.DepartmentId == dto.DepartmentId &&
                    d.BranchId == dto.BranchId);

            if (!departmentExists)
            {
                return null;
            }

            // Registration number must remain unique
            var registrationExists = await _context.Doctors
                .AnyAsync(d =>
                    d.RegistrationNumber == dto.RegistrationNumber &&
                    d.DoctorId != id);

            if (registrationExists)
            {
                return null;
            }

            doctor.FullName = dto.FullName;
            doctor.RegistrationNumber = dto.RegistrationNumber;
            doctor.Email = dto.Email;
            doctor.MobileNumber = dto.MobileNumber;
            doctor.Gender = dto.Gender;
            doctor.DateOfBirth = dto.DateOfBirth;
            doctor.Qualification = dto.Qualification;
            doctor.Specialization = dto.Specialization;
            doctor.ExperienceYears = dto.ExperienceYears;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.ProfileImage = dto.ProfileImage;

            doctor.BranchId = dto.BranchId;
            doctor.DepartmentId = dto.DepartmentId;

            doctor.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(doctor.DoctorId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
            {
                return false;
            }

            _context.Doctors.Remove(doctor);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}