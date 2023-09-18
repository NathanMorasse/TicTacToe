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
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Button selected = null;
        int CoordX;
        int CoordY;

        public GamePage()
        {
            InitializeComponent();
        }

        private void Case_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = ((Button)sender);
            CoordX = Convert.ToInt32(clicked.Name[1].ToString());
            CoordY = Convert.ToInt32(clicked.Name[2].ToString());

            if (clicked.Content != "X" || clicked.Content != "O")
            {
                if (selected != null)
                {
                    selected.Background = Brushes.Transparent;
                }

                clicked.Background = new SolidColorBrush(Color.FromRgb(210,255,137));
                selected = clicked;
                Confirm_Move.IsEnabled = true;
            }
        }

        private void Confirm_Move_Click(object sender, RoutedEventArgs e)
        {
            selected.Background = Brushes.Transparent;
            Move move = new Move(true, CoordX, CoordY, false);
            if (Game.ValidateMove(move))
            {
                Game.CurrentBoard.SaveNewMove(move);
                if(Game.CurrentBoard.IsWinner() == "client")
                {
                    move.PossibleWin = true;
                }
                SocketManager.SendMessage(new Message("Move", move));
            }
            else
            {
                MessageBox.Show("Votre Tour est invalide. Veuillez recommencer", "Move Invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
