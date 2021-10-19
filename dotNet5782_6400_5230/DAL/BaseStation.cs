using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
        {
            public int IDnumber { get; set; }
            public string name { get; set; }
            public int chargingPositions { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
            public override string ToString() 
            {
                return ("ID: " + IDnumber + '\n' +
                    "name: " + name + '\n' +
                    "number of charging positins: " + chargingPositions + '\n' +
                    "longitude: " + longitude + '\n' +
                    "latitude: " + latitude + '\n');
            }
        }
    }
}
