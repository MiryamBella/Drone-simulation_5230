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
                BaseStation bs_w = new BaseStation(bl, bs);
                bs_w.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }

        private void Button_refreshe(object sender, RoutedEventArgs e)
        {
        //    /*  -----------------------the order of the program:---------------------------------------
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
        //     */
        //    //get the select of the weigh.
        //    ComboBoxItem w = (ComboBoxItem)Quadocopter_whait.SelectedItem;
        //    bool selected = true;
        //    //if w=null this mean the user didnt select something.
        //    if (w != null && (w.Content.ToString() == "easy" || w.Content.ToString() == "hevy" || w.Content.ToString() == "middle"))
        //        selected = true;
        //    else
        //    {
        //        selected = false;
        //        myCollection.Clear();
        //        foreach (BO.QuadocopterToList ql in bl.ListOfQ())
        //            myCollection.Add(ql);
        //    }

        //    if (selected)
        //    {
        //        try
        //        {
        //            List<BO.QuadocopterToList> l = bl.ListOfQ_of_weigh(w.Content.ToString());
        //            myCollection.Clear();
        //            foreach (BO.QuadocopterToList ql in l)
        //                myCollection.Add(ql);
        //        }
        //        catch (BO.BLException ex)
        //        {
        //            MessageBox.Show("Error! " + ex.Message);
        //        }
        //    }

        //    //get the select of the mode.
        //    BO.statusOfQ mode = new BO.statusOfQ();
        //    ComboBoxItem m = (ComboBoxItem)Quadocopter_mode.SelectedItem;
        //    selected = true;
        //    if (m == null)
        //        return;
        //    switch (m.Content.ToString())
        //    {
        //        case "available":
        //            mode = BO.statusOfQ.available;
        //            break;
        //        case "maintenance":
        //            mode = BO.statusOfQ.maintenance;
        //            break;
        //        case "delivery":
        //            mode = BO.statusOfQ.delivery;
        //            break;
        //        default:
        //            selected = false;
        //            break;
        //    }

        //    if (selected)
        //    {
        //        List<BO.QuadocopterToList> l = new List<BO.QuadocopterToList>();
        //        foreach (BO.QuadocopterToList ql in myCollection)
        //        {
        //            if (ql.mode == mode)
        //                l.Add(ql);
        //        }
        //        myCollection.Clear();
        //        foreach (BO.QuadocopterToList ql in l)
        //            myCollection.Add(ql);
        //    }

        }

    }
}
