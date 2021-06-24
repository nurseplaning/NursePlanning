using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNursePlanning.Model
{
	public class Patient 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDay { get; set; }
		public string Adress { get; set; }
		public string SocialSecurityNumber { get; set; }
		public virtual ICollection<Appointment> Appointments { get; set; }
	}
}
