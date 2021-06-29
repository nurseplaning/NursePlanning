using Dal;
using DomainModel;
using NUnit.Framework;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Tests
{
}

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1Async()
        {
            // Arrange
            PatientRepository patientRepository = new(new ApplicationDbContext());
            // Act
            var liste = await patientRepository.ListPatients();
            // Assert
            Assert.IsInstanceOf(typeof(IEnumerable<Patient>),liste);
        }
    }
}