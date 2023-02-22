using System;
using System.Data.Common;

namespace DomainModel
{
    public class HealthCare
    {
        public HealthCare() { }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        
    }
}