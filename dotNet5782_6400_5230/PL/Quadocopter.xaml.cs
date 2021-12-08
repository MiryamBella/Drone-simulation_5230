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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Quadocopter : Window
    {
        IBL.BO.Quadocopter newQ = new IBL.BO.Quadocopter();
        public Quadocopter(IBL.IBL ibl) //for adding quadocopter
        {
            InitializeComponent();
            
            Title = "add a quadocopter";
            showID.Visibility = Visibility.Hidden;
        }
        public Quadocopter(IBL.IBL ibl, IBL.BO.Quadocopter q)//for view of quadocopter
        {
            InitializeComponent();
            Title = "quadocopter";
            enterID.Visibility = Visibility.Hidden;
            showID.Text =  q.ID.ToString();
        }

        private void writedID(object sender, RoutedEventArgs e)
        {
            checkID.Visibility = Visibility.Visible;
            if (enterID.Text.Length == 9) checkID.Visibility = Visibility.Hidden;
            newQ.ID = int.Parse(enterID.Text);
        }
        //id, moodle, weight, battery, mode, package and location
    }
}
