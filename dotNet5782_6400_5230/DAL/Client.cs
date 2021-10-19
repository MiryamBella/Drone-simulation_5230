using System;

namespace IDAL
{

    namespace DO
    {
        public struct Client
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public int longitude { get; set; }
            public int latitude { get; set; }
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
}
