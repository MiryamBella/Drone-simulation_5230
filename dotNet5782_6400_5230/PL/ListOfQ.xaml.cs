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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ListOfQ : Window
    {
        IBL.IBL bl;
        private ObservableCollection<IBL.BO.QuadocopterToList> myCollection 
            = new ObservableCollection<IBL.BO.QuadocopterToList>();
        public ListOfQ(IBL.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;

            //DataContext = myCollection;
            foreach(IBL.BO.QuadocopterToList ql in bl.ListOfQ())
                myCollection.Add(ql);
            q_list.ItemsSource = myCollection;
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_addNewQ(object sender, RoutedEventArgs e)
        {
            Quadocopter q = new Quadocopter(bl);
            q.ShowDialog();
        }

        public void addQfromWindowQ(IBL.BO.QuadocopterToList qua)
        {
            myCollection.Add(qua);
        }


        private void MouseDoubleClick_showQ(object sender, MouseButtonEventArgs e)
        {
            Quadocopter q = new Quadocopter(bl, myCollection[1]);
            q.ShowDialog();
        }

        private void Button_raanen(object sender, RoutedEventArgs e)
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
            IBL.BO.WeighCategories weigh = new IBL.BO.WeighCategories();
            ComboBoxItem w =  (ComboBoxItem)Quadocopter_whait.SelectedItem;
            bool selected = true;
            switch (w.Content.ToString())
            {
                case "easy":
                    weigh = IBL.BO.WeighCategories.easy;
                    break;
                case "hevy":
                    weigh = IBL.BO.WeighCategories.hevy;
                    break;
                case "middle":
                    weigh = IBL.BO.WeighCategories.middle;
                    break;
                default:
                    selected = false;
                    myCollection.Clear();
                    foreach (IBL.BO.QuadocopterToList ql in bl.ListOfQ())
                        myCollection.Add(ql);
                    break;
            }

            if (selected)
            {
                List<IBL.BO.QuadocopterToList> l = new List<IBL.BO.QuadocopterToList>();
                foreach(IBL.BO.QuadocopterToList ql in bl.ListOfQ()){
                    if((int)ql.weight <= (int)weigh)
                        l.Add(ql);
                }
                myCollection.Clear();
                foreach (IBL.BO.QuadocopterToList ql in l)
                    myCollection.Add(ql);

            }

            IBL.BO.statusOfQ mode = new IBL.BO.statusOfQ();
            ComboBoxItem m = (ComboBoxItem)Quadocopter_whait.SelectedItem;
            selected = true;
            switch (m.Content.ToString())
            {
                case "available":
                    mode = IBL.BO.statusOfQ.available;
                    break;
                case "maintenance":
                    mode = IBL.BO.statusOfQ.maintenance;
                    break;
                case "delivery":
                    mode = IBL.BO.statusOfQ.delivery;
                    break;
                default:
                    selected = false;
                    break;
            }

            if (selected)
            {
                List<IBL.BO.QuadocopterToList> l = new List<IBL.BO.QuadocopterToList>();
                foreach (IBL.BO.QuadocopterToList ql in myCollection)
                {
                    if (ql.mode == mode)
                        l.Add(ql);
                }
                myCollection.Clear();
                foreach (IBL.BO.QuadocopterToList ql in l)
                    myCollection.Add(ql);
            }

        }
    }

    //public class myData
    //{
    //    public int id;
    //    public override string ToString()
    //    {
    //        return ("my Id: " + id.ToString());
    //    }
    //}
}
