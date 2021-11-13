using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class package
        {
            public int id { get; set; }
            public client sender { get; set; }
            public client receiver { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public QuadocopterInPackage q { get; set; }
            public DateTime time_Create { get; set; }
            public DateTime time_Belong_quadocopter { get; set; }
            public DateTime time_ColctedFromSender { get; set; }
            public DateTime time_ComeToColcter { get; set; }

        }
    }
}
