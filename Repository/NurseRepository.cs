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
	public class NurseRepository : INurseRepository
	{
		private readonly ApplicationDbContext _context;

		public NurseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Nurse>> ListNurses()
		{
			return await _context.Nurses.ToListAsync();
		}

		public async Task<Nurse> Details(string id)
		{
			return await _context.Nurses.FindAsync(id);
		}

		public async Task<Nurse> Create(Nurse nurse)
		{
			_context.Nurses.Add(nurse);
			await _context.SaveChangesAsync();
			return nurse;
		}

		public async Task Edit(Nurse nurse)
		{
			_context.Update(nurse);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Nurse nurse)
		{
			_context.Remove(nurse);
			await _context.SaveChangesAsync();
		}
	}
}