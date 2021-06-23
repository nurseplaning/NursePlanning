using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Absence
    {
        public Guid Id { get; set; }
        public DateTime Date{ get; set; }
        public string Motif { get; set; }

        public virtual Nurse Nurse { get; set; }
        public virtual string NurseId { get; set; }
    }
}