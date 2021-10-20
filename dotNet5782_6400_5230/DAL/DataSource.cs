using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    internal class DataSource
    {
        List<Quadocopter> l;
        


        internal class Config //new evrething in this class.
        {
            //i write all the indexs if we will changh our maind and change the lists to arry.
            int index_Quadocopter = 0;
            int index_baseStation = 0;
            int index_client = 0;
            int index_packagh = 0;

            int runNum = 0;//for the id packagh.

            static void Initialize() { 
            
            }
        }

    }

}
