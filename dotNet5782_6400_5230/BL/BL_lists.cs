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
            
            return new_bs;
        }



    }
}
