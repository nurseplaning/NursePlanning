using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Appointment
    {
        public string Id { get; set; }
        public DateTime AppointDate { get; set; }
        public bool AtHome { get; set; }
        public virtual string NurseId { get; set; }
        public virtual string PatientId { get; set; }
        public virtual string StatusId { get; set; }

    }
}
