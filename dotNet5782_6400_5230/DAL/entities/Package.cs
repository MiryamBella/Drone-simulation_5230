using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Package : ICloneable
    {
        public int id { get; set; }
        /// <summary>
        /// The ID of the sender of the packagh.
        /// </summary>
        public int sender { get; set; }
        /// <summary>
        /// The ID of the reciever of the packagh.
        /// </summary>
        public int receiver { get; set; }
        public WeighCategories weight { get; set; }
        /// <summary>
        /// The typ of this packagh: reggular, fast, emergency.
        /// </summary>
        public Priorities priority { get; set; }
        public int idQuadocopter { get; set; }
        public DateTime? time_Create { get; set; }
        public DateTime? time_Belong_quadocopter { get; set; }
        /// <summary>
        /// The time the packagh have been colected from the person who send the packagh.
        /// </summary>
        public DateTime? time_ColctedFromSender { get; set; }
        public DateTime? time_ComeToColcter { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //return the information as string.
        public override string ToString()
        {
            return ("ID: " + id + '\n' +
                "Sender: " + sender + '\n' +
                "Receiver: " + receiver + '\n' +
                "Weight: " + weight + '\n' +
                "Priority: " + priority + '\n' +
                "ID Quadocopter: " + idQuadocopter + '\n');
        }
    }
}
    

