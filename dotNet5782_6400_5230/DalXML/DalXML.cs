﻿using System;
using System.Collections.Generic;
using DalApi;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using DO;

namespace Dal
{
    public class DalXML //: IDAL
    {
        #region roots
        XElement clientRoot;
        string clientPath = @"ClientXml.xml";

        XElement baseStationRoot;
        string baseStationPath = @"BaseStationXml.xml";

        XElement packageRoot;
        string packagePath = @"PackageXml.xml";

        XElement quadocopterRoot;
        string quadocopterPath = @"QuadocopterXml.xml";

        XElement chargeRoot;
        string chargePath = @"ChargeXml.xml";

        XElement configRoot;
        string configPath = @"ConfigXml.xml";
        #endregion

        public DalXML()
        {
            if (!File.Exists(clientPath))
                CreateFiles(xmlRoots.clientR);
            else
                LoadData_c();

            if (!File.Exists(baseStationPath))
                CreateFiles(xmlRoots.baseStationR);
            else
                LoadData_bs();

            if (!File.Exists(packagePath))
                CreateFiles(xmlRoots.packageR);
            else
                LoadData_p();

            if (!File.Exists(quadocopterPath))
                CreateFiles(xmlRoots.quadocopterR);
            else
                LoadData_q();

            if (!File.Exists(chargePath))
                CreateFiles(xmlRoots.chargeR);
            else
                LoadData_charge();

            if (!File.Exists(configPath))
                CreateFiles(xmlRoots.configR);
            else
                LoadData_config();

        }
        private void CreateFiles(xmlRoots root)
        {
            switch (root)
            {
                case xmlRoots.clientR:
                    clientRoot = new XElement("clients");
                    clientRoot.Save(clientPath);
                    break;
                case xmlRoots.baseStationR:
                    baseStationRoot = new XElement("baseStations");
                    baseStationRoot.Save(baseStationPath);
                    break;
                case xmlRoots.packageR:
                    packageRoot = new XElement("packages");
                    packageRoot.Save(packagePath);
                    break;
                case xmlRoots.quadocopterR:
                    quadocopterRoot = new XElement("quadocopters");
                    quadocopterRoot.Save(quadocopterPath);
                    break;
                case xmlRoots.chargeR:
                    chargeRoot = new XElement("chargeing");
                    chargeRoot.Save(chargePath);
                    break;
                case xmlRoots.configR:
                    configRoot = new XElement("config");
                    startConfig();
                    configRoot.Save(configPath);
                    break;
                default:
                    Console.WriteLine("error criet files");
                    break;
            }
        }

