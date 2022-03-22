using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    class ServerTCP
    {



        //IPAddress.Any
        static TcpListener serverSocket = new TcpListener(IPAddress.Any, 6060);
        public static void InitializeNetwork()
        {



            Yazi.Log_Yaz("Paketleriniz Başlatılıyor");
            ServerHandleData.InitializePackets();
            serverSocket.Start();
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnet), null);

        }

        private static void OnClientConnet(IAsyncResult result)
        {

            TcpClient client = serverSocket.EndAcceptTcpClient(result);
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnet), null);
            ClientManager.CreateNewConnection(client);
        }
    }
}
