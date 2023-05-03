﻿using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class PatientModelBuider
	{
		public static void PatientModel(this ModelBuilder builder)
		{
			builder.Entity<Patient>()
				   .ToTable("Patients");

			builder.Entity<Patient>()
				   .Property(p => p.SocialRegime)
				   .HasMaxLength(100)
				   .IsRequired();

            
        }
	}
}