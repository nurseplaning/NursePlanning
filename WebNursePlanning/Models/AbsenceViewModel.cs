using System;
using System.ComponentModel.DataAnnotations;

namespace WebNursePlanning.Models
{
	public class AbsenceViewModel
	{
		[DataType(DataType.Date)]
		[Display(Name = "Date du départ")]
		public DateTime DateStart { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Heure de départ")]
		public DateTime TimeStart { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Date de retour")]
		public DateTime DateEnd { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Heure de retour")]
		public DateTime TimeEnd { get; set; }

		[Display(Name = "Motif")]
		public string Motif { get; set; }

		[Display(Name = "Infirmier(e)")]
		public virtual string NurseId { get; set; }
	}
}