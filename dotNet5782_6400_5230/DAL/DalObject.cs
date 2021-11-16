using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;


namespace DalObject 
{
    public class DalObject : IDAL.IDAL
    {
        ///When this class is built it first initializes the lists with the initial values defined in Initialize
        public DalObject() { DataSource.Initialize(); }
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
        public void AddQuadocopter(int id, string moodle, int weight)  ///adding new Quadocopter
        {
            Quadocopter q = new Quadocopter();
            q.moodle = moodle;
            if (weight == 1) q.weight = WeighCategories.easy;
            else if (weight == 2) q.weight = WeighCategories.middle;
            else q.weight = WeighCategories.hevy;
            q.id = id;  /// I insert the id in according to the index
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
        public void AddPackage(int sender, int colecter, int weight, int priority) 
        {
            Packagh p = new Packagh();
            p.sender = sender;
            p.receiver = colecter;
            if (weight == 1)
                p.weight = WeighCategories.easy;
            else if (weight == 2)
                p.weight = WeighCategories.middle;
            else p.weight = WeighCategories.hevy;
            if (priority == 1)
                p.priority = Priorities.reggular;
            else if (priority == 2)
                p.priority = Priorities.fast;
            else p.priority = Priorities.emergency;
            p.id = DataSource.Config.runNum++;   // the id of the package will be according the run number
            p.idQuadocopter = 0;  // the package have not quadocopter
            p.time_Create = DateTime.Now;  // the time of the create is now
            DataSource.packagh.Add(p);  // enter the new package into the list
            //return p.id;
        }
        /// <summary>
        /// update package to be belong to a quadocopter.
        /// </summary>
        public void AssignPtoQ(Packagh P, int id_q) 
        {
            P.idQuadocopter = id_q;
            //int i = 0;
            //foreach(Packagh p in DataSource.packagh)// look for the index that the package in it
            //{
            //    if (p.id == id_p)
            //    {
            //        p.idQuadocopter = id_q;
            //        foreach (Quadocopter q in DataSource.qpter)
            //        {
            //            if (q.mode == statusOfQ.available)
            //                if (q.weight == p.weight)
            //                {
            //                    DataSource.packagh.Find(p.).idQuadocopter
            //                    break;
            //                }
            //        }
            //        break;

            //    }
            //}
            ////DataSource.qpter[j].mode = statusOfQ.delivery; //change the mode of the qpter to delivery
            ////DataSource.packagh[i].idQuadocopter = DataSource.qpter[j].id; //enter the id of the qptr to the package
            ////DataSource.packagh[i].time_Belong_quadocopter = DateTime.Now; //update the appropriate time to be now 
        }
        /// <summary>
        /// update package to be collected by quadocopter.
        /// </summary>
        public void CollectPbyQ(Packagh p) 
        {
            p.time_ColctedFromSender = DateTime.Now; //update the time

        }
        public void DeliveringPtoClient(Packagh p)
        {
                    p.time_ComeToColcter = DateTime.Now; //update the time
                    //foreach (Quadocopter q in DataSource.qpter) //look for index of the quadocopter whice take this package
                    //{
                    //    if (q.id == p.idQuadocopter)
                    //        DataSource.qpter[j].mode = statusOfQ.available; //update the qptr to be abailable
                    //}
        }
        /// <summary>
        /// Send the quadocopter to charging.
        /// </summary>
        public void SendQtoCharging(BaseStation b, Quadocopter q)
        {

            Charging c = new Charging();
            c.baseStationID = b.IDnumber;
            c.quadocopterID = q.id;
            b.chargingPositions--;
            //DataSource.qpter[iq].battery = 100;
            //DataSource.qpter[iq].mode = statusOfQ.maintenance ;
            DataSource.charge.Add(c);
        }
        /// <summary>
        /// release te quadocopter frp charging.
        /// </summary>
        public void ReleaseQfromCharging(BaseStation b, Quadocopter q)
        {
            b.chargingPositions++;
            //DataSource.qpter[iq].mode = statusOfQ.available;

            Charging c = new Charging();
            c.baseStationID = b.IDnumber; 
            c.quadocopterID =q.id;
            DataSource.charge.Remove(c);
        }
        /// <summary>
        /// print datails of statin
        /// </summary>
        public void StationDisplay(BaseStation b)//print datails of station 
        {
                    Console.WriteLine(b); //I print it
        }
        /// <summary>
        /// print datails of quadocopter.
        /// </summary>
        public void QuDisplay(Quadocopter q)//print datails of quadocopter
        {
                    Console.WriteLine(q); //I print it
        }
        /// <summary>
        /// print datails of client.
        /// </summary>
        public void ClientDisplay(Client c)//print datails of client
        {
                    Console.WriteLine(c); //I print it
        }
        /// <summary>
        /// print datails of package.
        /// </summary>
        public void PackageDisplay(Packagh p)//print datails of package
        {
                    Console.WriteLine(p); //I print it
        }

        /// <summary>
        /// print all the stations.
        /// </summary>
        public IEnumerable<BaseStation> ListOfStations() //return list of all the stations
        {
            List<BaseStation> l = new List<BaseStation>();
            foreach (BaseStation b in  DataSource.bstion) // I run of all the stations and print them
                l.Add(b);
            return l;
        }
        /// <summary>
        /// return list of all the quadocpters.
        /// </summary>
        public IEnumerable<IDAL.DO.Quadocopter> ListOfQ()//return list of all the quadocpters
        {
            List<IDAL.DO.Quadocopter> list = new List<Quadocopter>();
            foreach (Quadocopter q in DataSource.qpter) // I run of all the stations and print them
                list.Add(q);
            return list;
        }
        /// <summary>
        /// print all the clients
        /// </summary>
        public IEnumerable<Client> ListOfClients()//print all the clients
        {
            List<Client> l = new List<Client>();
            foreach (Client c in DataSource.cli) // I run of all the stations and print them
                l.Add(c);
            return l;
        }
        /// <summary>
        /// print all the packages.
        /// </summary>
        public IEnumerable<Packagh> ListOfPackages()//print all the packages
        {
            List<Packagh> l = new List<Packagh>();
            foreach (Packagh p in DataSource.packagh) // I run of all the stations and print them
                l.Add(p);
            return l;
        }
        /// <summary>
        /// print all the packages that dont assigned to quadocopter.
        /// </summary>
        public IEnumerable<Packagh> ListOfPwithoutQ()//return list of all the packages that dont assigned to quadocopter
        {
            List<Packagh> l = new List<Packagh>();
            foreach (Packagh p in DataSource.packagh) // I run of all the packages and print them if their idQuadocopter is 0
                if (p.idQuadocopter == 0)
                    l.Add(p);
            return l;

        }
        /// <summary>
        /// print all the stations that have empty changing positions.
        /// </summary>
        public IEnumerable<BaseStation> ListOfStationsForCharging()//print all the stations that have empty changing positions
        {
            List<BaseStation> lbs = new List<BaseStation>();
            // I run of all the stations and print them if their changingPosition is not 0
            foreach (BaseStation b in DataSource.bstion) 
                if (b.chargingPositions != 0)
                    lbs.Add(b);
            return lbs;
        }
        public double[] askForElectric()//the quadocopter ask.
        {
            double[] arry =new double[5];
            arry[0] = DataSource.Config.Available;
            arry[1] = DataSource.Config.easy;
            arry[2] = DataSource.Config.hevy;
            arry[3] = DataSource.Config.middle_toCare;
            arry[4] = DataSource.Config.charghingRate;
            return arry;
        }


        //---------------------------------------------------------------------------------------------------------------
        // func to help us.

        /// <summary>
        /// the func get quadocopter's id and base station's id from the user and chak if they in our data.
        /// </summary>
        /// <returns>if all datails from the user are treu</returns>
        //bool allwoToCharge(int[] indexs, bool charge=true)
        //{
        //    Console.WriteLine("Please enter the ID of the quadocopter you want to charge."); //accepting the id of the quadocopter
        //    string temp_str = Console.ReadLine();
        //    int id = int.Parse(temp_str);
        //    int i;
        //    bool exist = false;//to chak if we find the quadocopter's ID.
        //    for (i = 0; i < DataSource.Config.index_quadocopter; i++) //look for index of the quadocopter with the quadocopter ID the user put.
        //        if (DataSource.qpter[i].id == id)
        //        {
        //            exist = true;//we find the quadocopter.
        //            break;
        //        }
        //    if (!exist)//if we didnt find the quadocopter.
        //    {
        //        Console.WriteLine("The quadocopter's ID not exist in our data.");
        //        return false;
        //    }

        //    if (DataSource.qpter[i].mode != statusOfQ.available)
        //    {
        //        Console.WriteLine("The quadocopter are " + DataSource.qpter[i].mode + '.');
        //        return false;
        //    }

        //    if (charge)
        //        ListOfStationsForCharging();
        //    else
        //        ListOfStations();
        //    Console.WriteLine("Please enter the ID of the base station you want to charge in."); //accepting the id of the quadocopter
        //    temp_str = Console.ReadLine();
        //    id = int.Parse(temp_str);
        //    int j;
        //    exist = false;//to chak if we find the base station's ID.
        //    for (j = 0; j < DataSource.Config.index_baseStation; j++) //look for index of the base station with the base station ID the user put.
        //        if (DataSource.bstion[j].IDnumber == id)
        //        {
        //            exist = true;//we find the quadocopter.
        //            break;
        //        }
        //    if (!exist)//if we didnt find the quadocopter.
        //    {
        //        Console.WriteLine("The base station's ID not exist in our data.");
        //        return false;
        //    }
        //    indexs[0] = i;
        //    indexs[1] = j;
        //    return true;
        //}
    }
}
