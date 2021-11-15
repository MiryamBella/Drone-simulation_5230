using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class QuadocopterInPackage
        {
            public int ID { get; set; }
            public int battery { get; set; }
            public location thisLocation { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "the state of the bettery: " + battery + '\n' +
                    "the location: " + thisLocation + '\n');
            }
        }
    }
}
