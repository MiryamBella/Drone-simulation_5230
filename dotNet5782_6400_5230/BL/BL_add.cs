﻿using System;
using System.Collections.Generic;
//using Dal;
using BO;
using System.Device.Location;
using DAL;

namespace BlApi
{
    public partial class BL: IBL
    {
        internal static DalApi.IDAL dal;
        public BL() {
            try
            {
                dal = DAL.DalFactory.GetDal("string");
                //We need list of all thw quadopters so we get the list as stract and then we cover it to list of class
                help_list = dal.ListOfQ();
                q_list = cover_to_our_list(help_list);
            }
            catch(Exception ex)
            {
                throw new BLException(ex.Message);
            }

        }
        #region add base station
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
                if (b.thisLocation.Latitude==lat && b.thisLocation.Longitude==lon)
                    throw new BLException("In this location there is already base station.");
            }

            dal.AddBaseStation(id, name, numCharge, lon, lat);
        }
        #endregion;
        #region add qudocopter;
        public void AddQuadocopter(int id, string moodle, int weight, int id_bs)
        {
            if (id <= 0 || id_bs <=0)
                throw new BLException("Invalid id.");
            if(weight != 1 && weight != 2 && weight!= 3)
                throw new BLException("Weight must to be 1, 2, or 3.");

            ///chek if the id of the base station exist.
            bool chek = false;
            QuadocopterToList q = new QuadocopterToList();//the new q i will add.
            foreach (DO.BaseStation b in dal.ListOfStations())
            {
                if (b.IDnumber == id_bs)
                {
                    chek = true;
                    q.thisLocation.Latitude = b.latitude;
                    q.thisLocation.Longitude = b.longitude;
                    break;
                }
            }
            if(!chek)
                throw new BLException("The base station not exist.");
            ///chek if the id of the quadocopter exist.
            foreach (DO.Quadocopter qu in dal.ListOfQ())
                if (qu.id == id)
                    throw new BLException("The ID ollredy exist.");

            dal.AddQuadocopter(id, moodle, weight);
            q.moodle = moodle;
            if (weight == 1) q.weight = WeighCategories.easy;
            else if (weight == 2) q.weight = WeighCategories.middle;
            else q.weight = WeighCategories.hevy;
            q.battery = 100;
            q.ID= id;

            q_list.Add(q);
        }
        #endregion;
        #region add client;
        public void AddClient(int id, string name, double lon, double lat, int phoneNumber) ///adding new client
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
                if (c.thisLocation.Latitude == lat && c.thisLocation.Longitude == lon)
                    throw new BLException("In this location there is already other client.");
            }

            dal.AddClient(id, name, phoneNumber, lon, lat);
        }
        #endregion;
        #region add package;
        /// <summary>
        /// adding new package.
        /// </summary>
        /// <returns>The pacjagh ID we add.</returns>
        public void AddPackage(int id_sender, int id_colecter, WeighCategories weight, Priorities priority)
        {
            if (id_sender <= 99999999 || id_sender > 999999999)
                throw new BLException("Invalid sender id.");
            if (id_colecter <= 99999999 || id_colecter > 999999999)
                throw new BLException("Invalid receiver id.");
            bool isSender = false, isReceiver = false;
            foreach (DO.Client c in dal.ListOfClients())
            {
                if (c.ID == id_sender) isSender = true;
                if (c.ID == id_colecter) isReceiver = true;
                if (isSender && isReceiver) break;
            }
            if (!isSender || !isReceiver)
                throw new BLException("this sender or receiver not exist");
                dal.AddPackage(id_sender, id_colecter, (int)weight, (int)priority);
        }
        #endregion;


        public void startSimulator(int id, Action<int, BO.Package> report, Func<bool> isStop, IBL bl)
        {
            Simulator simulator = new Simulator(bl, id, report, isStop);
        }

    }
}
