using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
//using DAL.exceptions.DO;
using DalApi;


namespace Dal
{
    sealed class DalObject : DalApi.IDAL
    {
        static readonly IDAL instance = new DalObject();
        public static IDAL Instance { get => instance; }
        ///When this class is built it first initializes the lists with the initial values defined in Initialize
        public DalObject() { DataSource.Initialize(); }
        
        #region add funcs

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
            p.time_Belong_quadocopter = null;
            p.time_ColctedFromSender = null;
            p.time_ComeToColcter = null;
            DataSource.packagh.Add(p);  // enter the new package into the list
        }
        #endregion;

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
                    break;
                }
            if (finded)
            {
                newQ.moodle = modle;
                DataSource.qpter.Add(newQ);
            }
            else throw new DALException("ID not exist");
        }
        /// <summary>
        /// Send the quadocopter to charging.
        /// </summary>
        public void SendQtoCharging(int bID, int qID)
        {

            for (int i = 0; i < DataSource.bstion.Count; i++)
                if (DataSource.bstion[i].IDnumber == bID)
                {
                    if (DataSource.bstion[i].freechargingPositions <= 0)
                        throw new DALException("There is no place to charge in this base station.");
                    BaseStation b = DataSource.bstion[i];
                    b.freechargingPositions--;
                    DataSource.bstion[i] = b;
                    break;
                }
            bool exist = false;
            //i did normal for and not forheach becose i need the object himself and not iteretor of the object.
            for (int i = 0; i < DataSource.qpter.Count; i++)
                if (DataSource.qpter[i].id == qID)
                {
                    Quadocopter q = DataSource.qpter[i];
                    q.startCharge = DateTime.Now;
                    DataSource.qpter[i] = q;
                    exist = true;
                    break;
                }
            if (!exist)
                throw new DALException("the drone not exist.");

            Charging c = new Charging(); // add a charging
            c.baseStationID = bID;
            c.quadocopterID = qID;
            DataSource.charge.Add(c);                
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
        }
        #endregion;
        #region updateBaseStation;
        ///update name and number of charging positions of a base station
        public void updateBSdata(int id, string name, int chargingPositions)
        {
            BaseStation newBS = new BaseStation();
            bool finded = false;
            foreach (BaseStation bs in DataSource.bstion)
                if (bs.IDnumber == id)
                {
                    finded = true;
                    newBS = bs;

                    if (chargingPositions != bs.chargingPositions && chargingPositions < (bs.chargingPositions - bs.freechargingPositions))
                        throw new DALException("Thre is drones who in charge in those charging position.");

                    DataSource.bstion.Remove(bs);
                    break;
                }
            if (finded)
            {
                if (name != null) newBS.name = name;
                if (chargingPositions >= 0)
                {
                    int withQ = newBS.chargingPositions - newBS.freechargingPositions;
                    newBS.chargingPositions = chargingPositions;
                    newBS.freechargingPositions = chargingPositions - withQ;
                }
                DataSource.bstion.Add(newBS);
            }
            else throw new DALException("ID not exist");
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
                    break;
                }
            if (finded)
            {
                if (name != null) newC.name = name;
                if (phone != 0) newC.phoneNumber = phone;
                DataSource.cli.Add(newC);
            }
            else throw new DALException("ID not exist");
        }
        #endregion;
        #region updatePckage;
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
        public void CollectPbyQ(int pID)
        {
            for (int i = 0; i < DataSource.packagh.Count; i++)
                if (DataSource.packagh[i].id == pID)
                {
                    Package p = DataSource.packagh[i];
                    p.time_ColctedFromSender = DateTime.Now;
                    DataSource.packagh[i] = p;
                    break;
                }
        }
        public void DeliveringPtoClient(int pID)
        {
            for (int i = 0; i < DataSource.packagh.Count; i++)
                if (DataSource.packagh[i].id == pID)
                {
                    Package p = DataSource.packagh[i];
                    p.time_ComeToColcter = DateTime.Now;
                    DataSource.packagh[i] = p;
                    break;
                }

        }
        #endregion;

        #region display
        /// <summary>
        /// Print datails of base statin and get ID.
        /// </summary>
        public BaseStation StationDisplay(int id)//print datails of station 
        {
            foreach (BaseStation temp in DataSource.bstion)
            {
                if (temp.IDnumber == id)
                    return temp;
            }
            throw new DALException("ID not exsit.");
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
            throw new DALException("ID not exist.");
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
            throw new DALException("ID not exist.");
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

        #region lists
        public List<Charging> GetChargings()
        {
            return DataSource.charge;
        }

        /// <summary>
        /// print all the stations.
        /// </summary>
        public IEnumerable<BaseStation> ListOfStations() //return list of all the stations
        {
            return from b in DataSource.bstion select b;

            //List<BaseStation> l = new List<BaseStation>();
            //foreach (BaseStation b in DataSource.bstion) 
            //    l.Add((BaseStation)b.Clone());
            //return l;
        }
        /// <summary>
        /// return list of all the quadocpters.
        /// </summary>
        public IEnumerable<DO.Quadocopter> ListOfQ()//return list of all the quadocpters
        {
            return from q in DataSource.qpter select q;
            //List<DO.Quadocopter> list = new List<Quadocopter>();
            //foreach (Quadocopter q in DataSource.qpter) // I run of all the stations and print them
            //    list.Add(q);
            //return list;
        }
        /// print all the quadocpters acording to the weigh.
        public IEnumerable<Quadocopter> ListOfQ_of_weigh(string w)
        {
            WeighCategories weigh = new WeighCategories();
            switch (w)
            {
                case "easy":
                    weigh = WeighCategories.easy;
                    break;
                case "hevy":
                    weigh = WeighCategories.hevy;
                    break;
                case "middle":
                    weigh = WeighCategories.middle;
                    break;
                default:
                    throw new DALException("invelebel weigh statos.");
            }

            //List<Quadocopter> l = new List<Quadocopter>();
            //foreach (Quadocopter ql in DataSource.qpter)
            //{
            //    if ((int)ql.weight <= (int)weigh)
            //        l.Add(ql);
            //}
            List<Quadocopter> list = new List<Quadocopter>();
            var temp = (from q in DataSource.qpter
                        where (int)q.weight <= (int)weigh
                        select q).ToList();
            foreach (var q in DataSource.qpter)
                if(q.weight >=weigh)
                    list.Add(q);
            return list;
        }

        /// <summary>
        /// print all the clients
        /// </summary>
        public IEnumerable<Client> ListOfClients()//print all the clients
        {
            return (from c in DataSource.cli select c);
            //List<Client> l = new List<Client>();
            //foreach (Client c in DataSource.cli) // I run of all the stations and print them
            //    l.Add(c);
            //return l;
        }
        /// <summary>
        /// print all the packages.
        /// </summary>
        public IEnumerable<Package> ListOfPackages()//print all the packages
        {
            return from p in DataSource.packagh select p;
            //List<Package> l = new List<Package>();
            //foreach (Package p in DataSource.packagh) // I run of all the stations and print them
            //    l.Add(p);
            //return l;
        }
        /// <summary>
        /// print all the packages that dont assigned to quadocopter.
        /// </summary>
        public IEnumerable<Package> ListOfPwithoutQ()//return list of all the packages that dont assigned to quadocopter
        {
            return from p in DataSource.packagh
                   where p.idQuadocopter == 0
                   select p;
            //List<Package> l = new List<Package>();
            //foreach (Package p in DataSource.packagh) // I run of all the packages and print them if their idQuadocopter is 0
            //    if (p.idQuadocopter == 0)
            //        l.Add(p);
            //return l;

        }
        /// <summary>
        /// get all the stations that have empty changing positions.
        /// </summary>
        public IEnumerable<BaseStation> ListOfStationsForCharging()//print all the stations that have empty changing positions
        {
            return from b in DataSource.bstion
                   where b.freechargingPositions != 0
                   select b;
            //List<BaseStation> lbs = new List<BaseStation>();
            //// I run of all the stations and print them if their changingPosition is not 0
            //foreach (BaseStation b in DataSource.bstion)
            //    if (b.freechargingPositions != 0)
            //        lbs.Add(b);
            //return lbs;
        }
        
        /// return list of all the package that the accepte id is of its sender.
        public IEnumerable<Package> ListOfPackageFrom(int id)
        {
            return from p in DataSource.packagh
                   where p.sender == id
                   select p;
        }
        /// return list of all the package that the accepte id is of its receiver.
        public IEnumerable<Package> ListOfPackageTo( int id)
        {
            return from p in DataSource.packagh
                   where p.receiver == id
                   select p;
        }
        #endregion
        #endregion;

        public double[] askForElectric()//the quadocopter ask.
        {
            double[] arry = new double[5];
            arry[0] = DataSource.Config.Available;
            arry[1] = DataSource.Config.easy;
            arry[2] = DataSource.Config.middle_toCare;
            arry[3] = DataSource.Config.hevy;
            arry[4] = DataSource.Config.charghingRate;
            return arry;
        }
        /// <summary>
        /// accept id of qudocopter and return the package in it or null
        /// </summary>
        /// <param name="qID"><>
        /// <paramref name="sitoation">defulte is create</paramref>
        /// <returns>package in some sitiotion in q<int>
        public Package? searchPinQ(int qID, p_thet sitoation)
        {
            foreach (Package p in DataSource.packagh)
                if (p.idQuadocopter == qID)
                {
                    switch (sitoation)
                    {
                        case p_thet.Create:
                                return p;
                        case p_thet.Belong:
                            if (p.time_Belong_quadocopter == null)
                                return p;
                            break;
                        case p_thet.ColctedFromSender:
                            if (p.time_ColctedFromSender == null)
                                return p;
                            break;
                        case p_thet.ComeToColcter:
                            if (p.time_ComeToColcter == null)
                                return p;
                            break;
                        default:
                            break;
                    }
                }
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
        /// </summary>
        /// accept id of package of id of its sender/receiver and return the another client of this package(receiver/sender)
        /// </summary>
        public Client searchAnotherClient(int pID, int clientID)
        {
            foreach (Package p in DataSource.packagh)
                if (p.id == pID)
                    if (p.sender == clientID)
                    {
                        foreach (Client c in DataSource.cli)
                            if (c.ID == p.receiver)
                                return c;
                    }
                    else if (p.receiver == clientID)
                    {
                        foreach (Client c in DataSource.cli)
                            if (c.ID == p.sender)
                                return c;
                    }
                    else throw new DALException("there is no package in this client");
            throw new DALException("there is no package in this client");


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
                if ((p.time_ComeToColcter != null) && (p.time_ComeToColcter.Value.Year != 0001))
                    if (sendersID.Contains(p.sender) == false)
                        sendersID.Add(p.sender);
            List<Location> sendersL = new List<Location>();
            foreach (Client c in DataSource.cli)
                if (sendersID.Contains(c.ID))
                {
                    Location l = new Location() { latitude = c.latitude, longitude = c.longitude };
                    sendersL.Add(l);
                }
            if (sendersL.Count != 0)
            {
                int x = r.Next(0, sendersL.Count - 1);
                return sendersL[x];
            }
            Client dcli = DataSource.cli[0];
            return new Location() { latitude = dcli.latitude, longitude = dcli.longitude };
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
                help = GetDistance(l, new Location() { latitude = b.latitude, longitude = b.longitude });
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
                help = GetDistance(l, new Location() { latitude = b.latitude, longitude = b.longitude });
                if (help < minDistance && b.freechargingPositions != 0)
                {
                    minDistance = help;
                    bs = b;
                }
            }
            return bs;
        }
       
        /// <summary>
        /// accept a location of qudocopoter and its battery and return list of package that the q can take
        /// </summary>
        public List<Package> availablePtoQ(int battery, Location loc, IEnumerable<Package> packages)
        {
            List<Package> newPackages = new List<Package>();
            foreach (Package p in packages)
            {
                if (p.time_Belong_quadocopter == null)
                {
                    Location senderLocation = searchLocationOfclient(p.sender);
                    Location receiverL = searchLocationOfclient(p.receiver);
                    BaseStation stationL = searchCloseStation(receiverL);
                    Location stationLocation = new Location() { longitude = stationL.longitude, latitude = stationL.latitude };
                    double distance = GetDistance(loc, senderLocation) + GetDistance(senderLocation, receiverL) + GetDistance(receiverL, stationLocation);
                    int minBattery = (int)(distance * askForElectric()[(int)p.weight]);
                    if (battery >= minBattery)
                        newPackages.Add(p);
                }
            }
            return newPackages;
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
        public double GetDistance(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l1.latitude - l2.latitude, 2) + Math.Pow(l1.longitude - l2.longitude, 2));
        }
    }
}
