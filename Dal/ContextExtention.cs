using DomainModel;
using System;
using System.Linq;

namespace Dal
{
    public static class ContextExtention
    {
        public static void Initialize(this ApplicationDbContext context, bool dropAlways = false)
        {
            if (dropAlways)
                context.Database.EnsureDeleted();
            try
            {
                context.Database.EnsureCreated();

            }
            catch (Exception ex) 
            {
                Console.Error.WriteLine(ex.ToString()); 
            }

            if (context.Statuses.Any())
                return;

            var status = new Status[] {
                new Status { Name = "En attente" },
                new Status { Name = "Validé" },
                new Status { Name = "Annulé" },
                new Status { Name = "Rejeté" },
                new Status { Name = "Fermé" } };

            context.Statuses.AddRange(status);
            context.SaveChanges();
        }
    }
}