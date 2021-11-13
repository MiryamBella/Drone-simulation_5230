using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class PackageInTrans
        {
            public int id { get; set; }
            public client sender { get; set; }
            public client receiver { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public bool ifOnTheWay { get; set; }
            public location collection { get; set; }
            public location destination { get; set; }

        }
    }
}
