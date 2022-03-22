using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class ClientHandleData
    {


        public delegate void Packet(byte[] data);
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();
        public static ByteBuffer playerBuffer;

        public static void InitializePackets()
        {
            //paketler yüklenicek
            packets.Add((int)ServerPackets.SHosgeldinMesaji, DataReceiver.HandleHosgeldinMesaji);



            packets.Add((int)ServerPackets.SLoginGirisCevap, DataReceiver.HandleLoginGirisYap);


            packets.Add((int)ServerPackets.SChatCevap, DataReceiver.HandleChatCevap);


            packets.Add((int)ServerPackets.SListeCevap, DataReceiver.HandleListeCevap);
            packets.Add((int)ServerPackets.SCevapForm, DataReceiver.HandleCevapForm);
            packets.Add((int)ServerPackets.SPuanCevap, DataReceiver.HandlePuanCevap);
            
        }

        public static void HandleData(byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLength = 0;
            if (playerBuffer == null)
                playerBuffer = new ByteBuffer();


            playerBuffer.Bytes_Yaz(buffer);
            if (playerBuffer.Count() == 0)
            {
                playerBuffer.Clear();
                return;
            }
            if (playerBuffer.Length() >= 4)
            {
                pLength = playerBuffer.Int_Oku(false);
                if (pLength <= 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }
            while (pLength > 0 & pLength <= playerBuffer.Length() - 4)
            {
                if (pLength <= playerBuffer.Length() - 4)
                {

                    playerBuffer.Int_Oku();
                    data = playerBuffer.Bytes_Oku(pLength);
                    HandleDataPackets(data);

                }
                pLength = 0;
                if (playerBuffer.Length() >= 4)
                {
                    pLength = playerBuffer.Int_Oku(false);
                    if (pLength <= 0)
                    {
                        playerBuffer.Clear();
                        return;
                    }
                }

                if (pLength <= 1)
                {
                    playerBuffer.Clear();
                }

            }

        }


        private static void HandleDataPackets(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            buffer.Dispose();
            if (packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke(data);
            }
        }








    }
}
