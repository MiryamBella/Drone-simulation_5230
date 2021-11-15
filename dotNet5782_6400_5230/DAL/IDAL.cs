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
        /// update package to be belong to a quadocopter.
        public void AssignPtoQ(Packagh P, int id_q);
        /// update package to be collected by quadocopter.
        public void CollectPbyQ(Packagh p);
        public void DeliveringPtoClient(Packagh p);
        /// Send the quadocopter to charging.
        public void SendQtoCharging(BaseStation b, Quadocopter q);
        /// release te quadocopter frp charging.
        public void ReleaseQfromCharging(BaseStation b, Quadocopter q);
        /// print datails of statin
        public void StationDisplay(BaseStation b);
        /// print datails of quadocopter.
        public void QuDisplay(Quadocopter q);
        /// print datails of client.
        public void ClientDisplay(Client c);
        /// print datails of package.
        public void PackageDisplay(Packagh p);
        /// print all the stations.
        public void ListOfStations();
        /// print all the quadocpters.
        public List<Quadocopter> ListOfQ();
        /// print all the clients
        public void ListOfClients();
        /// print all the packages.
        public void ListOfPackages();
        /// print all the packages that dont assigned to quadocopter.
        public void ListOfPwithoutQ();
        /// print all the stations that have empty changing positions.
        public void ListOfStationsForCharging();
        ///the quadocopter ask.
        public double[] askForElectric();
    }
}
