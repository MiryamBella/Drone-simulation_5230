using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Packagh
        { 
            public int id { get; set; }
            public string sender { get; set; }
            public string receiver { get; set; }
            public WeighCategories weight { get; set; }
            public Priorities priority { get; set; }
            public int idQuadocopter { get; set; }

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
