using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class NurseModelBuilder
	{
		public static void NurseModel(this ModelBuilder builder)
		{
			builder.Entity<Nurse>()
				   .ToTable("Nurses");

			builder.Entity<Nurse>()
				   .Property(n => n.Rpps)
				   .HasMaxLength(11)
				   .IsRequired();

            builder.Entity<Nurse>()
                   .Property(n => n.Ordinal)
                   .HasMaxLength(7)
                   .IsRequired();

            builder.Entity<Nurse>()
                   .Property(n => n.Siret)
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Entity<Nurse>()
				   .HasIndex(n => n.Siret)
				   .IsUnique();
		}
	}
}