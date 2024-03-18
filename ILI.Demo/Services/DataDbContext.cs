using ILI.Demo.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ILI.Demo.Services
{
    public class DataDbContext : DbContext
    {
        public DbSet<Soldier> Soldiers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Training> Trainings { get; set; }

        public DataDbContext() : base("name=DataDbContext")
        {
        }
    }
}
