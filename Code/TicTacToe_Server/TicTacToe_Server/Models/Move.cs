using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class Move
    {
        public bool IsMyMoove { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string XImage { get; set; }
        public string OImage { get; set; }
        public bool PossibleWin { get; set; }

        public Move(bool isMyMoove, int coordinateX, int coordinateY, string xImage, string oImage, bool possibleWin)
        {
            IsMyMoove = isMyMoove;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            XImage = xImage;
            OImage = oImage;
            PossibleWin = possibleWin;
        }

        public Move() { }


    }
}
