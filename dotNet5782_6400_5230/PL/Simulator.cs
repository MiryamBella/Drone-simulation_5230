using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BlApi.BL;

namespace PL
{
    class Simulator
    {
        readonly double speedQuadocopter;
        readonly int stopTimeLong;//how long the time to stop.

        internal Simulator(double sq)
        {
            speedQuadocopter = sq;
            stopTimeLong = 1000;//one second.
        }

        internal void startSimulator()
        {

        }
    }
}
