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

        public IEnumerable<Appointment> List()
        {
            return _context.Appointments.ToList();
        }

        public Appointment Details(string id)
        {
            return _context.Appointments.Find(id);
        }

        public void Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Edit(Appointment appointment)
        {
            _context.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(Appointment appointment)
        {
            _context.Remove(appointment);
            _context.SaveChanges();
        }


    }
}
