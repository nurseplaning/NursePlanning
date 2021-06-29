using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApiNursePlanning.Controllers;
using WebPatientPlanning.Controllers;

namespace WebNursePlanning.Controllers
{
    [Authorize(Roles = "ROLE_SUPER_ADMIN")]
    public class SuperAdminController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SuperAdminController(ILogger<HomeController> logger,
            UserManager<Person> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Person> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        // GET: SuperAdminController
        public async Task<ActionResult> Index()
        {
            var listPatient = await userManager.GetUsersInRoleAsync("ROLE_USER");
            var listNurse = await userManager.GetUsersInRoleAsync("ROLE_ADMIN");

            return View(listNurse);
        }

        // GET: SuperAdminController/ActivateNurse/5
        public async Task<ActionResult> ActivateNurseAsync(string id)
        {
            var person = await userManager.FindByIdAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: SuperAdminController/ActivateNurse/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActivateNurseAsync(string Id, bool isActive)
        {
            var person = await userManager.FindByIdAsync(Id);
            person.IsActive = !isActive;
            var checkNurse = await userManager.UpdateAsync(person);
            if (person.GetType() == typeof(Nurse))
                return RedirectToAction(nameof(Index), "Nurse");

            return RedirectToAction(nameof(Index), "Patient");
        }
    }
}
