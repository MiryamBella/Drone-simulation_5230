using System;
using System.Collections.Generic;
using System.Text;
using BO;
using System.Device.Location;
using System.Linq;

namespace BlApi
{
    public partial class BL
    {
        #region update Q data;
        /// <updataQdata>
        /// update the modle of a qudocopter
        /// <int, string>
        public void updateQdata(int id, string modle)
        {
            bool flag = false;
            foreach (QuadocopterToList q in q_list)//update the q_list
                if (q.ID == id)
                {
                    if (q.moodle == modle) throw new BLException("the modle is the same as it was");
                    q.moodle = modle;
                    flag = true;
                    break;
                }
            try
            {
                if (!flag) throw new BLException("there is no qudocopter with this ID");
                else dal.updateQd(id, modle); //update the data by the dalObject
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion;
        #region updata BS data;
        /// <summary>
        /// update the name of the station or the number of its charging positions
        /// </summary>
        public void updateSdata(int id, string name = null, int chargingPositions = -1)
        {
            if (name == null && chargingPositions == -1)//inetgrity checking
                throw new BLException("no update data was accepted");
            if (chargingPositions < -1)
                throw new BLException("number of charging position must be positive");
            try
            {
                dal.updateBSdata(id, name, chargingPositions); //update the data by sending to the dalObject
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
        #region update Client data;
        /// <summary>
        /// update the name or the phone number of the client
        /// </summary>
        public void updateCdata(int id, string name = null, int phone = -1)
        {
            if (id < 100000000 || id > 999999999)//integrity checking
                throw new BLException("invalid ID");
            if (name == null && phone == -1)
                throw new BLException("no update to do");
            if (phone != -1 && phone < 100000000)
                throw new BLException("invalid phone number");
            try
            {
                dal.updateCdata(id, name, phone);
            }
            catch (Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
        #region send/release Q to Charge;
        /// update qudocopter to be send to a charging position
        public QuadocopterToList sendQtoChrge(int id)
        {
            bool flag = false;
            //i search the qudocopter in the q_list
            QuadocopterToList q = new QuadocopterToList(); 
            foreach (QuadocopterToList i in q_list)
                if (i.ID == id)
                {
                    flag = true;
                    q = i;
                    break;
                }
            //integrity checking
            if (!flag) throw new BLException("Id not found.");
            if (q.mode != statusOfQ.available) throw new BLException("the quadocopter is not available");
            //'dalL'= location in type dal

            try
            {
                //check if it have enough battery to go to base station
                DO.Location dalL = new DO.Location() { longitude = q.thisLocation.Longitude, latitude = q.thisLocation.Latitude };
                DO.BaseStation b = dal.searchCloseEmptyStation(dalL);//b is the closest base station to out location
                double distance = GetDistance(coverLtoL(dalL), new Location() { Longitude = b.longitude, Latitude = b.latitude });
                int minBattery = (int)(distance * dal.askForElectric()[0]);// we allredy chek that q is avilable so we put index 0
                if (q.battery < minBattery) throw new BLException("there is no enough battery to");

                dal.SendQtoCharging(b.IDnumber, q.ID);//update the data at the dal


                //update the data in BL listt.
                foreach (var temp in q_list)
                {
                    if (q.ID == temp.ID)
                    {
                        temp.battery -= minBattery;//update the list of qudocopter in the BL
                        temp.thisLocation.Longitude = b.longitude;//the drone get the location of the base station.
                        temp.thisLocation.Latitude = b.latitude;
                        temp.thisLocation.decSix = new DmsLocation();
                        temp.thisLocation.toBaseSix = new BaseSixtin();
                        temp.thisLocation.decSix = temp.thisLocation.toBaseSix.LocationSix(b.latitude, b.longitude);
                        temp.thisLocation.Location60 = temp.thisLocation.decSix.ToString();
                        temp.mode = statusOfQ.maintenance;
                        break;
                    }
                }
                return q;
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        /// <summary>
        /// update qudocopter to be released from a charging positions
        /// </summary>
        public int releaseQfromChrge(int id)
        {
            if (id <= 0)
                throw new BLException("Invalid id.");
            //if (hours < 0)
            //    throw new BLException("Number of hours must to be positive.");
           
            QuadocopterToList q = new QuadocopterToList();//i search the qudocopter in the q_list
            q = (from i in q_list
                 where i.ID == id
                 select i).FirstOrDefault();
            if (q == default) throw new BLException("Id not found.");

            try
            {
                double seconds = 0;
                DO.Quadocopter dalQ = dal.QuDisplay(id);
                DateTime t = dalQ.startCharge.Value;
                seconds = (DateTime.Now-t).Seconds;
                if (q.mode != statusOfQ.maintenance) throw new BLException("this q not in maintenance.");
                dal.ReleaseQfromCharging(id);

                //update the data in BL listt.
                foreach (var temp in q_list)
                {
                    if (q.ID == temp.ID)
                    {
                        temp.mode = statusOfQ.available;
                        temp.battery += (int)(dal.askForElectric()[4] * seconds);
                        if (temp.battery > 100) temp.battery = 100;
                        break;
                    }
                }

                return q.battery;
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }

        public int getBatteryCharge()
        { 
            return (int)dal.askForElectric()[4];
        }


        #endregion;
        #region assignPtoQ;
        /// <summary>
        /// update package to be assigned to a qudocopter
        /// </summary>
        public Package assignPtoQ(int qID)
        {
            Quadocopter q = new Quadocopter();
            //to find the qudocopter that with thie ID
            try { q = QuDisplay(qID); }
            catch (Exception ex) { throw new BLException(ex.Message); }

            try
            {
                var packages = from p in dal.ListOfPackages() //list of package in weight that the q can take
                               where (int)p.weight <= (int)q.weight
                               select p;
                if (packages.Count() == 0) throw new BLException("There is no package to assign.");//if there are no packages 
                DO.Location loc = new DO.Location() { latitude = q.thisLocation.Latitude, longitude = q.thisLocation.Longitude };
                //run on the list packages and return into it only the packages that the battery is good to them
                packages = dal.availablePtoQ(q.battery, loc, packages);
                if (packages.Count() == 0) throw new BLException("battery");//if there is no battery

                packages = packages.OrderBy(s => (int)s.priority).ThenBy(s => s.weight);
                var p_list = packages.ToList();
                p_list.Reverse();
                bool flag = false;
                foreach(var p in p_list)
                {
                    if(p.time_Belong_quadocopter==null)
                    {
                        dal.AssignPtoQ(p, q.ID);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    throw new BLException("There is no package to assign.");
                foreach (QuadocopterToList quadocopter in q_list)
                {
                    if (quadocopter.ID == qID)
                    {
                        quadocopter.mode = statusOfQ.delivery;
                        quadocopter.packageNumber++;
                        break;
                    }
                }
                return cover(packages.Last());
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
        #region collect package by drone;
        /// <summary>
        /// update package to be collected by qudocopter
        /// </summary>
        public void collectPbyQ(int qID)
        {
            Quadocopter q = new Quadocopter();
            try
            {
                q = QuDisplay(qID);
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
            if (q.mode != statusOfQ.delivery) throw new BLException("this qudocopter dont associated to a package");
            try
            {
                DO.Package? p = dal.searchPinQ(qID, DO.p_thet.ColctedFromSender);
                if (p == null)
                    throw new BLException("There is no pakage in this drone.");
                if (p.Value.time_ColctedFromSender != null) throw new BLException("the package was collocted already");
                //update the data of the qudocopter
                DO.Location senderL = dal.searchLocationOfclient(p.Value.sender);//update the battery
                double distance = GetDistance(q.thisLocation, new Location() { Longitude = senderL.longitude, Latitude = senderL.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });

                //update the data in BL listt.
                foreach(var temp in q_list)
                {
                    if (q.ID == temp.ID)
                    {
                        temp.battery -= (int)(distance * dal.askForElectric()[(int)p.Value.weight]);
                        temp.thisLocation.Longitude = senderL.longitude;//update the location 
                        temp.thisLocation.Latitude = senderL.latitude;
                        temp.thisLocation.decSix = new DmsLocation(senderL.latitude, senderL.longitude);
                        temp.thisLocation.toBaseSix = new BaseSixtin();
                        dal.CollectPbyQ(p.Value.id);
                        break;
                    }
                }

            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
        #region supply packge by Q;
        /// <summary>
        /// update package to be supplied to the client and the qudocopter to be free from package
        /// </summary>
        public void supplyPbyQ(int qID)
        {
            bool flag = false;
            QuadocopterToList q = new QuadocopterToList();
            foreach (QuadocopterToList qu in q_list)
                if (qu.ID == qID)
                {
                    flag = true;
                    q = qu;
                    break;
                };
            if (!flag) throw new BLException("this ID not exist");
            if (q.mode != statusOfQ.delivery) throw new BLException("this qudocopter dont associated to a package");
            try
            {
                DO.Package? p = dal.searchPinQ(qID, DO.p_thet.ComeToColcter);
                if (p.Value.time_ComeToColcter != null) throw new BLException("the package was collocted already");
                //update the data of the qudocopter
                DO.Location receiverL = dal.searchLocationOfclient(p.Value.receiver);//update the battery
                double distance = GetDistance(q.thisLocation, new Location() { Longitude = receiverL.longitude, Latitude = receiverL.latitude });

                //update the data in BL listt.
                foreach (var temp in q_list)
                {
                    if (q.ID == temp.ID)
                    {
                        temp.battery -= (int)(distance * dal.askForElectric()[(int)p.Value.weight]);
                        temp.thisLocation.Longitude = receiverL.longitude;//update the location 
                        temp.thisLocation.Latitude = receiverL.latitude;
                        temp.thisLocation.decSix = new DmsLocation(receiverL.latitude, receiverL.longitude);
                        temp.thisLocation.toBaseSix = new BaseSixtin();
                        dal.DeliveringPtoClient(p.Value.id);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
    }
}
