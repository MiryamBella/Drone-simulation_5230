using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class packageInClient
        {
            public int id { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public stateOfP state { get; set; }
            public clientInPackage theOtherClient { get; set; }
        }
    }
}
