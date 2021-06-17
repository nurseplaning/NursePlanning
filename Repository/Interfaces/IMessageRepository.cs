using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> ListMessages();

        public Task<Message> Details(string id);

        public Task Create(Message patient);

        public Task Edit(Message patient);

        public Task Delete(Message patient);
    }
}
