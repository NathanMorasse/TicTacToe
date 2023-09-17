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

namespace TicTacToe_Server.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame()
        {
            Random rnd = new Random();
            bool IsMyTurn = rnd.Next(0,1) == 0;

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

            switch (result)
            {
                case "Win":
                    {
                        exclamation = "Félicitation!!";
                        status = "Vous avez gagné cette partie de tic-tac-toe.";
                        ViewLink.GamePage.Reward_TextBlock.Visibility = Visibility.Hidden;
                        ViewLink.GamePage.Reward_Image.Visibility = Visibility.Visible;
                        break;
                    }
                case "Lose":
                    break;
                case "Tie":
                    break;
                default:
                    break;
            }

            ViewLink.GamePage.Exclamation_TextBlock.Text = exclamation;
            ViewLink.GamePage.Result_textBlock.Text = status;
            ViewLink.GamePage.Reward_TextBlock.Text = message;
            // Update view
        }
    }
}
