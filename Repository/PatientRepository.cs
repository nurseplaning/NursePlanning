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
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> List()
        {
            return await _context.Patients.ToList();
        }

        public async Task<Patient> Details(string id)
        {
            return await _context.Patients.Find(id);
        }

        public async Task Create(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChanges();
        }

        public async Task Edit(Patient patient)
        {
            _context.Update(patient);
            await _context.SaveChanges();
        }

        public async Task Delete(Patient patient)
        {
            _context.Remove(patient);
            await _context.SaveChanges();
        }
    }
}
