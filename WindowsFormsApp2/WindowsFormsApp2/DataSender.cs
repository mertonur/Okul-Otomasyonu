using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public enum ClientPackets
    {
        CMerhabaServer = 1,
        
        CLoginGiris = 3,

        

        CChat = 5,

       
        CListele = 7,
        CKullaniciSil=8,
        CKullaniciEkle = 9,
        CListele2 = 10,
        CAtamaCikarma=11,
        CListele3= 12,

        COgretmenPuan=13,
    }
    class DataSender
    {

        public static void SendMerhabaServer()
        {

            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CMerhabaServer);
            buffer.String_Yaz("Oyuncu Giriş Yaptı");
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }
       

        public static void SendLoginGiris(string kullanici_adi, string sifre, string ip, int yontem)
        {

            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CLoginGiris);
            buffer.String_Yaz(kullanici_adi);
            buffer.String_Yaz(sifre);
            buffer.String_Yaz(ip);
            buffer.Int_Yaz(yontem);
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }
     
        public static void SendChatMesaj(string mesaj,string alici,string gonderen)
        {

          

            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CChat);
            
            buffer.String_Yaz(mesaj);
            buffer.String_Yaz(alici);
            buffer.String_Yaz(gonderen);

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendOgretmenPuan(int puan, string alici, string gonderen,string derskodu)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.COgretmenPuan);

            buffer.Int_Yaz(puan);
            buffer.String_Yaz(alici);
            buffer.String_Yaz(gonderen);
            buffer.String_Yaz(derskodu);

            ClientTCP.SendData(buffer.ToArray());

            buffer.Dispose();
        }

        public static void Listele(string tur)
        {

          

            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CListele);
            buffer.String_Yaz(tur);
            

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

        public static void KullaniciSil(string isim)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CKullaniciSil);
            buffer.String_Yaz(isim);


            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

        public static void KullaniciEkle(string isim,string sifre,string rol)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CKullaniciEkle);
            buffer.String_Yaz(isim);
            buffer.String_Yaz(sifre);
            buffer.String_Yaz(rol);

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

        public static void Listele2(string tur,string derskodu)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CListele2);
            buffer.String_Yaz(tur);
            buffer.String_Yaz(derskodu);

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

        public static void AtamaCikarma(string isim, string derskodu,string tur, string tur2)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CAtamaCikarma);
            buffer.String_Yaz(isim);
            buffer.String_Yaz(derskodu);
            buffer.String_Yaz(tur);
            buffer.String_Yaz(tur2);

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

        public static void Listele3(string tur, string isim,string derskodu)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ClientPackets.CListele3);
            buffer.String_Yaz(tur);
            buffer.String_Yaz(isim);
            buffer.String_Yaz(derskodu);

            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();

        }

    }
}
