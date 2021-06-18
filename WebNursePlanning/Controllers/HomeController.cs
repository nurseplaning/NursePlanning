using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
				FirstName = "El",
				LastName = "Jefe",
				BirthDay = new DateTime(1980, 10, 10),
				Adress = "1 bis rue parallele 34000 Montpellier",
				SiretNumber = 12345678987654,
				Email = "eljefe@nurse.fr",
				PasswordHash = "mdpSuperNurse",
				PhoneNumber = "0600000001",
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				PhoneNumberConfirmed = true,
				LockoutEnabled = false,
				AccessFailedCount = 0,
			};

			IdentityResult chkUser = await _userManager.CreateAsync(superNurse, superNurse.PasswordHash);
			IdentityRole role = new IdentityRole()
			{
				Name = "ROLE_SUPER_ADMIN"
			};

			await _roleManager.CreateAsync(role);
			await _userManager.AddToRoleAsync(superNurse, "ROLE_SUPER_ADMIN");

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