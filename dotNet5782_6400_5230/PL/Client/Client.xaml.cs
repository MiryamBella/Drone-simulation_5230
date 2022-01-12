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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    
    public partial class Client : Window
    {
        BlApi.IBL bl;
        private ObservableCollection<BO.PackageToList> packageToList
            = new ObservableCollection<BO.PackageToList>();
        private ObservableCollection<BO.PackageToList> packageFromList
            = new ObservableCollection<BO.PackageToList>();
        public Client(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;
        }
        public Client(BlApi.IBL ibl, BO.Client c)
        {
            InitializeComponent();
            bl = ibl;
            #region initialize;
            enterID.Visibility = Visibility.Hidden;
            enterLat.Visibility = Visibility.Hidden;
            enterLon.Visibility = Visibility.Hidden;

            showID.Visibility = Visibility.Visible;
            showID.Text = c.ID.ToString();
            showLat.Visibility = Visibility.Visible;
            showLat.Text = c.thisLocation.Latitude.ToString();
            showLon.Visibility = Visibility.Visible;
            showLon.Text = c.thisLocation.Longitude.ToString();
            enterName.Text = c.name;
            enterPhoneNumber.Text = c.phoneNumber.ToString();


            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Visible;

            var a = from p in bl.ListOfPackages()
                    where p.senderName == c.name
                    select p;
            foreach (BO.PackageToList pa in a)
                packageToList.Add(pa);
            packageTo.ItemsSource = packageToList;
            var b = from p in bl.ListOfPackages()
                    where p.receiverName == c.name
                    select p;
            foreach (BO.PackageToList pa in b)
                packageFromList.Add(pa);
            packageFrom.ItemsSource = packageFromList;

            packageTo.Visibility = Visibility.Visible;
            packageFrom.Visibility = Visibility.Visible;
            pTo.Visibility = Visibility.Visible;
            pFrom.Visibility = Visibility.Visible;

            #endregion;
        }

        #region check input

        private void writedID(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterID.Text, out id))
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
        private void writedPhoneNumber(object sender, TextChangedEventArgs e)
        {
            int phone;
            if (int.TryParse(enterPhoneNumber.Text, out phone))
                checkPhoneNumber.Visibility = Visibility.Hidden;
            else checkPhoneNumber.Visibility = Visibility.Visible;
        }
        #endregion
      
        #region add client;
        private void adding(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkID.Visibility==Visibility.Visible || checkLat.Visibility == Visibility.Visible
                    || checkLon.Visibility == Visibility.Visible || checkPhoneNumber.Visibility == Visibility.Visible)
                    throw new Exception("ERROR! check if all the data are corect.");
                int id = int.Parse(enterID.Text);
                string name = enterName.Text;
                double lon = double.Parse(enterLon.Text);
                double lat = double.Parse(enterLon.Text);
                int phone = int.Parse(enterPhoneNumber.Text);
                bl.AddClient(id, name, lon, lat, phone);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion;
        
        #region update client;
        private void updating(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkPhoneNumber.Visibility == Visibility.Visible)
                    throw new Exception("ERROR! check if all the data are corect.");
                int id = int.Parse(showID.Text);
                string name = enterName.Text;
                int phone = int.Parse(enterPhoneNumber.Text);
                bl.updateCdata(id, name, phone);
                MessageBox.Show("uppdate complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion;
     
        #region open a package of this client;
        //MouseDoubleClick_packageTo
        private void MouseDoubleClick_packageTo(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Package p = bl.cover((BO.PackageToList)packageTo.SelectedItem);
                if (p == null) return;
                Package p_w = new Package(bl, p);
                Close();
                p_w.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        private void MouseDoubleClick_packageFrom(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Package p = (BO.Package)packageFrom.SelectedItem;
                if (p == null) return;
                Package p_w = new Package(bl, p);
                Close();
                p_w.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
        #endregion;
    }
}
