using ILI.Demo.Model;
using ILI.Demo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ILI.Demo.UT
{
    [TestClass]
    public class EmulationServiceTest
    {
        [TestMethod]
        public void EmulateSoldierLocations_GenerateLocations_LocationUpdated()
        {
            //Arrange
            IEmulationService emulationService = new EmulationService();
            int expectedSoldiersCount = 10;
            int locationUpdateEventCount = 0;
            emulationService.LocationUpdated += delegate (Soldier soldier)
            {
                locationUpdateEventCount++;
            };
            
            //Act
            emulationService.EmulateSoldierLocations(expectedSoldiersCount);
            var locations = emulationService.GetLocations();

            //Assert
            int actualSoldiersCount = locations.Count;
            Assert.AreEqual(expectedSoldiersCount, actualSoldiersCount);
            Assert.AreEqual(expectedSoldiersCount, locationUpdateEventCount);
        }
    }
}
