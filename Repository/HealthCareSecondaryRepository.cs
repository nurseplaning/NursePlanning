using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HealthCareSecondaryRepository : IHealthCareSecondaryRepository
    {
        private readonly ApplicationDbContext _context;

        public HealthCareSecondaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<HealthCareSecondary>> GetHealthCareSecondaryList()
        {
            return await _context.HealthCareSecondaries.ToListAsync();
        }

        public async Task<List<HealthCareSecondary>> GetHealthCareSecondaryList(int healthCarePrimaryid)
        {
            return await _context.HealthCareSecondaries.Where(h => h.HealthCarePrimaryId == healthCarePrimaryid).ToListAsync();
        }
    }
}
