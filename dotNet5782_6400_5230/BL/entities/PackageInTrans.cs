﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class PackageInTrans
    {
        public int ID { get; set; }
        public Client sender { get; set; }
        public Client receiver { get; set; }
        public WeighCategories weight { get; set; }
        public Priorities priority { get; set; }
        public bool ifOnTheWay { get; set; }
        public Location collection { get; set; }
        public Location destination { get; set; }
        public override string ToString()
        {
            return ("ID: " + ID + '\n' +
                "the sender: " + sender.name + '\n' +
                "the receiver: " + receiver.name + '\n' +
                "the weight: " + weight + '\n' +
                "the priority: " + priority + '\n' +
                "if the package start the translation " + ifOnTheWay + '\n' +
                "location to collect the package: " + collection + '\n' +
                "location of the destination: " + destination + '\n');
        }

    }
}

