using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using TicTacToe_Client.ViewModels;

namespace TicTacToe_Client.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame(bool IsClientTurn)
        {
            CurrentBoard = new Board();
            IsMyTurn = IsClientTurn;

            ViewLink.UpdateTurn();

            ViewLink.PageHolder.SwitchToGamePage();

            if (!IsMyTurn)
            {
                SocketManager.WaitForOpponentMessage();
            }
        }
        
        public static bool ValidateMove (Move move)
        {
            if (move.IsClientMove != IsMyTurn)
            {
                return false;
            }
            else if (move.coordY < 0 || move.coordX > 2)
            {
                return false;
            }
            else if (move.coordY < 0 || move.coordY > 2)
            {
                return false;
            }else if (CurrentBoard.Moves[move.coordX, move.coordY] != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void NextTurn()
        {
            IsMyTurn = !IsMyTurn;
            ViewLink.UpdateTurn();
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
                        status = "Vous avez perdu cette partie de tic-tac-toe.";
                        message = "Meilleur chance la prochaine fois!";
                        color = new SolidColorBrush(Color.FromRgb(198, 198, 198));
                        ViewLink.SwitchReward(false);
                        break;
                    }

                case "Tied":
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
