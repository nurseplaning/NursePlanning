using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "A domicile")]
        public bool AtHome { get; set; }
        public Nurse Nurse { get; set; }
        [Display(Name = "Infirmier(e)")]
        public string NurseId { get; set; }
        public Patient Patient { get; set; }
        [Display(Name = "Patient(e)")]
        public string PatientId { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status")]
        public Guid StatusId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
