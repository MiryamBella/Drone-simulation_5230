using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class packageToList
        {
            public int id { get; set; }
            public string senderName { get; set; }
            public string receiverName { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public stateOfP state { get; set; }
        }
    }
}
