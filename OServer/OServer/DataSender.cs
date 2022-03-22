using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    public enum ServerPackets
    {
        SHosgeldinMesaji = 1,

        SLoginGirisCevap = 6,

        SChatCevap = 8,


        SListeCevap = 11,
        SCevapForm = 12,
        SPuanCevap = 13,
    }
    static class DataSender
    {
        public static void SendHosgeldinMesaji(string connectionID)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SHosgeldinMesaji);
            buffer.String_Yaz(connectionID);       // Buraya bakk***********************************************
            buffer.String_Yaz("Merhaba Sunucuya Hoşgeldin ");

            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            // SendOyundakiler(connectionID);
            // SendKendiBilgilerimiOyuncularaGonder(connectionID);
            buffer.Dispose();
            Sabitler.bagli_client[connectionID].oyunda_mi = true;
        }



        public static void SendLoginGirisCevap(string connectionID, int cevap, string name, string rol)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SLoginGirisCevap);

            buffer.Int_Yaz(cevap);
            buffer.String_Yaz(name);
            buffer.String_Yaz(rol);
            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }



        public static void SendChatCevap(string connectionID, string mesaj, string alici, string gonderen)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SChatCevap);

            buffer.String_Yaz(mesaj);
            buffer.String_Yaz(alici);
            buffer.String_Yaz(gonderen);


            ClientManager.SendDataToAllWithYou(connectionID, buffer.ToArray());

            buffer.Dispose();

        }

        public static void SendPuanCevap(string connectionID, string derskodu, string alici, string gonderen)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SPuanCevap);

            buffer.String_Yaz(derskodu);
            buffer.String_Yaz(alici);
            buffer.String_Yaz(gonderen);


            ClientManager.SendDataToAllWithYou(connectionID, buffer.ToArray());

            buffer.Dispose();

        }


        public static void SendChatCevap2(string gonderilen, string name, string mesaj)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SChatCevap);
            buffer.String_Yaz(name);
            buffer.String_Yaz(mesaj);


            try
            {
                ClientManager.SendDataTo(Sabitler.bagli_clientName[gonderilen].connectionID, buffer.ToArray());
            }
            catch (System.Collections.Generic.KeyNotFoundException e) { }

            buffer.Dispose();

        }

        public static void SendListeCevap(string connectionID, string listeisim, string tur)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SListeCevap);
            buffer.String_Yaz(listeisim);
            buffer.String_Yaz(tur);


            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendCevapForm(string connectionID, string cumle, string renk)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SCevapForm);
            buffer.String_Yaz(cumle);
            buffer.String_Yaz(renk);


            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }


    }
}
