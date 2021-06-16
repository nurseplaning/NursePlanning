﻿using System.Collections.Generic;

namespace DomainModel
{
    public class Patient : Person
    {
        public int SocialSecurityNumber { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
