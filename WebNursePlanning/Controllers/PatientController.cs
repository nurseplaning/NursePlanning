using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebPatientPlanning.Controllers
{
    [Authorize(Roles = "ROLE_SUPER_ADMIN, ROLE_ADMIN")]
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> logger;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public PatientController(ILogger<PatientController> logger,
            UserManager<Person> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Person> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        // GET: PatientController
        public async Task<ActionResult> IndexAsync()
        {
            var people = await userManager.GetUsersInRoleAsync("ROLE_USER");
            var patients = new List<Patient>();
            foreach (var item in people)
                patients.Add(item as Patient);

            return View(patients);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var patient =  await userManager.FindByIdAsync(id) as Patient;
            if (patient is null)
            {
                return NotFound();
            }

            return View(patient);
        }
    }
}