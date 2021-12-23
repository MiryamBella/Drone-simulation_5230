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
using BlApi;
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for ListOfBaseStation.xaml
    /// </summary>
    public partial class ListOfBaseStation : Window
    {
        BlApi.IBL bl;
        private ObservableCollection<BO.BaseStationToList> myCollection
            = new ObservableCollection<BO.BaseStationToList>();

        public ListOfBaseStation(IBL ibl)
        {
            InitializeComponent();
            bl = ibl;

            foreach (BO.BaseStationToList bs in bl.ListOfBaseStations())
                myCollection.Add(bs);
            bs_list.ItemsSource = myCollection;
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_addNewBS(object sender, RoutedEventArgs e)
        {
            BaseStation bs = new BaseStation(bl);
            this.Close();
            bs.ShowDialog();
        }

        private void MouseDoubleClick_showBS(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.BaseStationToList bs = (BO.BaseStationToList)bs_list.SelectedItem;
                ViewBaseStation bs_w = new ViewBaseStation(bl, bs);
                Close();
                bs_w.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }

        private void Button_refreshe(object sender, RoutedEventArgs e)
        {
            #region the order of the program
            //    1)if the weigh select:
            //            clean the list and select acording to the weigh.
            //            A)if the mode select:
            //                    clean the list ans select acording to the weigh in the past our list.
            //            B)if the mode didnt select:
            //                    do nothing becose we all redy cleen the list to the original list.
            //    2)if the weigh didnt select:
            //            clean the list and add all the q in bl list.(so if there was selectation in the past it will be removed)
            //            A)if the mode select:
            //                    clean the list ans select acording to the weigh in the past our list.
            //            B)if the mode didnt select:
            //                    do nothing becose we all redy cleen the list to the original list.
            #endregion
            //get the select of the weigh.
            if (numCharghingPosition.SelectionLength > 0)
            {
                int minNum = int.Parse(numCharghingPosition.Text);
                myCollection.Clear();
                IEnumerable<BO.BaseStationToList> list = from BO.BaseStationToList bs in bl.ListOfBaseStations()
                                                         where bs.busyChargingPositions + bs.freeChargingPositions >= minNum
                                                         select bs;
                foreach (var bs in list)
                    myCollection.Add(bs);
            }
            else
            {
                myCollection.Clear();
                foreach (BO.BaseStationToList bs in bl.ListOfBaseStations())
                    myCollection.Add(bs);
            }
            if (freeCharghingPosition.IsChecked.Value)
            {
                IEnumerable<BO.BaseStationToList> list = (from BO.BaseStationToList bs in myCollection
                                                          where bs.freeChargingPositions > 0
                                                          select bs).ToList();
                myCollection.Clear();
                foreach (var bs in list)
                    myCollection.Add(bs);
            }
        }

        private void numCharghingPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x;
            bool h = int.TryParse(numCharghingPosition.Text, out x);
            if (!h)
                messge_minNum.Text="Enter only numbers.";
        }
    }
}
