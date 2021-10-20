using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Quadocopter
        {
            public int id { get; set; }
            public string moodle { get; set; }
            public WeighCategories weight { get; set; }
            public int battery { get; set; }
            public string mode { get; set; }

            public override string ToString()
            {
                return ("ID: " + id + '\n' +
                    "Moodle: " + moodle + '\n' +
                    "Weight: " + weight + '\n' +
                    "Battery: " + battery + '\n' +
                    "Mode: " + mode + '\n');
            }
        }
    }
}
