using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IHealthCareSecondaryRepository
    {
        public Task<List<HealthCareSecondary>> GetHealthCareSecondaryList();
        public Task<List<HealthCareSecondary>> GetHealthCareSecondaryList(int healthCarePrimaryid);
    }
}
