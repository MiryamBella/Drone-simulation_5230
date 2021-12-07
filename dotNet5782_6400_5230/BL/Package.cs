using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Package
        {
            public int ID { get; set; }
            public Client sender { get; set; }
            public Client receiver { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public QuadocopterInPackage q { get; set; }
            public DateTime? time_Create { get; set; }
            public DateTime? time_Belong_quadocopter { get; set; }
            public DateTime? time_ColctedFromSender { get; set; }
            public DateTime? time_ComeToColcter { get; set; }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "the sender: " + sender.name + '\n' +
                    "the receiver: " + receiver.name + '\n' +
                    "the weight: " + weight + '\n' +
                    "the priority: " + priority + '\n' +
                    "the time that it is created: " + time_Create + '\n' +
                    "the time that it is belonged to quadocopter: " + time_Belong_quadocopter + '\n' +
                    "the time that it is colected by quadocopter: " + time_ColctedFromSender + '\n' +
                    "the time that it is arrived to the distanation : " + time_ComeToColcter + '\n');
            }

        }
    }
}
