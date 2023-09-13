using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class Board
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        public Move LastMove { get; set; }

        public Board() { }

        public void SaveNewMove(Move move)
        {

        }

        public void DeleteMove()
        {

        }

        public string IsWinner()
        {
            throw new NotImplementedException();
        }
    }
}
