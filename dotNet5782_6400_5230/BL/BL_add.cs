using System;
using DalObject;
namespace IBL
{
    public partial class BL: IBL
    {
        IDAL.IDAL dal;
        BL() { 
            dal = new DalObject.DalObject();
            help_list=dal.ListOfQ();
           }
        

    }
}
