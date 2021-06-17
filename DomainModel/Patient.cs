using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Patient : Person
    {
        public int SocialSecurityNumber { get; set; }
        public Nurse Nurse { get; set; }
        public string NurseId { get; set; }

    }
}
