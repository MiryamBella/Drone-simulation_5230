﻿using System;
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
        public SimulatorQuadocopter(BlApi.IBL ibl, int id)
        {
            InitializeComponent();
            bl = ibl;
            localQ = bl.QuDisplay(id);
            IdShow.Text = localQ.ID.ToString();
            locationShwo.Text = localQ.thisLocation.decSix.ToString();
            localQ.thisLocation.Location60 = localQ.thisLocation.decSix.ToString();
            batteryShow.Text = localQ.battery.ToString();
            VisualBattery.Value = localQ.battery;
            stopSimulator = false;

            if (localQ.thisPackage == null)
            {
                changeVisubility(Visibility.Hidden);
                //ID_p.Visibility = Visibility.Hidden;
                //IDShow_p.Visibility = Visibility.Hidden;
                //nameS.Visibility = Visibility.Hidden;
                //nameR.Visibility = Visibility.Hidden;
                //senderName.Visibility = Visibility.Hidden;
                //reciverName.Visibility = Visibility.Hidden;
            }
            else
            {
                IDShow_p.Text = localQ.thisPackage.ID.ToString();
                senderName.Text = localQ.thisPackage.sender.name;
                reciverName.Text = localQ.thisPackage.receiver.name;
            }

            worker = new BackgroundWorker();
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
                bl.startSimulator(localQ.ID, reportProgress, isSTOP, bl);
            }
            catch(Exception ex)
            {
                //if(ex==)
                MessageBox.Show(ex.Message);
            }
        } 
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int addToBattery = e.ProgressPercentage;
            localQ.battery += addToBattery;
            if (localQ.battery > 100)
                localQ.battery = 100;
            batteryShow.Text = localQ.battery.ToString();
            VisualBattery.Value = localQ.battery;

            BO.Quadocopter q = bl.QuDisplay(localQ.ID);
            locationShwo.Text = q.thisLocation.Location60;
            localQ.thisLocation = q.thisLocation;

            if (e.UserState != null)
            {
                IDShow_p.Text = ((BO.Package)e.UserState).ID.ToString();
                senderName.Text = ((BO.Package)e.UserState).sender.name;
                reciverName.Text = ((BO.Package)e.UserState).receiver.name;
                ID_p.Visibility = Visibility.Visible;
                IDShow_p.Visibility = Visibility.Visible;
                nameS.Visibility = Visibility.Visible;
                nameR.Visibility = Visibility.Visible;
                senderName.Visibility = Visibility.Visible;
                reciverName.Visibility = Visibility.Visible;


                Ipackage.Visibility = Visibility.Visible;
                Iloading.Visibility = Visibility.Hidden;
            }
            else
            {
                ID_p.Visibility = Visibility.Hidden;
                IDShow_p.Visibility = Visibility.Hidden;
                nameS.Visibility = Visibility.Hidden;
                nameR.Visibility = Visibility.Hidden;
                senderName.Visibility = Visibility.Hidden;
                reciverName.Visibility = Visibility.Hidden;

                Iloading.Visibility = Visibility.Visible;
                Ipackage.Visibility = Visibility.Hidden;
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            if (stopSimulator)
                MessageBox.Show("There is no packages to take.");
            else
                MessageBox.Show("End of the simulator.");

            stopSimulator = false;
            stop.Visibility = Visibility.Hidden;
            start.Visibility = Visibility.Visible;
            Quadocopter q = new Quadocopter(bl, bl.cover(localQ));
            this.Close();
            q.Show();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stopSimulator = false;
                stop.Visibility = Visibility.Hidden;
                start.Visibility = Visibility.Visible;
                //worker.CancelAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stopSimulator = true;
                stop.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Hidden;
                if (worker.IsBusy == false)
                    worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        void changeVisubility(Visibility v)
        {
            ID_p.Visibility = v;
            IDShow_p.Visibility = v;
            nameS.Visibility = v;
            nameR.Visibility = v;
            senderName.Visibility = v;
            reciverName.Visibility = v;
        }
    }
}
