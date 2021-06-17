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
    public class NurseRepository : INurseRepository
    {
        private readonly ApplicationDbContext _context;

        public NurseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Nurse> List()
        {
            return _context.Nurses.ToList();
        }

        public Nurse Details(string id)
        {
            return _context.Nurses.Find(id);
        }

        public void Create(Nurse nurse)
        {
            _context.Nurses.Add(nurse);
            _context.SaveChanges();
        }

        public void Edit(Nurse nurse)
        {
            _context.Update(nurse);
            _context.SaveChanges();
        }

        public void Delete(Nurse nurse)
        {
            _context.Remove(nurse);
            _context.SaveChanges();
        }
    }
}
