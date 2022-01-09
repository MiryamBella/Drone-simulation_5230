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
using System.Threading;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorQuadocopter.xaml
    /// </summary>
    public partial class SimulatorQuadocopter : Window
    {

        BlApi.IBL bl;
        BO.Quadocopter localQ = new BO.Quadocopter();
        private Thread simulatorQ;
        bool simulatorIsRun;


        public SimulatorQuadocopter(IBL ibl, BO.Quadocopter q)
        {
            InitializeComponent();
            bl = ibl;
            localQ = q;
        }

        void simulatorBgine()
        {
            Simulator sim = new Simulator(1);
            simulatorQ = new Thread(sim.startSimulator);
            simulatorQ.Start();

        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
