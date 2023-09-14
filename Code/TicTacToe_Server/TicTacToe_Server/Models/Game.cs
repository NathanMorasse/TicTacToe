using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame(bool IsClientTurn)
        {

        }

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
        }

        public static void NextTurn()
        {

        }
    }
}
