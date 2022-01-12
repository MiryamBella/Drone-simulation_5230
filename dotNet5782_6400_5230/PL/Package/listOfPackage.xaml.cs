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


        private void MouseDoubleClick_showP(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.PackageToList pToList = new BO.PackageToList();
                pToList = (BO.PackageToList)p_list.SelectedItem;
                if (pToList == null) return;
                BO.Package c = new BO.Package();
                c = bl.cover(pToList);
                Package cl = new Package(bl, c);
                cl.ShowDialog();
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }
        private void Button_refresh(object sender, RoutedEventArgs e)
        {
            stateOfP s = 0;
            WeighCategories w = 0;
            Priorities u = 0;
            //get the select of the state.
            if (package_state.SelectedItem != null)
            {
                string state = ((ComboBoxItem)package_state.SelectedItem).Content.ToString();
                
                if (state == "created") s = stateOfP.Defined;
                else if (state == "associated") s = stateOfP.associated;
                else if (state == "collected") s = stateOfP.collected;
                else if (state == "provided") s = stateOfP.provided;
            }
            if (package_weight.SelectedItem != null)
            {
                string state = ((ComboBoxItem)package_weight.SelectedItem).Content.ToString();
                if (state == "easy") w = WeighCategories.easy;
                else if (state == "middle") w = WeighCategories.middle;
                else if (state == "heavy") w = WeighCategories.hevy; 
            }
            if (package_urgency.SelectedItem != null)
            {
                string state = ((ComboBoxItem)package_urgency.SelectedItem).Content.ToString();
                if (state == "reggular") u = Priorities.reggular;
                else if (state == "fast") u = Priorities.fast;
                else if (state == "emergency") u = Priorities.emergency;
            }
            IEnumerable<PackageToList> list = from p in bl.ListOfPackages()
                                                     where (s == 0 || p.state == s) && (w==0 ||p.weight == w) && (u == 0 || p.priority == u) 
                                                     select p;
            myCollection.Clear();
            foreach (var p in list)
                myCollection.Add(p);
        }
    }
}

