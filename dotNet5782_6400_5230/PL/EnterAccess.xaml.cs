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
    /// Interaction logic for EnterAccess.xaml
    /// </summary>
    public partial class EnterAccess : Window
    {
        BlApi.IBL bl;
        string user;
        public EnterAccess(BlApi.IBL ibl, string str)
        {
            InitializeComponent();
            bl = ibl;
            user = str;
            if (str == "manager")
            {
                asks.Text = "enter code access: (this is 1234)";
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user == "client")
                {
                    if (enterAccass.Text == null)
                    {
                        MessageBox.Show("you didnt enter any ID:");
                        return;
                    }
                    int id;
                    if (!(int.TryParse(enterAccass.Text, out id)))
                    {
                        MessageBox.Show("invalid ID:");
                        return;
                    }
                    id = int.Parse(enterAccass.Text);
                    Client c = new Client(bl);
                    bool exist = false;
                    foreach (BO.ClientToList cl in bl.ListOfClients())
                        if (cl.ID == id)
                        {
                            c = new Client(bl, bl.cover(cl));
                            exist = true;
                            break;
                        }
                    if (exist)
                    {
                        c.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("ERROR: the ID not exist in our data.");
                }
                else
                {
                    if (enterAccass.Text == null)
                    {
                        MessageBox.Show("you didnt enter any ID:");
                        return;
                    }
                    int code;
                    if (!(int.TryParse(enterAccass.Text, out code)))
                    {
                        MessageBox.Show("invalid ID:");
                        return;
                    }
                    code = int.Parse(enterAccass.Text);
                    if (code != 1234)
                        throw new Exception("This code not corect.");
                    Manager m = new Manager(bl);
                    m.Show();
                    this.Close();
                }
            }
            catch (BO.BLException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }

        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
