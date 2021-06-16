using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Appointment
    {
        public string Id { get; set; }
        public DateTime AppointDate { get; set; }
        public bool AtHome { get; set; }
        public virtual string NurseId { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual string PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual string StatusId { get; set; }
        public  virtual Status  Status { get; set; }
        public  virtual ICollection<Message> Messages { get; set; }
    }
}
