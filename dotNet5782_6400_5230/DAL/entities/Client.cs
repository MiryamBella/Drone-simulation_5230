using System;

namespace DO
    {
        public struct Client : ICloneable
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
            public override string ToString()
            {
                return ("ID: " + ID + '\n' +
                    "name: " + name + '\n' +
                    "phone number: " + phoneNumber + '\n' +
                    "longitude: " + longitude + '\n' +
                    "latitude: " + latitude + '\n');

            }
        }
    }

