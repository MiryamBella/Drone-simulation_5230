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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Quadocopter : Window
    {
        IBL.IBL bl;
        IBL.BO.Quadocopter newQ = new IBL.BO.Quadocopter();
        public Quadocopter(IBL.IBL ibl) //for adding quadocopter
        {
            bl = ibl;
            InitializeComponent();
            Title = "add a quadocopter";//Data adjustment to this constructor(hidden the shows of the second constuctor)
            showID.Visibility = Visibility.Hidden;
            showWeight.Visibility = Visibility.Hidden;
            showBattery.Visibility = Visibility.Hidden;
            showDelivery.Visibility = Visibility.Hidden;
            showState.Visibility = Visibility.Hidden;
            showLongitude.Visibility = Visibility.Hidden;
            showLatitude.Visibility = Visibility.Hidden;
        }
        public Quadocopter(IBL.IBL ibl, IBL.BO.QuadocopterToList q)//for view of quadocopter
        {
            bl = ibl;
            InitializeComponent();
            Title = "quadocopter" + q.ID; //Data adjustment to this constructor(hidden the shows of the second constuctor)
            enterID.Visibility = Visibility.Hidden;
            enterWeight.Visibility = Visibility.Hidden;
            enterBattery.Visibility = Visibility.Hidden;
            enterDelivery.Visibility = Visibility.Hidden;
            enterState.Visibility = Visibility.Hidden;
            enterLatitude.Visibility = Visibility.Hidden;
            enterLongitude.Visibility = Visibility.Hidden;

            showID.Text = q.ID.ToString();//data adjusment to this qudocopters data
            if (q.weight == IBL.BO.WeighCategories.easy) showWeight.Text = "easy";
            else if (q.weight == IBL.BO.WeighCategories.middle) showWeight.Text = "middle";
            else showWeight.Text = "heavy";
            enterModel.Text = q.moodle;
            showBattery.Text = q.battery.ToString();
            showDelivery.Text = "0";
            if (q.mode == IBL.BO.statusOfQ.available) showState.Text = "available";
            else if (q.mode == IBL.BO.statusOfQ.maintenance) showState.Text = "maintence";
            else showState.Text = "delivery";
            showLatitude.Text = q.thisLocation.latitude.ToString();
            showLongitude.Text = q.thisLocation.longitude.ToString();
        }

        private void writedID(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterID.Text, out id))
                newQ.ID = id;
            else checkID.Visibility = Visibility.Visible;
        }

        private void enterWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterWeight.SelectedItem == heavy) newQ.weight = IBL.BO.WeighCategories.hevy;
            if (enterWeight.SelectedItem == middle) newQ.weight = IBL.BO.WeighCategories.middle;
            else newQ.weight = IBL.BO.WeighCategories.easy;
        }
        private void writedModel(object sender, RoutedEventArgs e)
        {
                newQ.moodle = enterID.Text;
        }
        private void writedBattery(object sender, RoutedEventArgs e)
        {
            int b;
            if (int.TryParse(enterBattery.Text, out b))
            {
                checkBattery2.Visibility = Visibility.Hidden;
                if (b <= 100 && b >= 0) 
                { 
                    newQ.battery = b;
                    checkBattery.Visibility = Visibility.Hidden;
                }
                else checkBattery.Visibility = Visibility.Visible;
            }
            else checkBattery2.Visibility = Visibility.Visible;
        }
        private void writedDelivery(object sender, RoutedEventArgs e)
        {
            int d;
            if (int.TryParse(enterDelivery.Text, out d))
            {
                newQ.thisPackage.ID = d;
                checkDelivery.Visibility = Visibility.Hidden;
            }
            else checkDelivery.Visibility = Visibility.Visible;
        }
        private void enterState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterWeight.SelectedItem == available) newQ.mode = IBL.BO.statusOfQ.available;
            if (enterWeight.SelectedItem == maintenance) newQ.mode = IBL.BO.statusOfQ.maintenance;
            else newQ.mode = IBL.BO.statusOfQ.delivery;
        }
        private void writedLatitude(object sender, RoutedEventArgs e)
        {
            double l;
            if (double.TryParse(enterLatitude.Text, out l))
            {
                newQ.thisLocation.latitude = l;
                checkLatitude.Visibility = Visibility.Hidden;
            }
            else checkLatitude.Visibility = Visibility.Visible;
        }
        private void writedLongitude(object sender, RoutedEventArgs e)
        {
            double l;
            if (double.TryParse(enterLongitude.Text, out l))
            {
                newQ.thisLocation.longitude = l;
                checkLongitude.Visibility = Visibility.Hidden;
            }
            else checkLongitude.Visibility = Visibility.Visible;
        }

        private void adding(object sender, RoutedEventArgs e)
        {
            try 
            {
                bl.AddQuadocopter(newQ.ID, newQ.moodle, (int)newQ.weight, int.Parse(enterDelivery.Text));
            }
            catch (IBL.BO.BLException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
