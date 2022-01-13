using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;

namespace BlApi
{
    public partial class BL
    {
        #region display objects
        public BaseStation baseStationDisplay(int id)
        {
            if (id <= 0)
                throw new BLException("Invalid id.");
            DO.BaseStation bs=new DO.BaseStation();
            try
            {
                bs = dal.StationDisplay(id);
                return cover(bs);
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        public Quadocopter QuDisplay(int id)
        {
            Quadocopter returnQ = new Quadocopter();
            if (id <= 0)
                new BLException("Invalid ID.");
            returnQ = cover(dal.QuDisplay(id));
            return returnQ;
        }
        public Client ClientDisplay(int id)
        {
            if(id<=0)
                new BLException("Invalid id.");
            var client = dal.ClientDisplay(id);
            if (client.ID <= 0)
                throw new BLException("Id not found.");
            try
            {
                return cover(client);
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        public Package PackageDisplay(int id)
        {
            //Package returnP = new Package();
            if (id <= 0)
                throw new BLException("Invalid id.");
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

        #endregion

        #region normal lists
        public List<BO.BaseStationToList> ListOfBaseStations()
        {
            List<BaseStationToList> bs_l = new List<BaseStationToList>();

            IEnumerable<DO.BaseStation> bs_list = dal.ListOfStations();
            foreach (DO.BaseStation bs in bs_list)
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

            IEnumerable<DO.Client> old_list = dal.ListOfClients();
            foreach (DO.Client c in old_list)
            {
                ClientToList temp = new ClientToList();
                temp.ID = c.ID;
                temp.name = c.name;
                temp.phoneNumber = c.phoneNumber;
                temp.setAndNotDeliverP = 0;
                temp.setAndDeliverP = 0;
                temp.getAndNotDeliverP=0;
                temp.getAndDeliverP = 0;

                IEnumerable<DO.Package> p_list = dal.ListOfPackages();
                foreach(DO.Package p in p_list)
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

            List<QuadocopterToList> q_l = (from QuadocopterToList q in q_list
                                           select q).ToList();
            return q_l;
        }
        /// return list of all the packages.
        public List<BO.PackageToList> ListOfPackages()
        {
            List<PackageToList> p_l = new List<PackageToList>();

            IEnumerable<DO.Package> stractP_list = dal.ListOfPackages();
            foreach (DO.Package p in stractP_list)
            {
                Package temp = new Package();
                temp = cover(p);
                PackageToList p_tl = new PackageToList();
                p_tl.ID = temp.ID;
                p_tl.priority = temp.priority;
                p_tl.receiverName = temp.receiver.name;
                p_tl.senderName = temp.sender.name;

                ///find the state of the package.
                if (temp.time_ComeToColcter != null)
                    p_tl.state = stateOfP.provided;
                else if (temp.time_ColctedFromSender != null)
                    p_tl.state = stateOfP.collected;
                else if (temp.time_Belong_quadocopter != null)
                    p_tl.state = stateOfP.associated;
                else
                    p_tl.state = stateOfP.Defined;

                p_tl.weight = temp.weight;
                p_l.Add(p_tl);
            }

            return p_l;
        }

        #endregion

        #region some lists
        /// print all the quadocpters acording to the weigh.
        public List<BO.QuadocopterToList> ListOfQ_of_weigh(string w)
        {
            List<QuadocopterToList> q_l = new List<QuadocopterToList>();

            IEnumerable<DO.Quadocopter> stractQ_list = dal.ListOfQ_of_weigh(w);
            foreach (DO.Quadocopter q in stractQ_list)
            {
                QuadocopterToList temp = new QuadocopterToList();
                temp = cover_list(q);

                q_l.Add(temp);
            }

            return q_l;
        }
        /// return list of all the packages that dont assigned to quadocopter.
        public List<BO.PackageToList> ListOfPwithoutQ()
        {
            List<PackageToList> p_l = new List<PackageToList>();

            IEnumerable<DO.Package> stractP_list = dal.ListOfPackages();
            foreach (DO.Package p in stractP_list)
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

            IEnumerable<DO.BaseStation> bs_list = dal.ListOfStationsForCharging();
            foreach (DO.BaseStation bs in bs_list)
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

        public List<BO.Charging> GetChargings()
        {
            List<BO.Charging> l = new List<Charging>();//the list i will return.
            var dalCharge = dal.GetChargings();//the list of the dal.
            if (dalCharge == null)//chek if there is some q in charge.
                throw new BLException("There is no quadocopter who is charge.");
            IEnumerable<DO.Charging> oldL = from DO.Charging c in dalCharge
                        select c;
            foreach (var c in oldL)//put all the q in 'l'.
                l.Add(new Charging { baseStationID = c.baseStationID, quadocopterID = c.quadocopterID });
            return l;
        }

        #endregion

        /// <summary>
        /// Return the time of the the way the drone will go to his target.
        /// </summary>
        /// <param name="id">The ID of the drone</param>
        /// <param name="target">The target of the the drone, to where he fly.</param>
        /// <param name="id_bs">The defult it is -1, but if the target it is base station, so the user need to put the id of the base station.</param>
        /// <returns></returns>
        public int getTimeOfFlying(int id,int speed, BO.TargetQ target, int id_bs)
        {
            try
            {
                int seconds = 0;
                double distance = 0;//the distance it is in km, and the speeed it's in km to hour.
                Quadocopter q = QuDisplay(id);//get the drone with this ID.

                switch (target)
                {
                    case TargetQ.sender:
                        distance= GetDistance(q.thisLocation, q.thisPackage.sender.thisLocation);
                        break;
                    case TargetQ.receiver:
                        distance = GetDistance(q.thisLocation, q.thisPackage.receiver.thisLocation);
                        break;
                    case TargetQ.baseStation:
                        if (id_bs == -1)
                            throw new BLException("The drone go to base station but you didnt give his ID.");
                        BaseStation b = baseStationDisplay(id_bs);//b is the closest base station to out location
                        distance = GetDistance(b.thisLocation, q.thisLocation);
                        break;
                    default:
                        throw new BLException("Thre is some problem in the target.");
                }
                //the low it is: distance = time * speed
                seconds = (int)((distance / speed) );//to meke minutes to seconds.
                return seconds;
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        /// <summary>
        /// Chek the staus of flying acording to to the package the q is careing.
        /// </summary>
        /// <param name="id">The ID of the quadocopter.</param>
        /// <param name="id_bs">If the target it is base station, the functation get olso the ID of the base station, ele this id it's -1.</param>
        /// <returns>The battery the drone need to to have to come to his target.</returns>
        public int getBatteryToFly(int id, TargetQ target,int pID, int id_bs=-1)
        {
            try
            {
                int battery;
                double distance = 0;
                Quadocopter q = QuDisplay(id);//get the drone with this ID.

                //now i chek to where the drone want to fly.
                switch (target)
                {
                    case TargetQ.sender:
                        distance = GetDistance(q.thisLocation, q.thisPackage.sender.thisLocation);
                        break;
                    case TargetQ.receiver:
                        distance = GetDistance(q.thisLocation, q.thisPackage.receiver.thisLocation);
                        break;
                    case TargetQ.baseStation:
                        if (id_bs == -1)
                            throw new BLException("The drone go to base station but you didnt give his ID.");
                        BaseStation b = baseStationDisplay(id_bs);//b is the closest base station to out location
                        distance = GetDistance(q.thisLocation, b.thisLocation);
                        break;
                    default:
                        throw new BLException("ERROR: There is a problem with the target.");
                }
                //there is difrent typs of electric, acording the drone, so i chek what index to send to the func to get the true elctric.
                int index;
                if (target == TargetQ.baseStation)
                {
                    if (q.mode == statusOfQ.available)
                        index = 0;
                    else
                    {
                        switch (q.weight)
                        {
                            case WeighCategories.easy:
                                index = 1;
                                break;
                            case WeighCategories.middle:
                                index = 2;
                                break;
                            case WeighCategories.hevy:
                                index = 3;
                                break;
                            default:
                                throw new BLException("ERROR: There is problem in the wight of the drone.");
                        }
                    }
                }
                else
                {
                    Package p = PackageDisplay(pID);
                    index = (int)p.weight;
                }

                battery = (int)(distance * dal.askForElectric()[index]);
                return battery;
            }
            catch (Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }

    }
}