using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebNursePlanning.Models;

namespace WebNursePlanning.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<Person> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Person> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            #region Roles Generation

            IdentityRole roleSuperAdmin = new IdentityRole()
            {
                Name = "ROLE_SUPER_ADMIN"
            };
            await roleManager.CreateAsync(roleSuperAdmin);

            IdentityRole roleAdmin = new IdentityRole()
            {
                Name = "ROLE_ADMIN"
            };
            await roleManager.CreateAsync(roleAdmin);

            IdentityRole roleUser = new IdentityRole()
            {
                Name = "ROLE_USER"
            };
            await roleManager.CreateAsync(roleUser);

            #endregion Roles Generation

            #region Default Users Generation

            var users = userManager.GetUsersInRoleAsync("ROLE_SUPER_ADMIN");
            if (users.Result.Count == 0)
            {
                var password = "Superpassword.0";

                var superNurse = new Nurse()
                {
                    UserName = $"eljefe{DateTime.Now.ToString("yyyyymmddHHmmss")}",
                    FirstName = "El",
                    LastName = "Jefe",
                    BirthDay = new DateTime(1980, 10, 10),
                    Adress = "1 bis rue parallele 34000 Montpellier",
                    SiretNumber = "12345678987654",
                    Email = "eljefe@nurse.fr",
                    PhoneNumber = "0600000001",
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsActive = true
                };
                IdentityResult chkSuperNurse = await userManager.CreateAsync(superNurse, password);

                await userManager.AddToRoleAsync(superNurse, "ROLE_SUPER_ADMIN");

                var nurse = new Nurse()
                {
                    UserName = $"lopezlola{DateTime.Now.ToString("yyyyymmddHHmmss")}",
                    FirstName = "Lola",
                    LastName = "Lopez",
                    BirthDay = new DateTime(1990, 12, 12),
                    Adress = "1 rue principale 34000 Montpellier",
                    SiretNumber = "12345678987658",
                    Email = "lolalopez@nurse.fr",
                    PhoneNumber = "0600000000",
                    PasswordHash = "mdpNurse",
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsActive = true
                };

                IdentityResult chkNurse = await userManager.CreateAsync(nurse, password);
                await userManager.AddToRoleAsync(nurse, "ROLE_ADMIN");

                var patient = new Patient()
                {
                    UserName = $"malitoestoy{DateTime.Now.ToString("yyyyymmddHHmmss")}",
                    FirstName = "Estoy",
                    LastName = "Malito",
                    BirthDay = new DateTime(1980, 12, 12),
                    Adress = "3, rue diagonale 34000 Montpellier",
                    SocialSecurityNumber = "1234567898765",
                    Email = "estoymalito@patient.fr",
                    PasswordHash = "mdpPatient",
                    PhoneNumber = "0600000002",
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsActive = true
                };

                IdentityResult chkPatient = await userManager.CreateAsync(patient, password);
                await userManager.AddToRoleAsync(patient, "ROLE_USER");
            }

            #endregion Default Users Generation

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}