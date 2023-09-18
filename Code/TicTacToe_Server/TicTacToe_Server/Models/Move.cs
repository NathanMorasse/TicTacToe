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
        public int coordX { get; set; }
        public int coordY { get; set; }
        public bool PossibleWin { get; set; }

        public Move(bool isClientMove, int coordinateX, int coordinateY, bool possibleWin)
        {
            IsClientMove = isClientMove;
            coordX = coordinateX;
            coordY = coordinateY;
            PossibleWin = possibleWin;

        }

        public Move() { }


    }
}
