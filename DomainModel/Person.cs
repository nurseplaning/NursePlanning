using Microsoft.AspNetCore.Identity;
using System;

namespace DomainModel
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string ComplementaryAdressInformation { get; set; }
        public bool IsActive { get; set; }
    }
}