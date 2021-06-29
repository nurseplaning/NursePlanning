using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class StatusBuilderModel
	{
		public static void StatusModel(this ModelBuilder builder)
		{
			builder.Entity<Status>()
				.ToTable("Statuses");

			builder.Entity<Status>()
				.Property(n => n.Name)
				.IsRequired();
		}
	}
}