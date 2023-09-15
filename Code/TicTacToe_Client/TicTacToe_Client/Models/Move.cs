using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Client.Models
{
    public class Move
    {
        public bool IsServerMove { get; set; }
        public int coordX { get; set; }
        public int coordY { get; set; }
        public bool PossibleWin { get; set; }

        public Move(bool isServerMove, int coordX, int coordY, bool possibleWin)
        {
            IsServerMove = isServerMove;
            this.coordX = coordX;
            this.coordY = coordY;
            PossibleWin = possibleWin;
        }
    }
}
