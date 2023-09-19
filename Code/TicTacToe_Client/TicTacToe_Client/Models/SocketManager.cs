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
using TicTacToe_Client.ViewModels;
using System.Windows;

namespace TicTacToe_Client.Models
{
    public static class SocketManager
    {
        private static IPEndPoint remoteEp;
        private static Socket clientSocket;
        private static IPAddress ipAddress;

        public static bool ConnectToServer(string IpAddress,  int Port)
        {
            if (!string.IsNullOrEmpty(IpAddress) && Port != 0)
            {
                clientSocket = CreateSocket(IpAddress, Port);
                clientSocket.Connect(remoteEp);
            }
            WaitForOpponentMessage();
            return clientSocket.Connected;
        }

        private static Socket CreateSocket(string IpAddress, int Port)
        {
            /*if(IpAddress != "127.0.0.1")
            {
                ipAddress = parseIpAddress;// (Dns.GetHostEntry(IpAddress).AddressList[0]);
            }
            else
            {
                ipAddress = IPAddress.Parse(IpAddress);
            }*/

            ipAddress = IPAddress.Parse(IpAddress);

            remoteEp = new IPEndPoint(ipAddress, Port);
            Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            return clientSocket;
        }

        public static async void SendMessage(Message message)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(message);
                byte[] moveMsg = Encoding.ASCII.GetBytes(Json);
                int bytesSent = await clientSocket.SendAsync(moveMsg, SocketFlags.None);
                WaitForOpponentMessage();
            }catch(SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                ViewLink.NavigateToConnection();
            }
        }

        public static async void WaitForOpponentMessage()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = null;
                    string data = null;
                    Message message;
                    bytes = new byte[1_024];
                    int bytesRec = await clientSocket.ReceiveAsync(bytes, SocketFlags.None);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    message = JsonConvert.DeserializeObject<Message>(data);
                    if (message != null)
                    {
                        //Move 
                        if (message.Type == "Move")
                        {
                            if (Game.ValidateMove(message.MoveMessage))
                            {
                                Game.CurrentBoard.SaveNewMove(message.MoveMessage);
                                if (message.MoveMessage.PossibleWin)
                                {
                                    if (Game.CurrentBoard.IsWinner() == "serveur")
                                    {
                                        Game.EndGame("Lose");
                                        SendMessage(new Message("ValidateWin"));
                                    }
                                }
                                Game.NextTurn();
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
                            Game.EndGame("Tied");
                        }
                        //Invitation de relancement de partie
                        else if (message.Type == "Redo?")
                        {
                            ViewLink.ToggleRestartButton();
                        }
                        else if (message.Type == "Quitted")
                        {
                            clientSocket.Close();
                            ViewLink.NavigateToConnection();
                        }

                        break;
                    }
                }
            }
            catch(SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                ViewLink.NavigateToConnection();
            }
        }

        public static void SendRedo()
        {
            Message message = new Message("Redo!");

            try
            {
                string Json = JsonConvert.SerializeObject(message);
                byte[] moveMsg = Encoding.ASCII.GetBytes(Json);
                int bytesSent = clientSocket.Send(moveMsg);

                ViewLink.ResetGamePage();
                ViewLink.NavigateToGame();

                WaitForOpponentMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendQuit()
        {
            Message message = new Message("Quitted");

            try
            {
                string Json = JsonConvert.SerializeObject(message);
                byte[] moveMsg = Encoding.ASCII.GetBytes(Json);
                int bytesSent = clientSocket.Send(moveMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }

            clientSocket.Close();

            ViewLink.NavigateToConnection();
        }

        public static void SendQuittingMessage()
        {
            Message message = new Message("Quitting");
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
            clientSocket.Send(messageBytes);
        }
    }
}
