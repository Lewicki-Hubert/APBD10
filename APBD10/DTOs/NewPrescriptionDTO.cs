namespace APBD10.DTOs;
 
public class NewPrescriptionDTO
{
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public NewPatientDTO Patient { get; set; } = null!;
    public ICollection<NewPrescriptionMedicamentDTO> Medicaments { get; set; } = new List<NewPrescriptionMedicamentDTO>();
}
 
public class NewPatientDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
 
public class NewPrescriptionMedicamentDTO
{
    public string Name { get; set; } = string.Empty;
    public int Dose { get; set; }
    public string? Description { get; set; }
}
 
public class PatientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<PrescriptionDTO> Prescriptions { get; set; } = new List<PrescriptionDTO>();
}
 
public class PrescriptionDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDTO Doctor { get; set; } = null!;
    public ICollection<PrescriptionMedicamentDTO> Medicaments { get; set; } = new List<PrescriptionMedicamentDTO>();
}
 
public class DoctorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
 
public class PrescriptionMedicamentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Dose { get; set; }
    public string? Description { get; set; }
}