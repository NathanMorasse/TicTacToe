using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace TicTacToe_Client.Models
{
    public static class SocketManager
    {
        private static IPEndPoint remoteEp;
        private static Socket clientSocket;
        private static IPHostEntry host;
        private static IPAddress ipAddress;

        public static void ConnectToServer(string IpAddress,  int Port)
        {
            if (!string.IsNullOrEmpty(IpAddress) && Port != 0)
            {
                clientSocket = CreateSocket(IpAddress, Port);
                clientSocket.Connect(remoteEp);

                //string data = null;
                //byte[] bytes = null;
                //while (true)
                //{
                //    bytes = new byte[1024];
                //    int bytesRec = clientSocket.Receive(bytes);
                //    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //    string connectionResponse = data.Trim();

                //    if (connectionResponse != null)
                //    {
                //        break;
                //    }

                //}
            }
        }

        private static Socket CreateSocket(string IpAddress, int Port)
        {
            host = Dns.GetHostEntry(IpAddress);
            ipAddress = host.AddressList[0];
            remoteEp = new IPEndPoint(ipAddress, Port);

            Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            return clientSocket;
        }

        public static void SendMessage(Message message)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(message);
                byte[] moveMsg = Encoding.ASCII.GetBytes(Json);
                int bytesSent = clientSocket.Send(moveMsg);
                WaitForOpponentMove();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public static void WaitForOpponentMove()
        {
            while (true)
            {
                byte[] bytes = null;
                string data = null;
                Message message;
                bytes = new byte[1024];
                int bytesRec = clientSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                message = JsonConvert.DeserializeObject<Message>(data);
                if(message != null)
                {
                    //Move 
                    if (message.Type == "Move")
                    {
                        if (Game.ValidateMove(message.MoveToSend))
                        {
                            if(message.MoveToSend.PossibleWin)
                            {
                                if(Game.CurrentBoard.IsWinner() == "serveur")
                                {
                                    Game.EndGame("Lose");
                                    SocketManager.SendMessage(new Message("ValidateWin"));
                                }
                            }
                            Game.CurrentBoard.SaveNewMove(message.MoveToSend);
                        }
                        else
                        {
                            SendMessage(new Message("InvalidMove"));
                        }
                    }
                    //Partie Commencée
                    else if (message.Type == "GameStarted")
                    {
                        Game.StartNewGame(message.IsClientTurn);
                    }
                    //Move Invalide
                    else if (message.Type == "InvalidMove")
                    {
                        Game.CurrentBoard.DeleteMove();
                    }
                    //Validation Gagnant
                    else if (message.Type == "ValidateWin")
                    {
                        Game.EndGame("Win");
                    }
                    //Égalitée
                    else if (message.Type == "Tied")
                    {
                        Game.EndGame("Tie");
                    }
                }
            }
        }
    }
}
