using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace WebNursePlanning.Controllers
{
    [Authorize(Roles = "ROLE_SUPER_ADMIN, ROLE_ADMIN, ROLE_USER")]
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private IMessageRepository _repository;

        public MessageController(ILogger<MessageController> logger, IMessageRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: MessageController
        public async Task<IActionResult> Index()
        {
            return View(await _repository.ListMessages());
        }

        // GET: MessageController/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            //Asynchrone
            if (id == null)
            {
                return NotFound();
            }

            var message = await _repository.Details((string)id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: MessageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Content,Person,PersonId,Appointment,AppointmentId")] Message message)
        {
            try
            {
                //Asynchorne
                if (ModelState.IsValid)
                {
                    await _repository.Create(message);
                    return RedirectToAction(nameof(Index));
                }
                return View(message);
            }
            catch
            {
                return View();
            }
        }

        // GET: MessageController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //Asynchrone
            if (id == null)
            {
                return NotFound();
            }

            var message = await _repository.Details(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: MessageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,BirthDate,Adress,SocialSecurityNumber")] Message message)
        {
            //Asynchrone
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.Edit(message);
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: MessageController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _repository.Details(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: MessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _repository.Details(id);
            await _repository.Delete(message);
            return RedirectToAction(nameof(Index));
        }
    }
}
