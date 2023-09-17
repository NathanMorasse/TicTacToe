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
            bool noTie = true;
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
                    noTie = false;
                }
            }

            if (noTie)
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
                        winner = "Client";
                        break;
                    }
                }
            }
            return winner;
        }

        private string ValidateBoardColumn()
        {
            string winner = "Aucun";
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
                        winner = "Client";
                    }
                }
            }
            return winner;
        }

        private string ValidateBoardDiagonals()
        {
            string winnerString = "Aucun";
            if (Moves[0,0] == Moves[1,1] && Moves[1,1] == Moves[3, 3]
                || Moves[1,3] == Moves[1,1] && Moves[1,1] == Moves[0,3])
            {
                if (Moves[1, 1].IsServerMove)
                {
                    winnerString = "server";
                }
                else
                {
                    winnerString = "Client";
                }
            }

            return winnerString;
        }
    }
}
