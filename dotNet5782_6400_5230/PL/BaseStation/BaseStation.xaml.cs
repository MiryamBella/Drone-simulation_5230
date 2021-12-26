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
    /// Interaction logic for BaseStation.xaml
    /// </summary>
    public partial class BaseStation : Window
    {
        BlApi.IBL bl;
        //BO.BaseStation newBS = new BO.BaseStation();
        public BaseStation(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;
        }

        #region chek input
        private void writedID(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterID.Text, out id) && int.Parse(enterID.Text) >= 0)
                checkID.Visibility = Visibility.Hidden;
            else checkID.Visibility = Visibility.Visible;

        }

        private void writedLatitude(object sender, RoutedEventArgs e)
        {
            double l;
            if (double.TryParse(enterLat.Text, out l))
                checkLat.Visibility = Visibility.Hidden;
            else checkLat.Visibility = Visibility.Visible;

        }
        private void writedLongitude(object sender, RoutedEventArgs e)
        {
            double l;
            if (double.TryParse(enterLon.Text, out l))
                checkLon.Visibility = Visibility.Hidden;
            else checkLon.Visibility = Visibility.Visible;
        }
        private void writedNumCharge(object sender, TextChangedEventArgs e)
        {
            int id;
            if (int.TryParse(enterNumCharge.Text, out id) && int.Parse(enterNumCharge.Text)>=0)
                checkNumCharging.Visibility = Visibility.Hidden;
            else checkNumCharging.Visibility = Visibility.Visible;
        }
        #endregion
        private void adding(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkID.Visibility==Visibility.Visible || checkLat.Visibility == Visibility.Visible
                    || checkLon.Visibility == Visibility.Visible || checkNumCharging.Visibility == Visibility.Visible)
                    throw new Exception("ERROR! chek if all the data are corect.");
                int id = int.Parse(enterID.Text);
                string name = enterName.Text;
                double lon = double.Parse(enterLon.Text);
                double lat = double.Parse(enterLon.Text);
                int numCh = int.Parse(enterNumCharge.Text);
                bl.AddBaseStation(id, name, lon, lat, numCh);
                ListOfBaseStation l = new ListOfBaseStation(bl);
                this.Close();
                l.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
