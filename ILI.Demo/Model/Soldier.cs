using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILI.Demo.Model
{
    public class Soldier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Country { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
