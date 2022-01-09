using System;
using System.Collections.Generic;
using System.Text;
using DalApi;
using BO;
using System.Device.Location;
using System.Linq;


namespace BlApi
{
    public partial class BL
    {
        List<QuadocopterToList> q_list = new List<QuadocopterToList>();
        /// <summary>
        /// to cover fro list from IDAL to list from IBL.
        /// </summary>
        IEnumerable<DO.Quadocopter> help_list = new List<DO.Quadocopter>();

        #region cover base station
        //--------cover base station---------------------------------------------------

        List<BaseStation> cover_to_our_list(IEnumerable<DO.BaseStation> old_l)
        {
            return (from x in old_l
                   select cover(x)).ToList();
        }
        BaseStation cover(DO.BaseStation b)
        {
            BaseStation new_bs = new BaseStation();
            new_bs.ID = b.IDnumber;
            new_bs.name = b.name;
            new_bs.thisLocation.Latitude =b.latitude;
            new_bs.thisLocation.Longitude = b.longitude;
            new_bs.thisLocation.decSix = new_bs.thisLocation.toBaseSix.LocationSix(b.latitude, b.longitude);
            new_bs.freeChargingPositions = b.freechargingPositions;
            // now we will get the: new_bs.qudocopters.

            ///get all tha q that charging
            List<DO.Charging> chrgh_list = new List<DO.Charging>();
            bool exsit = false;
            chrgh_list = dal.GetChargings();
            if (chrgh_list == null)
            {
                new_bs.qudocopters = null;
                return new_bs;
            }
            foreach (DO.Charging chargeh in chrgh_list)
            {
                ///find what q is charge in our base station
                if (chargeh.baseStationID == new_bs.ID)
                {
                    ///chek if the q is exsit in our data base.
                    foreach (QuadocopterToList q in q_list)
                    {

                        if (q.ID == chargeh.quadocopterID)
                        {
                            ///the q is exist.
                            exsit = true;
                            QuadocopterInCharge chargeQ = new QuadocopterInCharge();
                            chargeQ.battery = q.battery;
                            chargeQ.ID = q.ID;
                            new_bs.qudocopters.Add(chargeQ);
                            break;
                        }
                    }
                    if (!exsit)
                        throw new BLException("There is q who charge but not count in the base station.");
                }
            }


            return new_bs;
        }

        BaseStationToList cover(BaseStation bs)
        {
            BaseStationToList bsl = new BaseStationToList();

            bsl.ID = bs.ID;
            bsl.name = bs.name;
            bsl.busyChargingPositions = bs.qudocopters.Count;
            bsl.freeChargingPositions = bs.freeChargingPositions;

            return bsl;
        }
        #endregion

        #region cover client
        //--------cover client---------------------------------------------------
        List<Client> cover_to_our_list(IEnumerable<DO.Client> old_l)
        {
            List<Client> new_l = new List<Client>();
            foreach (DO.Client q in old_l)
                new_l.Add(cover(q));
            return new_l;
        }
        public Client cover(DO.Client c)
        {
            
            Client new_c = new Client();
            new_c.ID = c.ID;
            new_c.name = c.name;
            //returnC.packageFrom;  thos two we find next.
            //returnC.packageTo;
            new_c.phoneNumber = c.phoneNumber;
            new_c.thisLocation.Latitude = c.latitude;
            new_c.thisLocation.Longitude = c.longitude;
            new_c.thisLocation.decSix = new DmsLocation();
            new_c.thisLocation.toBaseSix = new BaseSixtin();

            IEnumerable<DO.Package> p_l = dal.ListOfPackages();
            foreach (DO.Package p in p_l)
            {
                //id=the ID of our client.
                if (p.sender == new_c.ID)
                    new_c.packageFrom.Add(cover(cover(p), c.ID));
                if (p.receiver == new_c.ID)
                    new_c.packageTo.Add((cover(cover(p), c.ID)));
            }

            return new_c;
        }
        public Client cover(ClientToList cli)
        {
            return cover(dal.ClientDisplay(cli.ID));
        }
        #endregion

