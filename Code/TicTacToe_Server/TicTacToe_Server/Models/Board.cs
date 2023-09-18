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
            //Détermine si il y a un gagnant
            bool isWinner = false;
            string winner = "Aucun";
            bool Tied = true;

            // Horizontals
            for (int x = 0; x < 3; x++)
            {
                if ((Moves[x,0] != null) && Moves[x, 0].IsClientMove == Moves[x, 1].IsClientMove && Moves[x, 1].IsClientMove == Moves[x, 2].IsClientMove)
                {
                    if (Moves[x, 0].IsClientMove)
                    {
                        winner = "Client";
                        break;
                    }
                    else
                    {
                        winner = "server";
                        break;
                    }
                }
            }

            // Verticals
            if (isWinner == false)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (Moves[0, y].IsClientMove == Moves[1, y].IsClientMove && Moves[1, y].IsClientMove == Moves[2, y].IsClientMove)
                    {
                        if (Moves[0, y].IsClientMove)
                        {
                            winner = "Client";
                        }
                        else
                        {
                            winner = "server";
                        }
                    }
                }
            }

            // Diagonals
            if (isWinner == false)
            {
                if (Moves[0, 0].IsClientMove == Moves[1, 1].IsClientMove && Moves[1, 1].IsClientMove == Moves[3, 3].IsClientMove
                    || Moves[1, 3].IsClientMove == Moves[1, 1].IsClientMove && Moves[1, 1].IsClientMove == Moves[0, 3].IsClientMove)
                {
                    if (Moves[1, 1].IsClientMove)
                    {
                        winner = "Client";
                    }
                    else
                    {
                        winner = "server";
                    }
                }
            }

            foreach (Move move in Moves)
            {
                if (move == null)
                {
                    Tied = false;
                }
            }

            if (Tied)
            {
                winner = "Tied";
            }

            return winner;
        }
    }
}
