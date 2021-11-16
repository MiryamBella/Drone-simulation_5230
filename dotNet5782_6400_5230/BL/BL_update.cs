using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// update the modle of a qudocopter
        /// </summary>
        public void updateQdata(int id, string modle)
        {
            if (id < 100000000 || id > 999999999) // integrity checking
                Console.WriteLine("ERROR");
            bool exsist = false; 
            
            List<IDAL.DO.Quadocopter> l =  dal.ListOfQ();//i accept the list of q and print the q i want
            IDAL.DO.Quadocopter theQ = new IDAL.DO.Quadocopter();
            QuadocopterToList qToList = new QuadocopterToList();
            foreach (IDAL.DO.Quadocopter q in l)
                if (q.id == id) 
                {
                    exsist = true;
                    theQ = q;
                }
            if (exsist) // if the q was founded, i send it to the dal function
            {
                dal.updateQd(theQ, modle);
                
                foreach (QuadocopterToList q in q_list)//update the q_list
                    if (q.ID == id)
                        qToList = q;
                qToList.moodle = modle;
            }
            else Console.WriteLine("ERROR");
        }
        /// <summary>
        /// update the name of the station or the number of its charging positions
        /// </summary>
        public void updateSdata(int id, string name = null, int chargingPositions = -1)
        {
            if (name == null && chargingPositions == -1)
                Console.WriteLine("ERROR");
            if (chargingPositions < -1)
                Console.WriteLine("ERROR");
            if (id < 100000000 || id > 999999999)
                Console.WriteLine("ERROR");
            
            bool exsist = false;
            List<IDAL.DO.BaseStation> l = dal.ListOfStations();
            IDAL.DO.BaseStation theS = new IDAL.DO.BaseStation();

            foreach (IDAL.DO.BaseStation b in l)
                if (b.IDnumber == id)
                {
                    theS = b;
                    exsist = true;
                }
            if (exsist)
                dal.updateSdata(theS, name, chargingPositions);
            else Console.WriteLine("ERROR");
        }
        /// <summary>
        /// update the name or the phone number of the client
        /// </summary>
        public void updateCdata(int id, string name = null, int phone = -1)
        {
            if (id < 100000000 || id > 999999999)
                Console.WriteLine("ERROR");
            if (name == null && phone == -1)
                Console.WriteLine("ERROR");
            if (phone != -1 && phone < 100000000)
                Console.WriteLine("ERROR");

            bool exsist = false;
            List<IDAL.DO.Client> l = dal.ListOfClients();
            IDAL.DO.Client theC = new IDAL.DO.Client();

            foreach (IDAL.DO.Client c in l)
                if (c.ID == id)
                {
                    exsist = true;
                    theC = c;
                }

            if (exsist)
                dal.updateCdata(theC, name, phone);
            else
                Console.WriteLine("ERROR");
        }
        /// <summary>
        /// update qudocopter to be send to a charging position
        /// </summary>
        public void sendQtoChrge(int id)
        {
            

        }
        /// <summary>
        /// update qudocopter to be released from a charging positions
        /// </summary>
        public void releaseQfromChrge()
        {

        }
        /// <summary>
        /// update package to be assigned to a qudocopter
        /// </summary>
        public void assignPtoQ()
        {

        }
        /// <summary>
        /// updat package to be collected by qudocopter
        /// </summary>
        public void collectPbyQ()
        {

        }
        /// <summary>
        /// update package to be supplied to the client and the qudocopter to be free from package
        /// </summary>
        public void supplyPbyQ()
        {

        }




       /* public void AssignPtoQ(Packagh P, int id_q)
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
        */
        
        /*
        /// <summary>
        /// release te quadocopter frp charging.
        /// </summary>
        public void ReleaseQfromCharging(BaseStation b, Quadocopter q)
        {
            b.chargingPositions++;
            //DataSource.qpter[iq].mode = statusOfQ.available;

            Charging c = new Charging();
            c.baseStationID = b.IDnumber;
            c.quadocopterID = q.id;
            DataSource.charge.Remove(c);
        }
    }
}
*/