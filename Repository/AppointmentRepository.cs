using Dal;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Appointments.Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Status).ToListAsync();
        }

        public async Task<Appointment> Details(Guid? id)
        {
            var appointment = await _context.Appointments
                                    .Include(a => a.Nurse)
                                    .Include(a => a.Patient)
                                    .Include(a => a.Status)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            return appointment;
        }

        public async Task<Appointment> Create(Appointment appointment)
        {

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task Edit(Appointment appointment)
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid? id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid? id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }
        [Authorize]
        public async Task<IEnumerable<Appointment>> ListAppointmentsById(string idPerson)
        {
            return await _context.Appointments.Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Status).Where(p => p.NurseId == idPerson || p.PatientId == idPerson).ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByNurseId(string id)
        {
            var list = await _context.Appointments.Where(a => a.NurseId.Equals(id)).Include(a => a.Patient).ToListAsync();
            return list;

        }
      
    }
}
