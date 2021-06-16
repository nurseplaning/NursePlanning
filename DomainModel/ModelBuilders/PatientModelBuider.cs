using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class PatientModelBuider
    {
        public static void PatientModel(this ModelBuilder builder)
        {
            builder.Entity<Patient>().ToTable("Patients");
            builder.Entity<Patient>().Property(p => p.SocialSecurityNumber).IsRequired();
        }
    }
}
