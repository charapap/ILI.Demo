﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILI.Demo.Model
{
    public class Location
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(double latitude, double longitude) 
        { 
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
