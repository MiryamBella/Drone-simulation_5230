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
            showLat.Text = b.thisLocation.latitude.ToString();
            showLon.Text = b.thisLocation.longitude.ToString();
            showNumCharging.Text = b.freeChargingPositions.ToString();
            show_location_six.Text = b.thisLocation.toBaseSix.LocationSix(b.thisLocation.latitude, b.thisLocation.longitude).ToString();
        }

        private void listQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ourID = int.Parse(showID.Text);
                var l = (from BO.QuadocopterToList q in bl.ListOfQ()
                         from BO.Charging c in bl.GetChargings()
                         where c.baseStationID == ourID && q.ID == c.quadocopterID
                         select q).ToList();
                if (l.Count > 0)
                {
                    ListOfQ lq = new ListOfQ(bl, l);
                    lq.Show();
                }
                else MessageBox.Show("There is no quadocopters in charge in this base station.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
