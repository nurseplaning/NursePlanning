using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebNursePlanning.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace WebNursePlanning.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INurseRepository _nurseRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public AppointmentsController(IAppointmentRepository appointmentRepository, INurseRepository nurseRepository, IPatientRepository patientRepository, IStatusRepository statusRepository, UserManager<Person> userManager, SignInManager<Person> signInManager)
        {
            _appointmentRepository = appointmentRepository;
            _nurseRepository = nurseRepository;
            _patientRepository = patientRepository;
            _statusRepository = statusRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var listAppointments = await _appointmentRepository.ListAppointmentsById(user.Id);
            return View(listAppointments);
        }


        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Details(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            //recherche de tous les infirmiers , les patients pour creer un rdv "en cours de validation"
            //var listNurses = ;
            var listNurses = await _nurseRepository.ListNurses();
            var dicoNurses = listNurses.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["NurseId"] = new SelectList(dicoNurses, "Key", "Value");

            var listPatients = await _patientRepository.ListPatients();
            var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["PatientId"] = new SelectList(dicoPatients, "Key", "Value");

            ViewData["StatusId"] = await _statusRepository.GetStatusId("En attente");
            
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                //appointment.Id = Guid.NewGuid();
                await _appointmentRepository.Create(appointment);

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Details(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var listNurses = await _nurseRepository.ListNurses();
            var dicoNurses = listNurses.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["NurseId"] = new SelectList(dicoNurses, "Key", "Value", appointment.NurseId);

            var listPatients = await _patientRepository.ListPatients();
            var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["PatientId"] = new SelectList(dicoPatients, "Key", "Value", appointment.PatientId);
            ViewData["StatusId"] = new SelectList(await _statusRepository.ListStatuses(), "Id", "Name", appointment.StatusId);
           
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,Description,AtHome,NurseId,PatientId,StatusId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _appointmentRepository.Edit(appointment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var listNurses = await _nurseRepository.ListNurses();
            var dicoNurses = listNurses.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["NurseId"] = new SelectList(dicoNurses, "Key", "Value", appointment.NurseId);

            var listPatients = await _patientRepository.ListPatients();
            var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["PatientId"] = new SelectList(dicoPatients, "Key", "Value", appointment.PatientId);
            ViewData["StatusId"] = new SelectList(await _statusRepository.ListStatuses(), "Id", "Name", appointment.StatusId);

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Details(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appointmentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(Guid id)
        {
            return _appointmentRepository.Exists(id);
        }
    }
}