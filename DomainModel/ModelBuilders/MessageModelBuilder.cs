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
                .Property(d => d.Content)
                .HasMaxLength(150)
                .IsRequired();

        }
    }
}
