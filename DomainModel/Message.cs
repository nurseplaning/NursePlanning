using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Person Person { get; set; }
        public string PersonId { get; set; }
        public Appointment Appointment { get; set; }
        public Guid AppointmentId { get; set; }

    }
}
