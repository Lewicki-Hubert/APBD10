using Microsoft.EntityFrameworkCore;

using APBD10.Database;

using APBD10.Models;
 
namespace APBD10.Services;
 
public class DbService : IDbService

{

    private readonly DatabaseContext _context;
 
    public DbService(DatabaseContext context)

    {

        _context = context;

    }
 
    public async Task<ICollection<Prescription>> GetPrescriptionsForPatient(int patientId)

    {

        return await _context.Prescriptions

            .Include(p => p.Patient)

            .Include(p => p.Doctor)

            .Include(p => p.PrescriptionMedicaments)

            .ThenInclude(pm => pm.Medicament)

            .Where(p => p.PatientId == patientId)

            .OrderBy(p => p.DueDate)

            .ToListAsync();

    }
 
    public async Task<bool> DoesPatientExist(int patientId)

    {

        return await _context.Patients.AnyAsync(p => p.Id == patientId);

    }
 
    public async Task<bool> DoesMedicamentExist(string medicamentName)

    {

        return await _context.Medicaments.AnyAsync(m => m.Name == medicamentName);

    }
 
    public async Task AddNewPatient(Patient patient)

    {

        await _context.AddAsync(patient);

        await _context.SaveChangesAsync();

    }
 
    public async Task AddNewPrescription(Prescription prescription)

    {

        await _context.AddAsync(prescription);

        await _context.SaveChangesAsync();

    }
 
    public async Task<Medicament?> GetMedicamentByName(string name)

    {

        return await _context.Medicaments.FirstOrDefaultAsync(m => m.Name == name);

    }

}
 
public interface IDbService

{

    Task<ICollection<Prescription>> GetPrescriptionsForPatient(int patientId);

    Task<bool> DoesPatientExist(int patientId);

    Task<bool> DoesMedicamentExist(string medicamentName);

    Task AddNewPatient(Patient patient);

    Task AddNewPrescription(Prescription prescription);

    Task<Medicament?> GetMedicamentByName(string name);

}