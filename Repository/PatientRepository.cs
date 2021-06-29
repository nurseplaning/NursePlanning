using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
	public class PatientRepository : IPatientRepository
	{
		private readonly ApplicationDbContext _context;

		public PatientRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Patient>> ListPatients()
		{
			return await _context.Patients.ToListAsync();
		}

		public async Task<Patient> Details(string id)
		{
			return await _context.Patients.FindAsync(id);
		}

		public async Task Create(Patient patient)
		{
			_context.Patients.Add(patient);
			await _context.SaveChangesAsync();
		}

		public async Task Edit(Patient patient)
		{
			_context.Update(patient);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Patient patient)
		{
			_context.Remove(patient);
			await _context.SaveChangesAsync();
		}
	}
}