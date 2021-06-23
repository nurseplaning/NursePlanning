using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
    public static class AppointementModelBuiler
    {
        public static void AppointementModel(this ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .ToTable("Appointments");

            builder.Entity<Appointment>()
                .Property(a => a.Description)
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.Date)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.AtHome)
                .HasColumnName("A domicile")
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.NurseId)
                .HasColumnName("Infirmier(e)")
                .IsRequired();

            builder.Entity<Appointment>()
               .Property(a => a.PatientId)
               .HasColumnName("Patient(e)")
               .IsRequired();

            builder.Entity<Appointment>()
              .Property(a => a.StatusId)
              .IsRequired();
        }
    }
}