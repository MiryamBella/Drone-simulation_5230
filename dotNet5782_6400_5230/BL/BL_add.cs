using System;
using DalObject;
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
        

    }
}
