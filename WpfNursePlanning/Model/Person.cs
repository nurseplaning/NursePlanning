using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNursePlanning.Model
{
	public class Person
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDay { get; set; }
		public string Adress { get; set; }

		public bool IsActive { get; set; }
	}
}
