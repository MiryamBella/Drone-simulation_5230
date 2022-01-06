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
        public int sendQtoChrge(int id)
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
                DO.Location dalL = new DO.Location() { longitude = q.thisLocation.longitude, latitude = q.thisLocation.latitude };
                DO.BaseStation b = dal.searchCloseEmptyStation(dalL);//b is the closest base station to out location
                double distance = GetDistance(coverLtoL(dalL), new location() { longitude = b.longitude, latitude = b.latitude });
                int minBattery = (int)(distance * dal.askForElectric()[0]);// we allredy chek that q is avilable so we put index 0
                if (q.battery < minBattery) throw new BLException("there is no enough battery to");

                dal.SendQtoCharging(b.IDnumber, q.ID);//update the data at the dal

                q.battery -= minBattery;//update the list of qudocopter in the BL
                q.thisLocation.longitude = dalL.longitude;
                q.thisLocation.latitude = dalL.latitude;
                q.thisLocation.decSix = new DmsLocation();
                q.thisLocation.toBaseSix = new BaseSixtin();
                q.mode = statusOfQ.maintenance;

                return q.battery;
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
            bool flag = false;
            QuadocopterToList q = new QuadocopterToList();//i search the qudocopter in the q_list
            foreach (QuadocopterToList i in q_list)
                if (i.ID == id)
                {
                    flag = true;
                    q = i;
                    break;
                }
            if (!flag) throw new BLException("Id not found.");

            try
            {
                double hours = 0;
                DateTime t = DateTime.Parse(dal.QuDisplay(id).startCharge.ToString());
                hours = (t - DateTime.Now).TotalHours;
                if (q.mode != statusOfQ.maintenance) throw new BLException("this q not in maintenance.");
                q.mode = statusOfQ.available;
                q.battery += (int)(dal.askForElectric()[4] * hours);
                if (q.battery > 100) q.battery = 100;
                dal.ReleaseQfromCharging(id);
                return q.battery;
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
        #region assignPtoQ;
        /// <summary>
        /// update package to be assigned to a qudocopter
        /// </summary>
        public void assignPtoQ(int qID)
        {
            bool flag = false;
            QuadocopterToList q = new QuadocopterToList();
            foreach (QuadocopterToList qu in q_list)//check if the quadocopter is exist
                if (q.ID == qID)
                {
                    flag = true;
                    q = qu;
                    break;
                }
            if (!flag) throw new BLException("this ID dont exist");

            try
            {
                var packages = dal.availablePtoQ(q.ID, new DO.Location() { longitude = q.thisLocation.longitude, latitude = q.thisLocation.latitude });//list of package that it can take according to this battery
                if (packages.Count == 0) throw new BLException("there is no package to assign");
                var package = packages.OrderBy(s => (int)s.priority).ThenBy(s => s.weight);
                DO.Priorities pr = new DO.Priorities();
                foreach (var a in package) { pr = a.priority; break; };
                var packagesInHighPri = from DO.Package p in package //list of the packages with the highest priority
                                        where p.priority == pr
                                        select p;
                var packageInHighWeight = from DO.Package p in package
                                          where (int)p.weight <= (int)q.weight
                                          select p;
                int w = 0;
                foreach (var a in packageInHighWeight) { w = (int)a.weight; break; }
                packageInHighWeight = from DO.Package p in package
                                      where (int)p.weight == w
                                      select p;
                bool isfirst = true;
                double distance = 0;
                DO.Package thePackage = new DO.Package();
                foreach (DO.Package p in packageInHighWeight)
                {
                    double d = GetDistance(q.thisLocation, coverLtoL(dal.searchLocationOfclient(p.sender)));
                    if (isfirst) { distance = d; thePackage = p; }
                    else if (d < distance) { distance = d; thePackage = p; }
                }
                dal.AssignPtoQ(thePackage, q.ID);
                q.mode = statusOfQ.delivery;
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
            bool flag = false;
            QuadocopterToList q = new QuadocopterToList();
            foreach (QuadocopterToList qu in q_list)
                if (qu.ID == qID)
                {
                    flag = true;
                    q = qu;
                };
            if (!flag) throw new BLException("this ID not exist");
            if (q.mode != statusOfQ.delivery) throw new BLException("this qudocopter dont associated to a package");
            try
            {
                DO.Package? p = dal.searchPinQ(qID);
                if (p.Value.time_ColctedFromSender.Value.Year != 0001) throw new BLException("the package was collocted already");
                //update the data of the qudocopter
                DO.Location senderL = dal.searchLocationOfclient(p.Value.sender);//update the battery
                double distance = GetDistance(q.thisLocation, new location() { longitude = senderL.longitude, latitude = senderL.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                q.battery -= (int)(distance * dal.askForElectric()[(int)p.Value.weight]);
                q.thisLocation.longitude = senderL.longitude;//update the location 
                q.thisLocation.latitude = senderL.latitude;
                q.thisLocation.decSix = new DmsLocation(senderL.latitude, senderL.longitude);
                q.thisLocation.toBaseSix = new BaseSixtin();
                dal.CollectPbyQ(p.Value.id);
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
                };
            if (!flag) throw new BLException("this ID not exist");
            if (q.mode != statusOfQ.delivery) throw new BLException("this qudocopter dont associated to a package");
            try
            {
                DO.Package? p = dal.searchPinQ(qID);
                if (p.Value.time_ComeToColcter.Value.Year != 0001) throw new BLException("the package was collocted already");
                //update the data of the qudocopter
                DO.Location receiverL = dal.searchLocationOfclient(p.Value.receiver);//update the battery
                double distance = GetDistance(q.thisLocation, new location() { longitude = receiverL.longitude, latitude = receiverL.latitude });
                q.battery -= (int)(distance * dal.askForElectric()[(int)p.Value.weight]);
                q.thisLocation.longitude = receiverL.longitude;//update the location 
                q.thisLocation.latitude = receiverL.latitude;
                q.thisLocation.decSix = new DmsLocation(receiverL.latitude, receiverL.longitude);
                q.thisLocation.toBaseSix = new BaseSixtin();
                dal.DeliveringPtoClient(p.Value.id);
            }
            catch (Exception ex)
            {
                throw new BLException(ex.Message);
            }
        }
        #endregion;
    }
}