        #region cover package
        //---------------------------------cover package----------------------------------------------------------------------------------
        List<Package> cover_to_our_list(List<DO.Package> old_l)
        {
            return (from x in old_l
                    select cover(x)).ToList();
        }
        public Package cover(DO.Package p)
        {
            Package new_p = new Package();
            new_p.ID = p.id;
            new_p.priority =(Priorities)p.priority;
            ///new_p.q we find him in line 142. 
            new_p.time_Belong_quadocopter = p.time_Belong_quadocopter;
            new_p.time_ColctedFromSender = p.time_ColctedFromSender;
            new_p.time_ComeToColcter = p.time_ComeToColcter;
            new_p.time_Create = p.time_Create;
            new_p.weight = (WeighCategories)p.weight;

            //get the client receiver end the client sender.
            IEnumerable<DO.Client> c_l = dal.ListOfClients();
            bool exist_cs = false;
            bool exist_cr = false;
            foreach (DO.Client c in c_l)
            {
                if (c.ID == p.receiver)
                {
                    new_p.receiver = new clientInPackage() { ID = c.ID, name = c.name };
                    exist_cr = true;
                }
                if (c.ID == p.sender)
                {
                    new_p.sender = new clientInPackage() { ID = c.ID, name = c.name };
                    exist_cs = true;
                }
            }
            if ((!exist_cr) || (!exist_cs))
                throw new BLException("There is none person how recive or send this pachege");

            //get the q.
            bool exist = false;
            if (p.time_Belong_quadocopter != null)
            {
                foreach (QuadocopterToList q in q_list)
                {
                    if (q.ID == p.idQuadocopter)
                    {
                        exist = true;
                        new_p.q = new QuadocopterInPackage() { ID = q.ID, thisLocation = q.thisLocation, battery = q.battery };
                        break;
                    }
                }
                if (!exist)
                    throw new BLException("this q isnt exist");
            }
            else 
                new_p.q = null;
            return new_p;
        }
        public PackageInClient cover(Package p, int clientID)
        {
            PackageInClient new_p = new PackageInClient();
            new_p.ID = p.ID;
            new_p.priority = (Priorities)p.priority;
            new_p.weight = (WeighCategories)p.weight;
            if (p.time_ComeToColcter != null) new_p.state = stateOfP.provided;
            else if (p.time_ColctedFromSender != null) new_p.state = stateOfP.collected;
            else if (p.time_Belong_quadocopter != null) new_p.state = stateOfP.associated;
            else new_p.state = stateOfP.Defined;
            DO.Client c = new DO.Client();
            try
            {
                c = dal.searchAnotherClient(p.ID, clientID);
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }
            clientInPackage cINp = new clientInPackage() { ID = c.ID, name = c.name };
            new_p.theOtherClient = cINp;

            return new_p;
        }
        public Package cover(PackageToList p)
        {
            return cover(dal.PackageDisplay(p.ID));
        }
        #endregion

