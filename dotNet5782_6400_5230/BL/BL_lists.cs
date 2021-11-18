using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        List<QuadocopterToList> q_list = new List<QuadocopterToList>();
        /// <summary>
        /// to cover fro list from IDAL to list from IBL.
        /// </summary>
        List<IDAL.DO.Quadocopter> help_list = new List<IDAL.DO.Quadocopter>();
        List<QuadocopterToList> cover_to_our_list(List<IDAL.DO.Quadocopter> old_l)
        {
            List<QuadocopterToList> new_l = new List<QuadocopterToList>();
            foreach(IDAL.DO.Quadocopter q in old_l)
            {
                QuadocopterToList temp = new QuadocopterToList();
                temp = cover(q);
                new_l.Add(temp);
            }

            return new_l;
        }
        QuadocopterToList cover(IDAL.DO.Quadocopter q)
        {
            QuadocopterToList new_q = new QuadocopterToList();
            new_q.ID = q.id;


            return new_q;
        }

        #region cover base station
        //--------cover base station---------------------------------------------------

        List<BaseStation> cover_to_our_list(List<IDAL.DO.BaseStation> old_l)
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
            foreach (IDAL.DO.Charging chargeh in chrgh_list)
            {
                ///find what q is gharge in our base station
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
                        Console.WriteLine("error");
                }
            }


            return new_bs;
        }
        #endregion

        #region cover client
        //--------cover client---------------------------------------------------
        List<Client> cover_to_our_list(List<IDAL.DO.Client> old_l)
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

            List<IDAL.DO.Packagh> p_l = new List<IDAL.DO.Packagh>();
            foreach (IDAL.DO.Packagh p in p_l)
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
        List<Package> cover_to_our_list(List<IDAL.DO.Packagh> old_l)
        {
            List<Package> new_l = new List<Package>();
            foreach (IDAL.DO.Packagh p in old_l)
                new_l.Add(cover(p));
            return new_l;
        }
        Package cover(IDAL.DO.Packagh p)
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
                Console.WriteLine("error");

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


        //--------cover qudocopter---------------------------------------------------

        //List<Quadocopter> cover_to_our_list(List<IDAL.DO.Quadocopter> old_l)
        //{
        //    List<Quadocopter> new_l = new List<Quadocopter>();
        //    foreach (IDAL.DO.Quadocopter q in old_l)
        //        new_l.Add(cover(q);
        //    return new_l;
        //}
        //Quadocopter cover(IDAL.DO.Quadocopter q)
        //{
        //    Quadocopter new_q = new Quadocopter();
        //    new_q.ID = q.id;
        //    new_q.moodle = q.moodle;
        //    if (q.weight == IDAL.DO.WeighCategories.easy) new_q.weight = WeighCategories.easy;
        //    else if (q.weight == IDAL.DO.WeighCategories.middle) new_q.weight = WeighCategories.middle;
        //    else if(q.weight == IDAL.DO.WeighCategories.hevy) new_q.weight = WeighCategories.hevy;
        //    new_q.thisLocation.latitude = q.latitude;
        //    new_q.thisLocation.longitude = q.longitude;
        //    new_q.thisLocation.decSix = new_q.thisLocation.toBaseSix.LocationSix(q.latitude, q.longitude);

        //    return new_q;
        //}

    }
}
