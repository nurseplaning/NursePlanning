using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebNursePlanning.Models
{
	public class AppointmentViewModel
	{
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Date du rendez-vous")]
		public DateTime Date { get; set; }

		[Required]
		[DataType(DataType.Time)]
		public DateTime Time { get; set; }

		[Required]
		[Display(Name = "A domicile")]
		public bool AtHome { get; set; }

		[Required]
		[Display(Name = "Infirmier(e)")]
		public string NurseId { get; set; }

		[Required(ErrorMessage = "ups")]
		[Display(Name = "Patient(e)")]
		public string PatientId { get; set; }

		[Required]
		[Display(Name = "Motif du rendez-vous")]
		public string Reason { get; set; }

		public Guid StatusId { get; set; }

		public ICollection<Message> Messages { get; set; }
	}
}