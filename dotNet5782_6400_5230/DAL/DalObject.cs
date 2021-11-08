using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;


namespace DalObject
{
    public class DalObject
    {
        public DalObject() { DataSource.Initialize(); }///When this class is built it first initializes the lists with the initial values defined in Initialize
        public void AddBaseStation(int id, string name, int chargingPositions, double longitude, double latitude) ///adding new base station
        {
            BaseStation station = new BaseStation(); /// i did new BaseStation
            station.name = name;
            station.chargingPositions = chargingPositions;
            station.longitude = longitude;
            station.latitude = latitude;
            
            //make the location in base 60.
            station.toBaseSix = new BaseSixtin();
            station.decSix = new DmsLocation();
            station.decSix = station.toBaseSix.LocationSix(station.latitude, station.longitude);

            station.IDnumber = id;
            DataSource.bstion.Add(station); /// i insert the new station into the list
        }
        public void AddQuadocopter(int id, string moodle, int weight, )  ///adding new Quadocopter
        {
            Quadocopter q = new Quadocopter();
            q.moodle = moodle;
            if (weight == 1) q.weight = WeighCategories.easy;
            else if (weight == 2) q.weight = WeighCategories.middle;
            else q.weight = WeighCategories.hevy;
            q.id = id;  /// I insert the id in according to the index
            q.battery = 100;   /// mode of bettery in the begining is 100%
            q.mode = statusOfQ.available; ///the Q in the begining is available
            DataSource.qpter.Add(q); ///I insert the new qptr to the array
        }
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat) ///adding new client
        {///I accept all the data from the user
            Client c = new Client();
            c.ID = id;
            c.name = name;
            c.phoneNumber = phoneNumber;
            c.longitude = lon;
            c.latitude = lat;
            DataSource.cli.Add(c);
        }

