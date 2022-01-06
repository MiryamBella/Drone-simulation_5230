using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Quadocopter : ICloneable
    {
        public int id { get; set; }
        public string moodle { get; set; }
        public WeighCategories weight { get; set; }
        public DateTime? startCharge { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return ("ID: " + id + '\n' +
                "Moodle: " + moodle + '\n' +
                "Weight: " + weight + '\n');
        }
    }
}

