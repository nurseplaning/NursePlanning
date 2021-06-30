using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [Display(Name = "Date du rendez-vous")]
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public bool AtHome { get; set; }
        public Nurse Nurse { get; set; }
        [Display(Name = "Infirmier(e)")]
        public string NurseId { get; set; }
        public Patient Patient { get; set; }
        [Display(Name = "Patient(e)")]
        public string PatientId { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Statut")]
        public Guid StatusId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}