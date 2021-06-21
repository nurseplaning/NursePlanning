namespace Dal
{
	public static class ContextExtention
	{
		public static void Initialize(this ApplicationDbContext context, bool dropAlways = false)
		{
			if (dropAlways)
				context.Database.EnsureDeleted();

			context.Database.EnsureCreated();
		}
	}
}