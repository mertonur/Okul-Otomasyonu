using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OServer
{
    class Client
    {
        public TcpClient socket;
        public NetworkStream stream;
        private byte[] recBuffer;
        public M_ByteBuffer buffer;


        //Oyuncu Bilgileri
        public string OyuncuAdi;
        public string connectionID;
        public bool oyunda_mi = false;

        public bool girisyapti = false;
        public float xCord = 9;
        public float yCord = 0;
        public float zCord = 5;


        private MySqlConnection baglanti_;
        public MySqlConnection baglanti
        {
            get
            {
                baglanti_control(baglanti_);
                return baglanti_;
            }
            set
            {
                baglanti_ = value;
            }
        }


        public void baglanti_control(MySqlConnection baglanti)
        {

            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
                Yazi.Log_Yaz(connectionID.ToString() + "Kullanıcısının Bağlantısı Yeniden Açıldı");
            }



        }



        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            stream = socket.GetStream();
            recBuffer = new byte[4096];
            stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);
            Sabitler.oyuncu_baglandi(connectionID.ToString());


        }

        private void OnReceiveData(IAsyncResult result)
        {
            try
            {
                int length = stream.EndRead(result);
                if (length <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = new byte[length];
                Array.Copy(recBuffer, newBytes, length);
                ServerHandleData.HandleData(connectionID, newBytes);
                stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);

            }
            catch (Exception)
            {

                CloseConnection();
                return;
            }


        }

        private void CloseConnection()
        {
            if (Sabitler.bagli_client[connectionID].girisyapti == true)
            {
                Sabitler.oyuncu_cikti(Sabitler.bagli_client[connectionID].OyuncuAdi);
                Sabitler.bagli_clientName.Remove(Sabitler.bagli_client[connectionID].OyuncuAdi);
                Sabitler.bagli_client.Remove(connectionID);
            }
            else
            {
                Sabitler.bagli_client.Remove(connectionID);
                Sabitler.oyuncu_cikti(connectionID.ToString());
            }

            socket.Close();

        }








    }
}
