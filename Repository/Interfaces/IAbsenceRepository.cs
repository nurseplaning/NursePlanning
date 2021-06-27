using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IAbsenceRepository
	{
		public Task<IEnumerable<Absence>> ListAbsences();

		public Task<IEnumerable<Absence>> ListAbsenceById(string id);

		public Task<Absence> Details(Guid? id);

		public Task<Absence> Create(Absence appointment);

		public Task Edit(Absence appointment);

		public Task Delete(Guid? id);

		public bool Exists(Guid? id);
	}
}