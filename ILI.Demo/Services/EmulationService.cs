using ILI.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ILI.Demo.Services
{
    public class EmulationService : IEmulationService
    {
        System.Timers.Timer _timer = new System.Timers.Timer(1000);
        private readonly Random random = new Random();

        public event Action<Soldier> LocationUpdated;

        Dictionary<int, Location> _soldierLocations = new Dictionary<int, Location>();
        List<Soldier> _soldiers = new List<Soldier>();
        int _soldierCount = 1000;

        public EmulationService() 
        {
            InitializeSoldiers(_soldierCount);
        }

        public void StartEmulation()
        {
            _timer.Enabled = true;
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            EmulateSoldierLocations(_soldierCount);
        }

        public void EmulateSoldierLocations(int soldierCount)
        {
            for (int i = 1; i <= soldierCount; i++)
            {
                if (_soldierLocations.ContainsKey(i))
                {
                    _soldierLocations[i] = GenerateRandomLocation(_soldierLocations[i].Latitude, _soldierLocations[i].Longitude, 20);
                }
                else
                {
                    Location newLoc = GenerateRandomLocation(46.9470672, 7.4461226, 20);
                    _soldierLocations.Add(i, newLoc);
                }
                LocationUpdated?.Invoke(new Soldier() { Id = i, Location = _soldierLocations[i] });
            }
        }

        private Location GenerateRandomLocation(double centerLatitude, double centerLongitude, double radiusInMeters)
        {
            double radiusInDegrees = radiusInMeters / 111300.0;

            double latOffset = random.NextDouble() * radiusInDegrees;
            double lonOffset = random.NextDouble() * radiusInDegrees;

            latOffset *= random.Next(2) == 1 ? 1 : -1;
            lonOffset *= random.Next(2) == 1 ? 1 : -1;

            double newLatitude = centerLatitude + latOffset;
            double newLongitude = centerLongitude + lonOffset;

            return new Location(newLatitude, newLongitude);
        }

        public Dictionary<int, Location> GetLocations()
        {
            return _soldierLocations;
        }

        private void InitializeSoldiers(int soldierCount)
        {
            _soldiers = new List<Soldier>();
            for (int i = 1; i <= soldierCount; i++)
            {
                Soldier soldier = new Soldier()
                {
                    Id = i,
                    Name = $"Soldier {i}",
                    Country = "Switzerland",
                    Rank = $"{i%10}", 
                    Training = new Training()
                    {
                        Name = "Test training"
                    }
                };
                _soldiers.Add(soldier);
            }
        }

        public Soldier GetSoldier(int id)
        {
            return _soldiers.FirstOrDefault(x => x.Id == id);
        }
    }
}
