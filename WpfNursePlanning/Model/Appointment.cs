using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNursePlanning.Model
{
	public class Appointment
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public bool AtHome { get; set; }
		public Nurse Nurse { get; set; }
		public string NurseId { get; set; }
		public Patient Patient { get; set; }
		public string PatientId { get; set; }
		public Status Status { get; set; }
		public Guid StatusId { get; set; }

        public string Name { get; set; }

    }

}
