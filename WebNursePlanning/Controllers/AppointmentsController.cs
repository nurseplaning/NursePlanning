using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainModel;
using Dal;

namespace WebNursePlanning.Controllers
{
	public class AppointmentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AppointmentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Appointments
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Appointments.Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Status);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Appointments/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments
				.Include(a => a.Nurse)
				.Include(a => a.Patient)
				.Include(a => a.Status)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		// GET: Appointments/Create
		public IActionResult Create()
		{
			ViewData["NurseId"] = new SelectList(_context.Nurses, "Id", "Id");
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id");
			ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
			return View();
		}

		// POST: Appointments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Date,AtHome,NurseId,PatientId,StatusId")] Appointment appointment)
		{
			if (ModelState.IsValid)
			{
				appointment.Id = Guid.NewGuid();
				_context.Add(appointment);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["NurseId"] = new SelectList(_context.Nurses, "Id", "Id", appointment.NurseId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
			ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", appointment.StatusId);
			return View(appointment);
		}

		// GET: Appointments/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment == null)
			{
				return NotFound();
			}
			ViewData["NurseId"] = new SelectList(_context.Nurses, "Id", "Id", appointment.NurseId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
			ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", appointment.StatusId);
			return View(appointment);
		}

		// POST: Appointments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,AtHome,NurseId,PatientId,StatusId")] Appointment appointment)
		{
			if (id != appointment.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(appointment);
					await _context.SaveChangesAsync();
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
			ViewData["NurseId"] = new SelectList(_context.Nurses, "Id", "Id", appointment.NurseId);
			ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
			ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", appointment.StatusId);
			return View(appointment);
		}

		// GET: Appointments/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments
				.Include(a => a.Nurse)
				.Include(a => a.Patient)
				.Include(a => a.Status)
				.FirstOrDefaultAsync(m => m.Id == id);
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
			var appointment = await _context.Appointments.FindAsync(id);
			_context.Appointments.Remove(appointment);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AppointmentExists(Guid id)
		{
			return _context.Appointments.Any(e => e.Id == id);
		}
	}
}