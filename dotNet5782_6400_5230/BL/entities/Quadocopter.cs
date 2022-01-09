using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Quadocopter
    {
        public Quadocopter()
        {
            ID = 0; moodle = null; weight = 0; battery = 0; mode = 0;
            thisPackage = new PackageInTrans();
            thisLocation = new Location();
        }
        public int ID { get; set; }
        public string moodle { get; set; }
        public WeighCategories weight { get; set; }
        public int battery { get; set; }
        public statusOfQ mode { get; set; }
        public PackageInTrans thisPackage { get; set; }
        public Location thisLocation { get; set; }
        public override string ToString()
        {
            return ("ID: " + ID + '\n' +
                "the modle: " + moodle + '\n' +
                "the weight: " + weight + '\n' +
                "the state of the bettery: " + battery + '\n' +
                "the status: " + mode + '\n' +
                "the location: " + thisLocation + '\n' +
                "the ID of the package that it carry:" + thisPackage.ID);
        }
    }
}

