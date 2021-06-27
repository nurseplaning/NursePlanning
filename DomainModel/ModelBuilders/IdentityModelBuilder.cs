using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class IdentityModelBuilder
	{
		public static void IdentityModel(this ModelBuilder builder)
		{
			builder.Entity<IdentityUser>()
				   .Property(u => u.Email)
				   .IsRequired();

			builder.Entity<IdentityUser>()
				   .HasIndex(u => u.Email)
				   .IsUnique();

			builder.Entity<IdentityUser>()
				   .Property(u => u.PasswordHash)
				   .IsRequired();

			builder.Entity<IdentityUser>()
				   .Property(u => u.PhoneNumber)
				   .HasMaxLength(10)
				   .IsRequired();
		}
	}
}