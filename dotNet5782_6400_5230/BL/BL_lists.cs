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
    }
}
