using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class QuadocopterToList
    {
        public QuadocopterToList()
        {
            ID = 0; moodle = null; weight = 0; battery = 0; mode = 0; packageNumber = 0;
            thisLocation = new location();
        }
        public int ID { get; set; }
        public string moodle { get; set; }
        public WeighCategories weight { get; set; }
        public int battery { get; set; }
        public statusOfQ mode { get; set; }
        public int packageNumber { get; set; }
        public location thisLocation { get; set; }
        public override string ToString()
        {
            return ("ID: " + ID + '\n' +
                "the modle: " + moodle + '\n' +
                "the weight: " + weight + '\n' +
                "the state of the bettery: " + battery + '\n' +
                "the status: " + mode + '\n' +
                "the location: " + thisLocation + '\n' +
                "number of packages that it carried:" + packageNumber);
        }
    }
}
