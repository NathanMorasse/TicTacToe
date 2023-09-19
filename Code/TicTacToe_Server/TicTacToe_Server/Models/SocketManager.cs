using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TicTacToe_Server.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace TicTacToe_Server.Models
{
    public static class SocketManager
    {
        private static IPAddress ip = IPAddress.Any;
        private static IPEndPoint endPoint = new IPEndPoint(ip, 11000);
        private static Socket socket;
        private static Socket handler;

        public async static void WaitingForConnection()
        {
            try
            {
                socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socket.Bind(endPoint);
                socket.Listen(10);
                handler = await socket.AcceptAsync();

                ViewLink.WaitingPage.Message_TextBlock.Text = "Un joueur s'est connecté.";
                ViewLink.WaitingPage.Status_Image.Source = new BitmapImage(new Uri("../Img/Connected.png", UriKind.Relative));
                ViewLink.WaitingPage.Waiting_TextBlock.Text = "Vous pouvez lancer la partie";
                ViewLink.WaitingPage.Launch_Game.IsEnabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion impossible: \n" + e.Message);
                WaitingForConnection();
            }
        }

        private static void CreateSocket()
        {
            socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(endPoint);
            WaitingForConnection();
        }

        public static void NotifyClientNewGame(bool isClientTurn)
        {
            Message message = new Message("GameStarted", isClientTurn);
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
            handler.Send(messageBytes);
        }

        public async static void WaitForOpponentMessage()
        {
            string data = null;
            byte[] bytes = null;

            try
            {
                while (true)
                {
                    var buffer = new byte[1_024];
                    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    data += Encoding.UTF8.GetString(buffer, 0, received);
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
                    case "InvalidMove":
                        Game.CurrentBoard.DeleteMove();
                        break;
                    case "ValidateWin":
                        Game.EndGame("Win");
                        break;
                    case "Tied":
                        Game.EndGame("Tie");
                        break;
                    case "Redo!":
                        Game.StartNewGame();
                        break;
                case "Quitted":
                    socket.Close();
                        WaitingForConnection();
                        ViewLink.NavigateToWait();
                        break;
                    default:
                        break;
                }
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void SendMove(Move move)
        {
            try
            {
                Message moveMessage = new Message("Move", move);
                string jsonMove = JsonConvert.SerializeObject(moveMessage);
                byte[] moveBytes = Encoding.UTF8.GetBytes(jsonMove);
                handler.Send(moveBytes);
                WaitForOpponentMessage();
            }
            catch(SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void SendInvalidMoveMessage()
        {
            try
            {
                Message message = new Message("InvalidMove");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
                handler.Send(messageBytes);
                WaitForOpponentMessage();
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void ValidateOpponentWin()
        {
            try
            {
                Message message = new Message("ValidateWin");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
                handler.Send(messageBytes);
                WaitForOpponentMessage();
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void SendTieMessage()
        {
            try
            {
                Message message = new Message("Tied");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
                handler.Send(messageBytes);
                WaitForOpponentMessage();
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void SendRedo()
        {
            try
            {
                Message message = new Message("Redo?");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
                handler.Send(messageBytes);
        }

        public static void SendQuit()
        {
            Message message = new Message("Quitted");
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
            handler.Send(messageBytes);

            socket.Close();

            ViewLink.NavigateToWait();

            WaitingForConnection();
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }

        public static void SendQuittingMessage()
        {
            try
            {
                Message message = new Message("Quitting");
                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
                handler.Send(messageBytes);

                WaitForOpponentMessage();
            }
            catch (SocketException Se)
            {
                MessageBox.Show(Se.Message, Se.ErrorCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                CreateSocket();
                ViewLink.NavigateToWait();
                ViewLink.WaitingPage.Launch_Game.IsEnabled = false;
            }
        }
    }
}
