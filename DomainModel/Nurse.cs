using System.Collections.Generic;

namespace DomainModel
{
    public class Nurse : Person
    {
        public int SiretNumber { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
