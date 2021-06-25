using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool AtHome { get; set; }
        public Nurse Nurse { get; set; }
        public string NurseId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public Status Status { get; set; }
        public Guid StatusId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}