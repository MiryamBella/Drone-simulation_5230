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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IBL;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bl = new BL();
            }
            catch (IBL.BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListOfQ l = new ListOfQ(bl);
                l.Show();
            }
            catch (IBL.BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
    }
}
