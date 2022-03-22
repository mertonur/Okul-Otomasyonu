using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class ClientTCP
    {

        private static TcpClient clientSocket;
        private static NetworkStream myStream;
        private static byte[] recBuffer;



        public static void InitializingNetworking()
        {
            clientSocket = new TcpClient();
            clientSocket.ReceiveBufferSize = 4096;
            clientSocket.SendBufferSize = 4096;
            recBuffer = new byte[4096 * 2];

            string ipAdresi = DisplayIPAddresses();

            //otomasyon server ip
            string Serverİp = "";
            clientSocket.BeginConnect(Serverİp, 6060, new AsyncCallback(ClientConnectCallback), clientSocket);



           
        }

        public static string DisplayIPAddresses()
        {
            string returnAddress = String.Empty;

            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection)
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {
                // Read the IP configuration for each network
                IPInterfaceProperties properties = network.GetIPProperties();

                if (network.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                       network.OperationalStatus == OperationalStatus.Up &&
                       !network.Description.ToLower().Contains("virtual") &&
                       !network.Description.ToLower().Contains("pseudo"))
                {
                    // Each network interface may have multiple IP addresses
                    foreach (IPAddressInformation address in properties.UnicastAddresses)
                    {
                        // We're only interested in IPv4 addresses for now
                        if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                            continue;

                        // Ignore loopback addresses (e.g., 127.0.0.1)
                        if (IPAddress.IsLoopback(address.Address))
                            continue;

                        returnAddress = address.Address.ToString();
                        Console.WriteLine(address.Address.ToString() + " (" + network.Name + " - " + network.Description + ")");
                    }
                }
            }

            return returnAddress;
        }


        private static void ClientConnectCallback(IAsyncResult result)
        {
            try
            {
                clientSocket.EndConnect(result);
                
            }
            catch (Exception e) {
                Global.form1.Yazi("Sunucu Kapalı Bağlantı Kurulamadı","Kırmızı");
            }
            if (clientSocket.Connected == false)
            {

                return;
            }
            else
            {
                

                clientSocket.NoDelay = true;
                myStream = clientSocket.GetStream();

                myStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallback, null);
            }

        }

        private static void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int length = myStream.EndRead(result);
                if (length <= 0)
                    return;

                byte[] newBytes = new byte[length];
                Array.Copy(recBuffer, newBytes, length);
             
                    ClientHandleData.HandleData(newBytes);
          
                myStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallback, null);
            }
            catch (Exception)
            {
                Disconnect();
                return;
            }
        }

        public static void SendData(byte[] data)
        {
            if (myStream == null)
            {
             
            }
            else
            {
                ByteBuffer buffer = new ByteBuffer();
                buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
                buffer.Bytes_Yaz(data);
                if (myStream == null) { }
               
                    myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
                
                buffer.Dispose();
            }


        }

        public static void Disconnect()
        {
            clientSocket.Close();
           
        }






    }
}
