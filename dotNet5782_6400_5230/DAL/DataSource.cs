using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    internal class DataSource // evrething is new in this class.
    {
 
        /* i warite arrys and not lists because i think it will be more easy for us to get the data.
         */
        internal static Quadocopter[] qpter = new Quadocopter[10];
        internal static BaseStation[] bstion = new BaseStation[5];
        internal static Client[] cli = new Client[100];
        static Packagh[] packagh = new Packagh[1000];

        internal class Config
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
            //i reset the arrys.
            bstion[Config.index_baseStation++] = new BaseStation();//1
            bstion[Config.index_baseStation++] = new BaseStation();//2

            qpter[Config.index_quadocopter++] = new Quadocopter();//1
            qpter[Config.index_quadocopter++] = new Quadocopter();//2
            qpter[Config.index_quadocopter++] = new Quadocopter();//3
            qpter[Config.index_quadocopter++] = new Quadocopter();//4
            qpter[Config.index_quadocopter++] = new Quadocopter();//5

            cli[Config.index_client++] = new Client();//1
            cli[Config.index_client++] = new Client();//2
            cli[Config.index_client++] = new Client();//3
            cli[Config.index_client++] = new Client();//4
            cli[Config.index_client++] = new Client();//5
            cli[Config.index_client++] = new Client();//6
            cli[Config.index_client++] = new Client();//7
            cli[Config.index_client++] = new Client();//8
            cli[Config.index_client++] = new Client();//9
            cli[Config.index_client++] = new Client();//10

            packagh[Config.index_packagh++] = new Packagh();//1
            packagh[Config.index_packagh++] = new Packagh();//2
            packagh[Config.index_packagh++] = new Packagh();//3
            packagh[Config.index_packagh++] = new Packagh();//4
            packagh[Config.index_packagh++] = new Packagh();//5
            packagh[Config.index_packagh++] = new Packagh();//6
            packagh[Config.index_packagh++] = new Packagh();//7
            packagh[Config.index_packagh++] = new Packagh();//8
            packagh[Config.index_packagh++] = new Packagh();//9
            packagh[Config.index_packagh++] = new Packagh();//10
        }

    }

}
