﻿using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface INurseRepository
	{
		public Task<IEnumerable<Nurse>> ListNurses();

		public Task<Nurse> Details(string id);
	}
}