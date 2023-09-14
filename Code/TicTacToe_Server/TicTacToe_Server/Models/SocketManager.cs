using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace TicTacToe_Server.Models
{
    public static class SocketManager
    {
        //private static IPHostEntry host = Dns.GetHostEntry("localhost");
        //private static IPAddress ip = host.AddressList[1];
        private static IPAddress ip = IPAddress.Parse("10.99.62.143");
        private static IPEndPoint endPoint = new IPEndPoint(ip, 11000);
        private static Socket socket;
        private static Socket handler;

        public static void WaitingForConnection()
        {
            try
            {
                socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(endPoint);
                socket.Listen(10);
                handler = socket.Accept();

                string confirmationMessage = "Connexion établie";
                byte[] confirmationBytes = Encoding.UTF8.GetBytes(confirmationMessage);
                handler.Send(confirmationBytes);

                //Connected need to update page
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion impossible: \n" + e.Message);
            }
        }

        public static void SendMove(Move move)
        {
            string jsonMove = JsonConvert.SerializeObject(move);
            byte[] moveBytes = Encoding.UTF8.GetBytes(jsonMove);
            handler.Send(moveBytes);
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
