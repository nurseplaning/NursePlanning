using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IPatientRepository
	{
		public Task<IEnumerable<Patient>> ListPatients();

		public Task<Patient> Details(string id);
	}
}