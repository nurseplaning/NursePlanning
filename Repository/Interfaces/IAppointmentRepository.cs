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
        public Task<IEnumerable<Appointment>> List();

        public Task<Appointment> Details(string id);

        public Task Create(Appointment appointment);

        public Task Edit(Appointment appointment);
        public Task Delete(Appointment appointment);


    }
}
