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
            public location place { get; set; }
            
            public List<package> packageFrom = new List<package>();
            public List<package> packageTo = new List<package>();

        }
    }
}
