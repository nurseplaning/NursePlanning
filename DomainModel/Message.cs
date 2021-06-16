using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table ("Messages")]
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

    }
}
