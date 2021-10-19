using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public class Packagh
        { 
            int id { get; set; }
            string sender { get; set; }
            string receiver { get; set; }
            string weight{ get; set; }
            Priorities priority { get; set; }
            int idQuadocopter { get; set; }

            public override string ToString()
            {
                return ("ID: "+id+'\n'+
                    "Sender: " + sender+'\n'+
                    "Receiver: " +receiver +'\n'+
                    "Weight: " + weight+'\n'+
                    "Priority: " +priority+'\n'+
                    "ID Quadocopter: "+idQuadocopter+'\n');
            } 
        }
    }
    
}
