using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNursePlanning.Models
{
    public class PatientViewModel
    {
        public DateTime Date { get; set; }
        public bool AtHome { get; set; }
        public Nurse Nurse { get; set; }
        public string NurseId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
