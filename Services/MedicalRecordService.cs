using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalRecordResponseDto>> GetAllAsync()
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<MedicalRecordResponseDto>> GetByPatientIdAsync(
            int patientId)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.PatientId == patientId)
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<MedicalRecordResponseDto>> GetByDoctorIdAsync(
            int doctorId)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.DoctorId == doctorId)
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<MedicalRecordResponseDto>> GetByAppointmentIdAsync(
            int appointmentId)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.AppointmentId == appointmentId)
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<List<MedicalRecordResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.BranchId == branchId)
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<MedicalRecordResponseDto?> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.MedicalRecordId == id)
                .Include(m => m.Branch)
                    .ThenInclude(b => b.Hospital)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(m => m.Appointment)
                .Select(m => new MedicalRecordResponseDto
                {
                    MedicalRecordId = m.MedicalRecordId,
                    RecordNumber = m.RecordNumber,
                    VisitDate = m.VisitDate,

                    ChiefComplaint = m.ChiefComplaint,
                    Symptoms = m.Symptoms,
                    Diagnosis = m.Diagnosis,
                    Treatment = m.Treatment,
                    ClinicalNotes = m.ClinicalNotes,
                    FollowUpDate = m.FollowUpDate,
                    CreatedAt = m.CreatedAt,

                    AppointmentId = m.AppointmentId,
                    AppointmentNumber = m.Appointment.AppointmentNumber,

                    PatientId = m.PatientId,
                    PatientNumber = m.Patient.PatientNumber,
                    PatientName = m.Patient.FullName,

                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor.FullName,

                    BranchId = m.BranchId,
                    BranchName = m.Branch.BranchName,

                    HospitalId = m.Branch.HospitalId,
                    HospitalName = m.Branch.Hospital.HospitalName,

                    DepartmentId = m.Doctor.DepartmentId,
                    DepartmentName = m.Doctor.Department.DepartmentName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MedicalRecordResponseDto?> CreateAsync(
            MedicalRecordCreateDto dto)
        {
            // ========================================================
            // 1. Validate Appointment
            // ========================================================

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a =>
                    a.AppointmentId == dto.AppointmentId);

            if (appointment == null)
            {
                return null;
            }

            // ========================================================
            // 2. Validate Patient
            // ========================================================

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p =>
                    p.PatientId == dto.PatientId);

            if (patient == null)
            {
                return null;
            }

            // Patient must belong to the same branch.
            if (patient.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 3. Validate Doctor
            // ========================================================

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId);

            if (doctor == null)
            {
                return null;
            }

            // Doctor must belong to the same branch.
            if (doctor.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 4. Validate Branch
            // ========================================================

            var branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // ========================================================
            // 5. STRICT APPOINTMENT RELATIONSHIP
            // ========================================================

            // Appointment must belong to the same patient.
            if (appointment.PatientId != dto.PatientId)
            {
                return null;
            }

            // Appointment must belong to the same doctor.
            if (appointment.DoctorId != dto.DoctorId)
            {
                return null;
            }

            // Appointment must belong to the same branch.
            if (appointment.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 6. Doctor must belong to appointment department
            // ========================================================

            if (appointment.DepartmentId != doctor.DepartmentId)
            {
                return null;
            }

            // ========================================================
            // 7. Prevent duplicate medical record
            // ========================================================

            var recordExists = await _context.MedicalRecords
                .AnyAsync(m =>
                    m.AppointmentId == dto.AppointmentId);

            if (recordExists)
            {
                return null;
            }

            // ========================================================
            // 8. Visit date must match appointment date
            // ========================================================

            if (dto.VisitDate.Date != appointment.AppointmentDate.Date)
            {
                return null;
            }

            // ========================================================
            // 9. Follow-up date cannot be before visit date
            // ========================================================

            if (dto.FollowUpDate.HasValue &&
                dto.FollowUpDate.Value.Date < dto.VisitDate.Date)
            {
                return null;
            }

            // ========================================================
            // 10. Generate Medical Record Number
            // ========================================================

            var lastRecordId = await _context.MedicalRecords
                .OrderByDescending(m => m.MedicalRecordId)
                .Select(m => (int?)m.MedicalRecordId)
                .FirstOrDefaultAsync();

            var nextRecordId = (lastRecordId ?? 0) + 1;

            var recordNumber =
                $"MR-{nextRecordId:D6}";

            // ========================================================
            // 11. Create Medical Record
            // ========================================================

            var medicalRecord = new MedicalRecord
            {
                RecordNumber = recordNumber,

                VisitDate = dto.VisitDate,

                ChiefComplaint = dto.ChiefComplaint,
                Symptoms = dto.Symptoms,
                Diagnosis = dto.Diagnosis,
                Treatment = dto.Treatment,
                ClinicalNotes = dto.ClinicalNotes,

                FollowUpDate = dto.FollowUpDate,

                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId,

                CreatedAt = DateTime.UtcNow
            };

            _context.MedicalRecords.Add(medicalRecord);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(
                medicalRecord.MedicalRecordId);
        }

        public async Task<MedicalRecordResponseDto?> UpdateAsync(
            int id,
            MedicalRecordUpdateDto dto)
        {
            // ========================================================
            // 1. Find existing medical record
            // ========================================================

            var medicalRecord = await _context.MedicalRecords
                .FirstOrDefaultAsync(m =>
                    m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return null;
            }

            // ========================================================
            // 2. Validate Appointment
            // ========================================================

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a =>
                    a.AppointmentId == dto.AppointmentId);

            if (appointment == null)
            {
                return null;
            }

            // ========================================================
            // 3. Validate Patient
            // ========================================================

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p =>
                    p.PatientId == dto.PatientId);

            if (patient == null)
            {
                return null;
            }

            if (patient.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 4. Validate Doctor
            // ========================================================

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId);

            if (doctor == null)
            {
                return null;
            }

            if (doctor.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 5. Validate Branch
            // ========================================================

            var branchExists = await _context.Branches
                .AnyAsync(b =>
                    b.BranchId == dto.BranchId);

            if (!branchExists)
            {
                return null;
            }

            // ========================================================
            // 6. STRICT APPOINTMENT RELATIONSHIP
            // ========================================================

            if (appointment.PatientId != dto.PatientId)
            {
                return null;
            }

            if (appointment.DoctorId != dto.DoctorId)
            {
                return null;
            }

            if (appointment.BranchId != dto.BranchId)
            {
                return null;
            }

            // ========================================================
            // 7. Doctor / Department relationship
            // ========================================================

            if (appointment.DepartmentId != doctor.DepartmentId)
            {
                return null;
            }

            // ========================================================
            // 8. Prevent using another appointment's record
            // ========================================================

            var duplicateAppointmentRecord =
                await _context.MedicalRecords
                    .AnyAsync(m =>
                        m.AppointmentId == dto.AppointmentId &&
                        m.MedicalRecordId != id);

            if (duplicateAppointmentRecord)
            {
                return null;
            }

            // ========================================================
            // 9. Visit date must match appointment date
            // ========================================================

            if (dto.VisitDate.Date != appointment.AppointmentDate.Date)
            {
                return null;
            }

            // ========================================================
            // 10. Follow-up date validation
            // ========================================================

            if (dto.FollowUpDate.HasValue &&
                dto.FollowUpDate.Value.Date < dto.VisitDate.Date)
            {
                return null;
            }

            // ========================================================
            // 11. Update medical record
            // ========================================================

            medicalRecord.VisitDate = dto.VisitDate;

            medicalRecord.ChiefComplaint =
                dto.ChiefComplaint;

            medicalRecord.Symptoms =
                dto.Symptoms;

            medicalRecord.Diagnosis =
                dto.Diagnosis;

            medicalRecord.Treatment =
                dto.Treatment;

            medicalRecord.ClinicalNotes =
                dto.ClinicalNotes;

            medicalRecord.FollowUpDate =
                dto.FollowUpDate;

            medicalRecord.BranchId =
                dto.BranchId;

            medicalRecord.PatientId =
                dto.PatientId;

            medicalRecord.DoctorId =
                dto.DoctorId;

            medicalRecord.AppointmentId =
                dto.AppointmentId;

            // RecordNumber and CreatedAt remain unchanged.

            await _context.SaveChangesAsync();

            return await GetByIdAsync(
                medicalRecord.MedicalRecordId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicalRecord = await _context.MedicalRecords
                .FirstOrDefaultAsync(m =>
                    m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return false;
            }

            _context.MedicalRecords.Remove(medicalRecord);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}