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
				   .Property(n => n.SiretNumber)
				   .HasMaxLength(14)
				   .IsRequired();

			builder.Entity<Nurse>()
				   .HasIndex(n => n.SiretNumber)
				   .IsUnique();
		}
	}
}