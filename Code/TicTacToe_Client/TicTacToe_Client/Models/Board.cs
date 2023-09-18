using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe_Client.ViewModels;

namespace TicTacToe_Client.Models
{
    public class Board
    {
        public Move[,] Moves { get; set; }
        public Move LastMove { get; set; }

        public Board()
        {
            Moves = new Move[3, 3];
        }

        public void SaveNewMove(Move move)
        {
            Moves[move.coordX, move.coordY] = move;
            LastMove = move;
            object moveCase = ViewLink.GamePage.FindName("C" + move.coordX + "" + move.coordY);
            Button moveButton = moveCase as Button;
            if (move.IsClientMove)
            {
                moveButton.Content = "X"; 
                moveButton.Foreground = new SolidColorBrush(Color.FromRgb(64, 172, 226));
            }
            else
            {
                moveButton.Content = "O";
                moveButton.Foreground = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            }
        }

        public void DeleteMove()
        {
            Moves[LastMove.coordX, LastMove.coordY].IsClientMove = false;
        }

        public string IsWinner()
        {
            string winnerString = ValidateBoardLine();
            bool Tie = true;
            if (winnerString != "Aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardColumn();

            if(winnerString != "Aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardDiagonals();

            if(winnerString != "Aucun")
            {
                return winnerString;
            }

            foreach(Move move in Moves)
            {
                if(move == null)
                {
                    Tie = false;
                }
            }

            if (Tie)
            {
                SocketManager.SendMessage(new Message("Tied"));
                Game.EndGame("Tied");
                return "Tied";
            }
            return "Aucun";
        }

        private string ValidateBoardLine()
        {
            string winner = "Aucun";
            int y = 1;

            for (int x = 0; x < 3; x++)
            {
                if (Moves[x, y - 1] != null && Moves[x, y] != null && Moves[x, y+1] != null)
                {
                    if (Moves[x, y -1].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x, y + 1].IsClientMove)
                    {
                        if (Moves[x, y].IsClientMove)
                        {
                            return "Client";
                        }
                        else
                        {
                            return "serveur";
                        }
                    }
                    else
                    {
                        winner = "Aucun";
                    }
                }
            }
            return winner;
        }

        private string ValidateBoardColumn()
        {
            string winner = "Aucun";
            int x = 1;

            for (int y = 0; y < 3; y++)
            {
                if (Moves[x - 1, y] != null && Moves[x, y] != null && Moves[x + 1, y] != null)
                {
                    if(Moves[x - 1, y].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x + 1, y].IsClientMove)
                    {
                        if (Moves[x, y].IsClientMove)
                        {
                            return "Client";
                        }
                        else
                        {
                            return "serveur";
                        }
                    }
                }
                else
                {
                    winner = "Aucun";
                }
            }
            return winner;
        }

        private string ValidateBoardDiagonals()
        {
            string winnerString = "Aucun";
            int x = 1;
            int y = 1;

            if (Moves[x - 1, y - 1] != null && Moves[x, y] != null && Moves[x + 1, y + 1] != null)
            {
                if (Moves[x - 1, y - 1].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x + 1, y + 1].IsClientMove)
                {
                    if (Moves[x, y].IsClientMove)
                    {
                        return "Client";
                    }
                    else
                    {
                        return "serveur";
                    }
                }
                else
                {
                    winnerString = "Aucun";
                }
            }
            else if (Moves[x - 1, y + 1] != null && Moves[x, y] != null && Moves[x + 1, y - 1] != null)
            {
                if (Moves[x - 1, y + 1].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x + 1, y - 1].IsClientMove)
                {
                    if (Moves[x, y].IsClientMove)
                    {
                        return "Client";
                    }
                    else
                    {
                        return "serveur";
                    }
                }
                else
                {
                    winnerString = "Aucun";
                }
            }
            else
            {
                winnerString = "Aucun";
            }

                return winnerString;
        }
    }
}
