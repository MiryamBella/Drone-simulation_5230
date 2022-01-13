using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using static BlApi.BL;
using BO;
using System.Linq;

namespace BlApi
{
    class Simulator
    {
        ///static BlApi.BL bL;
        int speedDrone = 1;
        internal Simulator(IBL bl, int id, Action<int, BO.Package> report, Func<bool> isStop)
        {
            while (isStop())
            {
                try
                {
                    BO.Package package = new Package();
                    package =bl.assignPtoQ(id);
                    int time = bl.getTimeOfFlying(id, speedDrone, BO.TargetQ.sender, -1);
                    int battery = bl.getBatteryToFly(id, BO.TargetQ.sender,package.ID, -1);
                    if (time == 0)
                    {
                        battery *= -1;
                        report(battery, package);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        battery /= time;
                        battery *= -1;
                        while (time > 0)
                        {
                            time -= 1;
                            report(battery, package);
                            Thread.Sleep(1000);
                        }
                    }
                    bl.collectPbyQ(id);
                    time = bl.getTimeOfFlying(id, speedDrone, BO.TargetQ.receiver, -1);
                    battery = bl.getBatteryToFly(id, BO.TargetQ.receiver,package.ID, -1);
                    if (time == 0)
                    {
                        battery *= -1;
                        report(battery, package);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        battery /= time;
                        battery *= -1;
                        while (time > 0)
                        {
                            time -= 1;
                            report(battery, package);
                            Thread.Sleep(1000);
                        }
                    }

                    bl.supplyPbyQ(id);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "There is no package to assign.")
                        break;
                    else if (ex.Message == "battery")
                    {
                        int i = bl.sendQtoChrge(id).battery;
                        int batteryPerSecond = bl.getBatteryCharge();
                        while (i < 100)
                        {
                            i += batteryPerSecond;
                            report(batteryPerSecond, null);
                            Thread.Sleep(1000);
                        }
                        bl.releaseQfromChrge(id);
                    }
                    else
                        throw new BLException(ex.Message);
                }

            }

        }
    }


}
