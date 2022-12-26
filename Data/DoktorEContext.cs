using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class DoktorEContext : IdentityDbContext<ApplicationUser>
    {
        public DoktorEContext(DbContextOptions<DoktorEContext> options) : base(options)
        {
        }

        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<BloodDonation>? BloodDonations { get; set; }
        public DbSet<Clinic>? Clinics { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Prescription>? Prescriptions { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Invoice)
            .WithOne(i => i.Appointment)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Patient)
            .WithMany(p => p.Invoices)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<BloodDonation>().ToTable("BloodDonation");
            modelBuilder.Entity<Clinic>().ToTable("Clinic");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Prescription>().ToTable("Prescription");
        }
    
    }
}