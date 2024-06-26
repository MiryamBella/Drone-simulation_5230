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
    /// Interaction logic for ViewBaseStation.xaml
    /// </summary>
    public partial class ViewBaseStation : Window
    {
        BlApi.IBL bl;
        public ViewBaseStation(BlApi.IBL ibl, BO.BaseStationToList bs)
        {
            InitializeComponent();
            bl = ibl;
            Title = "base station: " + bs.ID; //Data adjustment to this constructor(hidden the shows of the second constuctor)
            var b = bl.baseStationDisplay(bs.ID);
            showID.Text = b.ID.ToString();//data adjusment to this qudocopters data
            showName.Text = b.name;
            update_name.Text = b.name;

            showLat.Text = b.thisLocation.Latitude.ToString();
            showLon.Text = b.thisLocation.Longitude.ToString();
            show_location_six.Text = b.thisLocation.toBaseSix.LocationSix(b.thisLocation.Latitude, b.thisLocation.Longitude).ToString();

            showNumCharging.Text = b.freeChargingPositions.ToString();
            uppdate_numCharge.Text = b.freeChargingPositions.ToString();

            try
            {
                //look for the drones in that charge un our base station.
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
            try
            {
                if (list_q.SelectedItem == null)
                    throw new Exception("There is no select of a quadocopters, or the base station not charhe any drone.");
                BO.Quadocopter q = new BO.Quadocopter();
                q = bl.QuDisplay( int.Parse(list_q.Text) );
                BO.QuadocopterToList ql= new BO.QuadocopterToList();
                ql = bl.cover(q);
                Quadocopter qPL = new Quadocopter(bl, ql);
                qPL.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region uppdate
        private void writedNumCharge(object sender, TextChangedEventArgs e)
        {
            int num;
            if (int.TryParse(uppdate_numCharge.Text, out num) && int.Parse(uppdate_numCharge.Text) >= 0)
                checkNumCharging.Visibility = Visibility.Hidden;
            else checkNumCharging.Visibility = Visibility.Visible;
        }

        private void updateBS_Click(object sender, RoutedEventArgs e)
        {
            ///show the boxs to write the new data.
            update_name.Visibility = Visibility.Visible;
            update_name.Text = showName.Text;
            uppdate_numCharge.Visibility = Visibility.Visible;
            uppdate_numCharge.Text = showNumCharging.Text;
            //hide the box of the old data.
            showName.Visibility = Visibility.Hidden;
            showNumCharging.Visibility = Visibility.Hidden;
            //show the buttons to uppdate the new data.
            updateBS.Visibility = Visibility.Hidden;
            updateBS_change.Visibility = Visibility.Visible;
            updateBS_notchange.Visibility = Visibility.Visible;

            numCharging.Text = "Number of charge position:";
        }
        private void updateBS_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkNumCharging.Visibility==Visibility.Visible || update_name.Text==null)
                    throw new Exception("ERROR! chek if all the data are corect.");
                string name = update_name.Text;
                int numCh = int.Parse(uppdate_numCharge.Text);
                bl.updateSdata(int.Parse(showID.Text), name, numCh);

                //reset the data.
                showName.Text = update_name.Text;
                showNumCharging.Text = uppdate_numCharge.Text;

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

            //reset the data.
            update_name.Text = showName.Text;
            uppdate_numCharge.Text = showNumCharging.Text;
            numCharging.Text = "Number of free charghing position:";
        }
        #endregion

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}