using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class IdentityModelBuilder
    {
        public static void IdentityModel(this ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().Property(u => u.Email).IsRequired();
            builder.Entity<IdentityUser>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<IdentityUser>().Property(u => u.PasswordHash).IsRequired();
            builder.Entity<IdentityUser>().Property(u => u.PhoneNumber).IsRequired();
        }
    }
}
