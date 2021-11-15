using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class packageToList
        {
            public int ID { get; set; }
            public string senderName { get; set; }
            public string receiverName { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public stateOfP state { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "the sender: " + senderName + '\n' +
                    "the receiver: " + receiverName + '\n' +
                    "the weight: " + weight + '\n' +
                    "the priority: " + priority + '\n' +
                    "the state of the package " + state + '\n');
            }
        }
    }
}
