using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
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
			return View(await repository.Details(id));
		}

		// GET: NurseController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NurseController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: NurseController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: NurseController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
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