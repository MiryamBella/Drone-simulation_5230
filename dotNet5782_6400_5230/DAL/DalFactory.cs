using System;
using System.Collections.Generic;
using System.Text;
using DalApi;

namespace DAL
{
    public class DalFactory
    {
        public static IDAL GetDal(string str)
        {
            if (true)
            {
                DalObject.DalObject dal = new DalObject.DalObject();
                return dal;
            }
            //else if (true)
            //  return new DalObject.DataSource();
            else
                throw new exceptions.DO.DALException("");
        }
    }
}
