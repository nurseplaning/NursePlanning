using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNursePlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository repository;

        public PatientController(IPatientRepository patientRepository)
        {
            repository = patientRepository;
        }
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return Ok(await repository.ListPatients());
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await repository.Details(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(string id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.Edit(patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PatientExists(string id)
        {
            if (repository.Details(id) is null)
                return false;
            return true;
        }

    }
}
