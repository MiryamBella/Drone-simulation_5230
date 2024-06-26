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

namespace PL
{
    /// <summary>
    /// Interaction logic for Package.xaml
    /// </summary>
    public partial class Package : Window
    {
        BlApi.IBL bl;
        BO.Package p = new BO.Package();
        bool isPriority = false, isWeight = false;
        bool isFromClient = false;
        public Package(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;
        }
        public Package(BlApi.IBL ibl, BO.Package p)
        {
            InitializeComponent();
            bl = ibl;

            #region initialize;
            
            enterSender.Visibility = Visibility.Hidden;
            enterReceiver.Visibility = Visibility.Hidden;
            enterPriority.Visibility = Visibility.Hidden;
            enterWeight.Visibility = Visibility.Hidden;

            showID.Text = p.ID.ToString();
            showSender.Text = p.sender.ID.ToString();
            showReceiver.Text = p.receiver.ID.ToString();
            showPriority.Text = p.priority.ToString();
            showWeight.Text = p.weight.ToString();

            ID.Visibility = Visibility.Visible;
            showID.Visibility = Visibility.Visible;
            showSender.Visibility = Visibility.Visible;
            showReceiver.Visibility = Visibility.Visible;
            showPriority.Visibility = Visibility.Visible;
            showWeight.Visibility = Visibility.Visible;

            if (p.q != null)
            {
                showQudocopter.Text = p.q.ID.ToString();
                qudocopter.Visibility = Visibility.Visible;
                showQudocopter.Visibility = Visibility.Visible;
                collect.Visibility = Visibility.Visible;
                provide.Visibility = Visibility.Visible;
            }

            add.Visibility = Visibility.Hidden;
            
            #endregion;
        }
        public Package(BlApi.IBL ibl, string id)
        {
            InitializeComponent();
            bl = ibl;
            p.sender = new BO.clientInPackage();
            p.sender.ID = int.Parse(id);
            enterSender.Visibility = Visibility.Hidden;
            showSender.Text = id;
            showSender.Visibility = Visibility.Visible;
            isFromClient = true;
        }

        #region check input and enter into the new p

        private void writedSender(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterSender.Text, out id))
            {
                checkSender.Visibility = Visibility.Hidden;
                p.sender = new BO.clientInPackage() { ID = id };
            }
            else checkSender.Visibility = Visibility.Visible;
        }
        private void writedReceiver(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterReceiver.Text, out id))
            {
                checkReceiver.Visibility = Visibility.Hidden;
                p.receiver = new BO.clientInPackage() { ID = id };
            }
            else checkReceiver.Visibility = Visibility.Visible;
        }
        private void enterWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterWeight.SelectedItem == heavy) p.weight = BO.WeighCategories.hevy;
            else if (enterWeight.SelectedItem == easy) p.weight = BO.WeighCategories.easy;
            else p.weight = BO.WeighCategories.middle;
            isWeight = true;
        }
        private void enterPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterPriority.SelectedItem == reggular) p.priority = BO.Priorities.reggular;
            else if (enterPriority.SelectedItem == emergency) p.priority = BO.Priorities.emergency;
            else p.priority = BO.Priorities.fast;
            isPriority = true;
        }
        #endregion

        #region add package;
        private void adding(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkReceiver.Visibility == Visibility.Visible
                    || checkSender.Visibility == Visibility.Visible|| p.receiver.ID == 0 || p.sender.ID == 0
                    ||!isWeight || !isPriority)
                    throw new Exception("ERROR! check if all the data are correct.");
                
                bl.AddPackage(p.sender.ID, p.receiver.ID, p.weight, p.priority);
               
                    ListOfPackage l = new ListOfPackage(bl);
                    this.Close();
                if (!isFromClient)
                    l.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion;
        #region collected/provided;
        /// the two functation do nothing but we did them as button for the buty.
        private void colected(object sender, RoutedEventArgs e)
        { }
        private void provided(object sender, RoutedEventArgs e)
        { }
        #endregion;
    }
}
