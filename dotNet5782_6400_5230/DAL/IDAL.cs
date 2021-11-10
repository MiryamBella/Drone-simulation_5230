using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    interface IDAL
    {
        public void AddBaseStation(int id, string name, int chargingPositions, double longitude, double latitude); ///adding new base station
        public void AddQuadocopter(int id, string moodle, int weight); ///adding new Quadocopter
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat); ///adding new client
        public void AddPackage(int sender, int colecter, int weigh, int priority);
        public void AssignPtoQ(int id);
        public void CollectPbyQ(int id);
        public void DeliveringPtoClient(int id);
        /// <summary>
        /// Send the quadocopter to charging.
        /// </summary>
        public void SendQtoCharging();
        /// <summary>
        /// release te quadocopter frp charging.
        /// </summary>
        public void ReleaseQfromCharging();

        /// <summary>
        /// print datails of statin
        /// </summary>
        public void StationDisplay();//print datails of station 
        /// <summary>
        /// print datails of quadocopter.
        /// </summary>
        public void QuDisplay();//print datails of quadocopter
        /// <summary>
        /// print datails of client.
        /// </summary>
        public void ClientDisplay();//print datails of client
        /// <summary>
        /// print datails of package.
        /// </summary>
        public void PackageDisplay();//print datails of package

        /// <summary>
        /// print all the stations.
        /// </summary>
        public void ListOfStations(); //print all the stations
        /// <summary>
        /// print all the quadocpters.
        /// </summary>
        public void ListOfQ();//print all the quadocpters
        /// <summary>
        /// print all the clients
        /// </summary>
        public void ListOfClients();//print all the clients
        /// <summary>
        /// print all the packages.
        /// </summary>
        public void ListOfPackages();//print all the packages
        public void ListOfPwithoutQ();//print all the packages that dont assigned to quadocopter
        /// <summary>
        /// print all the stations that have empty changing positions.
        /// </summary>
        public void ListOfStationsForCharging();//print all the stations that have empty changing positions
        public double[] askForElectric();//the quadocopter ask.
    }
}
