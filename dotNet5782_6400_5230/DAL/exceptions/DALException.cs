using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.exceptions
{
    namespace DO
    {
        public class DALException : Exception
        {
            public DALException(string message) : base(message) { }
        }
    }
}
