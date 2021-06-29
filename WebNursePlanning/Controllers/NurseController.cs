using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiNursePlanning.Controllers
{
    [Authorize(Roles = "ROLE_SUPER_ADMIN")]
    public class NurseController : Controller
    {
        private readonly ILogger<NurseController> logger;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public NurseController(ILogger<NurseController> logger,
            UserManager<Person> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Person> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        // GET: NurseController
        public async Task<ActionResult> IndexAsync()
        {
            var people = await userManager.GetUsersInRoleAsync("ROLE_ADMIN");
            var nurses = new List<Nurse>();
            foreach (var item in people)
                nurses.Add(item as Nurse);

            return View(nurses);
        }

        // GET: NurseController/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var nurse = await userManager.FindByIdAsync(id) as Nurse;
            if (nurse is null)
            {
                return NotFound();
            }

            return View(nurse);
        }
    }
}