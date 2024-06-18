using System.Transactions;
using APBD10.Database;
using APBD10.DTOs;
using APBD10.Models;
using APBD10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;
    private readonly DatabaseContext _context;

    public PrescriptionsController(IDbService dbService, DatabaseContext context)
    {
        _dbService = dbService;
        _context = context;
    }
 
    [HttpPost]
    public async Task<IActionResult> AddNewPrescription(NewPrescriptionDTO newPrescription)
    {
        if (newPrescription.Medicaments.Count > 10)
        {
            return BadRequest("Prescription cannot contain more than 10 medicaments");
        }
 
        if (newPrescription.DueDate < newPrescription.Date)
        {
            return BadRequest("DueDate cannot be earlier than Date");
        }
 
        var patient = new Patient
        {
            FirstName = newPrescription.Patient.FirstName,
            LastName = newPrescription.Patient.LastName
        };
 
        if (!await _dbService.DoesPatientExist(patient.Id))
        {
            await _dbService.AddNewPatient(patient);
        }
        else
        {
            patient = await _context.Patients.SingleOrDefaultAsync(p => p.FirstName == newPrescription.Patient.FirstName && p.LastName == newPrescription.Patient.LastName);
        }
 
        var prescription = new Prescription
        {
            Patient = patient,
            DoctorId = newPrescription.DoctorId,
            Date = newPrescription.Date,
            DueDate = newPrescription.DueDate
        };
 
        foreach (var newMedicament in newPrescription.Medicaments)
        {
            var medicament = await _dbService.GetMedicamentByName(newMedicament.Name);
            if (medicament == null)
            {
                return NotFound($"Medicament with name '{newMedicament.Name}' does not exist");
            }
 
            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                Medicament = medicament,
                Dose = newMedicament.Dose,
                Description = newMedicament.Description
            });
        }
 
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddNewPrescription(prescription);
            scope.Complete();
        }
 
        return Created("api/prescriptions", new { Id = prescription.Id });
    }
}