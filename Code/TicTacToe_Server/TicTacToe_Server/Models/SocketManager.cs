using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server.Models
{
    public static class SocketManager
    {
        private static IPHostEntry host = Dns.GetHostEntry("localhost");
        private static IPAddress ip = host.AddressList[0];
        private static IPEndPoint endPoint = new IPEndPoint(ip, 11000);
        private static Socket socket;

        public static void WaitingForConnection()
        {
            try
            {
                socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(endPoint);
                socket.Listen(10);
                Socket handler = socket.Accept();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendMove(Move move)
        {

        }

        public static void NotifyClientNewGame(bool isClientTurn)
        {

        }

        public static void WaitForOpponentMove()
        {

        }

        public static void SendInvalidMoveMessage()
        {

        }

        public static void ValidateOpponentWin()
        {

        }
    }
}
