using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
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
    class DataReceiver
    {
        public static void HandleHosgeldinMesaji(byte[] data)
        {

          

            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string connectionID = buffer.String_Oku();
            string msg = buffer.String_Oku();
      

            buffer.Dispose();
            Console.WriteLine("Port Numaran : " + connectionID.ToString() + "Mesaj : " + msg);
           




        }

    

     

     
      

        public static void HandleLoginGirisYap(byte[] data)
        {
            
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            int cevap = buffer.Int_Oku();
            string NickName = buffer.String_Oku();
            string Rol = buffer.String_Oku();
            buffer.Dispose();
           
            if (cevap == 1)
            {
                //DataSender.SendMerhabaServer();

                Global.kullaniciadi = NickName;
                Global.rol = Rol;

               Console.WriteLine(Global.kullaniciadi+" Giriş Başarılı - "+Global.rol+" Rolü");
                
               Global.form1.Yazi(Global.kullaniciadi + " Giriş Başarılı [" + Global.rol + "]","Yeşil");




                if (Global.rol == "Administrator")
                {
                    Global.form1.gizlilik1(true);
                    Global.form1.gizlilik2(false);
                    Global.form1.YenileFunc();
                }
                if (Global.rol == "Öğrenci")
                {
                    Global.form1.gizlilik3(true);
                    Global.form1.gizlilik2(false);
                    Global.form1.YenileFuncOgrenci();
                   
                }
                if (Global.rol == "Öğretmen")
                {
                    Global.form1.gizlilik4(true);
                    Global.form1.gizlilik2(false);
                    Global.form1.YenileFuncOgretmen();

                }

            }
            else if (cevap == 0)
            {

                Console.WriteLine("Kullanıcı Adı Yada Parola Yanlış");

                Global.form1.Yazi("Kullanıcı Adı Yada Parola Yanlış","Kırmızı");
               
                






            }
            else if (cevap == 2)
            {
                DataSender.SendMerhabaServer();
                Console.WriteLine("Kayıt Olundu Giriş Başarılı");
                Global.form1.Yazi("Kayıt Olundu Giriş Başarılı", "Kırmızı");

            }
            else if (cevap == 3)
            {

               
                Console.WriteLine("Bu Kullanıcı Adı Kullanılıyor");
                Global.form1.Yazi("Bu Kullanıcı Adı Kullanılıyor", "Kırmızı");

            }
            else if (cevap == 4)
            {
                Console.WriteLine("Bu Kullanıcı Adı Kullanılıyor");
                Global.form1.Yazi("Bu Kullanıcı Adı Kullanılıyor", "Kırmızı");


            }
            else if (cevap == 5)
            {
                Console.WriteLine(NickName+" Hesabı Başka Bir Cihazda Açık");
                Global.form1.Yazi(NickName + " Hesabı Başka Bir Cihazda Açık", "Kırmızı");



            }

           
        }




     
        public static void HandleChatCevap(byte[] data)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string mesaj = buffer.String_Oku();
            string alici = buffer.String_Oku();
            string gonderen = buffer.String_Oku();

            if (alici == Global.kullaniciadi || gonderen == Global.kullaniciadi) {
                //mesaj penceresi açık ise
               
                if (Otomasyon.sonogrencichat == alici || Otomasyon.sonogrencichat == gonderen)
                {
                    
                    if (Global.rol == "Öğrenci")
                    {
                        Global.form1.mesajpenceresitemizleogr();
                        Global.form1.ListeleGelenKutusuOgr();
                        DataSender.Listele3("MesajlarıYenile", gonderen, alici);
                    }

                    if (Global.rol == "Öğretmen")
                    {
                        Global.form1.mesajpenceresitemizleogrt();
                        Global.form1.ListeleGelenKutusuOgrt();
                        DataSender.Listele3("MesajlarıYenile", gonderen, alici);
                    }

                }
                else {
                  
                    if (Global.rol == "Öğrenci")
                    {
                       
                    Global.form1.mesajpenceresitemizleogr();
                    Global.form1.ListeleGelenKutusuOgr();

                    }

                    if (Global.rol == "Öğretmen")
                    {

                        Global.form1.mesajpenceresitemizleogrt();
                        Global.form1.ListeleGelenKutusuOgrt();

                    }
                }
               //mesaj penceresi kapalı ama mesaj geldi
            }


            buffer.Dispose();
            
        }




        public static void HandlePuanCevap(byte[] data)
        {



            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string derskodu = buffer.String_Oku();
            string alici = buffer.String_Oku();
            string gonderen = buffer.String_Oku();


            //mesaj penceresi açık ise
            if (Otomasyon.sonderskodu == derskodu)
            {


                if (Global.rol == "Öğrenci")
                {

                    DataSender.Listele2("DersKoduPuanOgrenci", derskodu);
                  
                }

                if (Global.rol == "Öğretmen")
                {
                    DataSender.Listele2("DersKoduPuanOgretmen", derskodu);
                }
            }
                
           
                //mesaj penceresi kapalı ama mesaj geldi

            

            buffer.Dispose();

        }






        public static void HandleListeCevap(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string listeisim = buffer.String_Oku();
            string tur = buffer.String_Oku();

            if(tur== "AdminKullaniciListeYenile") { Global.form1.isimekle(listeisim); }
            if (tur == "DersListeYenile") { Global.form1.dersekle(listeisim); }
            if (tur == "DersOgretmenYenile") { Global.form1.dersogretmenekle(listeisim); }
            if (tur == "DersOgrenciYenile") { Global.form1.dersogrenciekle(listeisim); }
            if (tur == "OgretmenListeYenile") { Global.form1.ogretmenlisteekle(listeisim); }
            if (tur == "OgrenciListeYenile") { Global.form1.ogrencilisteekle(listeisim); }
            if (tur == "OgrenciDersListesiYenile") { Global.form1.ogrenciderslistesiyenile(listeisim); }
            if (tur == "OgrenciOgretmenListesiYenile") { Global.form1.ogrenciogretmenlistesiyenile(listeisim); }
            if (tur == "OgrenciOgrenciListesiYenile") { Global.form1.ogrenciogrencilistesiyenile(listeisim); }
            if (tur == "MesajlarıYenile") { Global.form1.mesajlariyenileogr(listeisim); }
            if (tur == "GelenListesiYenileOgr") { Global.form1.gelenkutusuogryenile(listeisim); }

            if (tur == "OgretmenOgretmenListesiYenile") { Global.form1.ogretmenogretmenlistesiyenile(listeisim); }
            if (tur == "OgretmenOgrenciListesiYenile") { Global.form1.ogretmenogrencilistesiyenile(listeisim); }
            if (tur == "OgretmenDersListesiYenile") { Global.form1.ogretmenderslistesiyenile(listeisim); }
            if (tur == "GelenListesiYenileOgrt") { Global.form1.gelenkutusuogrtyenile(listeisim); }

            if (tur == "DersKoduPuanOgrenci") { Global.form1.progressbarpuan(Int32.Parse(listeisim)); }
            if (tur == "DersKoduPuanOgretmen") { Global.form1.progressbarpuan(Int32.Parse(listeisim));  }

            buffer.Dispose();

        }

        public static void HandleCevapForm(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string cumle = buffer.String_Oku();
            string renk = buffer.String_Oku();

            Global.form1.Yazi(cumle, renk);
           

            buffer.Dispose();

        }



    }
}
