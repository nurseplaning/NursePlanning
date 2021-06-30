using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStatusRepository
    {
        public Task<Guid> GetStatusId(string statusname);
        public Task Edit(Status status);
        public Task<IEnumerable<Status>> ListStatuses();
    }
}
