using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
