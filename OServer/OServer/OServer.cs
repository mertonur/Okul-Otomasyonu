using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OServer
{
    public partial class OServer : Form
    {
        public OServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            General.Sunucuyu_Baslat();
            button1.Enabled = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     




        private void timer1_Tick(object sender, EventArgs e)
        {



            label2.Text = "Sistem Saati:" + DateTime.Now.ToString();
            DateTime dateDate = DateTime.Now;
            // int result = dateDate.Year * 10000 + dateDate.Month * 100
            // + dateDate.Day + dateDate.Hour + dateDate.Minute + dateDate.Second;
            int result = dateDate.Minute;


        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void OServer_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendText("Sunucu Başlatılıyor");
            richTextBox2.AppendText("Sunucu Başlatılıyor");
        }
    }

}
