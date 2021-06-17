using DomainModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNursePlanning.Data;

namespace Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> List()
        {
            return await _context.Appointments.ToList();
        }

        public async Task<Appointment> Details(string id)
        {
            return await _context.Appointments.Find(id);
        }

        public async Task Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChanges();
        }

        public async Task Edit(Appointment appointment)
        {
            _context.Update(appointment);
            await _context.SaveChanges();
        }

        public async Task Delete(Appointment appointment)
        {
            _context.Remove(appointment);
            await _context.SaveChanges();
        }


    }
}
