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
        public IEnumerable<Patient> List();

        public Patient Details(string id);

        public void Create(Patient patient);

        public void Edit(Patient patient);

        public void Delete(Patient patient);

    }
}
