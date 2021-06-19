using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Patient : Person
    {
        public long SocialSecurityNumber { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
