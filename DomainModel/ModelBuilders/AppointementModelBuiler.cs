using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModelBuilders
{
    public static class AppointementModelBuiler
    {
        public static void AppointementModel(this ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .ToTable("Appointments");

            builder.Entity<Appointment>()
                .Property(n => n.Description)
                .IsRequired();
            
            builder.Entity<Appointment>()
                .Property(n => n.Date)
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(n => n.AtHome)
                .IsRequired();
        }
    }
}
