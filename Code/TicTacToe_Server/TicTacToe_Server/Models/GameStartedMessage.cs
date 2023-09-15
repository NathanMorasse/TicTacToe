using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public class GameStartedMessage
    {
        public string Message { get; set; }
        public bool IsClientTurn { get; set; }

        public GameStartedMessage(string message, bool isClientTurn)
        {
            Message = message;
            IsClientTurn = isClientTurn;
        }
    }
}
