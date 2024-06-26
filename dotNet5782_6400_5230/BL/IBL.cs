﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlApi
{
    public interface IBL
    {
        /*add functations*/
        #region add
        public void AddBaseStation(int id, string name, double lon, double lat, int numCharge);
        public void AddQuadocopter(int id, string moodle, int weight, int id_bs);
        public void AddClient(int id, string name, double lon, double lat, int phoneNumber);
        public void AddPackage(int id_sender, int id_colecter, BO.WeighCategories weight, BO.Priorities priority);
        #endregion

        /*update functations*/
        #region update
        public void updateQdata(int id, string modle);
        public void updateSdata(int id, string name = null, int chargingPositions = -1);
        public void updateCdata(int id, string name = null, int phone = -1);
        public BO.QuadocopterToList sendQtoChrge(int id);
        public int releaseQfromChrge(int id);
        public int getBatteryCharge();
        public BO.Package assignPtoQ(int qID);//the packge belonge to q.
        public void collectPbyQ(int qID);//the q take the p from the sender.
        public void supplyPbyQ(int qID);//the q come to the client and give ho the p.
        public int getTimeOfFlying(int id, int speed, BO.TargetQ target, int id_bs=-1);
        public int getBatteryToFly(int id, BO.TargetQ target,int pID, int id_bs=-1);
        #endregion

        /*print functations*/
        public BO.BaseStation baseStationDisplay(int id);
        public BO.Quadocopter QuDisplay(int id);
        public BO.Client ClientDisplay(int id);
        public BO.Package PackageDisplay(int id);

        #region lists
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
        public List<BO.QuadocopterToList> ListOfQ_of_weigh(string w);
        //public List<BO.ClientToList> ListOfC_of_weigh(string w);
        public List<BO.Charging> GetChargings();
        #endregion

        public BO.Quadocopter cover(BO.QuadocopterToList ql);
        public BO.QuadocopterToList cover(BO.Quadocopter q);
        public BO.Client cover(BO.ClientToList c);
        public BO.Package cover(BO.PackageToList p);

        public void startSimulator(int id, Action<int, BO.Package> report, Func<bool> isStop, IBL bl);
    }
}
