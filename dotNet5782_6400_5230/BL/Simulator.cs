﻿using System;
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
        static BlApi.BL bL;
        internal Simulator(BL bl, int id, Action<int> report, Func<bool> isStop)
        {
            while (isStop())
            {
                try
                {
                    bl.assignPtoQ(id);
                    int time = bl.getTimeOfFlying(id, BO.TargetQ.sender, -1);
                    int battery = bl.getBatteryToFly(id, BO.TargetQ.sender, -1);
                    battery /= time;
                    battery *= -1;
                    while (time > 0)
                    {
                        Thread.Sleep(1000);
                        time -= 1;
                        report(battery);
                    }
                    bl.collectPbyQ(id);
                    time = bl.getTimeOfFlying(id, BO.TargetQ.receiver, -1);
                    battery = bl.getBatteryToFly(id, BO.TargetQ.receiver, -1);
                    battery /= time;
                    battery *= -1;
                    while (time > 0)
                    {
                        Thread.Sleep(1000);
                        time -= 1;
                        report(battery);
                    }

                    bl.supplyPbyQ(id);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "there is no package to assign")
                        Thread.Sleep(500);
                    else if (ex.Message == "battery")
                    {
                        bl.sendQtoChrge(id);
                        int i = bl.QuDisplay(id).battery;
                        int batteryPerSecond = bl.getBatteryCharge();
                        while (i < 100)
                        {
                            i += batteryPerSecond;
                            report(i);
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