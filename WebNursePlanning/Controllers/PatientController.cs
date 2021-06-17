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

        // GET: PatientController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,FirstName,LastName,BirthDate,Adress,SocialSecurityNumber")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patient/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _repository.Details((string)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,FirstName,LastName,BirthDate,Adress,SocialSecurityNumber")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Edit(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
        // GET: Patient/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _repository.Details((string)id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _repository.Delete(patient);
            return RedirectToAction(nameof(Index));
        }

    }
}
