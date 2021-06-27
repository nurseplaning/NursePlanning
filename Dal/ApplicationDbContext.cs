using DomainModel;
using DomainModel.ModelBuilders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Person> People { get; set; }
		public DbSet<Nurse> Nurses { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Absence> Absences { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public ApplicationDbContext()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.IdentityModel();
			builder.MessageModel();
			builder.PersonModel();
			builder.NurseModel();
			builder.PatientModel();
			builder.AppointementModel();
			builder.StatusModel();

			base.OnModelCreating(builder);
		}
	}
}