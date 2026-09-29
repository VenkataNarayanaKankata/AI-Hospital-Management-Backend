using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class DischargeService : IDischargeService
    {
        private readonly ApplicationDbContext _context;

        public DischargeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DischargeDto>> GetAllDischargesAsync()
        {
            return await _context.Discharges
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Bed)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Room)
                .Include(d => d.Doctor)
                .Select(d => new DischargeDto
                {
                    DischargeId = d.DischargeId,
                    DischargeNumber = d.DischargeNumber,
                    DischargeDate = d.DischargeDate,
                    DischargeType = d.DischargeType,
                    DischargeSummary = d.DischargeSummary,
                    FinalDiagnosis = d.FinalDiagnosis,
                    TreatmentSummary = d.TreatmentSummary,
                    DoctorInstructions = d.DoctorInstructions,
                    FollowUpInstructions = d.FollowUpInstructions,
                    ConditionAtDischarge = d.ConditionAtDischarge,

                    AdmissionId = d.AdmissionId,
                    AdmissionNumber = d.Admission.AdmissionNumber,

                    PatientId = d.Admission.PatientId,
                    PatientName = d.Admission.Patient.FullName,

                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.FullName,

                    BedId = d.Admission.BedId,
                    BedNumber = d.Admission.Bed.BedNumber,

                    RoomId = d.Admission.RoomId,
                    RoomNumber = d.Admission.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<DischargeDto?> GetDischargeByIdAsync(int id)
        {
            return await _context.Discharges
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Bed)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Room)
                .Include(d => d.Doctor)
                .Where(d => d.DischargeId == id)
                .Select(d => new DischargeDto
                {
                    DischargeId = d.DischargeId,
                    DischargeNumber = d.DischargeNumber,
                    DischargeDate = d.DischargeDate,
                    DischargeType = d.DischargeType,
                    DischargeSummary = d.DischargeSummary,
                    FinalDiagnosis = d.FinalDiagnosis,
                    TreatmentSummary = d.TreatmentSummary,
                    DoctorInstructions = d.DoctorInstructions,
                    FollowUpInstructions = d.FollowUpInstructions,
                    ConditionAtDischarge = d.ConditionAtDischarge,

                    AdmissionId = d.AdmissionId,
                    AdmissionNumber = d.Admission.AdmissionNumber,

                    PatientId = d.Admission.PatientId,
                    PatientName = d.Admission.Patient.FullName,

                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.FullName,

                    BedId = d.Admission.BedId,
                    BedNumber = d.Admission.Bed.BedNumber,

                    RoomId = d.Admission.RoomId,
                    RoomNumber = d.Admission.Room.RoomNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DischargeDto>> GetDischargesByPatientAsync(
            int patientId)
        {
            return await _context.Discharges
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Bed)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Room)
                .Include(d => d.Doctor)
                .Where(d => d.Admission.PatientId == patientId)
                .Select(d => new DischargeDto
                {
                    DischargeId = d.DischargeId,
                    DischargeNumber = d.DischargeNumber,
                    DischargeDate = d.DischargeDate,
                    DischargeType = d.DischargeType,
                    DischargeSummary = d.DischargeSummary,
                    FinalDiagnosis = d.FinalDiagnosis,
                    TreatmentSummary = d.TreatmentSummary,
                    DoctorInstructions = d.DoctorInstructions,
                    FollowUpInstructions = d.FollowUpInstructions,
                    ConditionAtDischarge = d.ConditionAtDischarge,

                    AdmissionId = d.AdmissionId,
                    AdmissionNumber = d.Admission.AdmissionNumber,

                    PatientId = d.Admission.PatientId,
                    PatientName = d.Admission.Patient.FullName,

                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.FullName,

                    BedId = d.Admission.BedId,
                    BedNumber = d.Admission.Bed.BedNumber,

                    RoomId = d.Admission.RoomId,
                    RoomNumber = d.Admission.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DischargeDto>> GetDischargesByDoctorAsync(
            int doctorId)
        {
            return await _context.Discharges
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Bed)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Room)
                .Include(d => d.Doctor)
                .Where(d => d.DoctorId == doctorId)
                .Select(d => new DischargeDto
                {
                    DischargeId = d.DischargeId,
                    DischargeNumber = d.DischargeNumber,
                    DischargeDate = d.DischargeDate,
                    DischargeType = d.DischargeType,
                    DischargeSummary = d.DischargeSummary,
                    FinalDiagnosis = d.FinalDiagnosis,
                    TreatmentSummary = d.TreatmentSummary,
                    DoctorInstructions = d.DoctorInstructions,
                    FollowUpInstructions = d.FollowUpInstructions,
                    ConditionAtDischarge = d.ConditionAtDischarge,

                    AdmissionId = d.AdmissionId,
                    AdmissionNumber = d.Admission.AdmissionNumber,

                    PatientId = d.Admission.PatientId,
                    PatientName = d.Admission.Patient.FullName,

                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.FullName,

                    BedId = d.Admission.BedId,
                    BedNumber = d.Admission.Bed.BedNumber,

                    RoomId = d.Admission.RoomId,
                    RoomNumber = d.Admission.Room.RoomNumber
                })
                .ToListAsync();
        }

        public async Task<DischargeDto> CreateDischargeAsync(
            CreateDischargeDto dto)
        {
            var admission = await _context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Bed)
                .Include(a => a.Room)
                .FirstOrDefaultAsync(a =>
                    a.AdmissionId == dto.AdmissionId);

            if (admission == null)
                throw new ArgumentException("Admission not found.");

            if (admission.Status != "Admitted")
                throw new InvalidOperationException(
                    "Only an active admission can be discharged.");

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            var dischargeExists = await _context.Discharges
                .AnyAsync(d =>
                    d.AdmissionId == dto.AdmissionId);

            if (dischargeExists)
                throw new InvalidOperationException(
                    "This admission already has a discharge record.");

            var dischargeNumberExists = await _context.Discharges
                .AnyAsync(d =>
                    d.DischargeNumber == dto.DischargeNumber);

            if (dischargeNumberExists)
                throw new ArgumentException(
                    "Discharge number already exists.");

            var validDischargeTypes = new[]
            {
                "Normal",
                "LAMA",
                "Transfer",
                "Death"
            };

            if (!validDischargeTypes.Contains(dto.DischargeType))
                throw new ArgumentException(
                    "Invalid discharge type.");

            var discharge = new Discharge
            {
                DischargeNumber = dto.DischargeNumber,
                DischargeDate = dto.DischargeDate,
                DischargeType = dto.DischargeType,
                DischargeSummary = dto.DischargeSummary,
                FinalDiagnosis = dto.FinalDiagnosis,
                TreatmentSummary = dto.TreatmentSummary,
                DoctorInstructions = dto.DoctorInstructions,
                FollowUpInstructions = dto.FollowUpInstructions,
                ConditionAtDischarge = dto.ConditionAtDischarge,
                AdmissionId = dto.AdmissionId,
                DoctorId = dto.DoctorId
            };

            admission.Status = "Discharged";
            admission.ActualDischargeDate = dto.DischargeDate;

            if (admission.Bed.Status == "Occupied")
                admission.Bed.Status = "Available";

            _context.Discharges.Add(discharge);

            await _context.SaveChangesAsync();

            return new DischargeDto
            {
                DischargeId = discharge.DischargeId,
                DischargeNumber = discharge.DischargeNumber,
                DischargeDate = discharge.DischargeDate,
                DischargeType = discharge.DischargeType,
                DischargeSummary = discharge.DischargeSummary,
                FinalDiagnosis = discharge.FinalDiagnosis,
                TreatmentSummary = discharge.TreatmentSummary,
                DoctorInstructions = discharge.DoctorInstructions,
                FollowUpInstructions = discharge.FollowUpInstructions,
                ConditionAtDischarge = discharge.ConditionAtDischarge,

                AdmissionId = admission.AdmissionId,
                AdmissionNumber = admission.AdmissionNumber,

                PatientId = admission.PatientId,
                PatientName = admission.Patient.FullName,

                DoctorId = doctor.DoctorId,
                DoctorName = doctor.FullName,

                BedId = admission.BedId,
                BedNumber = admission.Bed.BedNumber,

                RoomId = admission.RoomId,
                RoomNumber = admission.Room.RoomNumber
            };
        }

        public async Task<DischargeDto?> UpdateDischargeAsync(
            int id,
            UpdateDischargeDto dto)
        {
            var discharge = await _context.Discharges
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Bed)
                .Include(d => d.Admission)
                    .ThenInclude(a => a.Room)
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(d =>
                    d.DischargeId == id);

            if (discharge == null)
                return null;

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d =>
                    d.DoctorId == dto.DoctorId);

            if (doctor == null)
                throw new ArgumentException("Doctor not found.");

            var validDischargeTypes = new[]
            {
                "Normal",
                "LAMA",
                "Transfer",
                "Death"
            };

            if (!validDischargeTypes.Contains(dto.DischargeType))
                throw new ArgumentException(
                    "Invalid discharge type.");

            discharge.DischargeDate = dto.DischargeDate;
            discharge.DischargeType = dto.DischargeType;
            discharge.DischargeSummary = dto.DischargeSummary;
            discharge.FinalDiagnosis = dto.FinalDiagnosis;
            discharge.TreatmentSummary = dto.TreatmentSummary;
            discharge.DoctorInstructions = dto.DoctorInstructions;
            discharge.FollowUpInstructions = dto.FollowUpInstructions;
            discharge.ConditionAtDischarge = dto.ConditionAtDischarge;
            discharge.DoctorId = dto.DoctorId;

            discharge.Admission.Status = "Discharged";
            discharge.Admission.ActualDischargeDate =
                dto.DischargeDate;

            if (discharge.Admission.Bed.Status == "Occupied")
                discharge.Admission.Bed.Status = "Available";

            await _context.SaveChangesAsync();

            return new DischargeDto
            {
                DischargeId = discharge.DischargeId,
                DischargeNumber = discharge.DischargeNumber,
                DischargeDate = discharge.DischargeDate,
                DischargeType = discharge.DischargeType,
                DischargeSummary = discharge.DischargeSummary,
                FinalDiagnosis = discharge.FinalDiagnosis,
                TreatmentSummary = discharge.TreatmentSummary,
                DoctorInstructions = discharge.DoctorInstructions,
                FollowUpInstructions = discharge.FollowUpInstructions,
                ConditionAtDischarge = discharge.ConditionAtDischarge,

                AdmissionId = discharge.AdmissionId,
                AdmissionNumber =
                    discharge.Admission.AdmissionNumber,

                PatientId = discharge.Admission.PatientId,
                PatientName =
                    discharge.Admission.Patient.FullName,

                DoctorId = doctor.DoctorId,
                DoctorName = doctor.FullName,

                BedId = discharge.Admission.BedId,
                BedNumber =
                    discharge.Admission.Bed.BedNumber,

                RoomId = discharge.Admission.RoomId,
                RoomNumber =
                    discharge.Admission.Room.RoomNumber
            };
        }

        public async Task<bool> DeleteDischargeAsync(int id)
        {
            var discharge = await _context.Discharges
                .FirstOrDefaultAsync(d =>
                    d.DischargeId == id);

            if (discharge == null)
                return false;

            throw new InvalidOperationException(
                "Discharge records are part of the patient's medical history and cannot be deleted.");
        }
    }
}