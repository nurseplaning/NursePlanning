using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class AbsenceModelBuilder
    {
        public static void AbsenceModel(this ModelBuilder builder)
        {
            builder.Entity<Absence>()
                .ToTable("Absences");

            builder.Entity<Absence>()
                .Property(n => n.Reason)
                .IsRequired();

            builder.Entity<Absence>()
                .Property(n => n.DateStart)
                .IsRequired();

            builder.Entity<Absence>()
                .Property(n => n.DateEnd)
                .IsRequired();

            builder.Entity<Absence>()
                .Property(i => i.NurseId)
                .IsRequired();
        }
    }
}
