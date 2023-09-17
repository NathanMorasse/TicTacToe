using System;
using System.CodeDom.Compiler;
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
using System.Windows.Threading;
using TicTacToe_Server.Models;

namespace TicTacToe_Server.Views
{
    /// <summary>
    /// Logique d'interaction pour WaitingPage.xaml
    /// </summary>
    public partial class WaitingPage : Page
    {
        public WaitingPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SocketManager.WaitingForConnection();
        }

        private void Launch_Game_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
