using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebNursePlanning.Models;
using WebNursePlanning.Shared.Components;

namespace WebNursePlanning.Controllers
{
    [Authorize(Roles = "ROLE_SUPER_ADMIN, ROLE_ADMIN, ROLE_USER")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INurseRepository _nurseRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public AppointmentsController(IAppointmentRepository appointmentRepository,
                                        INurseRepository nurseRepository,
                                        IPatientRepository patientRepository,
                                        IStatusRepository statusRepository,
                                        UserManager<Person> userManager,
                                        SignInManager<Person> signInManager)
        {
            _appointmentRepository = appointmentRepository;
            _nurseRepository = nurseRepository;
            _patientRepository = patientRepository;
            _statusRepository = statusRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string id = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var listAppointments = await _appointmentRepository.ListAppointmentsById(id is null ? user.Id : id);
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
            //ViewData["StatusId"] = await _statusRepository.GetStatusId("En cours de validation");
            var liste = await _statusRepository.ListStatuses();
            ViewData["StatusId"] = liste.FirstOrDefault(s => s.Name == "En attente").Id;

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
            return View(appointment);
        }

        public IActionResult GenerateCalendar(string personId, int decalage)
        {
            return ViewComponent("CreateAppointmentCalendar", new { id = personId, decalage });
        }
        public async Task<IActionResult> Transfer(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var appointments = await _appointmentRepository.ListAppointments();
            var appointment = await _appointmentRepository.Details(id);
            //var list = _nurseRepository.ListNursesWithAppointment();
            //var dateOfWeek = appointment.Date;
            //var c = _appointmentRepository.CheckAvailabilityAppointment(appointments, dateOfWeek);
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

        // POST: Appointments/Transfer
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(Guid id, [Bind("Id,Date,Reason,AtHome,NurseId,PatientId,StatusId")] Appointment appointment)
        {

            if (id != appointment.Id)
            {
                return NotFound();
            }

            var listAppointments = await _appointmentRepository.ListAppointmentsById(appointment.NurseId);
            var dateOfWeek = appointment.Date;
            var isAvailable = _appointmentRepository.CheckAvailabilityAppointment(listAppointments, dateOfWeek);

            if (ModelState.IsValid)
            {
                try
                {
                    if (isAvailable)
                        await _appointmentRepository.Edit(appointment);
                    else if (isAvailable == false)
                    {
                        //ViewBag["Message"]= "L'infirmier est occupé";
                        return Content("<script language='javascript' type='text/javascript'>alert('L'infirmier est occupé');</script>");
                    }
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


            ViewData["StatusId"] = new SelectList(await _statusRepository.ListStatuses(), "Id", "Name", appointment.StatusId);

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,Reason,AtHome,NurseId,PatientId,StatusId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldAppointment = _appointmentRepository.Details(id).GetAwaiter().GetResult();

                    if (oldAppointment.Reason != appointment.Reason ||
                        oldAppointment.Date != appointment.Date ||
                        oldAppointment.NurseId != appointment.NurseId)
                    {
                                          oldAppointment.StatusId = await _statusRepository.GetStatusId("En attente");
                    }
                    else
                    {
                        oldAppointment.StatusId = appointment.StatusId;
                    }

                    oldAppointment.Reason = appointment.Reason;
                    oldAppointment.Date = appointment.Date;
                    oldAppointment.NurseId = appointment.NurseId;
                    oldAppointment.PatientId = appointment.PatientId;
                    oldAppointment.AtHome = appointment.AtHome;
                                      
                    await _appointmentRepository.Edit(oldAppointment);
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
            var appointment = await _appointmentRepository.Details(id);
    
            appointment.StatusId = await _statusRepository.GetStatusId("Annulé"); 

            await _appointmentRepository.Edit(appointment);

            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(Guid id)
        {
            return _appointmentRepository.Exists(id);
        }
        public async Task<IActionResult> ValidateStatus(Guid? id)
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
        // POST: Appointments/Validation
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateStatus(Guid id)
        {
            var app = await _appointmentRepository.Details(id);
            var statusId = await _statusRepository.GetStatusId("Validé");
            app.StatusId = statusId;
            await _appointmentRepository.Edit(app);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> RejectStatus(Guid? id)
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
        // POST: Appointments/Validation
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectStatus(Guid id)
        {
            var app = await _appointmentRepository.Details(id);
            var statusId = await _statusRepository.GetStatusId("Rejeté");
            app.StatusId = statusId;
            await _appointmentRepository.Edit(app);
            return RedirectToAction(nameof(Index));

        }
    }
}