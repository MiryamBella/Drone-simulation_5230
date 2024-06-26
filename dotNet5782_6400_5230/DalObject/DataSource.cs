﻿using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace Dal
{
    internal class DataSource
    {
        internal static List<Quadocopter> qpter = new List<Quadocopter>();
        internal static List<BaseStation> bstion = new List<BaseStation>();
        internal static List<Client> cli = new List<Client>();
        internal static List<Package> packagh = new List<Package>();

        /// <summary>
        /// List of all the quadocopter that are charging.
        /// This for the funcs "SendQtoCharging" and "ReleaseQfromCharging".
        /// </summary>
        internal static List<Charging> charge=new List<Charging>();
        internal class Config
        {
            internal static int runNum = 1;//for the id packagh.
                                           //i did the integger as static becuse i think it need to be only one like that because any id need to be difrent.
            internal static double Available { get { return 1; } }
            internal static double easy { get { return 2; } }
            internal static double middle_toCare { get { return 3; } }
            internal static double hevy { get { return 4; } }
            internal static double charghingRate { get { return 5; } }
        }

        /// <summary>
        /// The func reset the arrys to random data.
        /// It reset 2 base station, 5 quadocopter, 10 clients and 10 packagh.
        /// </summary>
        internal static void Initialize()
        {
            //i reset the arrys.
            Random r = new Random();
            #region base station
            BaseStation b = new BaseStation();//1
            b.IDnumber = 100;
            b.name = "Jerusalem";
            int x = r.Next(1000);
            b.chargingPositions = x;
            b.freechargingPositions = x;
            b.longitude = r.Next(10);
            b.latitude = r.Next(10);
            b.toBaseSix = new BaseSixtin();
            b.decSix = new DmsLocation();
            b.decSix = b.toBaseSix.LocationSix(b.latitude, b.longitude);
            bstion.Add(b);


            BaseStation baseStation = new BaseStation();//2
            baseStation.IDnumber = 101;
            baseStation.name = "Tel Aviv";
            x = r.Next(1000);
            baseStation.chargingPositions = x;
            baseStation.freechargingPositions = x;
            baseStation.longitude = r.Next(10);
            baseStation.latitude = r.Next(10);
            baseStation.toBaseSix = new BaseSixtin();
            baseStation.decSix = new DmsLocation();
            baseStation.decSix = baseStation.toBaseSix.LocationSix(baseStation.latitude, baseStation.longitude);
            bstion.Add(baseStation);
            #endregion
            #region Quadocopter
            Quadocopter qa = new Quadocopter();//1
            qa.id = 100;
            qa.moodle = "a";
            qa.weight = WeighCategories.easy;
            ///qa.battery = r.Next(0, 101);
            ///qa.mode = statusOfQ.available;
            qpter.Add(qa);

            Quadocopter qb = new Quadocopter();//2
            qb = new Quadocopter();
            qb.id = 101;
            qb.moodle = "b";
            qb.weight = WeighCategories.hevy;
            ///qb.battery = r.Next(0, 101);
            ///qb.mode = statusOfQ.maintenance;
            qpter.Add(qb);

            Quadocopter qc = new Quadocopter();//3
            qc = new Quadocopter();
            qc.id = 102;
            qc.moodle = "c";
            qc.weight = WeighCategories.middle;
            ///qc.battery = r.Next(0, 101);
            ///qc.mode = statusOfQ.delivery;
            qpter.Add(qc);

            Quadocopter qd = new Quadocopter();//4
            qd = new Quadocopter();
            qd.id = 103;
            qd.moodle = "d";
            qd.weight = (WeighCategories)r.Next(0, 3);
            qpter.Add(qd);

            Quadocopter qe = new Quadocopter();//5
            qe = new Quadocopter();
            qe.id = 104;
            qe.moodle = "e";
            qe.weight = (WeighCategories)r.Next(0, 3);
            ///qe.battery = r.Next(0, 101);
            ///qe.mode = (statusOfQ)r.Next(0, 3);
            qpter.Add(qe);
            #endregion

            /*client*/
            for (int i = 1; i <= 10; i++)
            {
                Client c = new Client();
                c.ID = (int)(i * 100000000);
                c.name = getRandomName(i);
                c.phoneNumber = r.Next(100000000, 999999999);
                c.latitude = r.Next(10);
                c.longitude = r.Next(10);
                cli.Add(c);
            }


            /*Packagh*/
            //loop for reset all 10 packaghs.
            for (int i = 0; i < 9; i++)
            {
                Package p = new Package();
                p.id = Config.runNum;
                Config.runNum++;
                p.sender = cli[r.Next(0, cli.Count)].ID;
                p.receiver = cli[r.Next(0, cli.Count)].ID;
                p.weight = (WeighCategories)r.Next(1, 3);
                p.priority = (Priorities)r.Next(0, 3);
                p.idQuadocopter = 0;
                p.time_Belong_quadocopter = null;
                p.time_ColctedFromSender = null;
                p.time_ComeToColcter = null;
                p.time_Create = DateTime.Now;

                //the 3 package with difrent privorities and WeighCategories.
                if (i == 0)
                {
                    p.weight = WeighCategories.hevy;
                    p.priority = Priorities.fast;

                }
                else if (i == 1)
                {
                    p.weight = WeighCategories.easy;
                    p.priority = Priorities.emergency;

                }
                else if (i == 2)
                {

                    p.weight = WeighCategories.middle;
                    p.priority = Priorities.reggular;

                }
                packagh.Add(p);
            }


        }


        //-------------------funcs that exsit for us-----------------------------------------------

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

    }

}
