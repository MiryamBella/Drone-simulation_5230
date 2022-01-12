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
using BO;
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
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_addNewC(object sender, RoutedEventArgs e)
        {
            Client c = new Client(bl);
            c.ShowDialog();

            //reset the list.
            myCollection.Clear();
            foreach (BO.ClientToList cl in bl.ListOfClients())
                myCollection.Add(cl);

        }


        private void MouseDoubleClick_showC(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.ClientToList cToList = (BO.ClientToList)c_list.SelectedItem;
                BO.Client c = bl.cover(cToList);
                Client cl = new Client(bl, c);
                cl.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }
    }
}
