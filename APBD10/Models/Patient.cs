using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Models;
 
public class Patient
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
 
    public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
}
 
public class Doctor
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
 
    public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
}
 
public class Medicament
{
    [Key]
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;
 
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new HashSet<PrescriptionMedicament>();
}
 
public class Prescription
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
 
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;
    public int PatientId { get; set; }
 
    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; } = null!;
    public int DoctorId { get; set; }
 
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new HashSet<PrescriptionMedicament>();
}
 
[Table("PrescriptionMedicament")]
[PrimaryKey(nameof(PrescriptionId), nameof(MedicamentId))]
public class PrescriptionMedicament
{
    public int PrescriptionId { get; set; }
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
    [MaxLength(300)]
    public string? Description { get; set; }
 
    [ForeignKey(nameof(PrescriptionId))]
    public Prescription Prescription { get; set; } = null!;
    [ForeignKey(nameof(MedicamentId))]
    public Medicament Medicament { get; set; } = null!;
}