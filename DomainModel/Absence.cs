using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Absence
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public string Motif { get; set; }

        public virtual Nurse Nurse { get; set; }
        [Display(Name = "Infirmier(e)")]
        public virtual string NurseId { get; set; }
    }
}