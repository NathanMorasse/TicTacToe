using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Client.Models
{
    public class Message
    {
        public string Type { get; set; }
        public bool Isvalid { get; set; }
        public bool IsClientTurn { get; set; }
        public Move MoveToSend { get; set; } = null;

        public Message() { }

        public Message(string type)
        {
            Type = type;
        }

        public Message(string type, bool IsClientTurn)
        {
            Type = type;
            this.IsClientTurn = IsClientTurn;
        }

        public Message(string type, Move move)
        {
            Type = type;
            MoveToSend = move;
        }
    }
}
