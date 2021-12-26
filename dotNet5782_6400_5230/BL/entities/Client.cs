using System;
using System.Collections.Generic;
using System.Text;

namespace BO
    {
        public class Client
        {
            public Client()
            {
                ID = 0;
                name = null;
                phoneNumber =0; 
                thisLocation = new location();
            }
            public int ID { get; set; }
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public location thisLocation { get; set; }
            
            public List<PackageInClient> packageFrom = new List<PackageInClient>();
            public List<PackageInClient> packageTo = new List<PackageInClient>();
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
