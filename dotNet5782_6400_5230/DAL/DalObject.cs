using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;


namespace DalObject
{
    public class DalObject
    {
        DalObject() {  DataSource.Initialize)}//When this class is built it first initializes the lists with the initial values defined in Initialize

        public void AddBaseStation() //adding new base station
        {
            Console.WriteLine("Please enter name of station"); //I get information from the user and because it is received as a string I convert it to int
            string name = Console.ReadLine();
            Console.WriteLine("Please enter number of charging position");
            string helpSTR = Console.ReadLine();
            int numOfCharging = int.Parse(helpSTR);
            Console.WriteLine("Please enter longitude of station");
            helpSTR = Console.ReadLine();
            int longitude = int.Parse(helpSTR);
            Console.WriteLine("Please enter latitude of station");
            helpSTR = Console.ReadLine();
            int latitude = int.Parse(helpSTR);
            BaseStation station = new BaseStation();
            station.name = name;
            station.chargingPositions = numOfCharging;
            station.longitude = longitude;
            station.latitude = latitude;
            station.IDnumber = DataSource.Config.index;
            DataSource.Config


        }
        public void AddQuadocopter();
        public void AddClient();
        public int AddPackage();
        public void AssignPtoQ();//new: the methods of updating. p is package, q is quadocopter, because it so long
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
