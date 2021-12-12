using System;
using System.Collections.Generic;
using System.Text;

namespace BO
    {
        public class QuadocopterInPackage
        {
            public QuadocopterInPackage()
            {
                ID = 0; battery = 0; thisLocation = new location();
            }
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

