using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
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
		public async Task<IEnumerable<Nurse>> ListNursesWithAppointment()
		{
			return await _context.Nurses.Include(a => a.Appointments).ToListAsync();
		}

		public async Task<Nurse> Details(string id)
		{
			return await _context.Nurses.FindAsync(id);
		}
	}
}