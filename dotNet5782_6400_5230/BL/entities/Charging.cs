﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Charging
    {
        public int baseStationID { get; set; }
        public int quadocopterID { get; set; }
        public override string ToString()
        {
            return ("ID of base station: " + baseStationID + '\n' +
                "ID of quadocopter: " + quadocopterID + '\n');
        }

    }
}
