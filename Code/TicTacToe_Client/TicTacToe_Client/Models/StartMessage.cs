using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Client.Models
{
    public class StartMessage
    {
        public string Message { get; set; }
        public bool IsClientTurn { get; set; }

        public StartMessage() { }

        public StartMessage(string message, bool isClientTurn)
        {
            Message = message;
            IsClientTurn = isClientTurn;
        }
    }
}
