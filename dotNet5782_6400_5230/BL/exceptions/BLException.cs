using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class BLException : Exception
        {
            public BLException(string message) : base(message)
            {
            }
        }
    }
}
