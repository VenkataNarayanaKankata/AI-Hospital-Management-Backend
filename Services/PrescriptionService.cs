using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PrescriptionResponseDto>> GetAllAsync()
        {
            return await BuildQuery()
                .AsNoTracking()
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<List<PrescriptionResponseDto>> GetByPatientIdAsync(
            int patientId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<List<PrescriptionResponseDto>> GetByDoctorIdAsync(
            int doctorId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(p => p.DoctorId == doctorId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<List<PrescriptionResponseDto>> GetByAppointmentIdAsync(
            int appointmentId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(p => p.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task<List<PrescriptionResponseDto>> GetByMedicalRecordIdAsync(
            int medicalRecordId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(p => p.MedicalRecordId == medicalRecordId)
                .ToListAsync();
        }

        public async Task<List<PrescriptionResponseDto>> GetByBranchIdAsync(
            int branchId)
        {
            return await BuildQuery()
                .AsNoTracking()
                .Where(p => p.BranchId == branchId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<PrescriptionResponseDto?> GetByIdAsync(int id)
        {
            return await BuildQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<PrescriptionResponseDto?> CreateAsync(
            PrescriptionCreateDto dto)
        {
            var validationResult =
                await ValidatePrescriptionRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId,
                    dto.MedicalRecordId);

            if (!validationResult)
            {
                return null;
            }

            var medicineIds = dto.Items
                .Select(i => i.MedicineId)
                .ToList();

            if (medicineIds.Count != medicineIds.Distinct().Count())
            {
                return null;
            }

            var existingMedicineIds = await _context.Medicines
                .Where(m =>
                    medicineIds.Contains(m.MedicineId) &&
                    m.IsActive)
                .Select(m => m.MedicineId)
                .ToListAsync();

            if (existingMedicineIds.Count != medicineIds.Count)
            {
                return null;
            }

            var medicalRecord =
                await _context.MedicalRecords
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m =>
                        m.MedicalRecordId == dto.MedicalRecordId);

            if (medicalRecord == null)
            {
                return null;
            }

            if (dto.PrescriptionDate.Date !=
                medicalRecord.VisitDate.Date)
            {
                return null;
            }

            var prescriptionExists =
                await _context.Prescriptions
                    .AnyAsync(p =>
                        p.AppointmentId == dto.AppointmentId ||
                        p.MedicalRecordId == dto.MedicalRecordId);

            if (prescriptionExists)
            {
                return null;
            }

            var lastPrescriptionId =
                await _context.Prescriptions
                    .OrderByDescending(p => p.PrescriptionId)
                    .Select(p => (int?)p.PrescriptionId)
                    .FirstOrDefaultAsync();

            var nextPrescriptionId =
                (lastPrescriptionId ?? 0) + 1;

            var prescriptionNumber =
                $"RX-{nextPrescriptionId:D6}";

            var prescription = new Prescription
            {
                PrescriptionNumber = prescriptionNumber,
                PrescriptionDate = dto.PrescriptionDate,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,

                BranchId = dto.BranchId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId,
                MedicalRecordId = dto.MedicalRecordId
            };

            foreach (var item in dto.Items)
            {
                prescription.PrescriptionItems.Add(
                    new PrescriptionItem
                    {
                        MedicineId = item.MedicineId,
                        Dosage = item.Dosage,
                        Frequency = item.Frequency,
                        DurationDays = item.DurationDays,
                        Route = item.Route,
                        Instructions = item.Instructions
                    });
            }

            _context.Prescriptions.Add(prescription);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(
                prescription.PrescriptionId);
        }

        public async Task<PrescriptionResponseDto?> UpdateAsync(
            int id,
            PrescriptionUpdateDto dto)
        {
            var prescription =
                await _context.Prescriptions
                    .Include(p => p.PrescriptionItems)
                    .FirstOrDefaultAsync(p =>
                        p.PrescriptionId == id);

            if (prescription == null)
            {
                return null;
            }

            var validationResult =
                await ValidatePrescriptionRelationshipsAsync(
                    dto.BranchId,
                    dto.PatientId,
                    dto.DoctorId,
                    dto.AppointmentId,
                    dto.MedicalRecordId);

            if (!validationResult)
            {
                return null;
            }

            var medicineIds = dto.Items
                .Select(i => i.MedicineId)
                .ToList();

            if (medicineIds.Count != medicineIds.Distinct().Count())
            {
                return null;
            }

            var existingMedicineIds = await _context.Medicines
                .Where(m =>
                    medicineIds.Contains(m.MedicineId) &&
                    m.IsActive)
                .Select(m => m.MedicineId)
                .ToListAsync();

            if (existingMedicineIds.Count != medicineIds.Count)
            {
                return null;
            }

            var medicalRecord =
                await _context.MedicalRecords
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m =>
                        m.MedicalRecordId == dto.MedicalRecordId);

            if (medicalRecord == null)
            {
                return null;
            }

            if (dto.PrescriptionDate.Date !=
                medicalRecord.VisitDate.Date)
            {
                return null;
            }

            var duplicatePrescription =
                await _context.Prescriptions
                    .AnyAsync(p =>
                        p.PrescriptionId != id &&
                        (p.AppointmentId == dto.AppointmentId ||
                         p.MedicalRecordId == dto.MedicalRecordId));

            if (duplicatePrescription)
            {
                return null;
            }

            prescription.PrescriptionDate =
                dto.PrescriptionDate;

            prescription.Notes =
                dto.Notes;

            prescription.BranchId =
                dto.BranchId;

            prescription.PatientId =
                dto.PatientId;

            prescription.DoctorId =
                dto.DoctorId;

            prescription.AppointmentId =
                dto.AppointmentId;

            prescription.MedicalRecordId =
                dto.MedicalRecordId;

            _context.PrescriptionItems.RemoveRange(
                prescription.PrescriptionItems);

            prescription.PrescriptionItems.Clear();

            foreach (var item in dto.Items)
            {
                prescription.PrescriptionItems.Add(
                    new PrescriptionItem
                    {
                        PrescriptionId =
                            prescription.PrescriptionId,

                        MedicineId =
                            item.MedicineId,

                        Dosage =
                            item.Dosage,

                        Frequency =
                            item.Frequency,

                        DurationDays =
                            item.DurationDays,

                        Route =
                            item.Route,

                        Instructions =
                            item.Instructions
                    });
            }

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescription =
                await _context.Prescriptions
                    .FirstOrDefaultAsync(p =>
                        p.PrescriptionId == id);

            if (prescription == null)
            {
                return false;
            }

            _context.Prescriptions.Remove(prescription);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> ValidatePrescriptionRelationshipsAsync(
            int branchId,
            int patientId,
            int doctorId,
            int appointmentId,
            int medicalRecordId)
        {
            var branchExists =
                await _context.Branches
                    .AnyAsync(b =>
                        b.BranchId == branchId);

            if (!branchExists)
            {
                return false;
            }

            var patient =
                await _context.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                        p.PatientId == patientId);

            if (patient == null ||
                patient.BranchId != branchId)
            {
                return false;
            }

            var doctor =
                await _context.Doctors
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d =>
                        d.DoctorId == doctorId);

            if (doctor == null ||
                doctor.BranchId != branchId)
            {
                return false;
            }

            var appointment =
                await _context.Appointments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a =>
                        a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return false;
            }

            if (appointment.BranchId != branchId ||
                appointment.PatientId != patientId ||
                appointment.DoctorId != doctorId)
            {
                return false;
            }

            if (appointment.DepartmentId !=
                doctor.DepartmentId)
            {
                return false;
            }

            var medicalRecord =
                await _context.MedicalRecords
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m =>
                        m.MedicalRecordId == medicalRecordId);

            if (medicalRecord == null)
            {
                return false;
            }

            if (medicalRecord.BranchId != branchId ||
                medicalRecord.PatientId != patientId ||
                medicalRecord.DoctorId != doctorId ||
                medicalRecord.AppointmentId != appointmentId)
            {
                return false;
            }

            return true;
        }

        private IQueryable<PrescriptionResponseDto> BuildQuery()
        {
            return _context.Prescriptions
                .Select(p => new PrescriptionResponseDto
                {
                    PrescriptionId =
                        p.PrescriptionId,

                    PrescriptionNumber =
                        p.PrescriptionNumber,

                    PrescriptionDate =
                        p.PrescriptionDate,

                    Notes =
                        p.Notes,

                    CreatedAt =
                        p.CreatedAt,

                    BranchId =
                        p.BranchId,

                    BranchName =
                        p.Branch.BranchName,

                    HospitalId =
                        p.Branch.HospitalId,

                    HospitalName =
                        p.Branch.Hospital.HospitalName,

                    PatientId =
                        p.PatientId,

                    PatientNumber =
                        p.Patient.PatientNumber,

                    PatientName =
                        p.Patient.FullName,

                    DoctorId =
                        p.DoctorId,

                    DoctorName =
                        p.Doctor.FullName,

                    DepartmentId =
                        p.Doctor.DepartmentId,

                    DepartmentName =
                        p.Doctor.Department.DepartmentName,

                    AppointmentId =
                        p.AppointmentId,

                    AppointmentNumber =
                        p.Appointment.AppointmentNumber,

                    MedicalRecordId =
                        p.MedicalRecordId,

                    MedicalRecordNumber =
                        p.MedicalRecord.RecordNumber,

                    Items = p.PrescriptionItems
                        .Select(pi => new PrescriptionItemResponseDto
                        {
                            PrescriptionItemId =
                                pi.PrescriptionItemId,

                            MedicineId =
                                pi.MedicineId,

                            MedicineName =
                                pi.Medicine.MedicineName,

                            GenericName =
                                pi.Medicine.GenericName,

                            BrandName =
                                pi.Medicine.BrandName,

                            Strength =
                                pi.Medicine.Strength,

                            DosageForm =
                                pi.Medicine.DosageForm,

                            Dosage =
                                pi.Dosage,

                            Frequency =
                                pi.Frequency,

                            DurationDays =
                                pi.DurationDays,

                            Route =
                                pi.Route,

                            Instructions =
                                pi.Instructions
                        })
                        .ToList()
                });
        }
    }
}