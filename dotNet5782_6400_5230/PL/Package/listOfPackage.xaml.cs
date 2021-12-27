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
using BO;
using BlApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListOfPackage.xaml
    /// </summary>
    public partial class ListOfPackage : Window
    {
        BlApi.IBL bl;
        private ObservableCollection<BO.PackageToList> myCollection
            = new ObservableCollection<BO.PackageToList>();
        public ListOfPackage(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;

            foreach (BO.PackageToList p in bl.ListOfPackages())
                myCollection.Add(p);
            p_list.ItemsSource = myCollection;
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_addNewP(object sender, RoutedEventArgs e)
        {
            Package p = new Package(bl);
            this.Close();
            p.ShowDialog();
        }

        public void addPfromWindowP(BO.PackageToList p)
        {
            myCollection.Add(p);
        }


        private void MouseDoubleClick_showP(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.PackageToList pToList = (BO.PackageToList)p_list.SelectedItem;
                BO.Package c = bl.cover(pToList);
                Package cl = new Package(bl, c);
                cl.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }
    }
}

