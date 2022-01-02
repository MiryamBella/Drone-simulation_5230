using System;
using System.Collections.Generic;
using System.Text;

namespace DO
    {
        public class DALException : Exception
        {
            public DALException(string message) : base(message) { }
        }

    }
    namespace DalConfid
    {
        public class DalConfigException : Exception
        {
            public DalConfigException(string msg) : base(msg) { }
            public DalConfigException(string msg, Exception exs) : base(msg, exs) { }
        }

    }

