using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class QuadocopterInCharge
        {
            public int ID { get; set; }
            public int battery { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "the state of the bettery: " + battery + '\n');
            }
        }
    }
}
