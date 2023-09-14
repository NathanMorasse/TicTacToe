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

namespace TicTacToe_Client.Models
{
    public static class SocketManager
    {
        public static void ConnectToServer(string IpAddress,  int Port)
        {
            if (!string.IsNullOrEmpty(IpAddress) && Port != 0)
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEp = new IPEndPoint(ipAddress, 11000);

                Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                string data = null;
                byte[] bytes = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = clientSocket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    string connectionResponse = data.Trim();

                    if (connectionResponse != null)
                    {
                        break;
                    }

                }
            }
        }
    }
}
