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

namespace PL
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        BlApi.IBL bl;
        public Manager(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;
        }

        #region battuns
        private void listQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListOfQ l = new ListOfQ(bl);
                l.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void listBS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListOfBaseStation l = new ListOfBaseStation(bl);
                l.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void listClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListOfClients l = new ListOfClients(bl);
                l.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void listPackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListOfPackage l = new ListOfPackage(bl);
                l.Show();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        
        #endregion


    }
}
