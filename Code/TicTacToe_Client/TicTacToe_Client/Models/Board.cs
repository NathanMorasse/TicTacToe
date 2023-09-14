using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Moves[move.coordX, move.coordY] = move;
            LastMove = move;
        }

        public void SwitchMove()
        {

        }

        public void IsWinner()
        {

        }
    }
}
