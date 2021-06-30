using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
	public class Absence
	{
		public Guid Id { get; set; }
		[Display(Name = "Date de départ")]
		public DateTime DateStart { get; set; }
		[Display(Name = "Date de retour")]
		public DateTime DateEnd { get; set; }

		[Display(Name = "Motif")]
		public string Reason { get; set; }
		public virtual Nurse Nurse { get; set; }
		[Display(Name = "Infirmier(e)")]
		public virtual string NurseId { get; set; }
	}
}