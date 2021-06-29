using System;
using System.ComponentModel.DataAnnotations;

namespace WebNursePlanning.Models
{
	public class AbsenceViewModel
	{
		[DataType(DataType.Date)]
		public DateTime DateStart { get; set; }

		[DataType(DataType.Time)]
		public DateTime TimeStart { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateEnd { get; set; }

		[DataType(DataType.Time)]
		public DateTime TimeEnd { get; set; }

		public string Motif { get; set; }

		[Display(Name = "Infirmier(e)")]
		public virtual string NurseId { get; set; }
	}
}