using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using BO;
//using System.Device;
using System.Device.Location;


namespace BlApi
{
    public partial class BL
    {
        List<QuadocopterToList> q_list = new List<QuadocopterToList>();
        /// <summary>
        /// to cover fro list from IDAL to list from IBL.
        /// </summary>
        IEnumerable<IDAL.DO.Quadocopter> help_list = new List<IDAL.DO.Quadocopter>();

        #region cover base station
        //--------cover base station---------------------------------------------------

        List<BaseStation> cover_to_our_list(IEnumerable<IDAL.DO.BaseStation> old_l)
        {
            List<BaseStation> new_l = new List<BaseStation>();
            foreach (IDAL.DO.BaseStation q in old_l)
                new_l.Add(cover(q));
            return new_l;
        }
        BaseStation cover(IDAL.DO.BaseStation b)
        {
            BaseStation new_bs = new BaseStation();
            new_bs.ID = b.IDnumber;
            new_bs.name = b.name;
            new_bs.thisLocation.latitude =b.latitude;
            new_bs.thisLocation.longitude = b.longitude;
            new_bs.thisLocation.decSix = new_bs.thisLocation.toBaseSix.LocationSix(b.latitude, b.longitude);
            new_bs.freeChargingPositions = b.freechargingPositions;
            // now we will get the: new_bs.qudocopters.

            ///get all tha q that charging
            List<IDAL.DO.Charging> chrgh_list = new List<IDAL.DO.Charging>();
            bool exsit = false;
            chrgh_list = dal.GetChargings();
            if (chrgh_list == null)
            {
                new_bs.qudocopters = null;
                return new_bs;
            }
            foreach (IDAL.DO.Charging chargeh in chrgh_list)
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
        #endregion

        #region cover client
        //--------cover client---------------------------------------------------
        List<Client> cover_to_our_list(IEnumerable<IDAL.DO.Client> old_l)
        {
            List<Client> new_l = new List<Client>();
            foreach (IDAL.DO.Client q in old_l)
                new_l.Add(cover(q));
            return new_l;
        }
        Client cover(IDAL.DO.Client c)
        {
            
            Client new_c = new Client();
            new_c.ID = c.ID;
            new_c.name = c.name;
            //returnC.packageFrom;  thos two we find next.
            //returnC.packageTo;
            new_c.phoneNumber = c.phoneNumber;
            new_c.thisLocation.latitude = c.latitude;
            new_c.thisLocation.longitude = c.longitude;
            new_c.thisLocation.decSix = new_c.thisLocation.toBaseSix.LocationSix(c.latitude, c.longitude);

            List<IDAL.DO.Package> p_l = new List<IDAL.DO.Package>();
            foreach (IDAL.DO.Package p in p_l)
            {
                //id=the ID of our client.
                if (p.sender == new_c.ID)
                    new_c.packageFrom.Add(cover(p));
                if (p.receiver == new_c.ID)
                    new_c.packageTo.Add(cover(p));
            }

            return new_c;
        }
        #endregion

        #region cover package
        //--------cover package---------------------------------------------------
        List<Package> cover_to_our_list(List<IDAL.DO.Package> old_l)
        {
            List<Package> new_l = new List<Package>();
            foreach (IDAL.DO.Package p in old_l)
                new_l.Add(cover(p));
            return new_l;
        }
        Package cover(IDAL.DO.Package p)
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
            List<IDAL.DO.Client> c_l = new List<IDAL.DO.Client>();
            bool exist_cs = false;
            bool exist_cr = false;
            foreach (IDAL.DO.Client c in c_l)
            {
                if (c.ID == p.receiver)
                {
                    new_p.receiver = cover(c);
                    exist_cr = false;
                }
                if (c.ID == p.receiver)
                {
                    new_p.sender = cover(c);
                    exist_cs = false;
                }
            }
            if ((!exist_cr) || (!exist_cs))
                throw new BLException("There is none person how recive or send this pachege");

            //get the q.
            bool exist = false;
            foreach (QuadocopterToList q in q_list)
            {
                if (q.packageNumber == new_p.ID)
                {
                    exist = true;
                    new_p.q.ID = q.ID;
                    new_p.q.thisLocation = q.thisLocation;
                    new_p.q.battery = q.battery;
                }
            }
            if (!exist)
                new_p.q=null;

            return new_p;
        }
        #endregion

