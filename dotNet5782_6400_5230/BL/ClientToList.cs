﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ClientToList
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public int setAndDeliverP { get; set; }
            public int setAndNotDeliverP { get; set; }
            public int getAndDeliverP { get; set; } 
            public int getAndNotDeliverP { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "name: " + name + '\n' +
                    "phone number: " + phoneNumber + '\n' +
                    "number of packages that send from this client and delivered:" + setAndDeliverP + '\n' +
                    "number of packages that send from this client and not delivered:" + setAndNotDeliverP + '\n' +
                    "number of packages that send to this client and delivered:" + getAndDeliverP + '\n' +
                    "number of packages that send to this client and not delivered:" + getAndNotDeliverP + '\n');
            }
        }
    }
}
