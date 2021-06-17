using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPatientRepository
    {
        public Task<IEnumerable<Patient>> ListPatients();

        public Task<Patient> Details(string id);

        public Task Create(Patient patient);

        public Task Edit(Patient patient);

        public Task Delete(Patient patient);

    }
}
