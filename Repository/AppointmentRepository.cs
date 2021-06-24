using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Repository
{
	public class AppointmentRepository : IAppointmentRepository
	{
		private readonly ApplicationDbContext _context;

		public AppointmentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Appointment>> ListAppointments()
		{
			return await _context.Appointments.ToListAsync();
		}

		public async Task<Appointment> Details(string id)
		{
			return await _context.Appointments.FindAsync(id);
		}

		public async Task Create(Appointment appointment)
		{
			_context.Appointments.Add(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task Edit(Appointment appointment)
		{
			_context.Update(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Appointment appointment)
		{
			_context.Remove(appointment);
			await _context.SaveChangesAsync();
		}
	}
}