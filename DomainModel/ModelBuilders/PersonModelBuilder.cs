using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class PersonModelBuilder
	{
		public static void PersonModel(this ModelBuilder builder)
		{
			builder.Entity<Person>()
				.ToTable("People");

			builder.Entity<Person>()
				.Property(p => p.FirstName)
				.HasMaxLength(30)
				.IsRequired();

			builder.Entity<Person>()
				   .Property(p => p.LastName)
				   .HasMaxLength(30)
				   .IsRequired();

			builder.Entity<Person>()
				   .Property(p => p.BirthDay)
				   .IsRequired()
				   .HasColumnType("date");

			builder.Entity<Person>()
				   .Property(p => p.Adress)
				   .HasMaxLength(150)
				   .IsRequired();

            builder.Entity<Person>()
                   .Property(p => p.City)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Entity<Person>()
                   .Property(p => p.PostalCode)
                   .HasMaxLength(6)
                   .IsRequired();

			builder.Entity<Person>()
				   .Property(p => p.ComplementaryAdressInformation)
				   .HasMaxLength(150);

            builder.Entity<Person>()
				   .Property(p => p.IsActive)
				   .IsRequired();
		}
	}
}