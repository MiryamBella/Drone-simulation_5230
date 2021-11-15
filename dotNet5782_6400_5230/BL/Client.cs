using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class client
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public location thisLocation { get; set; }
            
            public List<package> packageFrom = new List<package>();
            public List<package> packageTo = new List<package>();
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "name: " + name + '\n' +
                    "phone number: " + phoneNumber + '\n' +
                    "location: " + thisLocation.ToString() + '\n' +
                    "package that send from this client:" + packageFrom + '\n' +
                    "package that send to this client:" + packageTo + '\n');
            }
        }
    }
}
