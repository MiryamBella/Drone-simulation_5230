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
using IBL;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ListOfQ : Window
    {
        IBL.IBL bL;
        int numOf_q;
        private List<myData> myCollection = new List<myData>();
        public ListOfQ(IBL.IBL ibl)
        {
            InitializeComponent();
            //bL = new BL();
            //bL = ibl;
            numOf_q = 0;
            DataContext = myCollection;
            myCollection.Add(new myData { id = 1 });
            for (int i = 0; i < 10; ++i)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = "Quadocopter " + (i + 1).ToString();
                q_list.Items.Add(newItem);

            }

        }

        private void addNew_Q(object sender, RoutedEventArgs e)
        {
            //new Quadocopter(bL).ShowDialog();
            //ListBoxItem newItem = new ListBoxItem();
            //newItem.Content = "Quadocopter " + numOf_q.ToString()+':';
            //q_list.Items.Add(newItem);
            //q_list
            //data
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
    }

    public class myData
    {
        public int id;
    }
}
