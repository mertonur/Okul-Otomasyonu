using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    class General
    {
        public static void Sunucuyu_Baslat()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ServerTCP.InitializeNetwork();
            Yazi.Log_Yaz("Sunucu Başlatıldı");

            Sabitler.Sunucu_MySql_Baglanti = Sabitler.MySql_Data.MySqlBaslat();
            Yazi.Log_Yaz("MySql Başlatıldı");

            sw.Stop();
            Yazi.Log_Yaz("Sunucuyu başlatma süresi : " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
