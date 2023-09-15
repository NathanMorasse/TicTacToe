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

namespace TicTacToe_Client.Models
{
    internal class Board
    {
        Move[,] Moves { get; set; }
        Move LastMove { get; set; }

        public Board()
        {
            Moves = new Move[3, 3];
        }

        public void SaveNewMove(Move move)
        {
            Moves[move.coordX - 1, move.coordY - 1] = move;
            LastMove = move;
        }

        public void DeleteMove()
        {
            Moves[LastMove.coordX - 1, LastMove.coordY - 1].IsServerMove = true;
        }

        public string IsWinner()
        {
            string winnerString = ValidateBoardLine();
            if(winnerString != "aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardColumn();

            if(winnerString != "aucun")
            {
                return winnerString;
            }

            winnerString = ValidateBoardDiagonals();

            if(winnerString != "aucun")
            {
                return winnerString;
            }

            return "aucun";
        }

        private string ValidateBoardLine()
        {
            string winner = "aucun";
            for(int x = 0; x < 3 ; x++)
            {
                if (Moves[x,0] == Moves[x,1] && Moves[x,1] == Moves[x, 2])
                {
                    if (Moves[x, 0].IsServerMove)
                    {
                        winner = "server";
                        break;
                    }
                    else
                    {
                        winner = "client";
                        break;
                    }
                }
            }
            return winner;
        }

        private string ValidateBoardColumn()
        {
            string winner = "aucun";
            for(int y = 0; y < 3 ; y++)
            {
                if (Moves[0,y] == Moves[1,y] && Moves[1,y] == Moves[2, y])
                {
                    if (Moves[0, y].IsServerMove)
                    {
                        winner = "server";
                    }
                    else
                    {
                        winner = "client";
                    }
                }
            }
            return winner;
        }

        private string ValidateBoardDiagonals()
        {
            string winnerString = "aucun";
            if (Moves[0,0] == Moves[1,1] && Moves[1,1] == Moves[3, 3]
                || Moves[1,3] == Moves[1,1] && Moves[1,1] == Moves[0,3])
            {
                if (Moves[1, 1].IsServerMove)
                {
                    winnerString = "server";
                }
                else
                {
                    winnerString = "client";
                }
            }

            return winnerString;
        }
    }
}
