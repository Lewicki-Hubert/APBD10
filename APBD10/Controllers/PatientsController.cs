using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using APBD10.DTOs;
using APBD10.Models;
using APBD10.Services;
 
namespace APBD10.Controllers;
 
[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;
 
    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }
 
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientData(int patientId)
    {
        var prescriptions = await _dbService.GetPrescriptionsForPatient(patientId);
 
        if (prescriptions == null || !prescriptions.Any())
        {
            return NotFound($"No data found for patient with ID {patientId}");
        }
 
        var patient = prescriptions.First().Patient;
        var patientDto = new PatientDTO
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Prescriptions = prescriptions.Select(p => new PrescriptionDTO
            {
                Id = p.Id,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorDTO
                {
                    Id = p.Doctor.Id,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDTO
                {
                    Id = pm.Medicament.Id,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Description
                }).ToList()
            }).ToList()
        };
 
        return Ok(patientDto);
    }
}