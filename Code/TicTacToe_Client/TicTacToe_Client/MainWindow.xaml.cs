using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using TicTacToe_Client.Models;

namespace TicTacToe_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectToServer(object sender, RoutedEventArgs e)
        {
            if (addressIp.Text != null && Port.Text != null)
            {
                
                int PortTransform;
                int.TryParse(Port.Text, out PortTransform);
                SocketManager.ConnectToServer(addressIp.Text, PortTransform);
            }
        }
    }
}
