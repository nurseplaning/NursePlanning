using DomainModel;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dal
{
	public static class ContextExtention
	{
		public static void Initialize(this ApplicationDbContext context, bool dropAlways = false)
		{
			//To drop database or not
			if (dropAlways)
				context.Database.EnsureDeleted();

			context.Database.EnsureCreated();

			//if (!context.Roles.Any())
			//{
			//	context.Roles.Add(new IdentityRole() { Name = "ROLE_SUPER_ADMIN" });
			//	context.Roles.Add(new IdentityRole() { Name = "ROLE_ADMIN" });
			//	context.Roles.Add(new IdentityRole() { Name = "ROLE_USER" });
			//	context.SaveChanges();
			//}

			//if db has been already seeded
			if (context.People.Any())
				return;

			var nurse = new Nurse()
			{
				FirstName = "Lola",
				LastName = "Lopez",
				BirthDay = new DateTime(1990, 12, 12),
				Adress = "1 rue principale 34000 Montpellier",
				SiretNumber = 12345678987654,
				Email = "lolalopez@nurse.fr",
				PhoneNumber = "0600000000",
				PasswordHash = "mdpNurse",
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				PhoneNumberConfirmed = true,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};

			//var userManager = new IdentityUser(context);
			//var passwordHash = userManager.PasswordHasher.HashPassword("mySecurePassword");

			context.Nurses.Add(nurse);
			context.SaveChanges();

			var patient = new Patient()
			{
				FirstName = "Estoy",
				LastName = "Malito",
				BirthDay = new DateTime(1980, 12, 12),
				Adress = "3, rue diagonale 34000 Montpellier",
				SocialSecurityNumber = 1234567898765,
				Email = "estoymalito@patient.fr",
				PasswordHash = "mdpPatient",
				PhoneNumber = "0600000002",
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				PhoneNumberConfirmed = true,
				LockoutEnabled = true,
				AccessFailedCount = 0
			};
			context.Patients.Add(patient);
			context.SaveChanges();
		}

		public static String Sha256_hash(String value)
		{
			using (SHA256 hash = SHA256Managed.Create())
			{
				return String.Concat(hash
				  .ComputeHash(Encoding.UTF8.GetBytes(value))
				  .Select(item => item.ToString("x2")));
			}
		}
	}
}