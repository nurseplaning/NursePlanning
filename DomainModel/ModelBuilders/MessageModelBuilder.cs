using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class MessageModelBuilder
	{
		public static void MessageModel(this ModelBuilder builder)
		{
			builder.Entity<Message>()
				   .ToTable("Messages");

			builder.Entity<Message>()
				   .Property(m => m.Content)
				   .HasMaxLength(150)
				   .IsRequired();

			builder.Entity<Message>()
				   .Property(m => m.Date)
				   .IsRequired();

			builder.Entity<Message>()
				   .HasIndex(m => m.PersonId)
				   .IsUnique();

			builder.Entity<Message>()
				   .HasIndex(m => m.AppointmentId)
				   .IsUnique();
		}
	}
}