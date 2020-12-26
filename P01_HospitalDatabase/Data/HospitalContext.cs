using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Config;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {

                entity.Property("FirstName").IsUnicode();
                entity.Property("LastName").IsUnicode();
                entity.Property("Address").IsUnicode();
                entity.Property("Email").IsUnicode(false);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.Property("Name").IsUnicode();
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.Property("Comments")
                .IsUnicode();

                entity
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(v => v.PatientId);

                entity
                .HasOne(v => v.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(v => v.DoctorId);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity
                .HasOne(pm => pm.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(pm => pm.PatientId);

                entity
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(pm => pm.MedicamentId);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.Property("Name").IsUnicode();
                entity.Property("Comments").IsUnicode();

                entity
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property("Name").IsUnicode();
                entity.Property("Specialty").IsUnicode();


            });
        }
    }
}
