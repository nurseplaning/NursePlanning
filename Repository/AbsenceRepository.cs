using Dal;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
	public class AbsenceRepository : IAbsenceRepository
	{
		private readonly ApplicationDbContext _context;

		public AbsenceRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Absence>> ListAbsences()
		{
			return await _context.Absences.Include(a => a.Nurse).ToListAsync();
		}

		public async Task<Absence> Details(Guid? id)
		{
			var absence = await _context.Absences
									.Include(a => a.Nurse)
									.FirstOrDefaultAsync(m => m.Id == id);
			return absence;
		}

		public async Task<Absence> Create(Absence absence)
		{
			_context.Absences.Add(absence);
			await _context.SaveChangesAsync();

			return absence;
		}

		public async Task Edit(Absence absence)
		{
			_context.Update(absence);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid? id)
		{
			var absence = await _context.Absences.FindAsync(id);
			_context.Absences.Remove(absence);

			await _context.SaveChangesAsync();
		}

		public bool Exists(Guid? id)
		{
			return _context.Absences.Any(a => a.Id == id);
		}

		[Authorize]
		public async Task<IEnumerable<Absence>> ListAbsenceById(string idNurse)
		{
			return await _context.Absences.Include(a => a.Nurse).Where(p => p.NurseId == idNurse).ToListAsync();
		}
	}
}