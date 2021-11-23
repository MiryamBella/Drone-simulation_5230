﻿using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using System.Device.Location;
using System.Linq;

namespace IBL
{
    public partial class BL
    {
        #region updateQdata;
        /// <updataQdata>
        /// update the modle of a qudocopter
        /// <int, string>
        public void updateQdata(int id, string modle)
        {
            bool flag = false;
            foreach (QuadocopterToList q in q_list)//update the q_list
                if (q.ID == id)
                {
                    q.moodle = modle;
                    flag = true;
                    break;
                }
            if (!flag) Console.WriteLine("error");
            else dal.updateQd(id, modle); //update the data by the dalObject
        }
        #endregion;
        #region updataBSdata;
        /// <summary>
        /// update the name of the station or the number of its charging positions
        /// </summary>
        public void updateSdata(int id, string name = null, int chargingPositions = -1)
        {
            if (name == null && chargingPositions == -1)//inetgrity checking
                Console.WriteLine("ERROR");
            if (chargingPositions < -1)
                Console.WriteLine("ERROR");
            dal.updateSdata(id, name, chargingPositions); //update the data by sending to the dalObject
        }
        #endregion;
        #region updateCdata;
        /// <summary>
        /// update the name or the phone number of the client
        /// </summary>
        public void updateCdata(int id, string name = null, int phone = -1)
        {
            if (id < 100000000 || id > 999999999)//integrity checking
                Console.WriteLine("ERROR");
            if (name == null && phone == -1)
                Console.WriteLine("ERROR");
            if (phone != -1 && phone < 100000000)
                Console.WriteLine("ERROR");
            dal.updateCdata(id, name, phone);
        }
        #endregion;
        #region sendQtoCharge;
        /// update qudocopter to be send to a charging position
        public void sendQtoChrge(int id)
        {
            bool flag = false;
            QuadocopterToList q = new QuadocopterToList();//i search the qudocopter in the q_list
            foreach (QuadocopterToList i in q_list)
                if (i.ID == id)
                {
                    flag = true;
                    q = i;
                }
            //integrity checking
            if (!flag) throw new BLException("Id not found.");
            if (q.mode != statusOfQ.available) throw new BLException("error");
            //check if it have enough battery to go to base station
            IDAL.DO.Location dalL = new IDAL.DO.Location() { longitude = q.thisLocation.longitude, latitude = q.thisLocation.latitude };
            IDAL.DO.BaseStation b = dal.searchCloseEmptyStation(dalL);
            double distance = dal.coverLtoG(dalL).GetDistanceTo(new GeoCoordinate(b.longitude, b.latitude));
            int minBattery = (int)(distance * dal.askForElectric()[0]);
            if (q.battery < minBattery) Console.WriteLine("error");

            dal.SendQtoCharging(b.IDnumber, q.ID);//update the data at the dal

            q.battery -= minBattery;//update the list of qudocopter in the BL
            q.thisLocation.longitude = dalL.longitude;
            q.thisLocation.latitude = dalL.latitude;
            q.mode = statusOfQ.maintenance;
        }
        #endregion;
        #region releaseQfronCharge;
        /// <summary>
        /// update qudocopter to be released from a charging positions
        /// </summary>
        public void releaseQfromChrge(int id, double hours)
        {
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
            if (q.mode != statusOfQ.maintenance) throw new BLException("this q not in maintenance.");
            q.mode = statusOfQ.available;
            q.battery += (int)(dal.askForElectric()[4] * hours);
            if (q.battery > 100) q.battery = 100;
            dal.ReleaseQfromCharging(id);
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
            if (!flag) Console.WriteLine("error");

            var packages = dal.availablePtoQ(q.ID, coverLtoG(q.thisLocation));//list of package that it can take according to this battery
            if (packages.Count == 0) Console.WriteLine("error");
            var package = packages.OrderBy(s => (int)s.priority).ThenBy(s => s.weight);
            IDAL.DO.Priorities pr = new IDAL.DO.Priorities();
            foreach (var a in package) { pr = a.priority; break; };
            var packagesInHighPri = from IDAL.DO.Package p in package //list of the packages with the highest priority
                                    where p.priority == pr
                                    select p;
            var packageInHighWeight = from IDAL.DO.Package p in package
                                      where (int)p.weight <= (int)q.weight
                                      select p;
            int w = 0;
            foreach (var a in packageInHighWeight) { w = (int)a.weight; break; }
            packageInHighWeight = from IDAL.DO.Package p in package
                                  where (int)p.weight == w
                                  select p;
            bool isfirst = true;
            double distance = 0;
            IDAL.DO.Package thePackage = new IDAL.DO.Package();
            foreach (IDAL.DO.Package p in packageInHighWeight)
            {
                double d = coverLtoG(q.thisLocation).GetDistanceTo(dal.coverLtoG(dal.searchLocationOfclient(p.sender)));
                if (isfirst) { distance = d; thePackage = p; }
                else if (d < distance) { distance = d; thePackage = p; }
            }
            dal.AssignPtoQ(thePackage, q.ID);
            q.mode = statusOfQ.delivery;
        }
        #endregion;
        #region collectPbyQ;
        /// <summary>
        /// updat package to be collected by qudocopter
        /// </summary>
        public void collectPbyQ()
        {

        }
        #endregion;
        #region supplyPbyQ;
        /// <summary>
        /// update package to be supplied to the client and the qudocopter to be free from package
        /// </summary>
        public void supplyPbyQ()
        {

        }
        #endregion;
    }
}