        #region cover qudocopter
        //--------cover qudocopter---------------------------------------------------
        List<QuadocopterToList> cover_to_our_list(IEnumerable<IDAL.DO.Quadocopter> old_l)
        {
            List<QuadocopterToList> new_l = new List<QuadocopterToList>();
            foreach (IDAL.DO.Quadocopter q in old_l)
            {
                QuadocopterToList temp = new QuadocopterToList();
                temp = cover_list(q);
                new_l.Add(temp);
            }

            return new_l;
        }
        QuadocopterToList cover_list(IDAL.DO.Quadocopter q)
        {
            Random r = new Random();
            QuadocopterToList new_q = new QuadocopterToList();
            new_q.ID = q.id; //id is as the id
            new_q.moodle = q.moodle; // moodle is the same
            if (q.weight == IDAL.DO.WeighCategories.easy) new_q.weight = WeighCategories.easy; //whiget converted to the categories of dal
            else if (q.weight == IDAL.DO.WeighCategories.middle) new_q.weight = WeighCategories.middle;
            else new_q.weight = WeighCategories.hevy;

            IDAL.DO.Package? p = dal.searchPinQ(q.id);//p is the package that assign to the q or null if it have not package
            if (p != null) //if the quadocopter have a package
            {
                new_q.mode = statusOfQ.delivery; //mode
                new_q.packageNumber = p.Value.id;//packageNumber
                IDAL.DO.Location lSender = dal.searchLocationOfclient(p.Value.sender); //the location of the sender
                if (p.Value.time_ColctedFromSender.Value.Year != 0001) //if the package was collected
                {
                    //the location of the q will be the location of the sender
                    location l = new location();
                    l.latitude = lSender.latitude;
                    l.longitude = lSender.longitude;
                    new_q.thisLocation = l;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    IDAL.DO.Location lReceiver = dal.searchLocationOfclient(p.Value.receiver);
                    IDAL.DO.BaseStation closeB = dal.searchCloseStation(lReceiver);
                    double distance = GetDistance(l, coverLtoL(lReceiver)) + GetDistance(coverLtoL(lReceiver), new location() {longitude = closeB.longitude, latitude = closeB.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                    int minBattery = (int)distance * (int)(dal.askForElectric()[(int)p.Value.weight]); //the minimum battery will be the distance*the amount of battery that the q need in km, according to the whigt of its package
                    new_q.battery = r.Next(minBattery, 100);
                }
                else //if the package didn't collected
                {
                    IDAL.DO.BaseStation b = dal.searchCloseStation(lSender);//the location will be the location of the closest station to the sender
                    new_q.thisLocation.latitude = b.latitude;
                    new_q.thisLocation.longitude = b.longitude;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    IDAL.DO.Location lReceiver = dal.searchLocationOfclient(p.Value.receiver);
                    IDAL.DO.BaseStation closeToReceiver = dal.searchCloseStation(lReceiver);
                    double distance = GetDistance(coverLtoL(lSender), coverLtoL(lReceiver)) + GetDistance(coverLtoL(lReceiver),new location() { longitude = closeToReceiver.longitude, latitude= closeToReceiver.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
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
                    IDAL.DO.Location l = dal.randomCwithPLocation(); //location will in one of the clients that accept a package.
                    if (l == null) throw new BLException("error");
                    new_q.thisLocation.latitude = l.latitude;
                    new_q.thisLocation.longitude = l.longitude;
                    //colclute the distance that the q will go in order to estimate the battery it need
                    IDAL.DO.BaseStation close = dal.searchCloseStation(l);
                    double distance = GetDistance( coverLtoL(l), new location() { longitude = close.longitude, latitude = close.latitude, decSix = new DmsLocation(), toBaseSix = new BaseSixtin() });
                    int minBattery = (int)distance * (int)(dal.askForElectric()[0]); //the minimum battery will be the distance*the amount of battery that the q need in km at available state
                    if (minBattery < 100) new_q.battery = r.Next(minBattery, 100);
                    else new_q.battery = 100;
                }

                else //if it in a maintence
                {
                    new_q.mode = statusOfQ.maintenance;
                    new_q.battery = r.Next(0, 20); 
                    var l = dal.randomStationLocation();
                    new_q.thisLocation.latitude = l.latitude;
                    new_q.thisLocation.longitude = l.longitude;
                }
            }
            return new_q;

        }
        double GetDistance(location l1, location l2)
        {
            return Math.Sqrt(Math.Pow(l1.latitude - l2.latitude, 2) + Math.Pow(l1.longitude - l2.longitude, 2));
        }
        location coverLtoL(IDAL.DO.Location l)
        {
            location newL = new location() {longitude = l.longitude, latitude = l.latitude,
                                            decSix = new DmsLocation(), toBaseSix = new BaseSixtin()};
            return newL;

        }
        GeoCoordinate coverLtoG(location l)
        {
            return new GeoCoordinate(l.longitude, l.latitude);
        }

        //to help in project PL.
        public Quadocopter cover(QuadocopterToList ql)
        {
            Quadocopter q = new Quadocopter();
            q.ID = ql.ID;
            q.mode = ql.mode;
            q.moodle = ql.moodle;
            q.thisLocation = ql.thisLocation;
            
            foreach(IDAL.DO.Package p in dal.ListOfPackages())
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
                    foreach(IDAL.DO.Client  c in dal.ListOfClients())
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
            q.weight = ql.weight;
            q.battery = ql.battery;

            return q;
        }
        #endregion
    }
}
