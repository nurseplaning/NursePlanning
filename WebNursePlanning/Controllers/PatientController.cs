using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace WebPatientPlanning.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository repository;

        public PatientController(IPatientRepository patientRepository)
        {
            repository = patientRepository;
        }

        // GET: PatientController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await repository.ListPatients());
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var patient = await repository.Details(id);
            if (patient is null)
            {
                return NotFound();
            }

            return View(patient);
        }
    }
}