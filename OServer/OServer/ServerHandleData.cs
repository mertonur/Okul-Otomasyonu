using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    static class ServerHandleData
    {
        public delegate void Packet(string connectionID, byte[] data);
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

        public static void InitializePackets()
        {
            //paketler yüklenicek
            packets.Add((int)ClientPackets.CMerhabaServer, DataReceiver.HandleMerhabaServer);

            packets.Add((int)ClientPackets.CLoginGiris, DataReceiver.HandleLoginGiris);

            packets.Add((int)ClientPackets.CChat, DataReceiver.HandleChat);


            packets.Add((int)ClientPackets.CListele, DataReceiver.HandleListele);
            packets.Add((int)ClientPackets.CKullaniciSil, DataReceiver.HandleKullaniciSil);
            packets.Add((int)ClientPackets.CKullaniciEkle, DataReceiver.HandleKullaniciEkle);
            packets.Add((int)ClientPackets.CListele2, DataReceiver.HandleListele2);
            packets.Add((int)ClientPackets.CAtamaCikarma, DataReceiver.HandleAtamaCikarma);
            packets.Add((int)ClientPackets.CListele3, DataReceiver.HandleListele3);
            packets.Add((int)ClientPackets.COgretmenPuan, DataReceiver.HandleOgretmenPuan);

        }

        public static void HandleData(string connectionID, byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLength = 0;
            if (Sabitler.bagli_client[connectionID].buffer == null)
                Sabitler.bagli_client[connectionID].buffer = new M_ByteBuffer();


            Sabitler.bagli_client[connectionID].buffer.Bytes_Yaz(buffer);
            if (Sabitler.bagli_client[connectionID].buffer.Count() == 0)
            {
                Sabitler.bagli_client[connectionID].buffer.Clear();
                return;
            }
            if (Sabitler.bagli_client[connectionID].buffer.Length() >= 4)
            {
                pLength = Sabitler.bagli_client[connectionID].buffer.Int_Oku(false);
                if (pLength <= 0)
                {
                    Sabitler.bagli_client[connectionID].buffer.Clear();
                    return;
                }
            }
            while (pLength > 0 & pLength <= Sabitler.bagli_client[connectionID].buffer.Length() - 4)
            {
                if (pLength <= Sabitler.bagli_client[connectionID].buffer.Length() - 4)
                {

                    Sabitler.bagli_client[connectionID].buffer.Int_Oku();
                    data = Sabitler.bagli_client[connectionID].buffer.Bytes_Oku(pLength);
                    HandleDataPackets(connectionID, data);

                }
                pLength = 0;
                if (Sabitler.bagli_client[connectionID].buffer.Length() >= 4)
                {
                    pLength = Sabitler.bagli_client[connectionID].buffer.Int_Oku(false);
                    if (pLength <= 0)
                    {
                        Sabitler.bagli_client[connectionID].buffer.Clear();
                        return;
                    }
                }

                if (pLength <= 1)
                {
                    Sabitler.bagli_client[connectionID].buffer.Clear();
                }

            }

        }


        private static void HandleDataPackets(string connectionID, byte[] data)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            buffer.Dispose();
            if (packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke(connectionID, data);
            }
        }






    }
}

