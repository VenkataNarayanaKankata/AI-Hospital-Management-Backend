using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly ApplicationDbContext _context;

        public AdmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdmissionDto>> GetAllAdmissionsAsync()
        {
            return await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .Select(a => new AdmissionDto
                {
                    AdmissionId = a.AdmissionId,
                    AdmissionNumber = a.AdmissionNumber,
                    AdmissionDate = a.AdmissionDate,
                    ExpectedDischargeDate = a.ExpectedDischargeDate,
                    ActualDischargeDate = a.ActualDischargeDate,
                    AdmissionType = a.AdmissionType,
                    Status = a.Status,
                    ReasonForAdmission = a.ReasonForAdmission,
                    Notes = a.Notes,
                    AdvanceAmount = a.AdvanceAmount,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    PatientId = a.PatientId,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    AppointmentId = a.AppointmentId,

                    WardId = a.WardId,
                    WardName = a.Ward.WardName,

                    RoomId = a.RoomId,
                    RoomNumber = a.Room.RoomNumber,

                    BedId = a.BedId,
                    BedNumber = a.Bed.BedNumber
                })
                .ToListAsync();
        }

        public async Task<AdmissionDto?> GetAdmissionByIdAsync(int id)
        {
            return await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .Where(a => a.AdmissionId == id)
                .Select(a => new AdmissionDto
                {
                    AdmissionId = a.AdmissionId,
                    AdmissionNumber = a.AdmissionNumber,
                    AdmissionDate = a.AdmissionDate,
                    ExpectedDischargeDate = a.ExpectedDischargeDate,
                    ActualDischargeDate = a.ActualDischargeDate,
                    AdmissionType = a.AdmissionType,
                    Status = a.Status,
                    ReasonForAdmission = a.ReasonForAdmission,
                    Notes = a.Notes,
                    AdvanceAmount = a.AdvanceAmount,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    PatientId = a.PatientId,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    AppointmentId = a.AppointmentId,

                    WardId = a.WardId,
                    WardName = a.Ward.WardName,

                    RoomId = a.RoomId,
                    RoomNumber = a.Room.RoomNumber,

                    BedId = a.BedId,
                    BedNumber = a.Bed.BedNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AdmissionDto>> GetAdmissionsByPatientAsync(int patientId)
        {
            return await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .Where(a => a.PatientId == patientId)
                .Select(a => new AdmissionDto
                {
                    AdmissionId = a.AdmissionId,
                    AdmissionNumber = a.AdmissionNumber,
                    AdmissionDate = a.AdmissionDate,
                    ExpectedDischargeDate = a.ExpectedDischargeDate,
                    ActualDischargeDate = a.ActualDischargeDate,
                    AdmissionType = a.AdmissionType,
                    Status = a.Status,
                    ReasonForAdmission = a.ReasonForAdmission,
                    Notes = a.Notes,
                    AdvanceAmount = a.AdvanceAmount,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    PatientId = a.PatientId,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    AppointmentId = a.AppointmentId,

                    WardId = a.WardId,
                    WardName = a.Ward.WardName,

                    RoomId = a.RoomId,
                    RoomNumber = a.Room.RoomNumber,

                    BedId = a.BedId,
                    BedNumber = a.Bed.BedNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AdmissionDto>> GetAdmissionsByBranchAsync(int branchId)
        {
            return await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .Where(a => a.BranchId == branchId)
                .Select(a => new AdmissionDto
                {
                    AdmissionId = a.AdmissionId,
                    AdmissionNumber = a.AdmissionNumber,
                    AdmissionDate = a.AdmissionDate,
                    ExpectedDischargeDate = a.ExpectedDischargeDate,
                    ActualDischargeDate = a.ActualDischargeDate,
                    AdmissionType = a.AdmissionType,
                    Status = a.Status,
                    ReasonForAdmission = a.ReasonForAdmission,
                    Notes = a.Notes,
                    AdvanceAmount = a.AdvanceAmount,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    PatientId = a.PatientId,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    AppointmentId = a.AppointmentId,

                    WardId = a.WardId,
                    WardName = a.Ward.WardName,

                    RoomId = a.RoomId,
                    RoomNumber = a.Room.RoomNumber,

                    BedId = a.BedId,
                    BedNumber = a.Bed.BedNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AdmissionDto>> GetAdmissionsByStatusAsync(string status)
        {
            return await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .Where(a => a.Status == status)
                .Select(a => new AdmissionDto
                {
                    AdmissionId = a.AdmissionId,
                    AdmissionNumber = a.AdmissionNumber,
                    AdmissionDate = a.AdmissionDate,
                    ExpectedDischargeDate = a.ExpectedDischargeDate,
                    ActualDischargeDate = a.ActualDischargeDate,
                    AdmissionType = a.AdmissionType,
                    Status = a.Status,
                    ReasonForAdmission = a.ReasonForAdmission,
                    Notes = a.Notes,
                    AdvanceAmount = a.AdvanceAmount,

                    BranchId = a.BranchId,
                    BranchName = a.Branch.BranchName,

                    PatientId = a.PatientId,
                    PatientName = a.Patient.FullName,

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FullName,

                    AppointmentId = a.AppointmentId,

                    WardId = a.WardId,
                    WardName = a.Ward.WardName,

                    RoomId = a.RoomId,
                    RoomNumber = a.Room.RoomNumber,

                    BedId = a.BedId,
                    BedNumber = a.Bed.BedNumber
                })
                .ToListAsync();
        }

        public async Task<AdmissionDto> CreateAdmissionAsync(CreateAdmissionDto dto)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchId == dto.BranchId);

            if (branch == null)
                throw new ArgumentException("Branch not found.");

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == dto.PatientId);

            if (patient == null)
                throw new ArgumentException("Patient not found.");

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            var ward = await _context.Wards
                .FirstOrDefaultAsync(w =>
                    w.WardId == dto.WardId &&
                    w.BranchId == dto.BranchId);

            if (ward == null)
                throw new ArgumentException(
                    "Ward does not belong to the selected branch.");

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r =>
                    r.RoomId == dto.RoomId &&
                    r.WardId == dto.WardId);

            if (room == null)
                throw new ArgumentException(
                    "Room does not belong to the selected ward.");

            var bed = await _context.Beds
                .FirstOrDefaultAsync(b =>
                    b.BedId == dto.BedId &&
                    b.RoomId == dto.RoomId);

            if (bed == null)
                throw new ArgumentException(
                    "Bed does not belong to the selected room.");

            if (bed.Status != "Available")
                throw new InvalidOperationException(
                    "Selected bed is not available.");

            if (!bed.IsActive)
                throw new InvalidOperationException(
                    "Selected bed is inactive.");

            if (string.IsNullOrWhiteSpace(dto.AdmissionNumber))
                throw new ArgumentException(
                    "Admission number is required.");

            var admissionExists = await _context.Admissions
                .AnyAsync(a =>
                    a.AdmissionNumber == dto.AdmissionNumber);

            if (admissionExists)
                throw new ArgumentException(
                    "Admission number already exists.");

            if (dto.AppointmentId.HasValue)
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(a =>
                        a.AppointmentId == dto.AppointmentId.Value);

                if (appointment == null)
                    throw new ArgumentException(
                        "Appointment not found.");

                if (appointment.PatientId != dto.PatientId)
                    throw new ArgumentException(
                        "Appointment does not belong to the selected patient.");

                if (appointment.DoctorId != dto.DoctorId)
                    throw new ArgumentException(
                        "Appointment does not belong to the selected doctor.");

                if (appointment.BranchId != dto.BranchId)
                    throw new ArgumentException(
                        "Appointment does not belong to the selected branch.");
            }

            var activeAdmissionExists = await _context.Admissions
                .AnyAsync(a =>
                    a.PatientId == dto.PatientId &&
                    a.Status == "Admitted");

            if (activeAdmissionExists)
                throw new InvalidOperationException(
                    "Patient already has an active admission.");

            var admission = new Admission
            {
                AdmissionNumber = dto.AdmissionNumber,
                AdmissionDate = dto.AdmissionDate,
                ExpectedDischargeDate = dto.ExpectedDischargeDate,
                AdmissionType = dto.AdmissionType,
                Status = "Admitted",
                ReasonForAdmission = dto.ReasonForAdmission,
                Notes = dto.Notes,
                AdvanceAmount = dto.AdvanceAmount,
                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId,
                WardId = dto.WardId,
                RoomId = dto.RoomId,
                BedId = dto.BedId
            };

            bed.Status = "Occupied";

            _context.Admissions.Add(admission);

            await _context.SaveChangesAsync();

            return new AdmissionDto
            {
                AdmissionId = admission.AdmissionId,
                AdmissionNumber = admission.AdmissionNumber,
                AdmissionDate = admission.AdmissionDate,
                ExpectedDischargeDate = admission.ExpectedDischargeDate,
                ActualDischargeDate = admission.ActualDischargeDate,
                AdmissionType = admission.AdmissionType,
                Status = admission.Status,
                ReasonForAdmission = admission.ReasonForAdmission,
                Notes = admission.Notes,
                AdvanceAmount = admission.AdvanceAmount,

                BranchId = branch.BranchId,
                BranchName = branch.BranchName,

                PatientId = patient.PatientId,
                PatientName = patient.FullName,

                DoctorId = doctor.DoctorId,
                DoctorName = doctor.FullName,

                AppointmentId = admission.AppointmentId,

                WardId = ward.WardId,
                WardName = ward.WardName,

                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,

                BedId = bed.BedId,
                BedNumber = bed.BedNumber
            };
        }

        public async Task<AdmissionDto?> UpdateAdmissionAsync(
            int id,
            UpdateAdmissionDto dto)
        {
            var admission = await _context.Admissions
                .Include(a => a.Branch)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Room)
                .Include(a => a.Bed)
                .FirstOrDefaultAsync(a => a.AdmissionId == id);

            if (admission == null)
                return null;

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            if (dto.AppointmentId.HasValue)
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(a =>
                        a.AppointmentId == dto.AppointmentId.Value);

                if (appointment == null)
                    throw new ArgumentException(
                        "Appointment not found.");

                if (appointment.PatientId != admission.PatientId)
                    throw new ArgumentException(
                        "Appointment does not belong to the patient.");

                if (appointment.DoctorId != dto.DoctorId)
                    throw new ArgumentException(
                        "Appointment does not belong to the selected doctor.");
            }

            var validStatuses = new[]
            {
                "Admitted",
                "Discharged",
                "Cancelled",
                "Transferred"
            };

            if (!validStatuses.Contains(dto.Status))
                throw new ArgumentException(
                    "Invalid admission status.");

            if (dto.Status == "Discharged" &&
                admission.Status != "Discharged")
            {
                admission.ActualDischargeDate =
                    DateTime.UtcNow;

                if (admission.Bed.Status == "Occupied")
                    admission.Bed.Status = "Available";
            }

            if (dto.Status == "Cancelled" &&
                admission.Status == "Admitted")
            {
                if (admission.Bed.Status == "Occupied")
                    admission.Bed.Status = "Available";
            }

            admission.ExpectedDischargeDate =
                dto.ExpectedDischargeDate;

            admission.AdmissionType =
                dto.AdmissionType;

            admission.Status =
                dto.Status;

            admission.ReasonForAdmission =
                dto.ReasonForAdmission;

            admission.Notes =
                dto.Notes;

            admission.AdvanceAmount =
                dto.AdvanceAmount;

            admission.DoctorId =
                dto.DoctorId;

            admission.AppointmentId =
                dto.AppointmentId;

            await _context.SaveChangesAsync();

            return new AdmissionDto
            {
                AdmissionId = admission.AdmissionId,
                AdmissionNumber = admission.AdmissionNumber,
                AdmissionDate = admission.AdmissionDate,
                ExpectedDischargeDate = admission.ExpectedDischargeDate,
                ActualDischargeDate = admission.ActualDischargeDate,
                AdmissionType = admission.AdmissionType,
                Status = admission.Status,
                ReasonForAdmission = admission.ReasonForAdmission,
                Notes = admission.Notes,
                AdvanceAmount = admission.AdvanceAmount,

                BranchId = admission.BranchId,
                BranchName = admission.Branch.BranchName,

                PatientId = admission.PatientId,
                PatientName = admission.Patient.FullName,

                DoctorId = admission.DoctorId,
                DoctorName = doctor.FullName,

                AppointmentId = admission.AppointmentId,

                WardId = admission.WardId,
                WardName = admission.Ward.WardName,

                RoomId = admission.RoomId,
                RoomNumber = admission.Room.RoomNumber,

                BedId = admission.BedId,
                BedNumber = admission.Bed.BedNumber
            };
        }

        public async Task<bool> DeleteAdmissionAsync(int id)
        {
            var admission = await _context.Admissions
                .Include(a => a.Bed)
                .FirstOrDefaultAsync(a => a.AdmissionId == id);

            if (admission == null)
                return false;

            if (admission.Status == "Admitted")
            {
                throw new InvalidOperationException(
                    "Active admission cannot be deleted. Discharge or cancel the admission first.");
            }

            if (admission.Bed.Status == "Occupied")
                admission.Bed.Status = "Available";

            _context.Admissions.Remove(admission);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}