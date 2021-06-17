using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        public IEnumerable<Appointment> List();

        public Appointment Details(string id);

        public void Create(Appointment appointment);

        public void Edit(Appointment appointment);

        public void Delete(Appointment appointment);


    }
}
