using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class BaseStation
    {
        public BaseStation()
        {
            ID = 0; name = null; freeChargingPositions = 0; thisLocation = new Location();
        }
        public int ID { get; set; }
        public string name { get; set; }
        public int freeChargingPositions { get; set; }
        public Location thisLocation { get; set; }

        public List<QuadocopterInCharge> qudocopters = new List<QuadocopterInCharge>();
        public override string ToString()
        {
            return ("ID: " + ID + '\n' +
                "name: " + name + '\n' +
                "number of free charging positins: " + freeChargingPositions + '\n' +
                "location: " + thisLocation.ToString() + '\n' +
                "qudocopters in the station:" + qudocopters + '\n');
        }
    }
}