        /// <summary>
        /// adding new package.
        /// </summary>
        /// <returns>The pacjagh ID we add.</returns>
        public void AddPackage() 
        {
            Packagh p = new Packagh();
            p.sender = int.Parse(help);
            Console.WriteLine("Please enter the ID of the receiver of the package"); //accepting the id of the receiver
            help = Console.ReadLine();
            p.receiver = int.Parse(help);
            Console.WriteLine("Please enter the weight of the package (enter 1 to easy,2 to middle,3 to heavy)"); //accepting the weight of the package
            help = Console.ReadLine();
            if (help == "1")
                p.weight = WeighCategories.easy;
            else if (help == "2")
                p.weight = WeighCategories.middle;
            else p.weight = WeighCategories.hevy;
            Console.WriteLine("Please enter the priority of the package (enter 1 to reggular,2 to fast,3 to emergency)"); //accepting the priority of the package
            help = Console.ReadLine();
            if (help == "1")
                p.priority = Priorities.reggular;
            else if (help == "2")
                p.priority = Priorities.fast;
            else p.priority = Priorities.emergency;
            //int index = DataSource.Config.index_client++; //find the index of the empty place in the array of the packages and update it
            p.id = DataSource.Config.runNum++;   // the id of the package will be according the run number
            p.idQuadocopter = 0;  // the package have not quadocopter
            p.time_Create = DateTime.Now;  // the time of the create is now
            DataSource.packagh.Add(p);  // enter the new package into the array
            //return p.id;
        }
        /// <summary>
        /// update package to be belong to a quadocopter.
        /// </summary>
        public void AssignPtoQ() 
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            int i = 0;
            for (; i < DataSource.Config.index_packagh; i++) // look for the index that the package in it
                if (DataSource.packagh[i].id == id)
                    break;
            int j = 0;
            for (; j < DataSource.Config.index_quadocopter; j++) // look for index that contain quadocopter whice can take this package
                if (DataSource.qpter[j].mode == statusOfQ.available)
                    if (DataSource.qpter[j].weight == DataSource.packagh[i].weight)
                        break;
            DataSource.qpter[j].mode = statusOfQ.delivery; //change the mode of the qpter to delivery
            DataSource.packagh[i].idQuadocopter = DataSource.qpter[j].id; //enter the id of the qptr to the package
            DataSource.packagh[i].time_Belong_quadocopter = DateTime.Now; //update the appropriate time to be now 
        }
        /// <summary>
        /// update package to be collected by quadocopter.
        /// </summary>
        public void CollectPbyQ() 
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_packagh; i++) // look for the index that the package in it
                if (DataSource.packagh[i].id == id)
                {
                    DataSource.packagh[i].time_ColctedFromSender = DateTime.Now; //update the time
                    break;
                }
        }
        public void DeliveringPtoClient()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            int i = 0;
            for (; i < DataSource.Config.index_packagh; i++)//look for the index of this package
                if (DataSource.packagh[i].id == id)
                    break;
            DataSource.packagh[i].time_ComeToColcter = DateTime.Now; //update the time
            for (int j = 0; j < DataSource.Config.index_quadocopter; j++) //look for index of the quadocopter whice take this package
                if (DataSource.qpter[j].id == DataSource.packagh[i].idQuadocopter)
                    DataSource.qpter[j].mode = statusOfQ.available; //update the qptr to be abailable
        }
        /// <summary>
        /// Send the quadocopter to charging.
        /// </summary>
        public void SendQtoCharging()
        {
            int[] indexs = new int[2];
            bool charge = true;
            bool allow = allwoToCharge(indexs, charge);
            int iq = indexs[0];
            int jb = indexs[1];
            if (!allow)
                return;
            if (DataSource.bstion[jb].chargingPositions == 0)
            {
                Console.WriteLine("The base station dont have plase to charging.");
                return;
            }


            //now after we chek if we can we will start the charging.
            Charging c = new Charging();
            c.baseStationID = DataSource.bstion[jb].IDnumber;
            c.quadocopterID = DataSource.qpter[iq].id;
            DataSource.bstion[jb].chargingPositions--;
            DataSource.qpter[iq].battery = 100;
            DataSource.qpter[iq].mode = statusOfQ.maintenance ;
            DataSource.charge.Add(c);
        }
        /// <summary>
        /// release te quadocopter frp charging.
        /// </summary>
        public void ReleaseQfromCharging()
        {

            int[] indexs = new int[2];
            bool charge = false;
            bool allow = allwoToCharge(indexs, charge);
            if (!allow)
                return;


            //now after we chek if we can we will start the charging.
            int iq = indexs[0];
            int jb = indexs[1];
            DataSource.bstion[jb].chargingPositions++;
            DataSource.qpter[iq].mode = statusOfQ.available;

            Charging c = new Charging();
            c.baseStationID = DataSource.bstion[jb].IDnumber; 
            c.quadocopterID =DataSource.qpter[iq].id;
            DataSource.charge.Remove(c);
        }
        /// <summary>
        /// print datails of statin
        /// </summary>
        public void StationDisplay()//print datails of station 
        {
            Console.WriteLine("please enter id of station"); // the printing is by the id  that i asked from the user
            string help = Console.ReadLine();
            int number = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_baseStation; i++)  //I run of all the array and looked for the one with the same id
                if (DataSource.bstion[i].IDnumber == number)
                {
                    Console.WriteLine(DataSource.bstion[i]); //I print it
                    break;
                }
        }
        /// <summary>
        /// print datails of quadocopter.
        /// </summary>
        public void QuDisplay()//print datails of quadocopter
        {
            Console.WriteLine("please enter id of quadocopter"); // the printing is by the id  that i asked from the user
            string help = Console.ReadLine();
            int number = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_quadocopter; i++)  //I run of all the array and looked for the one with the same id
                if (DataSource.qpter[i].id == number)
                {
                    Console.WriteLine(DataSource.qpter[i]); //I print it
                    break;
                }
        }
        /// <summary>
        /// print datails of client.
        /// </summary>
        public void ClientDisplay()//print datails of client
        {
            Console.WriteLine("please enter id of client"); // the printing is by the id  that i asked from the user
            string help = Console.ReadLine();
            int number = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_client; i++)  //I run of all the array and looked for the one with the same id
                if (DataSource.cli[i].ID == number)
                {
                    Console.WriteLine(DataSource.cli[i]); //I print it
                    break;
                }
        }
        /// <summary>
        /// print datails of package.
        /// </summary>
        public void PackageDisplay()//print datails of package
        {
            Console.WriteLine("please enter id of package"); // the printing is by the id  that i asked from the user
            string help = Console.ReadLine();
            int number = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_packagh; i++)  //I run of all the array and looked for the one with the same id
                if (DataSource.packagh[i].id == number)
                {
                    Console.WriteLine(DataSource.packagh[i]); //I print it
                    break;
                }
        }

        /// <summary>
        /// print all the stations.
        /// </summary>
        public void ListOfStations() //print all the stations
        {
            for (int i = 0; i < DataSource.Config.index_baseStation; i++) // I run of all the stations and print them
                Console.WriteLine(DataSource.bstion[i]);
        }
        /// <summary>
        /// print all the quadocpters.
        /// </summary>
        public void ListOfQ()//print all the quadocpters
        {
            for (int i = 0; i < DataSource.Config.index_quadocopter; i++) // I run of all the qudocopters and print them
                Console.WriteLine(DataSource.qpter[i]);
        }
        /// <summary>
        /// print all the clients
        /// </summary>
        public void ListOfClients()//print all the clients
        {
            for (int i = 0; i < DataSource.Config.index_client; i++) // I run of all the clients and print them
                Console.WriteLine(DataSource.cli[i]);
        }
        /// <summary>
        /// print all the packages.
        /// </summary>
        public void ListOfPackages()//print all the packages
        {
            for (int i = 0; i < DataSource.Config.index_packagh; i++) // I run of all the packages and print them
                Console.WriteLine(DataSource.packagh[i]);
        }
        /// <summary>
        /// print all the packages that dont assigned to quadocopter.
        /// </summary>
        public void ListOfPwithoutQ()//print all the packages that dont assigned to quadocopter
        {
            for (int i = 0; i < DataSource.Config.index_packagh; i++) // I run of all the packages and print them if their idQuadocopter is 0
               if (DataSource.packagh[i].idQuadocopter == 0)
                    Console.WriteLine(DataSource.packagh[i]);
        }
        /// <summary>
        /// print all the stations that have empty changing positions.
        /// </summary>
        public void ListOfStationsForCharging()//print all the stations that have empty changing positions
        {
            for (int i = 0; i < DataSource.Config.index_baseStation; i++) // I run of all the stations and print them if their changingPosition is not 0
                if (DataSource.bstion[i].chargingPositions != 0)
                    Console.WriteLine(DataSource.bstion[i]);
        }


        //---------------------------------------------------------------------------------------------------------------
        // func to help us.

        /// <summary>
        /// the func get quadocopter's id and base station's id from the user and chak if they in our data.
        /// </summary>
        /// <returns>if all datails from the user are treu</returns>
        bool allwoToCharge(int[] indexs, bool charge=true)
        {
            Console.WriteLine("Please enter the ID of the quadocopter you want to charge."); //accepting the id of the quadocopter
            string temp_str = Console.ReadLine();
            int id = int.Parse(temp_str);
            int i;
            bool exist = false;//to chak if we find the quadocopter's ID.
            for (i = 0; i < DataSource.Config.index_quadocopter; i++) //look for index of the quadocopter with the quadocopter ID the user put.
                if (DataSource.qpter[i].id == id)
                {
                    exist = true;//we find the quadocopter.
                    break;
                }
            if (!exist)//if we didnt find the quadocopter.
            {
                Console.WriteLine("The quadocopter's ID not exist in our data.");
                return false;
            }

            if (DataSource.qpter[i].mode != statusOfQ.available)
            {
                Console.WriteLine("The quadocopter are " + DataSource.qpter[i].mode + '.');
                return false;
            }

            if (charge)
                ListOfStationsForCharging();
            else
                ListOfStations();
            Console.WriteLine("Please enter the ID of the base station you want to charge in."); //accepting the id of the quadocopter
            temp_str = Console.ReadLine();
            id = int.Parse(temp_str);
            int j;
            exist = false;//to chak if we find the base station's ID.
            for (j = 0; j < DataSource.Config.index_baseStation; j++) //look for index of the base station with the base station ID the user put.
                if (DataSource.bstion[j].IDnumber == id)
                {
                    exist = true;//we find the quadocopter.
                    break;
                }
            if (!exist)//if we didnt find the quadocopter.
            {
                Console.WriteLine("The base station's ID not exist in our data.");
                return false;
            }
            indexs[0] = i;
            indexs[1] = j;
            return true;
        }
    }
}