        #region cover qudocopter
        //--------------------------------cover qudocopter---------------------------------------------------
        List<QuadocopterToList> cover_to_our_list(IEnumerable<DO.Quadocopter> old_l)
        {
            List<QuadocopterToList> new_l = new List<QuadocopterToList>();
            foreach (DO.Quadocopter q in old_l)
            {
                QuadocopterToList temp = new QuadocopterToList();
                temp = cover_list(q);
                new_l.Add(temp);
            }

            return new_l;
        }
        QuadocopterToList cover_list(DO.Quadocopter q)
        {
            Random r = new Random();
            QuadocopterToList new_q = new QuadocopterToList();
            new_q.ID = q.id; //id is as the id
            new_q.moodle = q.moodle; // moodle is the same
            if (q.weight == DO.WeighCategories.easy) new_q.weight = WeighCategories.easy; //whiget converted to the categories of dal
            else if (q.weight == DO.WeighCategories.middle) new_q.weight = WeighCategories.middle;
            else new_q.weight = WeighCategories.hevy;

            DO.Package? p = dal.searchPinQ(q.id);//p is the package that assign to the q or null if it have not package
            if (p != null) //if the quadocopter have a package
            {
                new_q.mode = statusOfQ.delivery; //mode
                new_q.packageNumber = p.Value.id;//packageNumber
                DO.Location lSender = dal.searchLocationOfclient(p.Value.sender); //the location of the sender
                if (p.Value.time_ColctedFromSender != null) //if the package was collected
                {
                    //the location of the q will be the location of the sender
                    Location l = new Location();
                    l.Latitude = lSender.latitude;
                    l.Longitude = lSender.longitude;
                    new_q.thisLocation = l;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    DO.Location lReceiver = dal.searchLocationOfclient(p.Value.receiver);
                    DO.BaseStation closeB = dal.searchCloseStation(lReceiver);
                    double distance = GetDistance(l, coverLtoL(lReceiver)) + GetDistance(coverLtoL(lReceiver), new Location() {Longitude = closeB.longitude, Latitude = closeB.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                    int minBattery = (int)distance * (int)(dal.askForElectric()[(int)p.Value.weight]); //the minimum battery will be the distance*the amount of battery that the q need in km, according to the whigt of its package
                    new_q.battery = r.Next(minBattery, 100);
                }
                else //if the package didn't collected
                {
                    DO.BaseStation b = dal.searchCloseStation(lSender);//the location will be the location of the closest station to the sender
                    new_q.thisLocation.Latitude = b.latitude;
                    new_q.thisLocation.Longitude = b.longitude;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    DO.Location lReceiver = dal.searchLocationOfclient(p.Value.receiver);
                    DO.BaseStation closeToReceiver = dal.searchCloseStation(lReceiver);
                    double distance = GetDistance(coverLtoL(lSender), coverLtoL(lReceiver)) + GetDistance(coverLtoL(lReceiver),new Location() { Longitude = closeToReceiver.longitude, Latitude= closeToReceiver.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                    distance += GetDistance(new_q.thisLocation, coverLtoL(lSender));
                    int minBattery = (int)distance * (int)(dal.askForElectric()[(int)p.Value.weight]); //the minimum battery will be the distance*the amount of battery that the q need in km, according to the whigt of its package
                    new_q.battery = r.Next(minBattery, 100);
                }
            }
            else // if the q have not package
            {
                int x = r.Next(0, 1); //it will be available or in maintence randomaly
                new_q.packageNumber = 0;
                if (x == 0) //if it will be available
                {
                    new_q.mode = statusOfQ.available;
                    DO.Location l = dal.randomCwithPLocation(); //location will in one of the clients that accept a package.
                    if (l == null) throw new BLException("error");
                    new_q.thisLocation.Latitude = l.latitude;
                    new_q.thisLocation.Longitude = l.longitude;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    DO.BaseStation close = dal.searchCloseStation(l);
                    double distance = GetDistance( coverLtoL(l), new Location() { Longitude = close.longitude, Latitude = close.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                    int minBattery = (int)distance * (int)(dal.askForElectric()[0]); //the minimum battery will be the distance*the amount of battery that the q need in km at available state
                    if (minBattery < 100) new_q.battery = r.Next(minBattery, 100);
                    else new_q.battery = 100;
                }

                else //if it in a maintence
                {
                    new_q.mode = statusOfQ.maintenance;
                    new_q.battery = r.Next(0, 20); 
                    var l = dal.randomStationLocation();
                    new_q.thisLocation.Latitude = l.latitude;
                    new_q.thisLocation.Longitude = l.longitude;
                    new_q.thisLocation.Location60 = new_q.thisLocation.toBaseSix.LocationSix(l.latitude, l.longitude).ToString();
                }
            }
            return new_q;

        }
        public QuadocopterToList cover(Quadocopter q)
        {
            QuadocopterToList ql = new QuadocopterToList();
            ql.ID = q.ID;
            ql.mode = q.mode;
            ql.moodle = q.moodle;
            //ql.packageNumber =   i find him in line 363
            ql.thisLocation = q.thisLocation;
            ql.weight = q.weight;
            ql.battery = q.battery;

            var list = (from DO.Package p in dal.ListOfPackages()
                        where p.idQuadocopter == ql.ID
                        select p).ToList();
            ql.packageNumber = list.Count;

            return ql;
        }

        //to help in project PL.
        public Quadocopter cover(QuadocopterToList ql)
        {
            Quadocopter q = new Quadocopter();
            q.ID = ql.ID;
            q.mode = ql.mode;
            q.moodle = ql.moodle;
            q.thisLocation = ql.thisLocation;
            //q.thisPackageneed to find
            q.weight = ql.weight;
            q.battery = ql.battery;
            
            foreach (DO.Package p in dal.ListOfPackages())
            {
                if(p.idQuadocopter==q.ID)
                {
                    q.thisPackage.ID = p.id;
                    if (q.mode == statusOfQ.delivery)
                        q.thisPackage.ifOnTheWay = true;
                    else
                        q.thisPackage.ifOnTheWay = false;
                    q.thisPackage.priority = (Priorities)p.priority;

                    //to get the sender and the reciver.
                    bool a = false;
                    bool b = false;
                    foreach(DO.Client  c in dal.ListOfClients())
                    {
                        if (c.ID == p.receiver)
                        {
                            q.thisPackage.receiver = cover(c);
                            a = true;
                        }
                        if (c.ID == p.sender)
                        {
                            q.thisPackage.sender = cover(c);
                            b = true;
                        }
                        if (a && b)
                            break;
                    }

                    q.thisPackage.weight = (WeighCategories)p.weight;
                    if (q.thisPackage.ifOnTheWay) {
                        q.thisPackage.collection = q.thisPackage.sender.thisLocation;
                        q.thisPackage.destination = q.thisPackage.receiver.thisLocation;
                    }
                    else
                    {
                        q.thisPackage.collection = null;
                        q.thisPackage.destination = null;
                    }
                    break;
                }
            }

            return q;
        }
        public Quadocopter cover(DO.Quadocopter dalq)
        {
            Quadocopter blq = new Quadocopter();

            blq.ID = dalq.id;
            blq.moodle = dalq.moodle;
            blq.weight = (WeighCategories)dalq.weight;

            /*need to find*/
            ///blq.mode=dalq.
            ///blq.thisLocation
            ///blq.battery
            ///blq.thisPackage
                
            bool exist = false;
            foreach (QuadocopterToList q in q_list)
            {

                if (q.ID == dalq.id)
                {
                    exist = true;
                    blq.mode = q.mode;
                    blq.thisLocation = q.thisLocation;
                    blq.battery = q.battery;
                    break;
                }
            }
            if (!exist)
                new BLException("The quadocopter not exist.");

            //to get 'thisPackage'.
            IEnumerable<DO.Package> p_list = dal.ListOfPackages();
            exist = false;
            foreach (DO.Package p in p_list)
            {
                if (p.idQuadocopter == blq.ID)
                {
                    exist = true;
                    PackageInTrans transP = new PackageInTrans();
                    transP.ID = p.id;
                    transP.priority = (Priorities)p.priority;
                    if (blq.mode == statusOfQ.delivery)
                        transP.ifOnTheWay = true;
                    else
                        transP.ifOnTheWay = false;
                    transP.receiver = ClientDisplay(p.receiver);
                    transP.sender = ClientDisplay(p.sender);
                    transP.weight = (WeighCategories)p.weight;
                    transP.collection = transP.sender.thisLocation;
                    transP.destination = transP.receiver.thisLocation;

                    blq.thisPackage = transP;
                }
            }
            if (!exist)
                blq.thisPackage = null;

            return blq;
        }

        #endregion

        double GetDistance(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l1.Latitude - l2.Latitude, 2) + Math.Pow(l1.Longitude - l2.Longitude, 2));
        }
        Location coverLtoL(DO.Location l)
        {
            Location newL = new Location()
            {
                Longitude = l.longitude,
                Latitude = l.latitude,
                decSix = new DmsLocation(),
                toBaseSix = new BaseSixtin()
            };
            return newL;

        }
        DO.Location cover(Location l)
        {
            DO.Location newL = new DO.Location()
            {
                longitude = l.Longitude,
                latitude = l.Latitude
            };
            return newL;

        }

        //GeoCoordinate coverLtoG(location l)
        //{
        //    return new GeoCoordinate(l.longitude, l.latitude);
        //}
    }
}
