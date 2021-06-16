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
                .IsRequired();

            builder.Entity<Person>()
                .Property(p => p.LastName)
                .IsRequired();

            builder.Entity<Person>()
                .Property(p => p.BirthDay)
                .IsRequired();

            builder.Entity<Person>()
                .Property(p => p.Adress)
                .IsRequired();
        }
    }
}
