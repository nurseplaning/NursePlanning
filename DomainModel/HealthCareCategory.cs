using System;

namespace DomainModel
{
    public class HealthCareCategory
    {
        public HealthCareCategory() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}