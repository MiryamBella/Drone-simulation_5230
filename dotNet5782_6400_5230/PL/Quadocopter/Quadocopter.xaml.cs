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
        BO.Quadocopter newQ = new BO.Quadocopter();
        public Quadocopter(BlApi.IBL ibl) //for adding quadocopter
        {
            bl = ibl;
            InitializeComponent();
            #region initialize;
            Title = "add a quadocopter";//Data adjustment to this constructor(hidden the shows of the second constuctor)
            showID.Visibility = Visibility.Hidden;
            showWeight.Visibility = Visibility.Hidden;
            showBattery.Visibility = Visibility.Hidden;
            showID_baseStation.Visibility = Visibility.Hidden;
            showState.Visibility = Visibility.Hidden;
            showLongitude.Visibility = Visibility.Hidden;
            showLatitude.Visibility = Visibility.Hidden;
            showPackage.Visibility = Visibility.Hidden;//show the button

            ///enter the ID of the base station in our data.
            foreach(var q in bl.ListOfBaseStations())
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
            Title = "quadocopter" + q.ID; //Data adjustment to this constructor(hidden the shows of the second constuctor)
            enterID.Visibility = Visibility.Hidden;
            enterWeight.Visibility = Visibility.Hidden;
            enterBattery.Visibility = Visibility.Hidden;
            ID_baseStation.Visibility = Visibility.Hidden;
            enterState.Visibility = Visibility.Hidden;
            enterLatitude.Visibility = Visibility.Hidden;
            enterLongitude.Visibility = Visibility.Hidden;
            addQ.Visibility=Visibility.Hidden;
            if(q.packageNumber>0)
                showPackage.Visibility = Visibility.Visible;

            showID.Text = q.ID.ToString();//data adjusment to this qudocopters data
            if (q.weight == BO.WeighCategories.easy) showWeight.Text = "easy";
            else if (q.weight == BO.WeighCategories.middle) showWeight.Text = "middle";
            else showWeight.Text = "heavy";
            enterModel.Text = q.moodle;
            showBattery.Text = q.battery.ToString();
            showID_baseStation.Text = ID_baseStation.Text;
            if (q.mode == BO.statusOfQ.available) showState.Text = "available";
            else if (q.mode == BO.statusOfQ.maintenance) showState.Text = "maintence";
            else showState.Text = "delivery";
            showLatitude.Text = q.thisLocation.latitude.ToString();
            showLongitude.Text = q.thisLocation.longitude.ToString();
            #endregion;
        }

        #region check data
        private void writedID(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(enterID.Text, out id))
                newQ.ID = id;
            else checkID.Visibility = Visibility.Visible;
        }
        private void enterWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterWeight.SelectedItem == heavy) newQ.weight = BO.WeighCategories.hevy;
            if (enterWeight.SelectedItem == middle) newQ.weight = BO.WeighCategories.middle;
            else newQ.weight = BO.WeighCategories.easy;
        }
        private void writedModel(object sender, RoutedEventArgs e)
        {
            newQ.moodle = enterModel.Text;
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
        private void enterState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enterState.SelectedItem == available) newQ.mode = BO.statusOfQ.available;
            if (enterState.SelectedItem == maintenance) newQ.mode = BO.statusOfQ.maintenance;
            else newQ.mode = BO.statusOfQ.delivery;
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
        #endregion

        private void adding(object sender, RoutedEventArgs e)
        {
            try
            {
                /*checking if evrything is ok with the input.*/
                ///if the user put wrong data.
                if (checkID.Visibility == Visibility.Visible || checkLongitude.Visibility == Visibility.Visible
                    || checkLongitude.Visibility == Visibility.Visible || checkBattery.Visibility == Visibility.Visible ||
                    checkBattery2.Visibility == Visibility.Visible || enterWeight.SelectedItem==null || 
                    enterState.SelectedItem == null || ID_baseStation.SelectedItem == null)
                    throw new Exception("ERROR! chek if all the data are corect.");
                ///if the user didnt put all the nessery data.
                if (ID_bs_text.Text == null || enterModel.Text == null || enterBattery.Text == null ||
                    enterLatitude.Text == null || enterLongitude.Text == null)
                    throw new Exception("ERROR: you miss some data to enter.");

                bl.AddQuadocopter(newQ.ID, newQ.moodle, (int)newQ.weight, int.Parse(ID_baseStation.SelectedItem.ToString()));
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
    }
}
