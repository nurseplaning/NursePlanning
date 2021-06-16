using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class NurseModelBuilder
    {
        public static void NurseModel(this ModelBuilder builder)
        {
            builder.Entity<Nurse>().ToTable("Nurses");
            builder.Entity<Nurse>().Property(n => n.SiretNumber).IsRequired();
        }
    }
}
