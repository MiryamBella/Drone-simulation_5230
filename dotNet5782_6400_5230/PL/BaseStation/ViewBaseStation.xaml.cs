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
    /// Interaction logic for ViewBaseStation.xaml
    /// </summary>
    public partial class ViewBaseStation : Window
    {
        BlApi.IBL bl;
        public ViewBaseStation(BlApi.IBL ibl, BO.BaseStationToList bs)
        {
            InitializeComponent();
            bl = ibl;
            Title = "quadocopter" + bs.ID; //Data adjustment to this constructor(hidden the shows of the second constuctor)
            var b = bl.baseStationDisplay(bs.ID);
            showID.Text = b.ID.ToString();//data adjusment to this qudocopters data
            showName.Text = b.name;
            showLat.Text = b.thisLocation.latitude.ToString();
            showLon.Text = b.thisLocation.longitude.ToString();
            showNumCharging.Text = b.freeChargingPositions.ToString();
            show_location_six.Text = b.thisLocation.toBaseSix.LocationSix(b.thisLocation.latitude, b.thisLocation.longitude).ToString();

            try
            {
                int ourID = int.Parse(showID.Text);
                var l = (from BO.QuadocopterToList q in bl.ListOfQ()
                         from BO.Charging c in bl.GetChargings()
                         where c.baseStationID == ourID && q.ID == c.quadocopterID
                         select q).ToList();
                if (l.Count > 0)
                {
                    foreach (var q in l)
                    {
                        ComboBoxItem newItem = new ComboBoxItem();
                        newItem.Content = q.ID;
                        list_q.Items.Add(newItem);
                    }

                }
                else MessageBox.Show("There is no quadocopters in charge in this base station.");
            }
            catch (Exception ex)
            {
                if("There is no quadocopter who is charge."!=ex.Message)
                    MessageBox.Show(ex.Message);
                //else, if the user will want to see the the quadocopters in charge, we wiil sey him there is no.
                
            }
        }

        private void chergeQ_Click(object sender, RoutedEventArgs e)
        {
            if(list_q.SelectedItem==null)
            {
                MessageBox.Show("There is no select of a quadocopters, or the base station not charhe any drone.");
                return;
            }    
            BO.Quadocopter q = new BO.Quadocopter();
            q = bl.QuDisplay(int.Parse(list_q.SelectedItem.ToString()));
            Quadocopter qpl = new Quadocopter(bl, bl.cover(q));
        }

        #region uppdate
        private void updateBS_Click(object sender, RoutedEventArgs e)
        {
            ///show the boxs to write the new data.
            update_name.Visibility = Visibility.Visible;
            uppdate_numCharge.Visibility = Visibility.Visible;
            //hide the box of the old data.
            showName.Visibility = Visibility.Hidden;
            showNumCharging.Visibility = Visibility.Hidden;
            //show the buttons to uppdate the new data.
            updateBS.Visibility = Visibility.Hidden;
            updateBS_change.Visibility = Visibility.Visible;
            updateBS_notchange.Visibility = Visibility.Visible;
        }

        private void writedNumCharge(object sender, TextChangedEventArgs e)
        {
            int num;
            if (int.TryParse(uppdate_numCharge.Text, out num) && int.Parse(uppdate_numCharge.Text) >= 0)
                checkNumCharging.Visibility = Visibility.Hidden;
            else checkNumCharging.Visibility = Visibility.Visible;
        }


        private void updateBS_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkNumCharging.IsEnabled || update_name.Text==null)
                    throw new Exception("ERROR! chek if all the data are corect.");
                string name = update_name.Text;
                int numCh = int.Parse(uppdate_numCharge.Text);
                bl.updateSdata(int.Parse(showID.Text), name, numCh);

                updateBS_notchange_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void updateBS_notchange_Click(object sender, RoutedEventArgs e)
        {
            update_name.Visibility = Visibility.Hidden;
            uppdate_numCharge.Visibility = Visibility.Hidden;

            showName.Visibility = Visibility.Visible;
            showNumCharging.Visibility = Visibility.Visible;
            updateBS.Visibility = Visibility.Visible;
            updateBS_change.Visibility = Visibility.Hidden;
            updateBS_notchange.Visibility = Visibility.Hidden;

        }
        #endregion
    }
}
