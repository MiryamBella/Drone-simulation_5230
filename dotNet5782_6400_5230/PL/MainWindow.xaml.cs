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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
//using System.Linq;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bl = BL.BlFactory.GetBL();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void manager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnterAccess enter = new EnterAccess(bl, "manager");
                enter.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnterAccess enter = new EnterAccess(bl, "client");
                enter.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void clientNEW_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client new_c = new Client(bl);
                new_c.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
    }
}
