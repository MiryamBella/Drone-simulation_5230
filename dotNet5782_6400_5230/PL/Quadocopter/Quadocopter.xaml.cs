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
        BlApi.IBL bl;
        BO.Quadocopter localQ = new BO.Quadocopter();

        public Quadocopter(BlApi.IBL ibl) //for adding quadocopter
        {
            bl = ibl;
            InitializeComponent();
            #region initialize;
            Title = "add a quadocopter";//Data adjustment to this constructor(hidden the shows of the second constuctor)
            showID.Visibility = Visibility.Hidden;
            showWeight.Visibility = Visibility.Hidden;
            battery.Visibility = Visibility.Hidden;
            showBattery.Visibility = Visibility.Hidden;
            showID_baseStation.Visibility = Visibility.Hidden;
            state.Visibility = Visibility.Hidden;
            showState.Visibility = Visibility.Hidden;
            showLongitude.Visibility = Visibility.Hidden;
            showLatitude.Visibility = Visibility.Hidden;
            showPackage.Visibility = Visibility.Hidden;//hidde the button
            uppdate.Visibility = Visibility.Hidden;
            charge.Visibility = Visibility.Hidden;
            latitude.Visibility = Visibility.Hidden;
            longitude.Visibility = Visibility.Hidden;

            ///enter the ID of the base station in our data.
            foreach (var q in bl.ListOfBaseStations())
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = q.ID;
                ID_baseStation.Items.Add(newItem);
            }
            #endregion;
        }
        public Quadocopter(BlApi.IBL ibl, BO.QuadocopterToList q)//for view of quadocopter
        {
            bl = ibl;
            InitializeComponent();
            #region initialize;
            Title = "drone" + q.ID; //Data adjustment to this constructor(hidden the shows of the second constuctor)
            enterID.Visibility = Visibility.Hidden;
            enterWeight.Visibility = Visibility.Hidden;
            ID_baseStation.Visibility = Visibility.Hidden;

            addQ.Visibility=Visibility.Hidden;
            uppdate.Visibility = Visibility.Visible;
            charge.Visibility = Visibility.Visible;

            //if the drone dont have packes so hiide the butun of the packes.
            if (q.packageNumber > 0)
                showPackage.Visibility = Visibility.Visible;

            showID.Text = q.ID.ToString();//data adjusment to this qudocopters data

            if (q.weight == BO.WeighCategories.easy) showWeight.Text = "easy";
            else if (q.weight == BO.WeighCategories.middle) showWeight.Text = "middle";
            else showWeight.Text = "heavy";

            enterModel.Text = q.moodle;
            showBattery.Text = q.battery.ToString();
            foreach(BO.BaseStationToList b in bl.ListOfBaseStations())
                if (bl.baseStationDisplay(b.ID).thisLocation == q.thisLocation)
                {
                    showID_baseStation.Text = b.ID.ToString();
                    break;
                }

            if (q.mode == BO.statusOfQ.available) showState.Text = "available";
            else if (q.mode == BO.statusOfQ.maintenance) showState.Text = "maintence";
            else showState.Text = "delivery";
            showLatitude.Text = q.thisLocation.Latitude.ToString();
            showLongitude.Text = q.thisLocation.Longitude.ToString();
            showLocation.Text = q.thisLocation.Location60;
            ///set the data to the local drone.
            localQ = bl.cover(q);
            if (q.mode == BO.statusOfQ.maintenance)
                charge.Content = "relese from charge";
            else if (q.mode == BO.statusOfQ.available)
                charge.Content = "send to charge";
            else
                charge.Visibility = Visibility.Hidden;
            #endregion;
        }

        #region check data
        private void writedID(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterID.Text, out id))
                localQ.ID = id;
            else checkID.Visibility = Visibility.Visible;
        }
        private void enterWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterWeight.SelectedItem == heavy) localQ.weight = BO.WeighCategories.hevy;
            if (enterWeight.SelectedItem == middle) localQ.weight = BO.WeighCategories.middle;
            else localQ.weight = BO.WeighCategories.easy;
        }
        private void writedModel(object sender, RoutedEventArgs e)
        {
            localQ.moodle = enterModel.Text;
        }
        //private void writedBattery(object sender, RoutedEventArgs e)
        //{
        //    int b;
        //    if (int.TryParse(enterBattery.Text, out b))
        //    {
        //        checkBattery2.Visibility = Visibility.Hidden;
        //        if (b <= 100 && b >= 0) 
        //        { 
        //            localQ.battery = b;
        //            checkBattery.Visibility = Visibility.Hidden;
        //        }
        //        else checkBattery.Visibility = Visibility.Visible;
        //    }
        //    else checkBattery2.Visibility = Visibility.Visible;
        //}
        //private void enterState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (enterState.SelectedItem == available) localQ.mode = BO.statusOfQ.available;
        //    if (enterState.SelectedItem == maintenance) localQ.mode = BO.statusOfQ.maintenance;
        //    else localQ.mode = BO.statusOfQ.delivery;
        //}
        //private void writedLatitude(object sender, RoutedEventArgs e)
        //{
        //    double l;
        //    if (double.TryParse(enterLatitude.Text, out l))
        //    {
        //        localQ.thisLocation.latitude = l;
        //        checkLatitude.Visibility = Visibility.Hidden;
        //    }
        //    else checkLatitude.Visibility = Visibility.Visible;
        //}
        //private void writedLongitude(object sender, RoutedEventArgs e)
        //{
        //    double l;
        //    if (double.TryParse(enterLongitude.Text, out l))
        //    {
        //        localQ.thisLocation.longitude = l;
        //        checkLongitude.Visibility = Visibility.Hidden;
        //    }
        //    else checkLongitude.Visibility = Visibility.Visible;
        //}
        #endregion

        private void adding(object sender, RoutedEventArgs e)
        {
            try
            {
                /*checking if evrything is ok with the input.*/
                ///if the user put wrong data.
                if (checkID.Visibility == Visibility.Visible ||
                    enterWeight.SelectedItem==null || 
                    ID_baseStation.SelectedItem == null)
                    throw new Exception("ERROR! chek if all the data are corect.");
                ///if the user didnt put all the nessery data.
                if (ID_bs_text.Text == null || enterModel.Text == null)
                    throw new Exception("ERROR: you miss some data to enter.");
                //if (int.Parse(enterLatitude.Text) > 10 || int.Parse(enterLatitude.Text) < 0)
                //    throw new Exception("The location is too far. Pleas enter latiude betwhin 0-10.");
                //if (int.Parse(enterLongitude.Text) > 10 || int.Parse(enterLongitude.Text) < 0)
                //    throw new Exception("The location is too far. Pleas enter longitude betwhin 0-10.");

                //get the location.
                int id_bs = int.Parse(ID_baseStation.Text.ToString());
                localQ.thisLocation = bl.baseStationDisplay(id_bs).thisLocation;

                localQ.battery = 100;
                bl.AddQuadocopter(localQ.ID, localQ.moodle, (int)localQ.weight, int.Parse(ID_baseStation.Text.ToString()));
                ListOfQ l = new ListOfQ(bl);
                this.Close();
                l.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showPackage_Click(object sender, RoutedEventArgs e)
        {
            BO.Quadocopter q = bl.QuDisplay(int.Parse(showID.Text));
            if(q.thisPackage==null)
            {
                MessageBox.Show("The quadocopter don't trans any package.");
                return;
            }
            MessageBox.Show(q.thisPackage.ToString());
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void uppdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(showID.Text);
            try
            {
                if (enterModel.Text == null)
                    throw new Exception("ERROR: invalid modle.");
                bl.updateQdata(id, enterModel.Text);
                MessageBox.Show("update complete.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (localQ.mode)
                {
                    case BO.statusOfQ.available:
                        localQ=bl.cover(bl.sendQtoChrge(localQ.ID));
                        charge.Content = "relese from charge";
                        showBattery.Text = localQ.battery.ToString();
                        showLongitude.Text = localQ.thisLocation.Longitude.ToString();
                        showLatitude.Text = localQ.thisLocation.Latitude.ToString();
                        break;

                    case BO.statusOfQ.maintenance:
                        localQ.battery=bl.releaseQfromChrge(localQ.ID);
                        charge.Content = "send to charge";
                        localQ.mode = BO.statusOfQ.available;
                        showBattery.Text = localQ.battery.ToString();
                        break;
                    default:
                        throw new Exception("The drone cant go to send or to relese.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #region simulator

        private void simulator_begin(object sender, RoutedEventArgs e)
        {
            SimulatorQuadocopter sq = new SimulatorQuadocopter(bl, localQ.ID);
            sq.Show();

            localQ = bl.QuDisplay(localQ.ID);
            showBattery.Text = localQ.battery.ToString();
            showLatitude.Text = localQ.thisLocation.Latitude.ToString();
            showLongitude.Text = localQ.thisLocation.Longitude.ToString();
            showLocation.Text = localQ.thisLocation.Location60;
        }
        #endregion
    }
}
