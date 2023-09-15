using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class Board
    {
        public Move[,] Moves { get; set; } = new Move[3,3];
        public Move LastMove { get; set; }

        public Board() { }

        public void SaveNewMove(Move move)
        {
            Moves[move.CoordinateY - 1, move.CoordinateX - 1] = move;
            LastMove = move;
        }

        // Change le dernier move pour le move de l'adversaire quand un message de move invalide est recu.
        public void DeleteMove()
        {
            Moves[LastMove.CoordinateY - 1, LastMove.CoordinateX - 1].IsClientMove = true;
        }

        //Cette fonction détermine si il y a un gagnant
        //et si oui lequel sous forme de string(serveur ou client) et si non retourne "aucun".
        public string IsWinner()
        {
            //Détermine si il y a un gagnant
            bool isWinner = false;
            string winner = "Aucun";

            // Horizontals
            for (int y = 0; y < Moves.GetLength(0); y++)
            {
                if (Moves[y, 0] == Moves[y, 1] && Moves[y, 0] == Moves[y, 2])
                {
                    isWinner = true;
                    if (Moves[y,0].IsClientMove)
                    {
                        winner = "Client";
                    }
                    else
                    {
                        winner = "server";
                    }
                }
            }

            // Verticals
            if (isWinner == false)
            {
                for (int x = 0; x < Moves.GetLength(0); x++)
                {
                    if (Moves[0, x] == Moves[1, x] && Moves[0, x] == Moves[2, x])
                    {
                        isWinner = true;
                        if (Moves[0, x].IsClientMove)
                        {
                            winner = "Client";
                        }
                        else
                        {
                            winner = "Server";
                        }
                    }
                }
            }

            // Diagonals
            if (isWinner == false)
            {
                if ((Moves[0, 0] == Moves[1, 1] && Moves[0,0] == Moves[2, 2])
                    || (Moves[2, 0] == Moves[1, 1] && Moves[2, 0] == Moves[0, 2]))
                {
                    isWinner = true;
                    if (Moves[1,1].IsClientMove)
                    {
                        winner = "Client";
                    }
                    else
                    {
                        winner = "Server";
                    }
                }
            }

            foreach (Move move in Moves)
            {
                if (move == null)
                {
                    winner = "Tied";
                }
            }

            return winner;
        }
    }
}
