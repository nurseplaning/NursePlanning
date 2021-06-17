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

        public IEnumerable<Patient> List()
        {
            return _context.Patients.ToList();
        }

        public Patient Details(string id)
        {
            return _context.Patients.Find(id);
        }

        public void Create(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Edit(Patient patient)
        {
            _context.Update(patient);
            _context.SaveChanges();
        }

        public void Delete(Patient patient)
        {
            _context.Remove(patient);
            _context.SaveChanges();
        }
    }
}
