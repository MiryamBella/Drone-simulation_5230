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
    /// Interaction logic for Package.xaml
    /// </summary>
    public partial class Package : Window
    {
        BlApi.IBL bl;
        public Package(BlApi.IBL ibl)
        {
            InitializeComponent();
            bl = ibl;
        }
    }
}
