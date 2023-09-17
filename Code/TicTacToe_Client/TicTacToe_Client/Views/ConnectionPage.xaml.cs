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
using TicTacToe_Client.Models;

namespace TicTacToe_Client.Views
{
    /// <summary>
    /// Logique d'interaction pour ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Page
    {
        public ConnectionPage()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            int Port_Number = 0;
            if(!int.TryParse(Port_Input.Text, out Port_Number))
            {
                MessageBox.Show("Le port saisi est invalide", "Port Invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    SocketManager.ConnectToServer(IP_Input.Text, Port_Number);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
                
        }

        private void EnableConnect(object sender, TextChangedEventArgs e)
        {
            if ((IP_Input.Text != null && !string.IsNullOrEmpty(IP_Input.Text))
                && (Port_Input.Text != null && !string.IsNullOrEmpty(Port_Input.Text)))
            {
                Connect.IsEnabled = true;
            }
        }
    }
}
