using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;
using WebNursePlanning.Models;

namespace WebNursePlanning.Controllers
{
	[Authorize(Roles = "ROLE_SUPER_ADMIN, ROLE_ADMIN")]
	public class AbsenceController : Controller
	{
		private readonly IAbsenceRepository _absenceRepository;
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly INurseRepository _nurseRepository;
		private readonly IStatusRepository _statusRepository;
		private readonly UserManager<Person> _userManager;
		private readonly SignInManager<Person> _signInManager;

		public AbsenceController(IAbsenceRepository absenceRepository, IAppointmentRepository appointmentRepository, INurseRepository nurseRepository, IStatusRepository statusRepository, UserManager<Person> userManager, SignInManager<Person> signInManager)
		{
			_absenceRepository = absenceRepository;
			_appointmentRepository = appointmentRepository;
			_nurseRepository = nurseRepository;
			_statusRepository = statusRepository;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// GET: AbsenceController
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			var listAbsences = await _absenceRepository.ListAbsenceById(user.Id);
			return View(listAbsences);
		}

		// GET: AbsenceController/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var absence = await _absenceRepository.Details(id);
			if (absence == null)
			{
				return NotFound();
			}

			return View(absence);
		}

		// GET: AbsenceController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AbsenceController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AbsenceViewModel absence)
		{
			if (ModelState.IsValid)
			{
				var dateStart = new DateTime(absence.DateStart.Year,
											absence.DateStart.Month,
											absence.DateStart.Day,
											absence.TimeStart.Hour,
											absence.TimeStart.Minute,
											absence.TimeStart.Second);

				var dateEnd = new DateTime(absence.DateEnd.Year,
											absence.DateEnd.Month,
											absence.DateEnd.Day,
											absence.TimeEnd.Hour,
											absence.TimeEnd.Minute,
											absence.TimeEnd.Second);
				var a = new Absence()
				{
					DateEnd = dateEnd,
					DateStart = dateStart,
					Reason = absence.Motif,
					NurseId = absence.NurseId
				};
				await _absenceRepository.Create(a);

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction("Index");
		}

		// GET: AbsenceController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AbsenceController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateStart,DateEnd,Motif,NurseId")] Absence absence)
		{
			if (id != absence.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _absenceRepository.Edit(absence);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AbsenceExist(absence.Id))
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

			return View(absence);
		}

		// GET: AbsenceController/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var absence = await _absenceRepository.Details(id);
			if (absence == null)
			{
				return NotFound();
			}

			return View(absence);
		}

		// POST: AbsenceController/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			await _absenceRepository.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool AbsenceExist(Guid id)
		{
			return _absenceRepository.Exists(id);
		}
	}
}