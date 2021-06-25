using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebNursePlanning.Models
{
    public class AppointmentViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Display(Name = "A domicile")]
        public bool AtHome { get; set; }
        [Display(Name = "Infirmier(e)")]
        public string NurseId { get; set; }
        [Display(Name = "Patient(e)")]
        public string PatientId { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status")]
        public Guid StatusId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
