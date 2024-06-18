using Microsoft.EntityFrameworkCore;
using APBD10.Models;
 
namespace APBD10.Database;
 
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
 
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
 
        // Przykładowe dane
        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient { Id = 1, FirstName = "Jan", LastName = "Kowalski" },
            new Patient { Id = 2, FirstName = "Anna", LastName = "Nowak" }
        });
 
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor { Id = 1, FirstName = "Adam", LastName = "Nowak" },
            new Doctor { Id = 2, FirstName = "Aleksandra", LastName = "Wiśniewska" }
        });
 
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament { Id = 1, Name = "Paracetamol", Description = "Pain reliever" },
            new Medicament { Id = 2, Name = "Ibuprofen", Description = "Anti-inflammatory" }
        });
    }
}