using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

                Message message = new Message("Connexion réussie");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage+"<EOF>");
                handler.Send(messageBytes);

                //Connected need to update page
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion impossible: \n" + e.Message);
            }
        }

        public static void SendMove(Move move)
        {
            Message moveMessage = new Message("Move", move);
            string jsonMove = JsonConvert.SerializeObject(moveMessage);
            byte[] moveBytes = Encoding.UTF8.GetBytes(jsonMove + "<EOF>");
            handler.Send(moveBytes);
        }

        public static void NotifyClientNewGame(bool isClientTurn)
        {
            Message message = new Message("GameStarted", isClientTurn);
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage + "<EOF>");
            handler.Send(messageBytes);
        }

        public static void WaitForOpponentMessage()
        {
            string data = null;
            byte[] bytes = null;

            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                string response = data.Trim();

                if (response != null)
                {
                    break;
                }
            }
            Message messageRecu = JsonConvert.DeserializeObject<Message>(data);

            switch (messageRecu.Type)
            {
                case "Move":
                    Game.ValidateMove(messageRecu.MoveMessage);
                    break;
                default:
                    break;
            }
        }

        public static void SendInvalidMoveMessage()
        {

        }

        public static void ValidateOpponentWin()
        {

        }

        public static void SendTieMessage()
        {

        }
    }
}
