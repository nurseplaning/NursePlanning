using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
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
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == statusName);
            return status.Id;
        }

        public async Task<IEnumerable<Status>> ListStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }
    }
}