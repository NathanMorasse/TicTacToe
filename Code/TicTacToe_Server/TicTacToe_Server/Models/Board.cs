using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe_Server.ViewModels;

namespace TicTacToe_Server.Models
{
    public class Board
    {
        public Move[,] Moves { get; set; } = new Move[3,3];
        public Move LastMove { get; set; }

        public Board() { }

        public void SaveNewMove(Move move)
        {
            Moves[move.CoordinateX, move.CoordinateY] = move;
            LastMove = move;
            object moveCase = ViewLink.GamePage.FindName("C"+move.CoordinateX+""+move.CoordinateY);
            Button moveButton = moveCase as Button;
            if (move.IsClientMove)
            {
                moveButton.Content = "O";
                moveButton.Foreground = new SolidColorBrush(Color.FromRgb(239, 35, 60));
            }
            else
            {
                moveButton.Content = "X";
                moveButton.Foreground = new SolidColorBrush(Color.FromRgb(64, 172, 226));
            }
        }

        // Change le dernier move pour le move de l'adversaire quand un message de move invalide est recu.
        public void DeleteMove()
        {
            Moves[LastMove.CoordinateX, LastMove.CoordinateY].IsClientMove = true;
        }

        //Cette fonction détermine si il y a un gagnant
        //et si oui lequel sous forme de string(serveur ou client) et si non retourne "aucun".
        public string IsWinner()
        {
            string winnerString = ValidateBoardLine();
            bool Tie = true;
            if (winnerString != "Aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardColumn();

            if (winnerString != "Aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardDiagonals();

            if (winnerString != "Aucun")
            {
                return winnerString;
            }

            foreach (Move move in Moves)
            {
                if (move == null)
                {
                    Tie = false;
                }
            }

            if (Tie)
            {
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
                if (Moves[x, y - 1] != null && Moves[x, y] != null && Moves[x, y + 1] != null)
                {
                    if (Moves[x, y - 1].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x, y + 1].IsClientMove)
                    {
                        if (Moves[x, y].IsClientMove)
                        {
                            return "Client";
                        }
                        else
                        {
                            return "serveur";
                        break;
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
                    if (Moves[x - 1, y].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x + 1, y].IsClientMove)
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
                if (Moves[x - 1, y - 1].IsClientMove == Moves[x, y].IsClientMove && Moves[x, y].IsClientMove == Moves[x + 1, y - 1].IsClientMove)
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
