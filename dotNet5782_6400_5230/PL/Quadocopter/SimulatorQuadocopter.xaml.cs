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
        BackgroundWorker worker; //field
        bool stopSimulator;
        public SimulatorQuadocopter(BlApi.IBL ibl, BO.Quadocopter q)
        {
            InitializeComponent();
            bl = ibl;
            localQ = q;
            IdShow.Text = localQ.ID.ToString();
            locationShwo.Text = localQ.thisLocation.decSix.ToString();
            localQ.thisLocation.Location60 = localQ.thisLocation.decSix.ToString();
            batteryShow.Text = localQ.battery.ToString();
            stopSimulator = false;
            //if (localQ.thisPackage == null)
            //{
            //    ID_p.Visibility = Visibility.Hidden;
            //    IDShow_p.Visibility = Visibility.Hidden;
            //    nameS.Visibility = Visibility.Hidden;
            //    nameR.Visibility = Visibility.Hidden;
            //    senderName.Visibility = Visibility.Hidden;
            //    reciverName.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    IDShow_p.Text = localQ.thisPackage.ID.ToString();
            //    senderName.Text = localQ.thisPackage.sender.name;
            //    reciverName.Text = localQ.thisPackage.receiver.name;
            //}

            worker = new BackgroundWorker();
            //delegate void simDELEGETE==(id, rep,stop) => bl.startSimulator(id, rep, stop);

            //worker.DoWork += simDELEGETE(localQ, reportProgress, isSTOP);

            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }
        void reportProgress(int battery, BO.Package p)
        {
            worker.ReportProgress(battery, p);
        }
        bool isSTOP()
        {
            return stopSimulator;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bl.startSimulator(localQ.ID, reportProgress, isSTOP);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //while (stopSimulator) {
            //    try
            //    {
            //        bl.assignPtoQ(localQ.ID);
            //        int time = bl.getTimeOfFlying(localQ.ID, BO.TargetQ.sender);
            //        int battery = bl.getBatteryToFly(localQ.ID, BO.TargetQ.sender);
            //        battery /= time;
            //        battery *= -1;
            //        while (time > 0)
            //        {
            //            Thread.Sleep(1000);
            //            time -= 1;
            //            worker.ReportProgress(battery);
            //            localQ.battery += battery;
            //        }
            //        bl.collectPbyQ(localQ.ID);
            //        time = bl.getTimeOfFlying(localQ.ID, BO.TargetQ.receiver);
            //        battery = bl.getBatteryToFly(localQ.ID, BO.TargetQ.receiver);
            //        battery /= time;
            //        battery *= -1;
            //        while (time > 0)
            //        {
            //            Thread.Sleep(1000);
            //            time -= 1;
            //            worker.ReportProgress(battery);
            //            localQ.battery += battery;
            //        }

            //        bl.supplyPbyQ(localQ.ID);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex.Message == "there is no package to assign")
            //            Thread.Sleep(500);
            //        else if (ex.Message == "add somthing ggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg")
            //        {
            //            bl.sendQtoChrge(localQ.ID);
            //            int i = -1;
            //            while (i < 100)
            //            {
            //                i = bl.getBatteryCharge(localQ.ID);
            //                worker.ReportProgress(i);
            //                Thread.Sleep(1000);
            //                localQ.battery += i;
            //            }
            //            bl.releaseQfromChrge(localQ.ID);
            //        }
            //        else
            //            MessageBox.Show(ex.Message);
            //    }

            //}
        } 
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int addToBattery = e.ProgressPercentage;
            batteryShow.Text = (localQ.battery + addToBattery).ToString();
            //if (e.UserState != null)
            //{
            //    IDShow_p.Text = ((BO.Package)e.UserState).ID.ToString();
            //    senderName.Text = ((BO.Package)e.UserState).sender.name;
            //    reciverName.Text = ((BO.Package)e.UserState).receiver.name;
            //    Ipackage.Visibility = Visibility.Visible;
            //    Iloading.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    IDShow_p.Visibility = Visibility.Hidden;
            //    senderName.Visibility = Visibility.Hidden;
            //    reciverName.Visibility = Visibility.Hidden;
            //    Iloading.Visibility = Visibility.Visible;
            //    Ipackage.Visibility = Visibility.Hidden;
            //}
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            this.Close();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            stopSimulator = false;
            stop.Visibility = Visibility.Hidden;
            start.Visibility = Visibility.Visible;
            worker.CancelAsync();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            stopSimulator = true;
            stop.Visibility = Visibility.Visible;
            start.Visibility = Visibility.Hidden;
            worker.RunWorkerAsync();

        }
    }
}
