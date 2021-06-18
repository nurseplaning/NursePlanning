using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebNursePlanning.Models;

namespace WebNursePlanning.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<Person> _userManager;
		private readonly SignInManager<Person> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public HomeController(ILogger<HomeController> logger,
			UserManager<Person> userManager,
			RoleManager<IdentityRole> roleManager,
			SignInManager<Person> signInManager)
		{
			_logger = logger;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> IndexAsync()
		{
			var superNurse = new Nurse()
			{
				UserName = "eljefe",
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
			};
			var password = "1MAY.2min";
			IdentityResult chkSuperNurse = await _userManager.CreateAsync(superNurse, password);
			IdentityRole roleSuperAdmin = new IdentityRole()
			{
				Name = "ROLE_SUPER_ADMIN"
			};
			await _roleManager.CreateAsync(roleSuperAdmin);
			await _userManager.AddToRoleAsync(superNurse, "ROLE_SUPER_ADMIN");

			var nurse = new Nurse()
			{
				UserName = "Lolita",
				FirstName = "Lola",
				LastName = "Lopez",
				BirthDay = new DateTime(1990, 12, 12),
				Adress = "1 rue principale 34000 Montpellier",
				SiretNumber = "12345678987654",
				Email = "lolalopez@nurse.fr",
				PhoneNumber = "0600000000",
				PasswordHash = "mdpNurse",
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				PhoneNumberConfirmed = true,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};
			var passwordNurse = "1MAY.2min";
			IdentityResult chkNurse = await _userManager.CreateAsync(nurse, passwordNurse);
			IdentityRole roleAdmin = new IdentityRole()
			{
				Name = "ROLE_ADMIN"
			};
			await _roleManager.CreateAsync(roleAdmin);
			await _userManager.AddToRoleAsync(nurse, "ROLE_ADMIN");

			var patient = new Patient()
			{
				UserName = "elEnfermo",
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
				AccessFailedCount = 0
			};

			var passwordPatient = "1MAY.2min";
			IdentityResult chkPatient = await _userManager.CreateAsync(patient, passwordPatient);
			IdentityRole roleUser = new IdentityRole()
			{
				Name = "ROLE_USER"
			};
			await _roleManager.CreateAsync(roleUser);
			await _userManager.AddToRoleAsync(superNurse, "ROLE_USER");

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