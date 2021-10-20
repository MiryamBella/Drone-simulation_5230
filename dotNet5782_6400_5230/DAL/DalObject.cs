using System;
using System.Collections.Generic;
using System.Text;

namespace DalObject
{
    public class DalObject
    {
        DalObject() {  DataSource.Initialize)}//new: When this class is built it first initializes the lists with the initial values defined in Initialize

        public void AddBaseStation(); //new: The methods of adding objects
        public void AddQuadocopter();
        public void AddClient();
        public int AddPackage();
        public void AssignPtoQ();//new: the methods of updating
        public void CollectPbyQ();
        public void DeliveringPtoClient();
        public void SendQtoCharging(int station);
        public void ReleaseQfromCharging();
        public void StationDisplay(); //new: the methods of display
        public void QuDisplay();
        public void ClientDisplay();
        public void PackageDisplay();
        public void ListOfStations();//new: the methods of view Lists
        public void ListOfQ();
        public void ListOfClients();
        public void ListOfPackages();
        public void ListOfPwithoutQ();
        public void ListOfStationsForCharging();


    }
}
