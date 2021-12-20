﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        /// <summary>
        /// Static constructor extracts Dal packages list and Dal type from
        /// Dal configuration file config.xml
        /// </summary>
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg)
                .ToDictionary(p => "" + p.Name, p => p.Value);

        }





        //static DalConfig()
        //{
        //    XElement dalConfig = XElement.Load(@"config.xml");
        //    DalName = dalConfig.Element("dal").Value;
        //    DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
        //                   select pkg).ToDictionary(p => "" + p.Name, p => p.Value);
        //}

        ///// <summary>
        ///// Represents errors during DalApi initialization
        ///// </summary>
        //[Serializable]
        //public class DalConfigException : Exception
        //{
        //    public DalConfigException(string message) : base(message) { }
        //    public DalConfigException(string message, Exception inner) : base(message, inner) { }
        //}
    }

}
