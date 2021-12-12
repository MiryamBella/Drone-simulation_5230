using System;
using System.Collections.Generic;
using System.Text;

namespace BO
    {
        public class BaseStationToList
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int freeChargingPositions { get; set; }
            public int busyChargingPositions { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "name: " + name + '\n' +
                    "number of free charging positins: " + freeChargingPositions + '\n' +
                    "number of busy charging positins: " + busyChargingPositions + '\n');
            }
        }
    }

