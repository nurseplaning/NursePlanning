using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStatusRepository
    {
        public Task<Guid> GetStatusId(string statusname);

        public Task<IEnumerable<Status>> ListStatuses();
    }
}
