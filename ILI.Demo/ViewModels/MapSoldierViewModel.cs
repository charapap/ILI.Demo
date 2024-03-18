using ILI.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILI.Demo.ViewModels
{
    public class MapSoldierViewModel : ViewModelBase
    {
        public Soldier Soldier { get; set; }
        public string Name => Soldier.Name;
        public double Latitude => Soldier.Location.Latitude;
        public double Longitude => Soldier.Location.Longitude;
    }
}
