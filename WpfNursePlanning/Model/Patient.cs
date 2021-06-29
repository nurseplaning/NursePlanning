using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNursePlanning.Model
{
    public class Patient : Person
    {
        public string SocialSecurityNumber { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
