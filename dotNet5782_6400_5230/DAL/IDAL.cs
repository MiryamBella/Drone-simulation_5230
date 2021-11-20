using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDAL
    {
        ///adding new base station    
        public void AddBaseStation(int id, string name, int chargingPositions, double longitude, double latitude);
        ///adding new Quadocopter
        public void AddQuadocopter(int id, string moodle, int weight);
        ///adding new client
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat);
        /// adding new package.
        public void AddPackage(int sender, int colecter, int weigh, int priority);
        /// update name of quadocopter
        public void updateQd(int id, string modle);
        ///update name and number of charging positions of a base station
        public void updateSdata(int id, string name = null, int chargingPositions = -1);
        /// update name and phone of client
        public void updateCdata(int id, string name = null, int phone = 0);
        /// update package to be belong to a quadocopter.
        public void AssignPtoQ(Package P, int id_q);
        /// update package to be collected by quadocopter.
        public void CollectPbyQ(Package p);
        public void DeliveringPtoClient(Package p);
        /// Send the quadocopter to charging.
        public void SendQtoCharging(BaseStation b, Quadocopter q);
        /// release te quadocopter frp charging.
        public void ReleaseQfromCharging(BaseStation b, Quadocopter q);
        /// print datails of statin
        public BaseStation StationDisplay(int id);
        /// print datails of quadocopter.
        public Quadocopter QuDisplay(int id);
        /// print datails of client.
        public Client ClientDisplay(int id);
        /// print datails of package.
        public Package PackageDisplay( int id);
        /// print all the stations.
        public IEnumerable<BaseStation> ListOfStations();
        /// print all the quadocpters.
        public IEnumerable<Quadocopter> ListOfQ();
        /// print all the clients
        public IEnumerable<Client> ListOfClients();
        /// print all the packages.
        public IEnumerable<Package> ListOfPackages();
        /// print all the packages that dont assigned to quadocopter.
        public IEnumerable<Package> ListOfPwithoutQ();
        /// return list of all the stations that have empty changing positions.
        public IEnumerable<BaseStation> ListOfStationsForCharging();
        ///the quadocopter ask.
        public List<Charging> GetChargings() { return DalObject.DataSource.charge; }
        public double[] askForElectric();
        /// <summary>
        ///accept id of qudocopter and return package that in it or null 
        /// </summary>
        public Package? searchPinQ(int qID);
        /// <summary>
        /// accept id of package and return the location of its sender
        /// </summary>
        public Location searchLocationOfsender(int pID);
        /// <summary>
        /// return location of randomaly station
        /// </summary>
        public Location randomStationLocation();
        /// <summary>
        /// return location of randomaly Client the get a package
        /// </summary>
        public Location randomCwithPLocation();
    }
}
