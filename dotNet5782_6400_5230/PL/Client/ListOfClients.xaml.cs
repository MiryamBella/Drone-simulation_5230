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
    /// Interaction logic for ListOfClients.xaml
    /// </summary>
    public partial class ListOfClients : Window
    {
        BlApi.IBL bl;
        private ObservableCollection<BO.ClientToList> myCollection
            = new ObservableCollection<BO.ClientToList>();
        public ListOfClients(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;

            foreach (BO.ClientToList c in bl.ListOfClients())
                myCollection.Add(c);
            c_list.ItemsSource = myCollection;
            //ComboBoxItem w = new ComboBoxItem();
            //Quadocopter_whait.SelectedItem = Quadocopter_whait.Items.n;
            //w.ContentStringFormat ="none";
            //ComboBoxItem m = (ComboBoxItem)Quadocopter_mode.SelectedItem;
            //m.Content = "none";
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_addNewC(object sender, RoutedEventArgs e)
        {
            Client c = new Client(bl);
            this.Close();
            c.ShowDialog();
        }

        public void addQfromWindowQ(BO.ClientToList c)
        {
            myCollection.Add(c);
        }


        private void MouseDoubleClick_showC(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.ClientToList c = (BO.ClientToList)c_list.SelectedItem;
                Client cl = new Client(bl, c);
                cl.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }

        private void Button_refresh(object sender, RoutedEventArgs e)
        {
            /*  -----------------------the order of the program:---------------------------------------
            1)if the weigh select:
                    clean the list and select acording to the weigh.
                    A)if the mode select:
                            clean the list ans select acording to the weigh in the past our list.
                    B)if the mode didnt select:
                            do nothing becose we all redy cleen the list to the original list.
            2)if the weigh didnt select:
                    clean the list and add all the q in bl list.(so if there was selectation in the past it will be removed)
                    A)if the mode select:
                            clean the list ans select acording to the weigh in the past our list.
                    B)if the mode didnt select:
                            do nothing becose we all redy cleen the list to the original list.
             */
            //get the select of the weigh.
            ComboBoxItem t = (ComboBoxItem)Client_to.SelectedItem;
            bool tSelected = true;
            //if t=null this mean the user didnt select something.
            if (t != null && t.Name != "none")
                tSelected = true;
            else
            {
                tSelected = false;
                myCollection.Clear();
                foreach (BO.ClientToList c in bl.ListOfClients())
                    myCollection.Add(c);
            }

            if (tSelected)
            {
                try
                {
                    List<BO.ClientToList> l = bl.ListOfc_of_to(t.Content.ToString());
                    myCollection.Clear();
                    foreach (BO.ClientToList c in l)
                        myCollection.Add(c);
                }
                catch (BO.BLException ex)
                {
                    MessageBox.Show("Error! " + ex.Message);
                }
            }

            //get the select of the mode.
            ComboBoxItem f = (ComboBoxItem)Client_from.SelectedItem;
            bool fSelected;
            //if f=null this mean the user didnt select something.
            if (f != null && f.Name != "none")
                fSelected = true;
            else
            {
                fSelected = false;
                myCollection.Clear();
                foreach (BO.ClientToList c in bl.ListOfClients())
                    myCollection.Add(c);
            }

            if (fSelected)
            {
                try
                {
                    List<BO.ClientToList> l = bl.ListOfc_of_from(f.Content.ToString());
                    myCollection.Clear();
                    foreach (BO.ClientToList c in l)
                        myCollection.Add(c);
                }
                catch (BO.BLException ex)
                {
                    MessageBox.Show("Error! " + ex.Message);
                }
            }

        }
    }
}
