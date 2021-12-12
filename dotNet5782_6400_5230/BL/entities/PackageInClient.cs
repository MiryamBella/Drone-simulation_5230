using System;
using System.Collections.Generic;
using System.Text;

namespace BO
    {
        public class PackageInClient
        {
            public int ID { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public stateOfP state { get; set; }
            public clientInPackage theOtherClient { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "the weight: " + weight + '\n' +
                    "the priority: " + priority + '\n' +
                    "the state: " + state + '\n' +
                    "the other client (sender/receiver): " + theOtherClient + '\n');
            }
        }
    }

