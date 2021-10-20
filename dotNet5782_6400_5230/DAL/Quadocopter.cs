using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Quadocopter
        {
            int id { get; set; }
            string moodle { get; set; }
            WeighCategories weight { get; set; }
            int battery { get; set; }
            string mode { get; set; }

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
