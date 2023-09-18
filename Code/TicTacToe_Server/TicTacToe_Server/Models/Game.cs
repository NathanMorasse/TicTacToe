using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TicTacToe_Server.ViewModels;
using TicTacToe_Server.Views;
using System.Windows.Media;

namespace TicTacToe_Server.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame()
        {
            Random rnd = new Random();
            bool IsMyTurn = rnd.Next(0,2) == 0;

            CurrentBoard = new Board();

            SocketManager.NotifyClientNewGame(!IsMyTurn);

            ViewLink.NavigateToGame();
        }

        /// <summary>
        /// Cette fonction recoit des moves du serveur et du client et dois les valider et décider quelle action faire selon le résultat
        /// </summary>
        /// <param name="move"></param>
        public static void ValidateMove(Move move)
        {
            bool valid = true;

            if (move.IsClientMove == IsMyTurn)
            {
                valid = false;
            }

            if ( move.CoordinateX < 1 || move.CoordinateX > 3)
            {
                valid = false;
            }

            if (move.CoordinateY < 1 || move.CoordinateY > 3)
            {
                valid = false;
            }

            if (valid)
            {
                CurrentBoard.SaveNewMove(move);
                string winner = CurrentBoard.IsWinner();
                if (IsMyTurn)
                {
                    switch (winner)
                    {
                        case "Server":
                            move.PossibleWin = true;
                            Game.NextTurn();
                            SocketManager.SendMove(move);
                            SocketManager.WaitForOpponentMessage();
                            break;
                        case "Tied":
                            SocketManager.SendTieMessage();
                            EndGame("Tied");
                            break;
                        case "Aucun":
                            SocketManager.SendMove(move);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (winner == "Client")
                    {
                        SocketManager.ValidateOpponentWin();
                        EndGame("Lost");
                    }
                }
            }
            else
            {
                if (IsMyTurn)
                {
                    MessageBox.Show("Coup invalide");
                }
                else
                {
                    SocketManager.SendInvalidMoveMessage();
                }
            }
        }

        public static void NextTurn()
        {
            IsMyTurn = !IsMyTurn;

            //update button
        }

        public static void EndGame(string result)
        {
            ViewLink.GamePage.Ingame_Layout.Visibility = Visibility.Hidden;
            ViewLink.GamePage.Finished_Layout.Visibility = Visibility.Visible;

            string exclamation = string.Empty;
            string status = string.Empty;
            string message = string.Empty;
            SolidColorBrush color = Brushes.Transparent;

            switch (result)
            {
                case "Win":
                    {
                        exclamation = "Félicitation!!";
                        status = "Vous avez gagné cette partie de tic-tac-toe.";
                        color = new SolidColorBrush(Color.FromRgb(210, 255, 137));
                        ViewLink.SwitchReward(true);
                        break;
                    }

                case "Lose":
                    {
                        exclamation = "Dommage!!";
                        status = "Vaous avez perdu cette partie de tic-tac-toe.";
                        message = "Meilleur chance la prochaine fois!";
                        color = new SolidColorBrush(Color.FromRgb(198, 198, 198));
                        ViewLink.SwitchReward(false);
                        break;
                    }

                case "Tie":
                    {
                        exclamation = "Bravo!!";
                        status = "La partie s'est terminée sur une égalité.";
                        message = "Vous l'aurez la prochaine fois!";
                        color = new SolidColorBrush(Color.FromRgb(198, 198, 198));
                        ViewLink.SwitchReward(false);
                        break;
                    }

                default:
                    break;
            }

            ViewLink.ApplyResult(exclamation, status, message, color);
        }
    }
}
