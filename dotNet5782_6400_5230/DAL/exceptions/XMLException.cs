using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class XMLException:Exception
    {
        public XMLException(string msg) : base(msg) { }
        public XMLException(string file, string msg, Exception ex) : base(file+msg, ex) { }

    }
}
