﻿using Microsoft.AspNetCore.Identity;
using System;

namespace DomainModel
{
    public abstract class Person : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Adress { get; set; }
    }
}
