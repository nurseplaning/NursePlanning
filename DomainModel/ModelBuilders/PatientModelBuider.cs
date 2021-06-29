using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class PatientModelBuider
	{
		public static void PatientModel(this ModelBuilder builder)
		{
			builder.Entity<Patient>()
				   .ToTable("Patients");

			builder.Entity<Patient>()
				   .Property(p => p.SocialSecurityNumber)
				   .HasMaxLength(13)
				   .IsRequired();

			builder.Entity<Patient>()
				   .HasIndex(p => p.SocialSecurityNumber)
				   .IsUnique();
		}
	}
}