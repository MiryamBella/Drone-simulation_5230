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
        //public int battery { get; set; }           /*in targil 2, we need to delate this methods.*/
        //public statusOfQ  mode { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return ("ID: " + id + '\n' +
                "Moodle: " + moodle + '\n' +
                "Weight: " + weight + '\n');
            //"Battery: " + battery + '\n' +
            //"Mode: " + mode + '\n');
        }
    }
}

