using DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebNursePlanning.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebNursePlanningBD;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                        .ToTable("Messages");

            modelBuilder.Entity<Message>()
                        .Property(p => p.Content)
                        .HasMaxLength(150)
                        .IsRequired();

            modelBuilder.Entity<Message>()
                        .Property(p => p.Date)
                        .IsRequired();


            base.OnModelCreating(modelBuilder);
        }

    }
}
