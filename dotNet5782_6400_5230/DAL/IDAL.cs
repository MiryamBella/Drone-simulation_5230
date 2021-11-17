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
        public void AssignPtoQ(Packagh P, int id_q);
        /// update package to be collected by quadocopter.
        public void CollectPbyQ(Packagh p);
        public void DeliveringPtoClient(Packagh p);
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
        public Packagh PackageDisplay( int id);
        /// print all the stations.
        public List<BaseStation> ListOfStations();
        /// print all the quadocpters.
        public List<Quadocopter> ListOfQ();
        /// print all the clients
        public List<Client> ListOfClients();
        /// print all the packages.
        public List<Packagh> ListOfPackages();
        /// print all the packages that dont assigned to quadocopter.
        public List<Packagh> ListOfPwithoutQ();
        /// return list of all the stations that have empty changing positions.
        public List<BaseStation> ListOfStationsForCharging();
        ///the quadocopter ask.
        public List<Charging> GetChargings() { return DalObject.DataSource.charge; }
        public double[] askForElectric();
    }
}
