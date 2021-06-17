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
        public IEnumerable<Nurse> List();

        public Nurse Details(string id);

        public void Create(Nurse nurse);

        public void Edit(Nurse nurse);

        public void Delete(Nurse nurse);
    }
}
