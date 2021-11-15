using System;
using System.Collections.Generic;
using DalObject;
using IBL.BO;

/// any plase thwt ther is error  i frint this and i will chanch that after we study.
namespace IBL
{
    public partial class BL: IBL
    {
        IDAL.IDAL dal;
        public BL() { 
            dal = new DalObject.DalObject();
            help_list=dal.ListOfQ();
            q_list = cover_to_our_list(help_list);

        }

        public void AddBaseStation(int id, string name, double lon, double lat, int numCharge)
        {
            if (id <= 0 || numCharge < 0)
                Console.WriteLine("error");
            List<BaseStation> temp = cover_to_our_list(dal.ListOfStations());
            foreach (BaseStation b in temp)
            {
                if (b.ID == id)
                    Console.WriteLine("error");
                if(b.thisLocation.latitude==lat && b.thisLocation.longitude==lon)
                    Console.WriteLine("error");
            }

            dal.AddBaseStation(id, name, numCharge, lon, lat);
        }
        public void AddQuadocopter(int id, string moodle, int weight, int id_bs)
        {
            if (id <= 0 || weight<=0)
                Console.WriteLine("error");
            if(weight != 1 && weight != 2 && weight!= 3)
                Console.WriteLine("error");

            List<BaseStation> temp = cover_to_our_list(dal.ListOfStations());
            bool chek = false;
            BaseStation bs_with_q = new BaseStation();
                foreach (BaseStation b in temp)
            {
                if (b.ID == id_bs)
                {
                    chek = true;
                    bs_with_q = b;
                    break;
                }
            }
            if(!chek)
                Console.WriteLine("error");

            dal.AddQuadocopter(id, moodle, weight);
            QuadocopterToList q = new QuadocopterToList();
            q.moodle = moodle;
            if (weight == 1) q.weight = WeighCategories.easy;
            else if (weight == 2) q.weight = WeighCategories.middle;
            else q.weight = WeighCategories.hevy;
            q.ID= id;
            q.thisLocation = bs_with_q.thisLocation;

            q_list.Add(q);
        }
        public void AddClient(int id, string name, int phoneNumber, double lon, double lat) ///adding new client
        {
            if (id <= 0 || phoneNumber < 0)
                Console.WriteLine("error");
            List<client> temp = cover_to_our_list(dal.ListOfClients());
            foreach (client c in temp)
            {
                if (c.ID == id)
                    Console.WriteLine("error");
                if (c.thisLocation.latitude == lat && c.thisLocation.longitude == lon)
                    Console.WriteLine("error");
            }

            dal.AddClient(id, name, phoneNumber, lon, lat);
        }
        /// <summary>
        /// adding new package.
        /// </summary>
        /// <returns>The pacjagh ID we add.</returns>
        public void AddPackage(int id_sender, int id_colecter, int weight, int priority)
        {
            if (id_sender <= 0 || id_colecter <= 0)
                Console.WriteLine("error");
            if (weight != 1 && weight != 2 && weight != 3)
                Console.WriteLine("error");
            if (priority != 1 && priority != 2 && priority != 3)
                Console.WriteLine("error");

            dal.AddPackage(id_sender, id_colecter, weight, priority);
        }

    }
}
