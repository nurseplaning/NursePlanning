using System.Collections.Generic;

namespace DomainModel
{
    public class Patient : Person
    {
        public string SocialRegime { get; set; }
        public bool IsParkingAvailable { get; set; }
        public ICollection<Appointment> Appointments { get; } = new List<Appointment>(); // Collection navigation containing dependents
    }
}