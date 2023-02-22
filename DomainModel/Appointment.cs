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

        [Display(Name = "Motif")]
        public string Reason { get; set; }

        [Display(Name = "A domicile")]
        public bool AtHome { get; set; }

        [Display(Name = "Prise de Sang")]
        public string BloodSample { get; set; }

        [Display(Name = "Depistage Covid")]
        public string CovidTest { get; set; }

        [Display(Name = "Pansements")]
        public string Bandage { get; set; }
        public Nurse Nurse { get; set; }

        [Display(Name = "Infirmier(e)")]
        public string NurseId { get; set; }

        public Patient Patient { get; set; }
        [Display(Name = "Patient(e)")]
        public string PatientId { get; set; }

        public Status Status { get; set; }
        [Display(Name = "Statut")]
        public Guid StatusId { get; set; }

        public virtual HealthCare HealthCare { get; set; }
        [Display(Name = "Soin")]
        public virtual Guid HealthCareId { get; set; }

        public virtual HealthCareCategory HealthCareCategory { get; set; }
        [Display(Name = "Catégorie de Soin")]
        public virtual Guid HealthCareCategoryId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}