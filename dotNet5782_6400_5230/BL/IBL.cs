using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    interface IBL
    {
        /*add functations*/
        public void AddBaseStation(int id, string name, double lon, double lat, int numCharge);
        public void AddQuadocopter(int id, string moodle, int weight, int id_bs);
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat);
        public void AddPackage(int id_sender, int id_colecter, int weight, int priority);

        /*update functations*/
        public void updateQdata(int id, string modle);
        public void updateSdata(int id, string name = null, int chargingPositions = -1);
        public void updateCdata(int id, string name = null, int phone = -1);
        public void sendQtoChrge(int id);
        public void releaseQfromChrge(int id, double hours);
        public void assignPtoQ(int qID);
        public void collectPbyQ(int qID);
        public void supplyPbyQ(int qID);

        /*print functations*/
        public BO.BaseStation baseStationDisplay(int id);
        public BO.Quadocopter QuDisplay(int id);
        public BO.Client ClientDisplay(int id);
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
