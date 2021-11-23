using System;
using System.Collections.Generic;
using DalObject;
using IBL.BO;

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
            if (id <= 0)
                throw new BLException("Invalid id.");
            if(numCharge < 0)
                throw new BLException("Number of charging fosition must to be positive.");
            List<BaseStation> temp = cover_to_our_list(dal.ListOfStations());
            foreach (BaseStation b in temp)
            {
                if (b.ID == id)
                    throw new BLException("The ID exis.");
                if (b.thisLocation.latitude==lat && b.thisLocation.longitude==lon)
                    throw new BLException("In this location there is already base station.");
            }

            dal.AddBaseStation(id, name, numCharge, lon, lat);
        }
        public void AddQuadocopter(int id, string moodle, int weight, int id_bs)
        {
            if (id <= 0 || id_bs <=0)
                throw new BLException("Invalid id.");
            if(weight != 1 && weight != 2 && weight!= 3)
                throw new BLException("Weight must to be 1, 2, or 3.");

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
                throw new BLException("The base station not exist.");

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
            if (id <= 99999999 || id>999999999)
                throw new BLException("Invalid id.");
            if (phoneNumber < 0)
                throw new BLException("Number phone must to be positive.");
            List<Client> temp = cover_to_our_list(dal.ListOfClients());
            foreach (Client c in temp)
            {
                if (c.ID == id)
                    throw new BLException("ID already exist.");
                if (c.thisLocation.latitude == lat && c.thisLocation.longitude == lon)
                    throw new BLException("In this location there is already other client.");
            }

            dal.AddClient(id, name, phoneNumber, lon, lat);
        }
        /// <summary>
        /// adding new package.
        /// </summary>
        /// <returns>The pacjagh ID we add.</returns>
        public void AddPackage(int id_sender, int id_colecter, int weight, int priority)
        {
            if (id_sender <= 99999999 || id_sender > 999999999)
                throw new BLException("Invalid id.");
            if(id_colecter <= 99999999 || id_colecter > 999999999)
                throw new BLException("Invalid id.");
            if (weight != 1 && weight != 2 && weight != 3)
                throw new BLException("Weight must to be 1, 2, or 3.");
            if (priority != 1 && priority != 2 && priority != 0)
                throw new BLException("Priority must to be 1, 2, or 0.");

            dal.AddPackage(id_sender, id_colecter, weight, priority);
        }

    }
}
