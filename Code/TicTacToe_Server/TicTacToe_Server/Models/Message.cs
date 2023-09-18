using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class Message
    {
        public string Type { get; set; }
        public bool IsClientTurn { get; set; }
        public Move MoveMessage { get; set; } = null;

        public Message() { }
        public Message(string type)
        {
            Type = type;
        }

        public Message(string type, bool isClientTurn)
        {
            Type = type;
            IsClientTurn = isClientTurn;
        }

        public Message(string type, Move moveMesssage)
        {
            Type = type;
            MoveMessage = moveMesssage;
        }
    }
}
