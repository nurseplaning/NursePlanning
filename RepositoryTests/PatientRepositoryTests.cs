using Dal;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Tests
{
    [TestClass()]
    public class PatientRepositoryTests
    {
        [TestMethod]
        public async void ListPatientsTest()
        {
            // Arrange
            PatientRepository patientRepository = new(new ApplicationDbContext());
            // Act
            var liste = await patientRepository.ListPatients();
            // Assert
            Assert.IsInstanceOfType(liste, typeof(IEnumerable<Patient>));
        }
    }
}