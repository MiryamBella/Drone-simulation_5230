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

        static int findQuadocopter()
        {
            Random r = new Random();
            for(int i = 0; 0 < Config.index_quadocopter; i++)
            {
                int j = r.Next(0, Config.index_quadocopter);
                if (qpter[j].mode != statusOfQ.maintenance)
                    return j;
            }
            return 0;
        }
        /// <summary>
        /// The func reset the arrys to random data.
        /// It reset 2 base station, 5 quadocopter, 10 clients and 10 packagh.
        /// </summary>
        /*internal*/ static void Initialize()
        {
            //i reset the arrys.
            Random r = new Random();
            int i;

            /*base station*/
            i = Config.index_baseStation++;

            bstion[i] = new BaseStation();//1
            bstion[i].IDnumber = i;
            bstion[i].name = "Jerusalem";
            bstion[i].chargingPositions = r.Next();
            bstion[i].longitude = r.Next();
            bstion[i].latitude = r.Next();

            i = Config.index_baseStation++;
            bstion[i] = new BaseStation();//2
            bstion[i].IDnumber = i;
            bstion[i].name = "Tel Aviv";
            bstion[i].chargingPositions = r.Next();
            bstion[i].longitude = r.Next();
            bstion[i].latitude = r.Next();


            /*Quadocopter*/
            i = Config.index_quadocopter++;//1
            qpter[i] = new Quadocopter();
            qpter[i].id = i;
            qpter[i].moodle = "a";
            qpter[i].weight = WeighCategories.easy;
            qpter[i].battery = r.Next(0, 101);
            qpter[i].mode = statusOfQ.available;

         i = Config.index_quadocopter++;//2
            qpter[i] = new Quadocopter();
            qpter[i].id = i;
            qpter[i].moodle = "b";
            qpter[i].weight = WeighCategories.hevy;
            qpter[i].battery = r.Next(0, 101);
            qpter[i].mode = statusOfQ.maintenance;

            i = Config.index_quadocopter++;//3
            qpter[i] = new Quadocopter();
            qpter[i].id = i;
            qpter[i].moodle = "c";
            qpter[i].weight = WeighCategories.middle;
            qpter[i].battery = r.Next(0, 101);
            qpter[i].mode = statusOfQ.delivery;

            i = Config.index_quadocopter++;//4
            qpter[i] = new Quadocopter();
            qpter[i].id = i;
            qpter[i].moodle = "d";
            qpter[i].weight = (WeighCategories)r.Next(0, 3);
            qpter[i].battery = r.Next(0, 101);
            qpter[i].mode = (statusOfQ)r.Next(0, 3);

            i = Config.index_quadocopter++;//5
            qpter[i] = new Quadocopter();
            qpter[i].id = i;
            qpter[i].moodle = "e";
            qpter[i].weight = (WeighCategories)r.Next(0, 3);
            qpter[i].battery = r.Next(0, 101);
            qpter[i].mode = (statusOfQ)r.Next(0, 3);


            /*client*/

            i = Config.index_client++;//1
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "David";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//2
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Miryam";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//3
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Rachel";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//4
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "David";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//5
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Shara";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//6
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Rebeka";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//7
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Lea";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//8
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Yosy";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();

            i = Config.index_client++;//9
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Yonatan";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();


            i = Config.index_client++;//10
            cli[i] = new Client();
            cli[i].ID = i;
            cli[i].name = "Moshe";
            cli[i].phoneNumber = r.Next();
            cli[i].latitude = r.Next();
            cli[i].longitude = r.Next();


            /*Packagh*/

            i = Config.index_packagh++;//1
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Shara";
            packagh[i].receiver = "Rebeka";
            packagh[i].weight = WeighCategories.easy;
            packagh[i].priority = Priorities.emergency;
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;//2
            packagh[i] = new Packagh();
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Miryam";
            packagh[i].receiver = "Rachel";
            packagh[i].weight = WeighCategories.hevy;
            packagh[i].priority = Priorities.fast;
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;//3
            packagh[i] = new Packagh();
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Yosy";
            packagh[i].receiver = "Yonatan";
            packagh[i].weight = WeighCategories.middle;
            packagh[i].priority = Priorities.reggular;
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;//4
            packagh[Config.index_packagh++] = new Packagh();
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Simcha";
            packagh[i].receiver = "Avraham";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();


            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//5
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "David";
            packagh[i].receiver = "Yosef";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//6
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Mendy";
            packagh[i].receiver = "Menachem";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//7
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Root";
            packagh[i].receiver = "Lea";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//8
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Shira";
            packagh[i].receiver = "Rebeka";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//9
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Shara";
            packagh[i].receiver = "Bella";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

            i = Config.index_packagh++;
            packagh[Config.index_packagh++] = new Packagh();//10
            packagh[i] = new Packagh();
            packagh[i].id = i;
            packagh[i].sender = "Rachel";
            packagh[i].receiver = "Rebeka";
            packagh[i].weight = (WeighCategories)r.Next(0, 3);
            packagh[i].priority = (Priorities)r.Next(0, 3);
            packagh[i].idQuadocopter = findQuadocopter();

        }

    }

}
