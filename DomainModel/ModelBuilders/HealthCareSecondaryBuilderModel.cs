﻿using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
	public static class HealthCareSecondaryBuilderModel
	{
		public static void HealthCareSecondaryModel(this ModelBuilder builder)
		{
			builder.Entity<HealthCareSecondary>()
				.ToTable("HealthCareSecondaries");

			
		}
	}
}