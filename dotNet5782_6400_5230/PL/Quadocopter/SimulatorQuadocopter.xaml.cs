using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorQuadocopter.xaml
    /// </summary>
    public partial class SimulatorQuadocopter : Window
    {
        BlApi.IBL bl;
        BO.Quadocopter localQ;
        public SimulatorQuadocopter(BlApi.IBL ibl, BO.Quadocopter q)
        {
            InitializeComponent();
            bl = ibl;
            localQ = q;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
        }
        BackgroundWorker worker; //field
        bool F;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //object obj = e.Argument;
            //Thread.Sleep(2000);
            //worker.ReportProgress(23);

            //e.Result = "finished ok!";
            while (F) {
                try
                {
                    bl.assignPtoQ(localQ.ID);
                    int time = bl.getFlyTime(localQ.ID);
                    int battery = bl.getBatteryToFly(localQ.ID);
                    battery /= time;
                    battery *= -1;
                    while (time > 0)
                    {
                        Thread.Sleep(1000);
                        time -= 1;
                        worker.ReportProgress(battery);
                        localQ.battery += battery;
                    }
                    bl.collectPbyQ(localQ.ID);
                    time = bl.getFlyTime(localQ.ID);
                    battery = bl.getBatteryToFly(localQ.ID);
                    battery /= time;
                    battery *= -1;
                    while (time > 0)
                    {
                        Thread.Sleep(1000);
                        time -= 1;
                        worker.ReportProgress(battery);
                        localQ.battery += battery;
                    }

                    bl.supplyPbyQ(localQ.ID);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "there is no package to assign")
                        Thread.Sleep(500);
                    else if (ex.Message == "add somthing ggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg")
                    {
                        bl.sendQtoChrge(localQ.ID);
                        int i = 0;
                        while (i < 100)
                        {
                            i = bl.getBatteryCharge(localQ.ID);
                            worker.ReportProgress(i);
                            Thread.Sleep(1000);
                            localQ.battery += i;
                        }
                        bl.releaseQfromChrge(localQ.ID);
                    }
                    else
                        MessageBox.Show(ex.Message);
                }

            }
        } 
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int addToBattery = e.ProgressPercentage;
            batteryShow.Text = (localQ.battery + addToBattery).ToString();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
        }


    }
}
