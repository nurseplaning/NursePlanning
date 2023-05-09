using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Nurse : Person
    {
        public string Ordinal { get; set; }
        public string Rpps { get; set; }
        public string Siret { get; set; }
        public string CabinetAdress { get; set; }
        public DateTime DiplomaGraduation { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
    }
}
