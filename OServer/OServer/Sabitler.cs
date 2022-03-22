using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OServer
{
    class Sabitler
    {
        public static OServer server = ((OServer)Application.OpenForms.OfType<OServer>().SingleOrDefault());
        public static Dictionary<string, Client> bagli_client = new Dictionary<string, Client>();
        public static Dictionary<string, Client> bagli_clientName = new Dictionary<string, Client>();

        public static MySql MySql_Data = new MySql();
        public static MySqlConnection Sunucu_MySql_Baglanti = null;

        public static void oyuncu_baglandi(string connectionID)
        {
            bagli_kullanici_sayisi++;
            Yazi.Log_Yaz("Kullanıcı Servera Bağlandı : " + connectionID);
            Sabitler.server.listBox1.Items.Add(connectionID);


        }

        public static void oyuncu_cikti(string connectionID)
        {
            bagli_kullanici_sayisi--;
            Yazi.Log_Yaz("Kullanıcı Sunucudan Ayrıldı : " + connectionID);
            Sabitler.server.listBox1.Items.Remove(connectionID);
        }
        public static void oyuncu_ciktiName(string connectionID)
        {
            bagli_kullanici_sayisi--;

            Sabitler.server.listBox1.Items.Remove(connectionID);
        }

        private static int bagli_kullanici_sayisi_ = 0;
        public static int bagli_kullanici_sayisi
        {
            get
            {
                return bagli_kullanici_sayisi_;
            }
            set
            {
                bagli_kullanici_sayisi_ = value;
                server.label1.Text = "Bağlı Kullanıcı Sayısı : " + bagli_kullanici_sayisi.ToString();
            }
        }





    }
}
