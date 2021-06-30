using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> GetStatusId(string statusName)
        {
            var liste = await ListStatuses();
            return liste.FirstOrDefault(s => s.Name == statusName).Id;
        }

        public async Task<IEnumerable<Status>> ListStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }
        public async Task Edit(Status status)
        {
            _context.Update(status);
            await _context.SaveChangesAsync();
        }
    }
}
