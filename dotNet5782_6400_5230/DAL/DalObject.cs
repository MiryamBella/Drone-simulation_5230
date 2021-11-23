using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using System.Device.Location;
using DAL.exceptions.DO;


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
            station.freechargingPositions = chargingPositions;
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
            Package p = new Package();
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
        //-----------------update functions----------
        #region updateQuadocopter;
        /// <summary>
        /// update the modle of qudocopter
        /// </summary>
        public void updateQd(int id, string modle)
        {
            Quadocopter newQ = new Quadocopter();
            bool finded = false;
            foreach (Quadocopter q in DataSource.qpter)
                if (q.id == id)
                {
                    finded = true;
                    newQ = q;
                    DataSource.qpter.Remove(q);
                }
            if (finded)
            {
                newQ.moodle = modle;
                DataSource.qpter.Add(newQ);
            }
            else Console.WriteLine("ERROR");        
        }
        #endregion;
        #region updateBaseStation;
        ///update name and number of charging positions of a base station
        public void updateSdata(int id, string name, int chargingPositions)
        {
            BaseStation newBS = new BaseStation();
            bool finded = false;
            foreach (BaseStation bs in DataSource.bstion)
                if (bs.IDnumber == id)
                {
                    finded = true;
                    newBS = bs;
                    DataSource.bstion.Remove(bs);
                }
            if (finded)
            {
                if (name != null) newBS.name = name;
                if (chargingPositions != -1) newBS.chargingPositions = chargingPositions;
                DataSource.bstion.Add(newBS);
            }
            else Console.WriteLine("ERROR");
        }
        #endregion;
        #region updateClient;
        /// <summary>
        /// update name and phone of client
        /// </summary>
        public void updateCdata(int id, string name = null, int phone = 0)
        {
            Client newC = new Client();
            bool finded = false;
            foreach (Client c in DataSource.cli)
                if (c.ID == id)
                {
                    finded = true;
                    newC = c;
                    DataSource.cli.Remove(c);
                }
            if (finded)
            {
                if (name != null) newC.name = name;
                if (phone != 0) newC.phoneNumber = phone;
                DataSource.cli.Add(newC);
            }
            else Console.WriteLine("ERROR");
        }
        #endregion;
        /// <summary>
        /// update package to be belong to a quadocopter.
        /// </summary>
        public void AssignPtoQ(Package P, int id_q) 
        {
            for (int i = 0; i < DataSource.packagh.Count; i++)
                if (DataSource.packagh[i].id == P.id)
                {
                    P.idQuadocopter = id_q;
                    P.time_Belong_quadocopter = DateTime.Now;
                    DataSource.packagh[i] = P;
                    break;
                }
        }
        /// <summary>
        /// update package to be collected by quadocopter.
        /// </summary>
        public void CollectPbyQ(Package p) 
        {
            p.time_ColctedFromSender = DateTime.Now; //update the time

        }
        public void DeliveringPtoClient(Package p)
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
        public void SendQtoCharging(int bID, int qID)
        {

            Charging c = new Charging(); // add a charging
            c.baseStationID = bID;
            c.quadocopterID = qID;
            DataSource.charge.Add(c);
            for (int i = 0; i < DataSource.bstion.Count; i++)
                if (DataSource.bstion[i].IDnumber == bID)
                {
                    BaseStation b = DataSource.bstion[i];
                    b.freechargingPositions--;
                    DataSource.bstion[i] = b;
                    break;
                }
        }
            Charging c = new Charging();
            foreach(BaseStation b in DataSource.bstion)
            {
                if (b.IDnumber == bID) ///find the base station
                { 
                    foreach(Quadocopter q in DataSource.qpter)
                    {
                        if (q.id == qID)///find the quadocopter.
                        {
                            SendQtoCharging_doingThat(b, q, c);
                            return;
                        }
                    }
                    throw new DALException("The ID of the quadocopter not exist.");
                }
            }
            throw new DALException("The ID of the base station not exist.");
        }
        /// <summary>
        /// release te quadocopter frp charging.
        /// </summary>
        public void ReleaseQfromCharging(int qID)
        {
            Charging c = new Charging();
            int bsID = 0;
            foreach (Charging ch in DataSource.charge)
                if (ch.quadocopterID == qID)
                {
                    c = ch;
                    bsID = ch.baseStationID;
                    break;
                }
            DataSource.charge.Remove(c);
            for (int i = 0; i < DataSource.bstion.Count; i++)
                if (DataSource.bstion[i].IDnumber == bsID)
                {
                    BaseStation b = DataSource.bstion[i];
                    b.freechargingPositions++;
                    DataSource.bstion[i] = b;
                    break;
                }
        public void ReleaseQfromCharging(int qID)
        {
            foreach(Quadocopter q in DataSource.qpter)
            {
                if (q.id == qID)///find if there is that quadocopter
                {
                    foreach (Charging c in DataSource.charge)
                    {
                        ///find if the quadocopter are charging
                        if (c.quadocopterID == q.id)
                        {
                            foreach (BaseStation b in DataSource.bstion)
                            {
                                ///find the base station he in charge
                                if (b.IDnumber == c.baseStationID)
                                {
                                    ReleaseQfromCharging_doingThat(b, q);
                                    return;
                                }
                            }
                            throw new DALException("The base statation the quadocopter not exist.");
                        }
                    }
                    throw new DALException("The quadocopter not charging.");
                }
            }
            throw new DALException("The ID of the quadocopter not exist.");
        }
        /// <summary>
        /// print datails of statin
        /// </summary>
        public BaseStation StationDisplay(int id)//print datails of station 
        {
            foreach (BaseStation temp in DataSource.bstion)
            {
                if (temp.IDnumber == id)
                    return temp;
            }
            BaseStation b = new BaseStation { IDnumber = (int)0 };
            return b;
        }
        /// <summary>
        /// print datails of quadocopter.
        /// </summary>
        public Quadocopter QuDisplay(int id)//print datails of quadocopter
        {
            foreach (Quadocopter temp in DataSource.qpter)
            {
                if (temp.id == id)
                    return temp;
            }
            Quadocopter q = new Quadocopter { id = 0 };

            return q;
        }
        /// <summary>
        /// print datails of client.
        /// </summary>
        public Client ClientDisplay(int id)//print datails of client
        {
            foreach (Client temp in DataSource.cli)
            {
                if (temp.ID == id)
                    return temp;
            }
            Client c = new Client { ID = 0 };

            return c;
        }
        /// <summary>
        /// print datails of package.
        /// </summary>
        public Package PackageDisplay(int id)//print datails of package
        {
            foreach (Package temp in DataSource.packagh)
            {
                if (temp.id == id)
                    return temp;
            }
            Package p = new Package { id = 0 };

            return p;
        }

        /// <summary>
        /// print all the stations.
        /// </summary>
        public IEnumerable<BaseStation> ListOfStations() //return list of all the stations
        {
            List<BaseStation> l = new List<BaseStation>();
            foreach (BaseStation b in  DataSource.bstion) // I run of all the stations and print them
                l.Add((BaseStation)b.Clone());
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
        public IEnumerable<Package> ListOfPackages()//print all the packages
        {
            List<Package> l = new List<Package>();
            foreach (Package p in DataSource.packagh) // I run of all the stations and print them
                l.Add(p);
            return l;
        }
        /// <summary>
        /// print all the packages that dont assigned to quadocopter.
        /// </summary>
        public IEnumerable<Package> ListOfPwithoutQ()//return list of all the packages that dont assigned to quadocopter
        {
            List<Package> l = new List<Package>();
            foreach (Package p in DataSource.packagh) // I run of all the packages and print them if their idQuadocopter is 0
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
                if (b.freechargingPositions != 0)
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
        /// <summary>
        /// accept id of qudocopter and return the package in it or null
        /// </summary>
        /// <param name="qID"><>
        /// <returns><int>
        public Package? searchPinQ(int qID)
        {
            foreach (Package p in DataSource.packagh)
                if (p.id == qID)
                    return p;
            return null;
        }
        /// <summary>
        /// accept id of package and return the location of its sender
        /// </summary>
        public Location searchLocationOfclient(int pID)
        {
            foreach (Client c in DataSource.cli)
                if (c.ID == pID)
                    return new Location { latitude = c.latitude, longitude = c.longitude };
            return null;
        }
        /// <summary>
        /// return location of ranomaly station
        /// </summary>
        public Location randomStationLocation()
        {
            Random r = new Random();
            int x = r.Next(0, DataSource.bstion.Count - 1);
            Location l = new Location();
            l.latitude = DataSource.bstion[x].latitude;
            l.longitude = DataSource.bstion[x].longitude;
            return l;
        }
        /// <summary>
        /// return location of randomaly Client the get a package
        /// </summary>
        public Location randomCwithPLocation()
        {
            Random r = new Random();
            List<int> sendersID = new List<int>();
            foreach (Package p in DataSource.packagh)
                if (p.time_ComeToColcter.Year != 0001)
                    if (sendersID.Contains(p.sender) == false)
                        sendersID.Add(p.sender);
            List<Location> sendersL = new List<Location>();
            foreach (Client c in DataSource.cli)
                if (sendersID.Contains(c.ID))
                {
                    Location l = new Location() { latitude = c.latitude, longitude = c.longitude };
                    sendersL.Add(l);
                }
            int x = r.Next(0, sendersL.Count - 1);
            return sendersL[x];
        }
        /// <summary>
        /// accept a location and return the closest base station
        /// </summary>
        public BaseStation searchCloseStation(Location l)
        {
            BaseStation bs = new BaseStation();
            double minDistance = 100000000, help = 0;
            foreach (BaseStation b in DataSource.bstion) 
            {
                help = new GeoCoordinate(b.latitude, b.longitude).GetDistanceTo(coverLtoG(l));
                if (help < minDistance)
                {
                    minDistance = help;
                    bs = b;
                } 
            }
            return bs;
        }
        /// <summary>
        /// accept a location and return the closest base station with a free charge position
        /// </summary>
        public BaseStation searchCloseEmptyStation(Location l)
        {
            BaseStation bs = new BaseStation();
            double minDistance = 100000000, help = 0;
            foreach (BaseStation b in DataSource.bstion)
            {
                help = new GeoCoordinate(b.latitude, b.longitude).GetDistanceTo(coverLtoG(l));
                if (help < minDistance && b.freechargingPositions != 0)
                {
                    minDistance = help;
                    bs = b;
                }
            }
            return bs;
        }
        //get a location and return it as geocoordinate
        public GeoCoordinate coverLtoG(Location l)
        {
            return new GeoCoordinate(l.longitude, l.latitude);
        }
        /// <summary>
        /// accept a location of qudocopoter and its battery and return list of package that the q can take
        /// </summary>
        public List<Package> availablePtoQ(int battery, GeoCoordinate loc)
        {
            List<Package> packages = new List<Package>();
            foreach (Package p in DataSource.packagh)
            {
                GeoCoordinate senderLocation = coverLtoG(searchLocationOfclient(p.sender));
                Location receiverL = searchLocationOfclient(p.receiver);
                GeoCoordinate receiverLocation = new GeoCoordinate(receiverL.longitude, receiverL.latitude);
                BaseStation stationL = searchCloseStation(receiverL);
                GeoCoordinate stationLocation = new GeoCoordinate(stationL.longitude, stationL.latitude);
                double distance = loc.GetDistanceTo(senderLocation) + senderLocation.GetDistanceTo(receiverLocation) + receiverLocation.GetDistanceTo(stationLocation);
                int minBattery = (int)(distance * askForElectric()[(int)p.weight]);
                if (battery >= minBattery)
                    packages.Add(p);
            }
            return packages;
        }
        ///---------------------------------------------------------------------------------------------------------------
        /// func to help us.
        void SendQtoCharging_doingThat(BaseStation b, Quadocopter q, Charging c)
        {
            c.baseStationID = b.IDnumber;
            c.quadocopterID = q.id;
            b.freechargingPositions--;
            DataSource.charge.Add(c);
        }
        public void ReleaseQfromCharging_doingThat(BaseStation b, Quadocopter q)
        {

            b.freechargingPositions++;
            Charging c = new Charging();
            c.baseStationID = b.IDnumber;
            c.quadocopterID = q.id;
            DataSource.charge.Remove(c);
        }

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
