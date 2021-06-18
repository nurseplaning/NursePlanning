using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace WebApiNursePlanning.Controllers
{
	public class NurseController : Controller
	{
		private readonly INurseRepository repository;

		public NurseController(INurseRepository nurseRepository)
		{
			repository = nurseRepository;
		}

		// GET: NurseController
		public async Task<ActionResult> IndexAsync()
		{
			return View(await repository.ListNurses());
		}

		// GET: NurseController/Details/5
		public async Task<ActionResult> DetailsAsync(string id)
		{
			if (id is null)
			{
				return BadRequest();
			}

			var nurse = await repository.Details(id);
			if (nurse is null)
			{
				return NotFound();
			}

			return View(nurse);
		}

		// GET: NurseController/Create
		public ActionResult Create()
		{
			return View();
		}

		// GET: NurseController/Edit/5
		public async Task<ActionResult> EditAsync(string id)
		{
			if (id is null)
			{
				return BadRequest();
			}

			var nurse = await repository.Details(id);
			if (nurse is null)
			{
				return NotFound();
			}

			return View(nurse);
		}

		// POST: NurseController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(string id, [Bind("Id,FirstName,LastName,BirthDate,Adress,SiretNumber")] Nurse nurse)
		{
			if (id != nurse.Id)
			{
				return BadRequest();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await repository.Edit(nurse);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!NurseExists(nurse.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(DetailsAsync), new { id });
			}
			return View(nurse);
		}

		private bool NurseExists(string id)
		{
			if (repository.Details(id) == null) return false;
			return true;
		}

		// GET: NurseController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: NurseController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(IndexAsync));
			}
			catch
			{
				return View();
			}
		}
	}
}