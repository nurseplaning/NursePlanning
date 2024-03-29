﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Appointment
    {
        public Guid Id { get; set; }

        [Display(Name = "Date du rendez-vous")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "La Durée du soin doit être renseignée")]
        [Display(Name = "Durée du Soin")]
        public int TimeSpanHealthCare { get; set; }

        [Display(Name = "A domicile")]
        public bool AtHome { get; set; }

        public Nurse Nurse { get; set; }

        [Display(Name = "Infirmier(e)")]
        public string NurseId { get; set; }// Foreign Key

        public Patient Patient { get; set; }
        [Display(Name = "Patient(e)")]
        public string PatientId { get; set; } // Foreign Key

        public Status Status { get; set; }
        [Display(Name = "Statut")]
        public Guid StatusId { get; set; }// Foreign Key

        public virtual HealthCarePrimary HealthCarePrimary { get; set; }
        [Display(Name = "Soin")]
        public virtual int HealthCarePrimaryId { get; set; }// Foreign Key
        public virtual HealthCareSecondary HealthCareSecondary { get; set; }
        [Display(Name = "Type de Soin")]
        public virtual int HealthCareSecondaryId { get; set; }// Foreign Key

        public virtual ICollection<Message> Messages { get; set; }
    }
}