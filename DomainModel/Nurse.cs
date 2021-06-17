using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Nurse : Person
    {
        public int SiretNumber { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
