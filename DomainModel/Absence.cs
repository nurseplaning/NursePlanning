using System;

namespace DomainModel
{
	public class Absence
	{
		public Guid Id { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public string Reason { get; set; }
		public virtual Nurse Nurse { get; set; }
		public virtual string NurseId { get; set; }
	}
}