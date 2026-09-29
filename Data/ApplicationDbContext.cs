using HospitalManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<LaboratoryTest> LaboratoryTests { get; set; }

        public DbSet<LabOrder> LabOrders { get; set; }

        public DbSet<LabOrderItem> LabOrderItems { get; set; }

        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<MedicineBatch> MedicineBatches { get; set; }

        public DbSet<PharmacySale> PharmacySales { get; set; }

        public DbSet<PharmacySaleItem> PharmacySaleItems { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        public DbSet<PatientInsurance> PatientInsurances { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<DoctorLeave> DoctorLeaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.MobileNumber)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Admins)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hospital>()
                .HasKey(h => h.HospitalId);

            modelBuilder.Entity<Hospital>()
                .HasIndex(h => h.Code)
                .IsUnique();

            // One Hospital → Many Branches
            modelBuilder.Entity<Hospital>()
                .HasMany(h => h.Branches)
                .WithOne(b => b.Hospital)
                .HasForeignKey(b => b.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Department>()
    .HasKey(d => d.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasIndex(d => new { d.BranchId, d.Code })
                .IsUnique();

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Departments)
                .WithOne(d => d.Branch)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Location>()
    .HasKey(l => l.LocationId);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Location)
                .WithMany(l => l.Branches)
                .HasForeignKey(b => b.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Doctor>()
    .HasKey(d => d.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Patient>()
    .HasKey(p => p.PatientId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Branch)
                .WithMany()
                .HasForeignKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
    .HasKey(a => a.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Appointments)
                .HasForeignKey(a => a.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Department)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorSchedule>()
    .HasOne(ds => ds.Doctor)
    .WithMany(d => d.DoctorSchedules)
    .HasForeignKey(ds => ds.DoctorId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorSchedule>()
    .HasOne(ds => ds.Branch)
    .WithMany()
    .HasForeignKey(ds => ds.BranchId)
    .OnDelete(DeleteBehavior.Restrict);
            // ============================================================
            // MEDICAL RECORD RELATIONSHIPS
            // ============================================================

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                // Record number must be unique
                entity.HasIndex(m => m.RecordNumber)
                    .IsUnique();

                // One Branch → Many Medical Records
                entity.HasOne(m => m.Branch)
                    .WithMany()
                    .HasForeignKey(m => m.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One Patient → Many Medical Records
                entity.HasOne(m => m.Patient)
                    .WithMany()
                    .HasForeignKey(m => m.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One Doctor → Many Medical Records
                entity.HasOne(m => m.Doctor)
                    .WithMany()
                    .HasForeignKey(m => m.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One Appointment → One Medical Record
                entity.HasOne(m => m.Appointment)
                    .WithOne()
                    .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                    .OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<Medicine>(entity =>
                {
                    entity.HasIndex(m => m.MedicineName)
                        .IsUnique();

                    entity.HasIndex(m => m.GenericName);
                });

                modelBuilder.Entity<Prescription>(entity =>
                {
                    entity.HasIndex(p => p.PrescriptionNumber)
                        .IsUnique();

                    entity.HasOne(p => p.Branch)
                        .WithMany()
                        .HasForeignKey(p => p.BranchId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(p => p.Patient)
                        .WithMany()
                        .HasForeignKey(p => p.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(p => p.Doctor)
                        .WithMany()
                        .HasForeignKey(p => p.DoctorId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(p => p.Appointment)
                        .WithOne()
                        .HasForeignKey<Prescription>(p => p.AppointmentId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(p => p.MedicalRecord)
                        .WithOne()
                        .HasForeignKey<Prescription>(p => p.MedicalRecordId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasMany(p => p.PrescriptionItems)
                        .WithOne(pi => pi.Prescription)
                        .HasForeignKey(pi => pi.PrescriptionId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<PrescriptionItem>(entity =>
                {
                    entity.HasOne(pi => pi.Medicine)
                        .WithMany(m => m.PrescriptionItems)
                        .HasForeignKey(pi => pi.MedicineId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasIndex(pi => new
                    {
                        pi.PrescriptionId,
                        pi.MedicineId
                    })
                    .IsUnique();
                });
                modelBuilder.Entity<Invoice>(entity =>
                {
                    entity.HasIndex(i => i.InvoiceNumber)
                        .IsUnique();

                    entity.HasOne(i => i.Branch)
                        .WithMany()
                        .HasForeignKey(i => i.BranchId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(i => i.Patient)
                        .WithMany()
                        .HasForeignKey(i => i.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(i => i.Appointment)
                        .WithOne()
                        .HasForeignKey<Invoice>(i => i.AppointmentId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(i => i.Doctor)
                        .WithMany()
                        .HasForeignKey(i => i.DoctorId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasMany(i => i.InvoiceItems)
                        .WithOne(ii => ii.Invoice)
                        .HasForeignKey(ii => ii.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasMany(i => i.Payments)
                        .WithOne(p => p.Invoice)
                        .HasForeignKey(p => p.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<InvoiceItem>(entity =>
                {
                    entity.Property(i => i.UnitPrice)
                        .HasPrecision(18, 2);

                    entity.Property(i => i.TotalAmount)
                        .HasPrecision(18, 2);
                });

                modelBuilder.Entity<Payment>(entity =>
                {
                    entity.HasIndex(p => p.PaymentNumber)
                        .IsUnique();

                    entity.Property(p => p.Amount)
                        .HasPrecision(18, 2);

                    entity.HasOne(p => p.Invoice)
                        .WithMany(i => i.Payments)
                        .HasForeignKey(p => p.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade);
                });
                modelBuilder.Entity<LaboratoryTest>(entity =>
                {
                    entity.HasIndex(t => t.TestCode)
                        .IsUnique()
                        .HasFilter("`TestCode` IS NOT NULL");

                    entity.Property(t => t.TestFee)
                        .HasPrecision(18, 2);

                    entity.HasMany(t => t.LabOrderItems)
                        .WithOne(i => i.LaboratoryTest)
                        .HasForeignKey(i => i.LaboratoryTestId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<LabOrder>(entity =>
                {
                    entity.HasIndex(o => o.LabOrderNumber)
                        .IsUnique();

                    entity.HasOne(o => o.Branch)
                        .WithMany()
                        .HasForeignKey(o => o.BranchId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(o => o.Patient)
                        .WithMany()
                        .HasForeignKey(o => o.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(o => o.Doctor)
                        .WithMany()
                        .HasForeignKey(o => o.DoctorId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(o => o.Appointment)
                        .WithMany()
                        .HasForeignKey(o => o.AppointmentId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasMany(o => o.LabOrderItems)
                        .WithOne(i => i.LabOrder)
                        .HasForeignKey(i => i.LabOrderId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<LabOrderItem>(entity =>
                {
                    entity.HasIndex(i => new
                    {
                        i.LabOrderId,
                        i.LaboratoryTestId
                    })
                    .IsUnique();

                    entity.HasOne(i => i.LabOrder)
                        .WithMany(o => o.LabOrderItems)
                        .HasForeignKey(i => i.LabOrderId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(i => i.LaboratoryTest)
                        .WithMany(t => t.LabOrderItems)
                        .HasForeignKey(i => i.LaboratoryTestId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne<LabResult>()
                        .WithOne(r => r.LabOrderItem)
                        .HasForeignKey<LabResult>(r => r.LabOrderItemId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<LabResult>(entity =>
                {
                    entity.HasIndex(r => r.LabOrderItemId)
                        .IsUnique();
                });
                modelBuilder.Entity<MedicineBatch>()
    .HasOne(mb => mb.Medicine)
    .WithMany(m => m.MedicineBatches)
    .HasForeignKey(mb => mb.MedicineId)
    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<MedicineBatch>()
                    .HasOne(mb => mb.Branch)
                    .WithMany(b => b.MedicineBatches)
                    .HasForeignKey(mb => mb.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PharmacySale>()
                    .HasOne(s => s.Branch)
                    .WithMany(b => b.PharmacySales)
                    .HasForeignKey(s => s.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PharmacySale>()
                    .HasOne(s => s.Patient)
                    .WithMany(p => p.PharmacySales)
                    .HasForeignKey(s => s.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PharmacySale>()
                    .HasOne(s => s.Prescription)
                    .WithMany()
                    .HasForeignKey(s => s.PrescriptionId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PharmacySaleItem>()
                    .HasOne(i => i.PharmacySale)
                    .WithMany(s => s.Items)
                    .HasForeignKey(i => i.PharmacySaleId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<PharmacySaleItem>()
                    .HasOne(i => i.MedicineBatch)
                    .WithMany()
                    .HasForeignKey(i => i.MedicineBatchId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Staff>()
    .HasOne(s => s.Branch)
    .WithMany()
    .HasForeignKey(s => s.BranchId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ward>()
    .HasOne(w => w.Branch)
    .WithMany()
    .HasForeignKey(w => w.BranchId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Room>()
    .HasOne(r => r.Ward)
    .WithMany()
    .HasForeignKey(r => r.WardId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bed>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Beds)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Admission>()
    .HasOne(a => a.Branch)
    .WithMany()
    .HasForeignKey(a => a.BranchId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Appointment)
                .WithMany()
                .HasForeignKey(a => a.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Ward)
                .WithMany()
                .HasForeignKey(a => a.WardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Room)
                .WithMany()
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Bed)
                .WithMany()
                .HasForeignKey(a => a.BedId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Discharge>()
    .HasOne(d => d.Admission)
    .WithMany()
    .HasForeignKey(d => d.AdmissionId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discharge>()
                .HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PatientInsurance>()
    .HasOne(pi => pi.Patient)
    .WithMany()
    .HasForeignKey(pi => pi.PatientId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientInsurance>()
                .HasOne(pi => pi.InsuranceProvider)
                .WithMany(ip => ip.PatientInsurances)
                .HasForeignKey(pi => pi.InsuranceProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceClaim>()
                .HasOne(ic => ic.PatientInsurance)
                .WithMany(pi => pi.InsuranceClaims)
                .HasForeignKey(ic => ic.PatientInsuranceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceClaim>()
                .HasOne(ic => ic.Admission)
                .WithMany()
                .HasForeignKey(ic => ic.AdmissionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorAvailability>()
    .HasOne(da => da.Doctor)
    .WithMany(d => d.DoctorAvailabilities)
    .HasForeignKey(da => da.DoctorId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorLeave>()
    .HasOne(dl => dl.Doctor)
    .WithMany(d => d.DoctorLeaves)
    .HasForeignKey(dl => dl.DoctorId)
    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}