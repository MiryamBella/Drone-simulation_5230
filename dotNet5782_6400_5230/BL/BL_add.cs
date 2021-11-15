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
        public void AddQuadocopter(int id, string moodle, int weigh, int id_bs)
        {
            if (id <= 0 || weigh<=0)
                Console.WriteLine("error");
            List<BaseStation> temp = cover_to_our_list(dal.ListOfStations());
            bool chek = false;
            foreach (BaseStation b in temp)
            {
                if (b.ID == id_bs)
                {
                    chek = true;
                    break;
                }
            }
            if(!chek)
                Console.WriteLine("error");

            dal.AddQuadocopter(id, moodle, weigh);

        }

    }
}
