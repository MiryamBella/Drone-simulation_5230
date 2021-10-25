using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;


namespace DalObject
{
    public class DalObject
    {
        DalObject() { DataSource.Initialize(); }///When this class is built it first initializes the lists with the initial values defined in Initialize

        public void AddBaseStation() ///adding new base station
        {
            BaseStation station = new BaseStation(); /// i did new BaseStation
            Console.WriteLine("Please enter name of station"); ///I get information from the user and enter it into the new station
            station.name = Console.ReadLine();                    
            Console.WriteLine("Please enter number of charging position");
            string helpSTR = Console.ReadLine();
            station.chargingPositions = int.Parse(helpSTR); /// i get a numbers as string and convert it to int
            Console.WriteLine("Please enter longitude of station");
            helpSTR = Console.ReadLine();
            station.longitude = int.Parse(helpSTR);
            Console.WriteLine("Please enter latitude of station");
            helpSTR = Console.ReadLine();
            station.latitude = int.Parse(helpSTR);

            int index = DataSource.Config.index_baseStation++; /// I save the first empty index and update it
            station.IDnumber = index + 1; ///the ID i dont asked from the user but decided to do numbers 1, 2, 3 and more as per the indexes
            DataSource.bstion[index] = station; /// i insert the new station into the array
        }
        public void AddQuadocopter()  ///adding new Quadocopter
        {
            Quadocopter q = new Quadocopter();
            Console.WriteLine("Please enter the modle of the quadocpter"); ///i get the modle of the Q and the Weight categories from the user
            q.moodle = Console.ReadLine();
            Console.WriteLine("Please enter the categorie of the weight (1 to easy, 2 to middle, 3 to heavy)");
            string help = Console.ReadLine();     /// I get from the user the weight by 1, 2 or 3 and in accordance to it i enter the weight
            if (help == "1") q.weight = WeighCategories.easy;
            else if (help == "2") q.weight = WeighCategories.middle;
            else q.weight = WeighCategories.hevy;
            int index = DataSource.Config.index_quadocopter++;
            q.id = index + 1;  /// I insert the id in according to the index
            q.battery = 100;   /// mode of bettery in the begining is 100%
            q.mode = statusOfQ.available; ///the Q in the begining is available
            DataSource.qpter[index] = q; ///I insert the new qptr to the array
        }
        public void AddClient() ///adding new client
        {///I accept all the data from the user
            Client c = new Client();
            Console.WriteLine("Please enter the ID of the client");
            string help = Console.ReadLine();  ///accepting it as a string
            c.ID = int.Parse(help);       ///convert the string to int
            Console.WriteLine("Please enter the name of the client");
            c.name = Console.ReadLine();
            Console.WriteLine("Please enter the phone number of the client");
            help = Console.ReadLine();
            c.phoneNumber = int.Parse(help);
            Console.WriteLine("Please enter the longitude of the home");
            help = Console.ReadLine();
            c.longitude = int.Parse(help);
            Console.WriteLine("Please enter the latitude of the home");
            help = Console.ReadLine();
            c.latitude = int.Parse(help);
            DataSource.cli[DataSource.Config.index_client++] = c;
        }
        public void AddPackage();
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
