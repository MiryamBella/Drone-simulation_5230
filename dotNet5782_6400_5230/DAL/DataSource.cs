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
        internal static Packagh[] packagh = new Packagh[1000];

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

        /// <summary>
        /// The func reset the arrys to random data.
        /// It reset 2 base station, 5 quadocopter, 10 clients and 10 packagh.
        /// </summary>
        internal static void Initialize()
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
            for (i = Config.index_client; i < 10; i++)
            {
                cli[i] = new Client();
                cli[i].ID = i;
                cli[i].name = getRandomName(i);
                cli[i].phoneNumber = r.Next();
                cli[i].latitude = r.Next();
                cli[i].longitude = r.Next();
            }
            Config.index_client += 10;


            /*Packagh*/

            //loop for reset all 10 packaghs.
            for (i = Config.index_packagh; i < 10; i++)
            {
                packagh[i] = new Packagh();
                packagh[i].id = i;
                packagh[i].sender = r.Next();
                packagh[i].receiver = r.Next();
                packagh[i].weight = (WeighCategories)r.Next(0, 3);
                packagh[i].priority = (Priorities)r.Next(0, 3);
                packagh[i].idQuadocopter = findQuadocopter();
            }
            Config.index_packagh += 10;//change the index.

            //the 3 package with difrent privorities and WeighCategories.
            packagh[i-1].weight = WeighCategories.hevy;
            packagh[i-1].priority = Priorities.fast;

            packagh[i - 2].weight = WeighCategories.easy;
            packagh[i - 2].priority = Priorities.emergency;

            packagh[i - 3].weight = WeighCategories.middle;
            packagh[i - 3].priority = Priorities.reggular;
        }


        //-------------funcs that exsit for us-----------------------------------------------
        
        /*2 funcs for the reset func.*/
        static string getRandomName(int num)
        {
            num = num % 10;
            switch (num)
            {
                case 0:
                    return "Moshe";
                case 1: 
                    return "Miryam";
                case 2:
                    return "Rachel";
                case 3:
                    return "David";
                case 4:
                    return "Shara";
                case 5: 
                    return "Rebeka";
                case 6:
                    return "Lea";
                case 7:
                    return "Yosy";
                case 8:
                    return "Yonatan";
                case 9:
                    return "Ester";
                default:
                    return "Dina";
            }
        }
        static int findQuadocopter()
        {
            Random r = new Random();
            for (int i = 0; 0 < Config.index_quadocopter; i++)
            {
                int j = r.Next(0, Config.index_quadocopter);
                if (qpter[j].mode != statusOfQ.maintenance)
                    return j;
            }
            return 0;
        }

    }

}
