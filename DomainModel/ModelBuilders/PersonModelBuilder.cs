using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class PersonModelBuilder
    {
        public static void PersonModel(this ModelBuilder builder)
        {
            builder.Entity<Person>().ToTable("People");
            builder.Entity<Person>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Person>().Property(p => p.LastName).IsRequired();
            builder.Entity<Person>().Property(p => p.BirthDate).IsRequired();
            builder.Entity<Person>().Property(p => p.Adress).IsRequired();
        }
    }
}
