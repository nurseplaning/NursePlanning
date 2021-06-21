using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interfaces;
using DomainModel;

namespace WebNursePlanning.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _repository;
        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }
        // GET: PatientController
        public async Task<IActionResult> Index()
        {
            return View(await _repository.ListPatients());
        }
        // GET: PatientController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _repository.Details(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        
    }
}
