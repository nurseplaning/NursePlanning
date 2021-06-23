using DomainModel;

namespace Dal
{
	public static class ContextExtention
	{
		public static void Initialize(this ApplicationDbContext context, bool dropAlways = false)
		{
			if (dropAlways)
				context.Database.EnsureDeleted();

			context.Database.EnsureCreated();

			var status = new Status { Name = "En attente" };
			var status1 = new Status { Name = "Validé" };
			var status2 = new Status { Name = "Annulé" };
			var status3 = new Status { Name = "Rejeté" };
			var status4 = new Status { Name = "Fermé" };

			context.Statuses.AddRange(entities: new Status[] { status, status1, status2, status3, status4 });
			context.SaveChanges();
		}
	}
}