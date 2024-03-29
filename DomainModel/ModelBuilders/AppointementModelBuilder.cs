﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DomainModel.ModelBuilders
{
    public static class AppointementModelBuilder
    {
        public static void AppointementModel(this ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .ToTable("Appointments");

            builder.Entity<Appointment>()
                .Property(a => a.Date)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.TimeSpanHealthCare)
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.AtHome)
                .IsRequired();

            builder.Entity<Appointment>()
                .Property(a => a.NurseId)
                .IsRequired();

            builder.Entity<Appointment>()
               .Property(a => a.PatientId)
               .IsRequired();

            builder.Entity<Appointment>()
              .Property(a => a.StatusId)
              .IsRequired();

            builder.Entity<Appointment>()
              .Property(a => a.HealthCarePrimaryId)
              .IsRequired();

            builder.Entity<Appointment>()
              .Property(a => a.HealthCareSecondaryId)
              .IsRequired();

            builder.Entity<Appointment>()
                .HasOne(a => a.HealthCarePrimary)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.HealthCareSecondary)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(e => e.Patient)
                .WithMany(e => e.Appointments)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}