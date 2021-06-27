using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IStatusRepository
	{
		public Task<Status> GetStatusId(string statusname);

		public Task<IEnumerable<Status>> ListStatuses();
	}
}