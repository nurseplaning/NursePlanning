using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DomainModel
{
    public class HealthCarePrimary
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

		public HealthCareSecondary HealthCareSecondaries { get; set; }
    }
}