using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wyklad10.Configurations;

namespace Wyklad10.Models
{
    public class HospitalDbContext : DbContext
    {

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public HospitalDbContext() { }

        public HospitalDbContext(DbContextOptions options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());

            modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(e => new { e.IdMedicament, e.IdPrescription });
        }
    }
}
