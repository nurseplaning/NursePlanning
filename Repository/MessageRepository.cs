using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public async Task Create(Message message)
		{
			_context.Messages.Add(message);
			await _context.SaveChangesAsync();
		}

		public async Task Edit(Message message)
		{
			_context.Update(message);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Message message)
		{
			_context.Remove(message);
			await _context.SaveChangesAsync();
		}
	}
}