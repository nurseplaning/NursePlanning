using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainModel;
using Repository.Interfaces;

namespace WebNursePlanning.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INurseRepository _nurseRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStatusRepository _statusRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository, INurseRepository nurseRepository, IPatientRepository patientRepository, IStatusRepository statusRepository)
        {
            _appointmentRepository = appointmentRepository;
            _nurseRepository = nurseRepository;
            _patientRepository = patientRepository;
            _statusRepository = statusRepository;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var listAppointments = _appointmentRepository.ListAppointments();
            return View(await listAppointments);
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

            //ViewData["StatusId"] = await _statusRepository.GetStatusId("En cours de validation");
            ViewData["StatusId"] = new SelectList(await _statusRepository.ListStatuses(), "Id", "Name");

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
            //ViewData["NurseId"] = new SelectList(_context.Nurses, "Id", "Id", appointment.NurseId);
            //ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
            //ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", appointment.StatusId);
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
            //ViewData["NurseId"] = new SelectList(await _nurseRepository.ListNurses(), "Id", "Id", appointment.NurseId);

            var listPatients = await _patientRepository.ListPatients();
            var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["PatientId"] = new SelectList(dicoPatients, "Key", "Value", appointment.PatientId);
            //ViewData["PatientId"] = new SelectList(await _patientRepository.ListPatients(), "Id", "Id", appointment.PatientId);
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
            //ViewData["NurseId"] = new SelectList(await _nurseRepository.ListNurses(), "Id", "Id", appointment.NurseId);

            var listPatients = await _patientRepository.ListPatients();
            var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
            ViewData["PatientId"] = new SelectList(dicoPatients, "Key", "Value", appointment.PatientId);
            //ViewData["PatientId"] = new SelectList(await _patientRepository.ListPatients(), "Id", "Id", appointment.PatientId);
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
