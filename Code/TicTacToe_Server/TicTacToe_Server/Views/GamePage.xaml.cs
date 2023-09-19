using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe_Server.Models;
using TicTacToe_Server.ViewModels;

namespace TicTacToe_Server.Views
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public Button selected = null;

        public GamePage()
        {
            InitializeComponent();
        }

        private void Case_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = ((Button)sender);

            if (clicked.Content != "X" || clicked.Content != "O")
            {
                if (selected != null)
                {
                    selected.Background = Brushes.Transparent;
                }

                clicked.Background = new SolidColorBrush(Color.FromRgb(210, 255, 137));
                selected = clicked;
                if (Game.IsMyTurn)
                {
                    Confirm_Move.IsEnabled = true;
                }
            }
        }

        private void Confirm_Move_Click(object sender, RoutedEventArgs e)
        {
            selected.Background = Brushes.Transparent;
            int x = int.Parse(selected.Name[1].ToString());
            int y = int.Parse(selected.Name[2].ToString());
            Move move = new Move(false, x, y, false);
            Game.ValidateMove(move);
        }

        private void Restart_Game_Click(object sender, RoutedEventArgs e)
        {
            SocketManager.SendRedo();
            ViewLink.ToggleRestartButton();
        }

        private void Quit_Game_Click(object sender, RoutedEventArgs e)
        {
            SocketManager.SendQuit();
        }

        //private void Restart_Game_Click(object sender, RoutedEventArgs e)
        //{
        //    SocketManager.SendRedo();
        //}
    }
}
