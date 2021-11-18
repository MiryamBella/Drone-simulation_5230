using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        public BaseStation baseStationDisplay(int id)
        {
            BaseStation bs = new BaseStation();
            if (bs.ID <= 0)
                Console.WriteLine("error");
            ///find if the base station in the data base.
            List<IDAL.DO.BaseStation> bs_list = new List<IDAL.DO.BaseStation>();
            bs_list = dal.ListOfStations();
            bool exsit = false;
            foreach (IDAL.DO.BaseStation bs_toList in bs_list)
            {
                if (bs_toList.IDnumber ==id)
                {
                    exsit = true;
                    bs = cover(bs_toList);
                    break;
                }
            }
            if (!exsit)
                Console.WriteLine("error");

            return bs;
        }
        public Quadocopter QuDisplay(int id)
        {
            Quadocopter returnQ = new Quadocopter();
            if (id <= 0)
                Console.WriteLine("error");
            bool exist = false;
            foreach (QuadocopterToList q in q_list)
            {
                if (q.ID == id)
                {
                    exist = true;
                    returnQ.ID = q.ID;
                    returnQ.mode = q.mode;
                    returnQ.moodle = q.moodle;
                    returnQ.thisLocation = q.thisLocation;
                    returnQ.weight = q.weight;
                    returnQ.battery = q.battery;
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("error");
            List<IDAL.DO.Packagh> p_list = new List<IDAL.DO.Packagh>();
            p_list = dal.ListOfPackages();
            foreach (IDAL.DO.Packagh p in p_list)
            {
                if (p.idQuadocopter == id)
                {
                    PackageInTrans transP = new PackageInTrans();
                    transP.ID = p.id;
                    transP.priority = (Priorities)p.priority;
                    if (returnQ.mode == statusOfQ.delivery)
                        transP.ifOnTheWay = true;
                    else
                        transP.ifOnTheWay = false;
                    transP.receiver = ClientDisplay(p.receiver);
                    transP.sender = ClientDisplay(p.sender);
                    transP.weight = (WeighCategories)p.weight;
                    transP.collection = transP.sender.thisLocation;
                    transP.destination = transP.receiver.thisLocation;

                    returnQ.thisPackage = transP;
                }
            }
            return returnQ;
        }
        public Client ClientDisplay(int id)
        {
            Client returnC = new Client();
            if(id<=0)
                Console.WriteLine("error");
            List<IDAL.DO.Client> c_l = new List<IDAL.DO.Client>();
            c_l = dal.ListOfClients();
            bool exist = false;
            foreach(IDAL.DO.Client c in c_l)
            {
                if (c.ID == id)
                {
                    exist = true;
                    returnC = cover(c);
                }
            }
            if(!exist)
                Console.WriteLine("error");

            return returnC;
        }
        public Package PackageDisplay(int id)
        {
            Package returnP = new Package();
            if (id <= 0)
                Console.WriteLine("error");

            List<IDAL.DO.Packagh> p_list = new List<IDAL.DO.Packagh>();
            p_list = dal.ListOfPackages();
            bool exist = false;
            foreach (IDAL.DO.Packagh p in p_list)
            {
                if (p.id == id)
                {
                    exist = true;
                    returnP = cover(p);
                }
            }
            if(!exist)
                Console.WriteLine("error");
            return returnP;
        }

        public List<BO.BaseStationToList> ListOfBaseStations()
        {
            List<BaseStationToList> bs_l = new List<BaseStationToList>();

            List<IDAL.DO.BaseStation> bs_list = new List<IDAL.DO.BaseStation>();
            bs_list = dal.ListOfStations();
            foreach (IDAL.DO.BaseStation bs in bs_list)
            {
                BaseStationToList temp = new BaseStationToList();
                temp.ID = bs.IDnumber;
                temp.name = bs.name;
                temp.busyChargingPositions = bs.chargingPositions - bs.freechargingPositions;
                temp.freeChargingPositions = bs.freechargingPositions;

                bs_l.Add(temp);
            }

            return bs_l;
        }
            /// return list of all the clients
            public List<BO.ClientToList> ListOfClients();
            public List<BO.QuadocopterToList> ListOfQ();
        /// return list of all the packages.
        public List<BO.PackageToList> ListOfPackages();
        /// return list of all the packages that dont assigned to quadocopter.
        public List<BO.PackageToList> ListOfPwithoutQ();
            /// return list of all the stations that have empty changing positions.
            public List<BO.BaseStationToList> ListOfStationsForCharging();

       
    }
}