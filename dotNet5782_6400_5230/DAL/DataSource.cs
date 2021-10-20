using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    internal class DataSource
    {
        // i warite arrys and not lists because i think it will be more easy for us to get the data.
        internal static Quadocopter[] qpter = new Quadocopter[10];
        internal static BaseStation[] bstion = new BaseStation[5];
        internal static Client[] cli = new Client[100];
        static Packagh[] packagh = new Packagh[1000];

        internal static class Config //new evrething in this class.
        {
            //i write all the indexs if we will.
            internal static int index_quadocopter = 0;
            internal static int index_baseStation = 0;
            internal static int index_client = 0;
            internal static int index_packagh = 0;

            internal static int runNum = 0;//for the id packagh.
                                  //i did the integger as static becuse i think it need to be only one like that because any id need to be difrent.
                           
            
        }
        static void Initialize()
        {
            packagh[0] = new Packagh();
            index_packagh++;

        }

    }

}
