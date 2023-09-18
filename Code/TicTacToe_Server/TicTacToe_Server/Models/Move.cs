using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class Move
    {
        public bool IsClientMove { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool PossibleWin { get; set; }

        public Move(bool isClientMove, int coordinateX, int coordinateY)
        {
            IsClientMove = isClientMove;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }

        public Move() { }


    }
}
