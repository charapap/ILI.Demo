using ILI.Demo.Model;
using System;
using System.Collections.Generic;

namespace ILI.Demo.Services
{
    public interface IEmulationService
    {
        event Action<Soldier> LocationUpdated;

        void EmulateSoldierLocations(int soldierCount);
        void StartEmulation();
        Dictionary<int, Location> GetLocations();
        Soldier GetSoldier(int id);
    }
}