        #region load data
        private void LoadData_bs()
        {
            try
            {
                baseStationRoot = XElement.Load(baseStationPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        private void LoadData_p()
        {
            try
            {
                packageRoot = XElement.Load(packagePath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        private void LoadData_q()
        {
            try
            {
                quadocopterRoot = XElement.Load(quadocopterPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadData_c()
        {
            try
            {
                clientRoot = XElement.Load(clientPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadData_charge()
        {
            try
            {
                chargeRoot = XElement.Load(chargePath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        private void LoadData_config()
        {
            try
            {
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }

        #endregion

        #region add
        ///adding new base station    
        public void AddBaseStation(int id, string name, int chargingPositions, double lon, double lat)
        {
            LoadData_bs();

            XElement ID = new XElement("ID", id);
            XElement Name = new XElement("Name", name);
            XElement ChargingPositions = new XElement("ChargingPositions", chargingPositions);
            XElement FreechargingPositions = new XElement("FreechargingPositions", chargingPositions);
            XElement Longitude = new XElement("Longitude", lon);
            XElement Latitude = new XElement("Latitude", lat);

            //make the location in base 60.
            BaseSixtin six = new BaseSixtin();
            XElement Location_baseSix = new XElement("BaseSix", six.LocationSix(lat, lon).ToString());


            XElement BaseStation = new XElement("Client", ID, Name, ChargingPositions, FreechargingPositions, Longitude, Latitude, Location_baseSix);

            baseStationRoot.Add(BaseStation);
            baseStationRoot.Save(baseStationPath);
        }
        ///adding new Quadocopter
        public void AddQuadocopter(int id, string moodle, int weight)
        {
            LoadData_q();
            XElement ID = new XElement("ID", id);
            XElement Moodle = new XElement("Moodle", moodle);
            XElement Weight;
            if (weight == 1)
                Weight = new XElement("Weight", WeighCategories.easy);
            else if (weight == 2)
                Weight = new XElement("Weight", WeighCategories.middle);
            else
                Weight = new XElement("Weight", WeighCategories.hevy);
            XElement StartCharging = new XElement("startCharging", 0);
            XElement quadocopter = new XElement("Quadocopter", ID, moodle, weight);
            quadocopterRoot.Add(quadocopter);
            quadocopterRoot.Save(quadocopterPath);
        }
        ///adding new client
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat)
        {
            LoadData_c();
            XElement ID = new XElement("id", id);
            XElement Name = new XElement("name", name);
            XElement PhoneNumber = new XElement("phoneNumber", phoneNumber);
            XElement Longitude = new XElement("longitude", lon);
            XElement Latitude = new XElement("latitude", lat);
            XElement Client = new XElement("Client", ID, Name, PhoneNumber, Longitude, Latitude);

            clientRoot.Add(Client);
            clientRoot.Save(clientPath);
        }
        /// adding new package.
        public void AddPackage(int sender, int colecter, int weight, int priority)
        {
            LoadData_p();
            LoadData_config();

            int run = int.Parse(configRoot.Element("runName").Value);
            configRoot.Element("runName").Value = (run + 1).ToString();
            configRoot.Save(configPath);

            XElement ID = new XElement("ID", run);// the id of the package will be according the accepter number
            XElement ID_Sender = new XElement("ID_Sender", sender);
            XElement ID_Reciver = new XElement("ID_Reciver", colecter);
            XElement ID_Quadocopter = new XElement("IDQ_Quadocopter", 0);// the package have not quadocopter
            XElement Weight;
            XElement Priority;
            XElement Time_Create = new XElement("Time_Create", DateTime.Now);// the time of the create is now
            XElement Time_Belong_quadocopter = new XElement("Time_Belong_quadocopter", null);
            XElement Time_ColctedFromSender = new XElement("Time_ColctedFromSender", null);
            XElement Time_ComeToColcter = new XElement("Time_ComeToColcter", null);

            XElement Times = new XElement("TimeOfPackage", Time_Create, Time_Belong_quadocopter, Time_ColctedFromSender, Time_ComeToColcter);// the time of the create is now


            if (weight == 1)
                Weight = new XElement("Weight", WeighCategories.easy);
            else if (weight == 2)
                Weight = new XElement("Weight", WeighCategories.middle);
            else
                Weight = new XElement("Weight", WeighCategories.hevy);

            if (priority == 1)
                Priority = new XElement("Priority", Priorities.reggular);
            else if (priority == 2)
                Priority = new XElement("Priority", Priorities.fast);
            else
                Priority = new XElement("Priority", Priorities.emergency);

            XElement Package = new XElement("Package", ID, ID_Sender, ID_Reciver, ID_Quadocopter, Weight, Priority, Times);

            packageRoot.Add(Package);// enter the new package into the list
            packageRoot.Save(baseStationPath);
        }
        #endregion

        #region update
        /// update name of quadocopter
        public void updateQd(int id, string modle)
        {
            try
            {
                LoadData_q();
                XElement q = (from qu in quadocopterRoot.Elements()
                              where Convert.ToInt32(qu.Element("ID").Value) == id
                              select qu).FirstOrDefault();
                if (q == null)
                    throw new Exception("ID not exist");
                q.Element("Moodle").Value = modle;
                quadocopterRoot.Save(quadocopterPath);

            }
            catch (Exception ex)
            {
                throw new DO.XMLException(quadocopterPath, "ERROR", ex);
            }

        }
        ///update name and number of charging positions of a base station
        public void updateBSdata(int id, string name = null, int chargingPositions = -1)
        {
            LoadData_bs();

            XElement bsElement = (from bs in baseStationRoot.Elements()
                                  where Convert.ToInt32(bs.Element("ID").Value) == id
                                  select bs).FirstOrDefault();
            if (bsElement == null)
                throw new XMLException("ID not exist");
            if (name != null) bsElement.Element("Name").Value = name;
            if (chargingPositions != -1) bsElement.Element("ChargingPositions").Value = chargingPositions.ToString();

            baseStationRoot.Save(baseStationPath);
        }

        /// update name and phone of client
        public void updateCdata(int id, string name = null, int phone = 0)
        {
            XElement clientElement = (from c in clientRoot.Elements()
                                      where Convert.ToInt32(c.Element("ID").Value) == id
                                      select c).FirstOrDefault();
            if (name != null) clientElement.Element("Name").Value = name;
            if (phone != 0) clientElement.Element("PhoneNumber").Value = phone.ToString();

            clientRoot.Save(clientPath);
        }




        /// update package to be belong to a quadocopter.
        public void AssignPtoQ(Package P, int id_q)
        {
            LoadData_p();


            P.idQuadocopter = id_q;
            P.time_Belong_quadocopter = DateTime.Now;

            var pack = (from XElement localP in packageRoot.Elements()
                        where int.Parse(localP.Element("ID").Value) == P.id
                        select localP).FirstOrDefault();

            pack.Element("IDQ_Quadocopter").Value = id_q.ToString();
            pack.Element("TimeOfPackage").Element("Time_Belong_quadocopter").Value = DateTime.Now.ToString();


            packageRoot.Save(packagePath);
        }
        /// update package to be collected by quadocopter.
        public void CollectPbyQ(int pID)
        {
            LoadData_p();


            var pack = (from XElement localP in packageRoot.Elements()
                        where int.Parse(localP.Element("ID").Value) == pID
                        select localP).FirstOrDefault();
            pack.Element("TimeOfPackage").Element("Time_ColctedFromSender").Value = DateTime.Now.ToString();

            packageRoot.Save(packagePath);
        }
        public void DeliveringPtoClient(int pID)
        {
            LoadData_p();


            var pack = (from XElement localP in packageRoot.Elements()
                        where int.Parse(localP.Element("ID").Value) == pID
                        select localP).FirstOrDefault();
            pack.Element("TimeOfPackage").Element("Time_ComeToColcter").Value = DateTime.Now.ToString();

            packageRoot.Save(packagePath);
        }
        /// Send the quadocopter to charging.
        public void SendQtoCharging(int bID, int qID)
        {
            try
            {
                LoadData_q();
                LoadData_bs();
                LoadData_charge();
                //find the qudocopter and the base station
                XElement q = (from qu in quadocopterRoot.Elements()
                              where Convert.ToInt32(qu.Element("ID").Value) == qID
                              select qu).FirstOrDefault();
                XElement b = (from bs in baseStationRoot.Elements()
                              where Convert.ToInt32(bs.Element("ID").Value) == bID
                              select bs).FirstOrDefault();
                if (q == null || b == null)
                    throw new Exception("ID not exist");
                if (Convert.ToInt32(b.Element("FreechargingPositions").Value) <= 0)
                    throw new Exception("There is no place to charge in this base station.");
                //change the startCharging of the q to be now, and free positions of charging of bs to be -1
                q.Element("StartCharging").Value = DateTime.Now.ToString();
                int free = Convert.ToInt32(b.Element("FreechargingPositions").Value);
                free--;
                b.Element("FreechargingPositions").Value = free.ToString();

                quadocopterRoot.Save(quadocopterPath);
                baseStationRoot.Save(baseStationPath);
                //add a item of 'charging'
                XElement BaseStation = new XElement("baseStationID", bID);
                XElement quadocopter = new XElement("quadocopterID", qID);
                XElement Charging = new XElement("charging", BaseStation, quadocopter);

                chargeRoot.Add(Charging);
                chargeRoot.Save(chargePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// release te quadocopter frp charging.
        public void ReleaseQfromCharging(int qID)
        {
            try
            {
                LoadData_q();
                LoadData_bs();
                LoadData_charge();
                //find the qudocopter and the base station
                XElement charge = (from ch in chargeRoot.Elements()
                              where Convert.ToInt32(ch.Element("Qudocopter").Value) == qID
                              select ch).FirstOrDefault();
                XElement b = (from bs in baseStationRoot.Elements()
                              where bs.Element("ID").Value == charge.Element("BaseStation").Value
                              select bs).FirstOrDefault();
                if (b == null)
                    throw new Exception("ID not exist");
                
                //change the free positions of charging of bs to be +1
                
                int free = Convert.ToInt32(b.Element("FreechargingPositions").Value);
                free++;
                b.Element("FreechargingPositions").Value = free.ToString();

                baseStationRoot.Save(baseStationPath);
                
                //remove a item of 'charging'
                charge.Remove();
                chargeRoot.Save(chargePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region print
        /// print datails of statin
        public BaseStation StationDisplay(int id)
        {
            LoadData_bs();

            DmsLocation dms = new DmsLocation();
            BaseStation station = new BaseStation();
            station = (from bs in baseStationRoot.Elements()
                       where Convert.ToInt32(bs.Element("id").Value) == id
                       select new BaseStation()
                       {
                           IDnumber = Convert.ToInt32(bs.Element("id").Value),
                           name = bs.Element("Name").Value,
                           chargingPositions=int.Parse(bs.Element("ChargingPositions").Value),
                           freechargingPositions=Convert.ToInt32(bs.Element("FreechargingPositions").Value),
                           longitude=int.Parse(bs.Element("Longitude").Value),
                           latitude=int.Parse(bs.Element("Latitude").Value),
                           toBaseSix = new BaseSixtin(),
                           decSix = GetBase(double.Parse(bs.Element("Latitude").Value), double.Parse(bs.Element("Longitude").Value))
                       }).FirstOrDefault();

            return station;
        }
        /// print datails of quadocopter.
        public Quadocopter QuDisplay(int id)
        {
            List<Quadocopter> qList = XMLTools.LoadListFromXMLSerializer<Quadocopter>(quadocopterPath);
            Quadocopter q = (from qu in qList
                             where qu.id == id
                             select qu).FirstOrDefault();
            return q; 
        }


        /// print datails of client.
        public Client ClientDisplay(int id)
        {
            List<Client> cList = XMLTools.LoadListFromXMLSerializer<Client>(clientPath);
            Client cli = (from c in cList
                             where c.ID == id
                             select c).FirstOrDefault();
            return cli;
        }
        /// print datails of package.
        public Package PackageDisplay(int id)
        {
            LoadData_p();

            Package pack=new Package();
            pack = (from p in packageRoot.Elements()
                    where Convert.ToInt32(p.Element("id").Value) == id
                    select new Package()
                    {
                        id = Convert.ToInt32(p.Element("ID").Value),
                        sender = int.Parse(p.Element("ID_Sender").Value),
                        receiver = int.Parse(p.Element("ID_Reciver").Value),
                        idQuadocopter = int.Parse(p.Element("IDQ_Quadocopter").Value),
                        priority = (Priorities)(getEnam(p.Element("Priority").Value)),
                        time_Belong_quadocopter = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_Belong_quadocopter").Value),
                        time_ColctedFromSender = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_ColctedFromSender").Value),
                        time_ComeToColcter = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_ComeToColcter").Value),
                        time_Create = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_Create").Value),
                        weight = (WeighCategories)(getEnam(p.Element("Weight").Value))
                    }).FirstOrDefault();

            return pack;
        }
        #endregion

        #region lists
        /// print all the stations.
        public IEnumerable<BaseStation> ListOfStations()
        {
            LoadData_bs();

            IEnumerable<BaseStation> l = from bs in baseStationRoot.Elements()
                    select new BaseStation()
                    {
                        IDnumber = Convert.ToInt32(bs.Element("id").Value),
                        name = bs.Element("Name").Value,
                        chargingPositions = int.Parse(bs.Element("ChargingPositions").Value),
                        freechargingPositions = Convert.ToInt32(bs.Element("FreechargingPositions").Value),
                        longitude = int.Parse(bs.Element("Longitude").Value),
                        latitude = int.Parse(bs.Element("Latitude").Value),
                        toBaseSix = new BaseSixtin(),
                        decSix = GetBase(double.Parse(bs.Element("Latitude").Value), double.Parse(bs.Element("Longitude").Value))
                    };
            return l;
        }
        /// print all the quadocpters.
        public IEnumerable<Quadocopter> ListOfQ()
        {
            IEnumerable<Quadocopter> qList = XMLTools.LoadListFromXMLSerializer<Quadocopter>(quadocopterPath);
            return qList;
            
        }
        /// print all the quadocpters acording to the weigh.
        public IEnumerable<Quadocopter> ListOfQ_of_weigh(string w)
    {
        
        List<Quadocopter> qList = XMLTools.LoadListFromXMLSerializer<Quadocopter>(quadocopterPath);

        WeighCategories weight = new WeighCategories();
        if (w == "easy") weight = WeighCategories.easy;
        else if (w == "middle") weight = WeighCategories.middle;
        else if (w == "heavy") weight = WeighCategories.hevy;

        return from q in qList
               where q.weight == weight
               select q;
    }
        /// print all the clients
        public IEnumerable<Client> ListOfClients()
        {
            IEnumerable<Client> cList = XMLTools.LoadListFromXMLSerializer<Client>(clientPath);
            return cList;
        }
        /// print all the packages.
        public IEnumerable<Package> ListOfPackages()
        {
            LoadData_p();


            var l = from p in baseStationRoot.Elements()
                    select new Package()
                    {
                        id = Convert.ToInt32(p.Element("ID").Value),
                        sender = int.Parse(p.Element("ID_Sender").Value),
                        receiver = int.Parse(p.Element("ID_Reciver").Value),
                        idQuadocopter = int.Parse(p.Element("IDQ_Quadocopter").Value),
                        priority = (Priorities)(getEnam(p.Element("Priority").Value)),
                        time_Belong_quadocopter = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_Belong_quadocopter").Value),
                        time_ColctedFromSender = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_ColctedFromSender").Value),
                        time_ComeToColcter = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_ComeToColcter").Value),
                        time_Create = DateTime.Parse(p.Element("TimeOfPackage").Element("Time_Create").Value),
                        weight = (WeighCategories)(getEnam(p.Element("Weight").Value))
                    };


            return l;
        }
        /// print all the packages that dont assigned to quadocopter.
        public IEnumerable<Package> ListOfPwithoutQ()
        {
            List<Package> l = XMLTools.LoadListFromXMLSerializer<Package>(packagePath);
            return from p in l
                   where p.idQuadocopter == 0
                   select p;
        }
        /// return list of all the stations that have empty changing positions.
        public IEnumerable<BaseStation> ListOfStationsForCharging()
        {
            List<BaseStation> l = new List<BaseStation>();
            l = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            return from bs in l
                   where bs.freechargingPositions != 0
                   select bs;
        }
        ///the quadocopter ask.
        public List<Charging> GetChargings()
        {
            LoadData_charge();


            var l = (from c in chargeRoot.Elements()
                     select new Charging()
                     {
                         baseStationID = int.Parse(c.Element("baseStationID").Value),
                         quadocopterID = int.Parse(c.Element("quadocopterID").Value)
                     }).ToList();

            return l;
        }
        /// return list of all the package from the same sender (with the ID we get).
        public IEnumerable<Package> ListOfPackageFrom(int id)
        {
            LoadData_p();

            List<Package> l = XMLTools.LoadListFromXMLSerializer<Package>(packagePath);
            return from p in l
                   where p.sender == id
                   select p;
        }
        /// return list of all the package from the same receiver (with the ID we get).
        public IEnumerable<Package> ListOfPackageTo(int id)
        {
            LoadData_p();

            List<Package> l = XMLTools.LoadListFromXMLSerializer<Package>(packagePath);
            return from p in l
                   where p.receiver == id
                   select p;
        }
        #endregion

        public double[] askForElectric()
        {
            LoadData_config();

            double[] arry = new double[5];
            arry[0] = double.Parse(configRoot.Element("available").Value);
            arry[1] = double.Parse(configRoot.Element("easy").Value);
            arry[2] = double.Parse(configRoot.Element("hevy").Value);
            arry[3] = double.Parse(configRoot.Element("middle_toCare").Value);
            arry[4] = double.Parse(configRoot.Element("charghingRate").Value);
            return arry;
        }
        /// <summary>
        ///accept id of qudocopter and return package that in it or null 
        /// </summary>
        public Package? searchPinQ(int qID)
        {
            IEnumerable<Package> pList = ListOfPackages();
            Package? p = (from i in pList
                          where i.id == qID
                          select i).FirstOrDefault();
            return p;
        }
        /// <summary>
        /// accept id of package and return the location of its sender
        /// </summary>
        public Location searchLocationOfclient(int pID)
        {
            IEnumerable<Client> cList = ListOfClients();
            return (from c in cList
                    where c.ID == pID
                    select new Location() { latitude = c.latitude, longitude = c.longitude }).FirstOrDefault();
        }
        /// <summary>
        /// return location of randomaly station
        /// </summary>
        public Location randomStationLocation()
        {
            LoadData_bs();

            List<BaseStation> bs = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            Random r = new Random();
            int x = r.Next(0, bs.Count - 1);
            Location l = new Location();
            l.latitude = bs[x].latitude;
            l.longitude = bs[x].longitude;
            return l;
        }
        /// <summary>
        /// return location of randomaly Client the get a package
        /// </summary>
        public Location randomCwithPLocation()
        {
            LoadData_c();
            LoadData_p();

            List<Package> pList = XMLTools.LoadListFromXMLSerializer<Package>(packagePath);
            List<Client> cList = XMLTools.LoadListFromXMLSerializer<Client>(clientPath);

            Random r = new Random();
            List<int> sendersID = new List<int>();
            foreach (Package p in pList)
                if ((p.time_ComeToColcter != null) && (p.time_ComeToColcter.Value.Year != 0001))
                    if (sendersID.Contains(p.sender) == false)
                        sendersID.Add(p.sender);
            List<Location> sendersL = new List<Location>();
            foreach (Client c in cList)
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
            Client dcli = cList[0];
            return new Location() { latitude = dcli.latitude, longitude = dcli.longitude };

            XMLTools.SaveListToXMLSerializer<Package>(pList, packagePath);
            XMLTools.SaveListToXMLSerializer<Client>(cList, clientPath);
        }
        /// <summary>
        /// accept a location and return the closest base station
        /// </summary>
        public BaseStation searchCloseStation(Location l)
        {
            IEnumerable<BaseStation> bsList = ListOfStations();
            BaseStation bs = new BaseStation();
            double minDistance = 100000000, help = 0;
            foreach (BaseStation b in bsList)
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
            IEnumerable<BaseStation> bList = ListOfStations();
            BaseStation bs = new BaseStation();
            double minDistance = 100000000, help = 0;
            foreach (BaseStation b in bList)
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
                Location senderLocation = searchLocationOfclient(p.sender);
                Location receiverL = searchLocationOfclient(p.receiver);
                BaseStation stationL = searchCloseStation(receiverL);
                Location stationLocation = new Location() { longitude = stationL.longitude, latitude = stationL.latitude };
                double distance = GetDistance(loc, senderLocation) + GetDistance(senderLocation, receiverL) + GetDistance(receiverL, stationLocation);
                int minBattery = (int)(distance * askForElectric()[(int)p.weight]);
                if (battery >= minBattery)
                    newPackages.Add(p);
            }
            return newPackages;
        }
        /// <summary>
        /// accept id of package of id of its sender/receiver and return the another client of this package(receiver/sender)
        /// </summary>
        public Client searchAnotherClient(int pID, int clientID)
        {
            IEnumerable<Package> pList = XMLTools.LoadListFromXMLSerializer<Package>(packagePath);
            IEnumerable<Client> cList = XMLTools.LoadListFromXMLSerializer<Client>(clientPath);

            foreach (Package p in pList)
                if (p.id == pID)
                    if (p.sender == clientID)
                    {
                        foreach (Client c in cList)
                            if (c.ID == p.receiver)
                                return c;
                    }
                    else if (p.receiver == clientID)
                    {
                        foreach (Client c in cList)
                            if (c.ID == p.sender)
                                return c;
                    }
                    else throw new DALException("there is no package in this client");
            throw new DALException("there is no package in this client");
        }

        /// <summary>
        /// active only once to start the data.
        /// </summary>
        void startConfig()
        {
            LoadData_config();

            XElement runNum = new XElement("runName", 0);

            XElement Available = new XElement("available", 1);
            XElement easy = new XElement("easy", 2);
            XElement hevy = new XElement("hevy", 3);
            XElement middle_toCare = new XElement("middle_toCare", 4);
            XElement charghingRate = new XElement("charghingRate", 5);

            XElement Electric = new XElement("Electric", Available, easy, hevy, middle_toCare, charghingRate);
            configRoot.Add(runNum, Electric);
            configRoot.Save(configPath);
        }

        DmsLocation GetBase(double lat, double len)
        {
            BaseSixtin ba = new BaseSixtin();

            return (ba.LocationSix(lat, len));
        }
        public double GetDistance(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l1.latitude - l2.latitude, 2) + Math.Pow(l1.longitude - l2.longitude, 2));
        }

        int getEnam(string str)
        {
            switch (str)
            {
                case "easy": return 1;
                case "middle": return 2;
                case "hevy": return 3;
                case "reggular": return 0;
                case "fast": return 1;
                case "emergency": return 2;
                default:
                    throw new XMLException("the string not exis in the enum priorities.");
            }
        }
    }
}