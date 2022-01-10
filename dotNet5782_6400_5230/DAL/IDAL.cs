using System;
using System.Collections.Generic;
using System.Text;
using DO;
//using System.Device.Location;

namespace DalApi
{
    public interface IDAL
    {
        #region add
        ///adding new base station    
        public void AddBaseStation(int id, string name, int chargingPositions, double longitude, double latitude);
        ///adding new Quadocopter
        public void AddQuadocopter(int id, string moodle, int weight);
        ///adding new client
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat);
        /// adding new package.
        public void AddPackage(int id, int sender, int colecter, int weigh, int priority);
        #endregion

        #region update
        /// update name of quadocopter
        public void updateQd(int id, string modle);
        ///update name and number of charging positions of a base station
        public void updateBSdata(int id, string name = null, int chargingPositions = -1);
        /// update name and phone of client
        public void updateCdata(int id, string name = null, int phone = 0);
        /// update package to be belong to a quadocopter.
        public void AssignPtoQ(Package P, int id_q);
        /// update package to be collected by quadocopter.
        public void CollectPbyQ(int pID);
        public void DeliveringPtoClient(int pID);
        /// Send the quadocopter to charging.
        public void SendQtoCharging(int bID, int qID);
        /// release te quadocopter frp charging.
        public void ReleaseQfromCharging(int qID);
        #endregion

        #region print
        /// print datails of statin
        public BaseStation StationDisplay(int id);
        /// print datails of quadocopter.
        public Quadocopter QuDisplay(int id);
        /// print datails of client.
        public Client ClientDisplay(int id);
        /// print datails of package.
        public Package PackageDisplay( int id);
        #endregion

        #region lists
        /// print all the stations.
        public IEnumerable<BaseStation> ListOfStations();
        /// print all the quadocpters.
        public IEnumerable<Quadocopter> ListOfQ();
        /// print all the quadocpters acording to the weigh.
        public IEnumerable<Quadocopter> ListOfQ_of_weigh(string w);
        /// print all the clients
        public IEnumerable<Client> ListOfClients();
        /// print all the packages.
        public IEnumerable<Package> ListOfPackages();
        /// print all the packages that dont assigned to quadocopter.
        public IEnumerable<Package> ListOfPwithoutQ();
        /// return list of all the stations that have empty changing positions.
        public IEnumerable<BaseStation> ListOfStationsForCharging();
        ///the quadocopter ask.
        public List<Charging> GetChargings();
        /// return list of all the package that the accepte id is of its sender.
        public IEnumerable<Package> ListOfPackageFrom(int id);
        /// return list of all the package that the accepte id is of its receiver.
        public IEnumerable<Package> ListOfPackageTo(int id);
        #endregion

        public double[] askForElectric();
        /// <summary>
        ///accept id of qudocopter and return package that in it or null 
        /// </summary>
        public Package? searchPinQ(int qID);
        /// <summary>
        /// accept id of package and return the location of its sender
        /// </summary>
        public Location searchLocationOfclient(int pID);
        /// <summary>
        /// return location of randomaly station
        /// </summary>
        public Location randomStationLocation();
        /// <summary>
        /// return location of randomaly Client the get a package
        /// </summary>
        public Location randomCwithPLocation();
        /// <summary>
        /// accept a location and return the closest base station
        /// </summary>
        public BaseStation searchCloseStation(Location l);
        /// <summary>
        /// accept a location and return the closest base station with a free charge position
        /// </summary>
        public BaseStation searchCloseEmptyStation(Location l);
        /// <summary>
        /// accept a location of qudocopoter and its battery and return list of package that the q can take
        /// </summary>
        public List<Package> availablePtoQ(int battery, Location loc, IEnumerable<Package> packages);
        /// <summary>
        /// accept id of package of id of its sender/receiver and return the another client of this package(receiver/sender)
        /// </summary>
        public Client searchAnotherClient(int pID, int clientID);
    }
}
