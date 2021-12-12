using System;
using System.Collections.Generic;
using System.Text;
using BlApi;

namespace BL
{
    public class BlFactory
    {
        public static BlApi.IBL GetBL()
        {
            BlApi.IBL bl = new BlApi.BL();
            return bl;
        }
    }
}