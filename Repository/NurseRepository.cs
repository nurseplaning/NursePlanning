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

        public async Task<IEnumerable<Nurse>> List()
        {
            return _context.Nurses.ToList();
        }

        public async Task<Nurse> Details(string id)
        {
            return await _context.Nurses.Find(id);
        }

        public async Task Create(Nurse nurse)
        {
            _context.Nurses.Add(nurse);
          await  _context.SaveChanges();
        }

        public async Task Edit(Nurse nurse)
        {
            _context.Update(nurse);
           await _context.SaveChanges();
        }

        public async Task Delete(Nurse nurse)
        {
            _context.Remove(nurse);
            await _context.SaveChanges();
        }
    }
}
