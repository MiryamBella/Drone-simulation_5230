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
            bs = cover(dal.StationDisplay(id));
            if (bs.ID <= 0)
                Console.WriteLine("error");
            ///get all tha q that charging
            List<IDAL.DO.Charging> chrgh_list = new List<IDAL.DO.Charging>();
            chrgh_list = dal.GetChargings();
            foreach (IDAL.DO.Charging chargeh in chrgh_list)
            {
                ///find what q is gharge in our base station
                if (chargeh.baseStationID == bs.ID)
                {
                    bool exsit = false;
                    ///chek if the q is exsit in our data base.
                    foreach (QuadocopterToList q in q_list)
                    {

                        if (q.ID == chargeh.quadocopterID)
                        {
                            ///the q is exist.
                            exsit = true;
                            QuadocopterInCharge chargeQ = new QuadocopterInCharge();
                            chargeQ.battery = q.battery;
                            chargeQ.ID = q.ID;
                            bs.qudocopters.Add(chargeQ);
                            break;
                        }
                    }
                    if (!exsit)
                        Console.WriteLine("error");
                }

                return bs;
            }
        }
        public BO.Quadocopter QuDisplay(int id)
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
        public BO.Client ClientDisplay(int id)
        {




        }
            public BO.Package PackageDisplay(int id);

            public List<BO.BaseStationToList> ListOfBaseStations();
            /// print all the clients
            public List<BO.ClientToList> ListOfClients();
            public List<BO.QuadocopterToList> ListOfQ();
            /// print all the packages.
            public List<BO.PackageToList> ListOfPackages();
            /// print all the packages that dont assigned to quadocopter.
            public List<BO.PackageToList> ListOfPwithoutQ();
            /// return list of all the stations that have empty changing positions.
            public List<BO.BaseStationToList> ListOfStationsForCharging();

       
    }
}