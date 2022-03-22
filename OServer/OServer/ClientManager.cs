using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    static class ClientManager
    {
        public static void CreateNewConnection(TcpClient tempClient)
        {

            bool var = false;
            Client newClient = new Client();
            newClient.socket = tempClient;
            //newClient.connectionID = (((IPEndPoint)tempClient.Client.RemoteEndPoint).Port).ToString();

            Random rastgele = new Random();
            int id = rastgele.Next(1, 999999999);
            while (var == false)
            {
                id = rastgele.Next(1, 999999999);

                newClient.connectionID = id.ToString();

                try
                {
                    if ((Sabitler.bagli_client[newClient.connectionID].connectionID) == "-1") { }
                    Yazi.Log_Yaz("Varmış " + id.ToString());
                }
                catch (System.Collections.Generic.KeyNotFoundException e)
                {
                    var = true;
                }
            }

            newClient.Start();
            Sabitler.bagli_client.Add(newClient.connectionID, newClient);
            Sabitler.bagli_client[newClient.connectionID].baglanti = Sabitler.MySql_Data.MySqlBaslat();
            DataSender.SendHosgeldinMesaji(newClient.connectionID);



        }


        public static void SendDataTo(string connectionID, byte[] data)  //tek kişiye bilgi
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            try { Sabitler.bagli_client[connectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null); }

            catch (System.Collections.Generic.KeyNotFoundException e) { }
            buffer.Dispose();

        }

        public static async void SendDataToInGameAll(string connectionID, byte[] data)  //Oyundaki Herkese bilgi
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            foreach (Client oyuncu in Sabitler.bagli_client.Values)
            {
                if (oyuncu != null && oyuncu.connectionID != connectionID && oyuncu.oyunda_mi)
                {
                    Sabitler.bagli_client[oyuncu.connectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
                }
            }
            await Task.Delay(20);
            buffer.Dispose();
        }

        public static void SendDataToAll(string connectionID, byte[] data)//lobi dahil herkese bilgi
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            foreach (Client oyuncu in Sabitler.bagli_client.Values)
            {
                if (oyuncu != null && oyuncu.connectionID != connectionID)
                {
                    Sabitler.bagli_client[oyuncu.connectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
                }
            }

            buffer.Dispose();

        }

        public static void SendDataToAllWithYou(string connectionID, byte[] data)//lobi dahil herkese bilgi
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            foreach (Client oyuncu in Sabitler.bagli_client.Values)
            {
                if (oyuncu != null)
                {
                    Sabitler.bagli_client[oyuncu.connectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
                }
            }

            buffer.Dispose();

        }




    }
}
