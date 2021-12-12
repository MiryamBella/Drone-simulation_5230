using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct BaseStation : ICloneable
    {

        public int IDnumber { get; set; }
        public string name { get; set; }
        public int chargingPositions { get; set; }
        public int freechargingPositions { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public BaseSixtin toBaseSix { get; set; }
        public DmsLocation decSix { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return ("ID: " + IDnumber + '\n' +
                "name: " + name + '\n' +
                "number of charging positins: " + chargingPositions + '\n' +
                "longitude: " + longitude + '\n' +
                "latitude: " + latitude + '\n') +
                "location in base 60: " + decSix;
        }


    }









    class DecimalLocation
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return string.Format("{0:f5}, {1:f5}",
                Latitude, Longitude);
        }
    }

    public class DmsLocation
    {
        public DmsPoint Latitude { get; set; }
        public DmsPoint Longitude { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                Latitude, Longitude);
        }
    }

    public class DmsPoint
    {
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public PointType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",
                Math.Abs(Degrees),
                Minutes,
                Seconds,
                Type == PointType.Lat
                    ? Degrees < 0 ? "S" : "N"
                    : Degrees < 0 ? "W" : "E");
        }
    }

    public enum PointType { Lat, Lon }



    //***********************************************************
    /// <summary>
    /// this class will cofer from normal to base 60 and from base 60.
    /// </summary>
    public class BaseSixtin
    {
        //----------------------------------
        //this part will cover from base 60.********************************************************************************
        DecimalLocation Convert_fromSix(DmsLocation dmsLocation)
        {
            if (dmsLocation == null)
            {
                return null;
            }

            return new DecimalLocation
            {
                Latitude = CalculateDecimal(dmsLocation.Latitude),
                Longitude = CalculateDecimal(dmsLocation.Longitude)
            };
        }

        decimal CalculateDecimal(DmsPoint point)
        {
            if (point == null)
            {
                return default(decimal);
            }
            return point.Degrees + (decimal)point.Minutes / 60 + (decimal)point.Seconds / 3600;
        }

        //-------------------------------

        //this part will cover to base 60.********************************************************************************
        /// <summary>
        /// the main func we will use, and she will call the  others and do all the hard work.
        /// </summary>
        /// <param name="decimalLocation"></param>
        /// <returns></returns>
        internal DmsLocation Convert_toSix(DecimalLocation decimalLocation)
        {
            if (decimalLocation == null)
            {
                return null;
            }

            return new DmsLocation
            {
                Latitude = new DmsPoint
                {
                    Degrees = ExtractDegrees(decimalLocation.Latitude),
                    Minutes = ExtractMinutes(decimalLocation.Latitude),
                    Seconds = ExtractSeconds(decimalLocation.Latitude),
                    Type = PointType.Lat
                },
                Longitude = new DmsPoint
                {
                    Degrees = ExtractDegrees(decimalLocation.Longitude),
                    Minutes = ExtractMinutes(decimalLocation.Longitude),
                    Seconds = ExtractSeconds(decimalLocation.Longitude),
                    Type = PointType.Lon
                }
            };
        }

        int ExtractDegrees(decimal value)
        {
            return (int)value;
        }

        int ExtractMinutes(decimal value)
        {
            value = Math.Abs(value);
            return (int)((value - ExtractDegrees(value)) * 60);
        }

        int ExtractSeconds(decimal value)
        {
            value = Math.Abs(value);
            decimal minutes = (value - ExtractDegrees(value)) * 60;
            return (int)Math.Round((minutes - ExtractMinutes(value)) * 60);
        }

        //*********************************************************************************

        /// <summary>
        /// get the Coordinates and make it to base 60. the firs num it is the Latitude and the second it's the Longitude.
        /// </summary>
        /// <param name="lat">get the Latitude</param>
        /// <param name="lon">get the Longitude</param>
        /// <returns>the location in base 60.</returns>
        public DmsLocation LocationSix(double lat, double lon)
        {
            DecimalLocation decimalLocation;
            DmsLocation dmsLocation;

            decimalLocation = new DecimalLocation
            {
                Latitude = (decimal)lat,
                Longitude = (decimal)lon
            };
            dmsLocation = Convert_toSix(decimalLocation);
            return dmsLocation;

            //Console.WriteLine("{0} -> {1}", decimalLocation, dmsLocation);
            //
            //dmsLocation = new DmsLocation
            //{
            //    Latitude = new DmsPoint
            //    {
            //        Degrees = 38,
            //        Minutes = 53,
            //        Seconds = 55,
            //        Type = PointType.Lat
            //    },
            //    Longitude = new DmsPoint
            //    {
            //        Degrees = -77,
            //        Minutes = 2,
            //        Seconds = 16,
            //        Type = PointType.Lon
            //    }
            //};
            //decimalLocation = Convert(dmsLocation);
            //Console.WriteLine("{0} -> {1}", dmsLocation, decimalLocation);

            // output:
            //    38.89861, -77.03778 -> 38 53 55 N, 77 2 16 W
            //    38 53 55 N, 77 2 16 W -> 38.89861, -76.96222
        }


    }




}

