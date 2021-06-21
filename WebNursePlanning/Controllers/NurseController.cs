using Microsoft.AspNetCore.Mvc;
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
    }
}