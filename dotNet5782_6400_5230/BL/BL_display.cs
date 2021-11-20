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
            //BaseStation bs = new BaseStation();
            if (id <= 0)
                throw new BLException("Invalid id.");
            var bs = dal.StationDisplay(id);
            if (bs.IDnumber <= 0)
                throw new BLException("Id not found");
            return cover(bs);
            ///find if the base station in the data base.
            //IEnumerable<IDAL.DO.BaseStation> bs_list = dal.ListOfStations();
            //bool exsit = false;
            //foreach (IDAL.DO.BaseStation bs_toList in bs_list)
            //{
            //    if (bs_toList.IDnumber ==id)
            //    {
            //        exsit = true;
            //        bs = cover(bs_toList);
            //        break;
            //    }
            //}
            //if (!exsit)
            //    Console.WriteLine("error");

            //return bs;
        }
        public Quadocopter QuDisplay(int id)
        {
            Quadocopter returnQ = new Quadocopter();
            if (id <= 0)
                new BLException("Invalid id.");
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
                new BLException("The quadocopter not exist.");
            IEnumerable<IDAL.DO.Package> p_list = dal.ListOfPackages();
            foreach (IDAL.DO.Package p in p_list)
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
            //Client returnC = new Client();
            if(id<=0)
                new BLException("Invalid id.");
            var client = dal.ClientDisplay(id);
            if (client.ID <= 0)
                throw new BLException("Id not found.");
            return cover(client);
            //List<IDAL.DO.Client> c_l = new List<IDAL.DO.Client>();
            //c_l = dal.ListOfClients();
            //bool exist = false;
            //foreach(IDAL.DO.Client c in c_l)
            //{
            //    if (c.ID == id)
            //    {
            //        exist = true;
            //        returnC = cover(c);
            //    }
            //}
            //if(!exist)
            //    new BLException("The client not exist.");

            //return returnC;
        }
        public Package PackageDisplay(int id)
        {
            
            //Package returnP = new Package();
            if (id <= 0)
                new BLException("Invalid id.");
            var pack = dal.PackageDisplay(id);
            if (pack.id <= 0)
                throw new BLException("Id not found.");
            return cover(pack);
            //List<IDAL.DO.Package> p_list = new List<IDAL.DO.Package>();
            //p_list = dal.ListOfPackages();
            //bool exist = false;
            //foreach (IDAL.DO.Package p in p_list)
            //{
            //    if (p.id == id)
            //    {
            //        exist = true;
            //        returnP = cover(p);
            //    }
            //}
            //if(!exist)
            //    new BLException("The package not exist..");
            //return returnP;
        }

        public List<BO.BaseStationToList> ListOfBaseStations()
        {
            List<BaseStationToList> bs_l = new List<BaseStationToList>();

            IEnumerable<IDAL.DO.BaseStation> bs_list = dal.ListOfStations();
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
        public List<BO.ClientToList> ListOfClients()
        {
            List<ClientToList> new_l = new List<ClientToList>();

            IEnumerable<IDAL.DO.Client> old_list = dal.ListOfClients();
            foreach (IDAL.DO.Client c in old_list)
            {
                ClientToList temp = new ClientToList();
                temp.ID = c.ID;
                temp.name = c.name;
                temp.phoneNumber = c.phoneNumber;
                temp.setAndNotDeliverP = 0;
                temp.setAndDeliverP = 0;
                temp.getAndNotDeliverP=0;
                temp.getAndDeliverP = 0;

                IEnumerable<IDAL.DO.Package> p_list = dal.ListOfPackages();
                foreach(IDAL.DO.Package p in p_list)
                {
                    if (p.sender == c.ID)
                    {
                        if (p.time_ColctedFromSender == null)
                            temp.setAndNotDeliverP++;
                        else
                            temp.setAndDeliverP++;
                    }
                    if (p.receiver == c.ID)
                    {
                        if (p.time_ColctedFromSender == null)
                            temp.getAndNotDeliverP++;
                        else
                            temp.getAndDeliverP++;
                    }

                }

                new_l.Add(temp);
            }

            return new_l;

        }
        public List<BO.QuadocopterToList> ListOfQ()
        {
            List<QuadocopterToList> q_l = new List<QuadocopterToList>();

            IEnumerable<IDAL.DO.Quadocopter> stractQ_list = dal.ListOfQ();
            foreach (IDAL.DO.Quadocopter q in stractQ_list)
            {
                QuadocopterToList temp = new QuadocopterToList();
                temp = cover_list(q);

                q_l.Add(temp);
            }

            return q_l;
        }
        /// return list of all the packages.
        public List<BO.PackageToList> ListOfPackages()
        {
            List<PackageToList> p_l = new List<PackageToList>();

            IEnumerable<IDAL.DO.Package> stractP_list = dal.ListOfPackages();
            foreach (IDAL.DO.Package p in stractP_list)
            {
                Package temp = new Package();
                temp = cover(p);
                PackageToList p_tl = new PackageToList();
                p_tl.ID = temp.ID;
                p_tl.priority = temp.priority;
                p_tl.receiverName = temp.receiver.name;
                p_tl.senderName = temp.sender.name;
                //p_tl.state = temp;     need to fix//*************************************!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!************************************!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*****************************************************
                p_tl.weight = temp.weight;

                p_l.Add(p_tl);
            }

            return p_l;
        }
        /// return list of all the packages that dont assigned to quadocopter.
        public List<BO.PackageToList> ListOfPwithoutQ()
        {
            List<PackageToList> p_l = new List<PackageToList>();

            IEnumerable<IDAL.DO.Package> stractP_list = dal.ListOfPackages();
            foreach (IDAL.DO.Package p in stractP_list)
            {
                if (p.idQuadocopter == 0)
                {

                    Package temp = new Package();
                    temp = cover(p);
                    PackageToList p_tl = new PackageToList();
                    p_tl.ID = temp.ID;
                    p_tl.priority = temp.priority;
                    p_tl.receiverName = temp.receiver.name;
                    p_tl.senderName = temp.sender.name;
                    //p_tl.state = temp;     need to fix//*************************************!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!************************************!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*****************************************************
                    p_tl.weight = temp.weight;

                    p_l.Add(p_tl);
                }
            }

            return p_l;

        }
        /// return list of all the stations that have empty changing positions.
        public List<BO.BaseStationToList> ListOfStationsForCharging()
        {
            List<BaseStationToList> bs_l = new List<BaseStationToList>();

            IEnumerable<IDAL.DO.BaseStation> bs_list = dal.ListOfStationsForCharging();
            foreach (IDAL.DO.BaseStation bs in bs_list)
            {
                if (bs.freechargingPositions != 0)
                {
                    BaseStationToList temp = new BaseStationToList();
                    temp.ID = bs.IDnumber;
                    temp.name = bs.name;
                    temp.busyChargingPositions = bs.chargingPositions - bs.freechargingPositions;
                    temp.freeChargingPositions = bs.freechargingPositions;

                    bs_l.Add(temp);
                }
            }

            return bs_l;

        }
    }
}