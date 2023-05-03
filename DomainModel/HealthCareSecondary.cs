using System;

namespace DomainModel
{
    public class HealthCareSecondary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HealthCarePrimaryId { get; set; }

    }
}