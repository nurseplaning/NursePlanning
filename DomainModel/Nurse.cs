using System.Collections.Generic;

namespace DomainModel
{
    public class Nurse : Person
    {
        public string AdeliNumber { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
    }
}
