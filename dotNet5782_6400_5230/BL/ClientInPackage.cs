﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class clientInPackage
        {
            public int ID { get; set; }
            public string name { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "name: " + name + '\n');
            }
        }
    }
}
