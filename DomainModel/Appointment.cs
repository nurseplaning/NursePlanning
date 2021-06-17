using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime AppointDate { get; set; }
        public bool AtHome { get; set; }
        public Nurse Nurse { get; set; }
        public string NurseId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
