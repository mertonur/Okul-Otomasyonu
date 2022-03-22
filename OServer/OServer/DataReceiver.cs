using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    public enum ClientPackets
    {
        CMerhabaServer = 1,

        CLoginGiris = 3,

        CChat = 5,


        CListele = 7,
        CKullaniciSil = 8,
        CKullaniciEkle = 9,
        CListele2 = 10,
        CAtamaCikarma = 11,
        CListele3 = 12,
        COgretmenPuan = 13,
    }
    static class DataReceiver
    {
        public static void HandleMerhabaServer(string connectionID, byte[] data)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string msg = buffer.String_Oku();
            buffer.Dispose();

            Yazi.Gelen_Mesaj(connectionID + "  " + msg);
            // DataSender.SendHosgeldinMesaji(connectionID);

        }


        public static void HandleLoginGiris(string connectionID, byte[] data)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string kullanici_adi = buffer.String_Oku();
            string sifre = buffer.String_Oku();
            string ip = buffer.String_Oku();
            int yontem = buffer.Int_Oku();
            if (yontem == 1)
            {
                string rol = (GirisDogruMu(connectionID, kullanici_adi, sifre));


                if (rol != "Yanlış")
                {



                    try
                    {
                        if ((Sabitler.bagli_clientName[kullanici_adi].connectionID) == "-1") { }
                        //Yazi.Log_Yaz("İsim Kullanılıyor");

                        DataSender.SendLoginGirisCevap(connectionID, 5, kullanici_adi, "Yetkisiz");

                    }
                    catch (System.Collections.Generic.KeyNotFoundException e)
                    {

                        Sabitler.bagli_client[connectionID].OyuncuAdi = kullanici_adi;
                        Sabitler.bagli_clientName.Add(kullanici_adi, Sabitler.bagli_client[connectionID]);
                        Yazi.Log_Yaz(Sabitler.bagli_clientName[kullanici_adi].connectionID);
                        Sabitler.bagli_client[connectionID].girisyapti = true;

                        Sabitler.oyuncu_ciktiName(connectionID);

                        Sabitler.oyuncu_baglandi(kullanici_adi);
                        DataSender.SendLoginGirisCevap(connectionID, 1, kullanici_adi, rol);

                    }




                }
                else
                {

                    DataSender.SendLoginGirisCevap(connectionID, 0, kullanici_adi, rol);

                }
            }


            buffer.Dispose();
        }

        public static string GirisDogruMu(string connectionID, string kullanici_adi, string sifre)
        {

            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
            string SqlCommand = "SELECT * FROM `kullanicilar` WHERE 1";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

            MySqlDataReader sonuc = guncelle.ExecuteReader();
            while (sonuc.Read())
            {

                if (kullanici_adi == (string)sonuc["kullanici"])
                {
                    if (sifre == (string)sonuc["sifre"])
                    {
                        string cikanrol = (string)sonuc["rol"];
                        sonuc.Close();
                        return cikanrol;
                    }
                    else
                    {
                        sonuc.Close();
                        return "Yanlış";
                    }
                }
            }

            sonuc.Close();
            return "Yanlış";


        }
        public static bool MySqlKayıtOlabilir(string connectionID, string kullanici_adi)
        {
            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
            string SqlCommand = "SELECT * FROM `tum_kullanicilar` WHERE 1";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

            MySqlDataReader sonuc = guncelle.ExecuteReader();
            while (sonuc.Read())
            {
                if (kullanici_adi == (string)sonuc["kullanici_adi"])
                {
                    sonuc.Close();
                    return false;

                }
            }

            sonuc.Close();
            return true;


        }
        public static void MySqlKayıt1(string connectionID, string kullanici_adi, string sifre, string ip)
        {
            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
            string SqlCommand = "INSERT INTO `tum_kullanicilar` (`id`, `kullanici_adi`, `sifre`, `ip`) VALUES (NULL, '" + kullanici_adi + "', '" + sifre + "', '" + ip + "');";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);
            if (guncelle != null)
            {
                guncelle.ExecuteNonQuery();
                DataSender.SendLoginGirisCevap(connectionID, 2, kullanici_adi, "Kayıt");


            }
            else
            {
                DataSender.SendLoginGirisCevap(connectionID, 4, kullanici_adi, "Kayıt");
            }
        }









        public static void HandleChat(string connectionID, byte[] data)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();

            string mesaj = buffer.String_Oku();
            string alici = buffer.String_Oku();
            string gonderen = buffer.String_Oku();
            buffer.Dispose();



            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


            string SqlCommand = "INSERT INTO `mesajlar`( `gonderen`, `alici`, `mesaj`, `okundu`, `tarih`) VALUES ('" + gonderen + "','" + alici + "','" + mesaj + "','0',DATE_SUB(NOW(), INTERVAL -1 HOUR))";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

            if (guncelle != null)
            {

                if (guncelle.ExecuteNonQuery() >= 0)
                {
                    //DataSender.SendCevapForm(connectionID, alici + "  Mesaj Gönderildi", "Yeşil");
                    // DataSender.SendChatCevap(connectionID, Name, mesaj);
                    DataSender.SendChatCevap(connectionID, mesaj, alici, gonderen);
                }
                else { DataSender.SendCevapForm(connectionID, alici + " İsmine Mesaj Gönderilemedi", "Kırmızı"); }

            }
            else { DataSender.SendCevapForm(connectionID, alici + " İsmine Mesaj Gönderilemedi", "Kırmızı"); }


        }


        public static void HandleOgretmenPuan(string connectionID, byte[] data)
        {
            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();

            int puan = buffer.Int_Oku();
            string alici = buffer.String_Oku();
            string gonderen = buffer.String_Oku();
            string derskodu = buffer.String_Oku();
            buffer.Dispose();

            int asama = 1;

            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
            string SqlCommand = "SELECT * FROM `puanlama` WHERE `gonderen`='" + gonderen + "' AND `alici`='" + alici + "' AND `derskodu`='" + derskodu + "'";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

            MySqlDataReader sonuc = guncelle.ExecuteReader();

            while (sonuc.Read())
            {
                if ((string)sonuc["gonderen"] == gonderen)
                {
                    asama = 0;
                    DataSender.SendCevapForm(connectionID, alici + " Daha Önce Puanlanmış", "Kırmızı");
                }

            }


            sonuc.Close();

            if (asama == 1)
            {
                baglanti = Sabitler.bagli_client[connectionID].baglanti;


                SqlCommand = "INSERT INTO `puanlama`( `gonderen`, `alici`, `derskodu`, `puan`) VALUES ('" + gonderen + "','" + alici + "','" + derskodu + "','" + puan + "')";

                guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {

                    if (guncelle.ExecuteNonQuery() >= 0)
                    {
                        DataSender.SendCevapForm(connectionID, alici + " İsmine Puan Verildi", "Yeşil");
                        DataSender.SendPuanCevap(connectionID, derskodu, alici, gonderen);
                    }
                    else { DataSender.SendCevapForm(connectionID, alici + " İsmine Puan Verilemedi", "Kırmızı"); }

                }
                else { DataSender.SendCevapForm(connectionID, alici + " İsmine Puan Verilemedi", "Kırmızı"); }
            }

        }



        public static void HandleListele(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string tur = buffer.String_Oku();



            ArrayList isimler = new ArrayList();


            buffer.Dispose();


            if (tur == "AdminKullaniciListeYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `kullanicilar` WHERE 1 ORDER BY `kullanicilar`.`id` ASC";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["kullanici"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "AdminKullaniciListeYenile");



                }
            }


            if (tur == "DersListeYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE 1 ORDER BY `dersler`.`id` ASC";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    if ((string)sonuc["ogretmen"] != "Tanımsız") { isimler.Add("✔️ " + sonuc["derskodu"]); }
                    else { isimler.Add("❌ " + sonuc["derskodu"]); }

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "DersListeYenile");



                }
            }

            if (tur == "OgretmenListeYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `kullanicilar` WHERE `rol`='Öğretmen' ORDER BY `kullanicilar`.`id` ASC";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["kullanici"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgretmenListeYenile");



                }
            }

            if (tur == "OgrenciListeYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `kullanicilar` WHERE `rol`='Öğrenci' ORDER BY `kullanicilar`.`id` ASC";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["kullanici"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgrenciListeYenile");



                }
            }





        }

        public static void HandleKullaniciSil(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string isim = buffer.String_Oku();






            buffer.Dispose();



            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
            if (isim != "Admin")
            {

                string SqlCommand = "DELETE FROM `kullanicilar` WHERE `kullanici`= '" + isim + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {

                    if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Başarıyla Silindi", "Gri"); }
                    else { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Silinemedi", "Kırmızı"); }


                }

                SqlCommand = "DELETE FROM `ogrenciler` WHERE `ogrenci`= '" + isim + "'";

                guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {

                    if (guncelle.ExecuteNonQuery() >= 0) { }
                    else { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısının Ders Kaydı Silinemedi", "Kırmızı"); }


                }

                SqlCommand = "UPDATE `dersler` SET `ogretmen`='Tanımsız' WHERE `ogretmen`='" + isim + "'";

                guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {

                    if (guncelle.ExecuteNonQuery() >= 0) { }
                    else { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısının Ders Kaydı Silinemedi-2", "Kırmızı"); }


                }

            }
            else
            {

                DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Silinemez", "Kırmızı");
            }






        }



        public static void HandleKullaniciEkle(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string isim = buffer.String_Oku();
            string sifre = buffer.String_Oku();
            string rol = buffer.String_Oku();





            buffer.Dispose();



            MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


            string SqlCommand = "INSERT INTO `kullanicilar`(`kullanici`, `sifre`, `rol`) VALUES ('" + isim + "','" + sifre + "','" + rol + "')";

            MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

            if (guncelle != null)
            {
                try
                {
                    if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Başarıyla Eklendi", "Yeşil"); }
                    else { DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Eklenemedi", "Kırmızı"); }
                }
                catch (Exception e)
                {
                    DataSender.SendCevapForm(connectionID, isim + " Kullanıcısı Eklenemez", "Kırmızı");
                }

            }









        }

        public static void HandleListele2(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string tur = buffer.String_Oku();
            string derskodu = buffer.String_Oku();



            ArrayList isimler = new ArrayList();


            buffer.Dispose();





            if (tur == "DersOgretmenYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE `derskodu`='" + derskodu + "' ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    isimler.Add(sonuc["ogretmen"]);
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "DersOgretmenYenile");



                }
            }


            if (tur == "DersOgrenciYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "' ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    isimler.Add(sonuc["ogrenci"]);
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "DersOgrenciYenile");



                }
            }

            if (tur == "OgrenciDersListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `ogrenciler` WHERE `ogrenci`='" + derskodu + "' ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    isimler.Add(sonuc["derskodu"]);
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgrenciDersListesiYenile");



                }
            }


            if (tur == "OgretmenDersListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE `ogretmen`='" + derskodu + "' ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    isimler.Add(sonuc["derskodu"]);
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgretmenDersListesiYenile");



                }
            }

            if (tur == "DersKoduPuanOgrenci")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `puanlama` WHERE `derskodu`='" + derskodu + "' ";

                int toplam = 0;
                int kisi = 0;
                int sonuct = 0;

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    kisi += 1;
                    toplam += (int)sonuc["puan"];

                }

                sonuc.Close();
                if (kisi != 0) { sonuct = (int)toplam / kisi; }


                DataSender.SendListeCevap(connectionID, sonuct.ToString(), "DersKoduPuanOgrenci");




            }


            if (tur == "DersKoduPuanOgretmen")
            {

                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `puanlama` WHERE `derskodu`='" + derskodu + "' ";

                int toplam = 0;
                int kisi = 0;
                int sonuct = 0;

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    kisi += 1;
                    toplam += (int)sonuc["puan"];

                }

                sonuc.Close();

                if (kisi != 0) { sonuct = (int)toplam / kisi; }

                DataSender.SendListeCevap(connectionID, sonuct.ToString(), "DersKoduPuanOgretmen");
            }

            if (tur == "GelenListesiYenileOgr")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `mesajlar` WHERE `gonderen`='" + derskodu + "' OR `alici`='" + derskodu + "' ORDER BY `mesajlar`.`tarih` DESC ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    if (!isimler.Contains(sonuc["gonderen"]))
                    {
                        if (derskodu != sonuc["gonderen"])
                        {

                            isimler.Add(sonuc["gonderen"]);
                        }
                    }
                    if (!isimler.Contains(sonuc["alici"]))
                    {
                        if (derskodu != sonuc["alici"])
                        {

                            isimler.Add(sonuc["alici"]);
                        }
                    }
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "GelenListesiYenileOgr");



                }
            }



            if (tur == "GelenListesiYenileOgrt")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `mesajlar` WHERE `gonderen`='" + derskodu + "' OR `alici`='" + derskodu + "' ORDER BY `mesajlar`.`tarih` DESC ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    if (!isimler.Contains(sonuc["gonderen"]))
                    {
                        if (derskodu != sonuc["gonderen"])
                        {

                            isimler.Add(sonuc["gonderen"]);
                        }
                    }
                    if (!isimler.Contains(sonuc["alici"]))
                    {
                        if (derskodu != sonuc["alici"])
                        {

                            isimler.Add(sonuc["alici"]);
                        }
                    }
                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "GelenListesiYenileOgrt");



                }
            }



        }




        public static void HandleAtamaCikarma(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string isim = buffer.String_Oku();
            string derskodu = buffer.String_Oku();
            string tur = buffer.String_Oku();
            string tur2 = buffer.String_Oku();




            buffer.Dispose();


            if (tur == "Öğretmen")
            {
                if (tur2 == "Atama")
                {
                    MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


                    string SqlCommand = "UPDATE `dersler` SET `ogretmen`='" + isim + "' WHERE `derskodu`='" + derskodu + "'";

                    MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                    if (guncelle != null)
                    {
                        try
                        {
                            if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Dersine " + isim + " Atandı", "Yeşil"); }
                            else { DataSender.SendCevapForm(connectionID, "Atama Gerçekleşmedi", "Kırmızı"); }
                        }
                        catch (Exception e)
                        {
                            DataSender.SendCevapForm(connectionID, "Atama Gerçekleşmedi", "Kırmızı");
                        }

                    }
                }
                if (tur2 == "Geri Çek")
                {
                    MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


                    string SqlCommand = "UPDATE `dersler` SET `ogretmen`='Tanımsız' WHERE `derskodu`='" + derskodu + "'";

                    MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                    if (guncelle != null)
                    {
                        try
                        {
                            if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Öğretmeni Tanımsız Atandı", "Yeşil"); }
                            else { DataSender.SendCevapForm(connectionID, "Geri Çek Gerçekleşmedi", "Kırmızı"); }
                        }
                        catch (Exception e)
                        {
                            DataSender.SendCevapForm(connectionID, "Geri Çek Gerçekleşmedi", "Kırmızı");
                        }

                    }
                }
            }



            if (tur == "Öğrenci")
            {
                if (tur2 == "Atama")
                {
                    int asama = 1;
                    MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                    string SqlCommand = "SELECT * FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "'";

                    MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                    MySqlDataReader sonuc = guncelle.ExecuteReader();
                    while (sonuc.Read())
                    {

                        if (isim == (string)sonuc["ogrenci"])
                        {

                            asama = 0;
                            DataSender.SendCevapForm(connectionID, isim + " Bu Derse Daha Önce Atanmış", "Kırmızı");
                        }
                    }

                    sonuc.Close();

                    if (asama == 1)
                    {
                        baglanti = Sabitler.bagli_client[connectionID].baglanti;




                        SqlCommand = "INSERT INTO `ogrenciler`( `derskodu`, `ogrenci`) VALUES ('" + derskodu + "','" + isim + "')";

                        guncelle = new MySqlCommand(SqlCommand, baglanti);

                        if (guncelle != null)
                        {
                            try
                            {
                                if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Dersine " + isim + " Atandı", "Yeşil"); }
                                else
                                {
                                    DataSender.SendCevapForm(connectionID, " Atama Gerçekleşmedi", "Kırmızı");

                                }
                            }
                            catch (Exception e)
                            {
                                DataSender.SendCevapForm(connectionID, " Atama Gerçekleşmedi", "Kırmızı");

                            }

                        }
                    }
                }
                if (tur2 == "Geri Çek")
                {
                    MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


                    string SqlCommand = "DELETE FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "' AND `ogrenci`='" + isim + "'";

                    MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                    if (guncelle != null)
                    {
                        try
                        {
                            if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, isim + " Başarıyla Geri Çekildi", "Yeşil"); }
                            else { DataSender.SendCevapForm(connectionID, " Geri Çek Gerçekleşmedi", "Kırmızı"); }
                        }
                        catch (Exception e)
                        {
                            DataSender.SendCevapForm(connectionID, " Geri Çek Gerçekleşmedi", "Kırmızı");
                        }

                    }



                }
            }

            if (tur == "Ders Ekle")
            {
                int asama = 1;
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE `derskodu`='" + derskodu + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();
                while (sonuc.Read())
                {

                    if (derskodu == (string)sonuc["derskodu"])
                    {

                        asama = 0;
                        DataSender.SendCevapForm(connectionID, isim + "Bu Ders İsmi Kullanılmaktadır", "Kırmızı");
                    }
                }

                sonuc.Close();

                if (asama == 1)
                {
                    baglanti = Sabitler.bagli_client[connectionID].baglanti;




                    SqlCommand = "INSERT INTO `dersler`( `derskodu`) VALUES ('" + derskodu + "')";

                    guncelle = new MySqlCommand(SqlCommand, baglanti);

                    if (guncelle != null)
                    {
                        try
                        {
                            if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Dersi Oluşturuldu", "Yeşil"); }
                            else
                            {
                                DataSender.SendCevapForm(connectionID, derskodu + " Dersi Oluşturulamadı", "Kırmızı");

                            }
                        }
                        catch (Exception e)
                        {
                            DataSender.SendCevapForm(connectionID, derskodu + " Dersi Oluşturulamadı", "Kırmızı");

                        }

                    }
                }
            }

            if (tur == "Ders Sil")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;


                string SqlCommand = "DELETE FROM `dersler` WHERE `derskodu`='" + derskodu + "' ";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {
                    try
                    {
                        if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Başarıyla Silindi", "Gri"); }
                        else { DataSender.SendCevapForm(connectionID, derskodu + " Silinemedi", "Kırmızı"); }
                    }
                    catch (Exception e)
                    {
                        DataSender.SendCevapForm(connectionID, derskodu + " Silinemedi", "Kırmızı");
                    }

                }

                SqlCommand = "DELETE FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "' ";

                guncelle = new MySqlCommand(SqlCommand, baglanti);

                if (guncelle != null)
                {
                    try
                    {
                        if (guncelle.ExecuteNonQuery() >= 0) { DataSender.SendCevapForm(connectionID, derskodu + " Öğrencileri Başarıyla Silindi", "Gri"); }
                        else { DataSender.SendCevapForm(connectionID, derskodu + " Öğrencileri Silinemedi", "Kırmızı"); }
                    }
                    catch (Exception e)
                    {
                        DataSender.SendCevapForm(connectionID, derskodu + " Öğrencileri Silinemedi", "Kırmızı");
                    }

                }

            }


        }


        public static void HandleListele3(string connectionID, byte[] data)
        {

            M_ByteBuffer buffer = new M_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packedID = buffer.Int_Oku();
            string tur = buffer.String_Oku();
            string isim = buffer.String_Oku();
            string derskodu = buffer.String_Oku();


            ArrayList isimler = new ArrayList();


            buffer.Dispose();


            if (tur == "OgrenciOgretmenListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE `derskodu`='" + derskodu + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["ogretmen"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgrenciOgretmenListesiYenile");



                }
            }


            if (tur == "OgrenciOgrenciListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["ogrenci"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgrenciOgrenciListesiYenile");



                }
            }


            if (tur == "MesajlarıYenile")
            {
                string gonderen = isim;
                string alici = derskodu;
                string mesaj = "";
                int saat = 0;
                int dakika = 0;
                string saats = "";
                string dakikas = "";

                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT *,HOUR(`tarih`),MINUTE(`tarih`) FROM `mesajlar` WHERE (`gonderen`='" + gonderen + "' AND `alici`='" + alici + "' )OR(`gonderen`='" + alici + "' AND `alici`='" + gonderen + "') ORDER BY `mesajlar`.`tarih` ASC";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {
                    saat = (int)sonuc["HOUR(`tarih`)"];
                    dakika = (int)sonuc["MINUTE(`tarih`)"];
                    saats = saat.ToString();
                    dakikas = dakika.ToString();
                    if ((int)saat < 10) { saats = "0" + saat.ToString(); }
                    if ((int)dakika < 10) { dakikas = "0" + dakika.ToString(); }
                    mesaj = "[" + saats + ":" + dakikas + "] " + sonuc["gonderen"] + ":" + sonuc["mesaj"];

                    isimler.Add(mesaj);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "MesajlarıYenile");



                }
            }


            if (tur == "OgretmenOgretmenListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `dersler` WHERE `derskodu`='" + derskodu + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["ogretmen"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgretmenOgretmenListesiYenile");



                }
            }

            if (tur == "OgretmenOgrenciListesiYenile")
            {
                MySqlConnection baglanti = Sabitler.bagli_client[connectionID].baglanti;
                string SqlCommand = "SELECT * FROM `ogrenciler` WHERE `derskodu`='" + derskodu + "'";

                MySqlCommand guncelle = new MySqlCommand(SqlCommand, baglanti);

                MySqlDataReader sonuc = guncelle.ExecuteReader();

                while (sonuc.Read())
                {

                    isimler.Add(sonuc["ogrenci"]);

                }




                sonuc.Close();

                for (int i = 0; i < isimler.Count; i++)
                {

                    DataSender.SendListeCevap(connectionID, (string)isimler[i], "OgretmenOgrenciListesiYenile");



                }
            }




        }





    }
}
