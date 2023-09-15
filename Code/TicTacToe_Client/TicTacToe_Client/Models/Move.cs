using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Client.Models
{
    internal class Move
    {
        public bool IsServerMove { get; set; }
        public int coordX { get; set; }
        public int coordY { get; set; }
    }
}
