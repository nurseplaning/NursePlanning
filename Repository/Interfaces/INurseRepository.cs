using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface INurseRepository
	{
		public Task<IEnumerable<Nurse>> ListNurses();

		public Task<Nurse> Details(string id);

		public Task<Nurse> Create(Nurse nurse);

		public Task Edit(Nurse nurse);

		public Task Delete(Nurse nurse);
	}
}