using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
	public class MessageRepository
	{
		private readonly ApplicationDbContext _context;

		public MessageRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Message>> ListMessages()
		{
			return await _context.Messages.ToListAsync();
		}

		public async Task<Message> Details(string id)
		{
			return await _context.Messages.FindAsync(id);
		}
	}
}