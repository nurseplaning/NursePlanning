using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DomainModel.ModelBuilders;

namespace Dal
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Person> People { get; set; }
		public DbSet<Nurse> Nurses { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Director> Directors { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Status> Statuses { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public ApplicationDbContext()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebNursePlanningBD;Trusted_Connection=True;MultipleActiveResultSets=true");
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.IdentityModel();
			builder.MessageModel();
			builder.PersonModel();
			builder.NurseModel();
			builder.PatientModel();
			builder.DirectorModel();
			builder.AppointementModel();
			builder.StatusModel();

			base.OnModelCreating(builder);
		}
	}
}