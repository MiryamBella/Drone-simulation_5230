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
            public int longitude { get; set; }
            public int latitude { get; set; }
            public ToString() { }
        }
    }
}
