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

using MySql.Data;
using MySql.Data.MySqlClient;




namespace WindowsFormsApp2
{
    public  partial class Otomasyon : Form
    {

        int degistirildi = 0;
        int degistirildi2 = 0;
        int degistirildi3 = 0;
        int acmaonay = 0;
        string acmaonayhata = "";


        public static string mesaj1 = "";
        public static string mesaj2 = "";
        public static string mesaj3 = "";
        public static string mesaj4 = "";

        public static int yazdi = 0;

        public static bool gizli1 = false;
        public static bool gizlilikdegisti1 = false;
        public static bool gizli2 = false;
        public static bool gizlilikdegisti2 = false;
        public static bool gizli3 = false;
        public static bool gizlilikdegisti3 = false;
        public static bool gizli4 = false;
        public static bool gizlilikdegisti4 = false;
        public static bool gizli5 = false;
        public static bool gizlilikdegisti5 = false;

        public static bool gizlilikdegistiogr6 = false;
        public static bool gizlilikdegistiogr7 = false;
        public static bool gizlilikdegistiogr8 = false;
        public static bool gizlilikdegistiogr9 = false;

        public static ArrayList isimler = new ArrayList();
        public static ArrayList dersler = new ArrayList();
        public static ArrayList dersogretmen = new ArrayList();
        public static ArrayList dersogrenciler = new ArrayList();
        public static ArrayList ogretmenler = new ArrayList();
        public static ArrayList ogrenciler = new ArrayList();
        public static ArrayList ogrenciderslerim = new ArrayList();
        public static ArrayList ogretmenderslerim = new ArrayList();
        public static ArrayList ogrenciogretmenim= new ArrayList();
        public static ArrayList ogrenciogrenciler = new ArrayList();
        public static ArrayList ogretmenogretmenim = new ArrayList();
        public static ArrayList ogretmenogrenciler = new ArrayList();
        public static ArrayList gelenkutusuogr = new ArrayList();
        public static ArrayList gelenkutusuogrt = new ArrayList();

        public static string ogrmesajlar = "";

        public static string sonogrencichat = "";
        public static string sonogrencichatonceki = "";
        public static int ogretmenpuan = 0;
        public static int ogretmenpuanonceki = 0;

        public static string sonderskodu = "";

        public Otomasyon()
        {
            
            InitializeComponent();
            Global.form1 = this;
            Global.form1.panel1.Visible = false;
            Global.form1.panel3.Visible = false;
            Global.form1.panel4.Visible = false;
           


            textBox2.PasswordChar = '*';


            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox4.PasswordChar = '*';
            textBox3.PasswordChar = '*';

            richTextBox2.Hide();
            richTextBox3.Hide();
            button13.Hide();


            comboBox1.Items.Add("Öğretmen");
            comboBox1.Items.Add("Öğrenci");
            comboBox1.Items.Add("Admin");

            

        }
        public void isimekle(string isim)
        {           
            isimler.Add(isim);         
        }

        public void dersekle(string isim)
        {
            dersler.Add(isim);
        }
        public void dersogretmenekle(string isim)
        {
            dersogretmen.Add(isim);
        }
        public void dersogrenciekle(string isim)
        {
            dersogrenciler.Add(isim);
        }

        public void ogretmenlisteekle(string isim)
        {
            ogretmenler.Add(isim);
        }
        public void ogrencilisteekle(string isim)
        {
            ogrenciler.Add(isim);
        }
        public void ogrenciderslistesiyenile(string isim)
        {
            ogrenciderslerim.Add(isim);
        }
        public void ogretmenderslistesiyenile(string isim)
        {
            ogretmenderslerim.Add(isim);
        }
        public void ogrenciogretmenlistesiyenile(string isim)
        {
            ogrenciogretmenim.Add(isim);
        }

        public void ogrenciogrencilistesiyenile(string isim)
        {
            ogrenciogrenciler.Add(isim);
        }
        public void ogretmenogretmenlistesiyenile(string isim)
        {
            ogretmenogretmenim.Add(isim);
        }
        public void ogretmenogrencilistesiyenile(string isim)
        {
            ogretmenogrenciler.Add(isim);
        }
        public void gelenkutusuogryenile(string isim)
        {
            if (isim != Global.kullaniciadi)
            {
                gelenkutusuogr.Add(isim);
            }
            
        }

        public void gelenkutusuogrtyenile(string isim)
        {
            if (isim != Global.kullaniciadi)
            {
                gelenkutusuogrt.Add(isim);
            }

        }

        public void mesajlariyenileogr(string isim)
        {
            if (ogrmesajlar == "") { ogrmesajlar = isim + "\n"; }
            else { ogrmesajlar += isim + "\n"; }
        }
        

        public void Yazi(string a,string renk)
        {
            if (renk == "Yeşil")
            {
                if (mesaj1 == "") { mesaj1 = a; }
                else { mesaj1 += "\n" + a; }
            }
            else if (renk == "Kırmızı") {
                if (mesaj2 == "") { mesaj2 = a; }
                else { mesaj2 += "\n" + a; }
            }
            else if (renk == "Siyah") {
                if (mesaj3 == "") { mesaj3 = a; }
                else { mesaj3 += "\n" + a; }
            }
            else if (renk == "Gri") {
                if (mesaj4 == "") { mesaj4 = a; }
                else { mesaj4 += "\n" + a; }
            }
           
        }

        public void gizlilik1(bool a)
        {
            gizli1 = a;
            gizlilikdegisti1 = true;
        }
        public void gizlilik2(bool a)
        {
            gizli2 = a;
            gizlilikdegisti2 = true;
        }
        public void gizlilik3(bool a)
        {
            gizli3 = a;
            gizlilikdegisti3 = true;
        }
        public void gizlilik4(bool a)
        {
            gizli4= a;
            gizlilikdegisti4 = true;
        }
        public void gizlilik5(bool a)
        {
            gizli5 = a;
            gizlilikdegisti5 = true;
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
           

            string user = textBox1.Text;
            string pass = textBox2.Text;

            //con.Open();
            // cmd.Connection = con;

            // cmd.CommandText = "SELECT * FROM kullanicilar where kullanici='" + user + "' AND sifre='" + pass + "'";
            //dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{

            //    label3.Visible = false;
            //    Form2 form2 = new Form2();//açılacak form
            //    form2.Show();
            //    this.Hide();
            //     //form 2 açılıyor.

            //}
            //else
            //{

            //    textBox2.Text = "";
            //    label3.Visible = true;


            //}
            //con.Close();
            DataSender.SendLoginGiris(user,pass,"1",1);

        }
      
        private void label1_Click(object sender, EventArgs e)
        {
           
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
     
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mesaj1 != "")
            {
                Global.form1.richTextBox1.SelectionColor = Color.Green;
                Global.form1.richTextBox1.AppendText(mesaj1 + "\n");
                mesaj1 = "";
            }
            if (mesaj2 != "")
            {
                Global.form1.richTextBox1.SelectionColor = Color.Red;
                Global.form1.richTextBox1.AppendText(mesaj2 + "\n");
                mesaj2 = "";
            }
            if (mesaj3 != "")
            {
                Global.form1.richTextBox1.SelectionColor = Color.Black;
                Global.form1.richTextBox1.AppendText(mesaj3 + "\n");
                mesaj3 = "";
            }
            if (mesaj4 != "")
            {
                Global.form1.richTextBox1.SelectionColor = Color.Gray;
                Global.form1.richTextBox1.AppendText(mesaj4 + "\n");
                mesaj4 = "";
            }
            if (gizlilikdegisti1)
            {
                if (gizli1) { 
                    Global.form1.panel1.Visible=true;
                   
                }
                else if (!gizli1) { 

                    Global.form1.panel1.Visible = false;
                }
                gizlilikdegisti1 = false;
            }
            if (gizlilikdegisti2)
            {
                if (gizli2)
                {
                    Global.form1.panel1.Visible = true;

                }
                else if (!gizli2)
                {
                  
                    Global.form1.panel2.Visible = false;
                }
                gizlilikdegisti2 = false;
            }
            if (gizlilikdegisti3)
            {
                if (gizli3)
                {
                    Global.form1.panel3.Visible = true;

                }
                else if (!gizli3)
                {
                    
                    Global.form1.panel3.Visible = false;
                }
                gizlilikdegisti3 = false;
            }
            if (gizlilikdegisti4)
            {
                if (gizli4)
                {
                    Global.form1.panel4.Visible = true;

                }
                else if (!gizli4)
                {
                   
                    Global.form1.panel4.Visible = false;
                }
                gizlilikdegisti4 = false;
            }
           


            if (gizlilikdegistiogr6)
            {
                
                    Global.form1.richTextBox2.Hide();
                    Global.form1.richTextBox2.Clear();
                    Global.form1.richTextBox2.Show();
                    Global.form1.richTextBox3.Show();
                    Global.form1.button13.Show();
                Global.form1.richTextBox2.HideSelection = false;

       

                gizlilikdegistiogr6 = false;
            }
            if (gizlilikdegistiogr7)
            {               
                Global.form1.listBox5.Items.Clear();               
                gizlilikdegistiogr7 = false;
            }

            if (gizlilikdegistiogr8)
            {
                Global.form1.listBox6.Items.Clear();
                gizlilikdegistiogr8 = false;
            }

            if (gizlilikdegistiogr9)
            {

                Global.form1.richTextBox5.Hide();
                Global.form1.richTextBox5.Clear();
                Global.form1.richTextBox5.Show();
                Global.form1.richTextBox4.Show();
                Global.form1.button14.Show();
                Global.form1.richTextBox5.HideSelection = false;



                gizlilikdegistiogr9 = false;
            }

            for (int i = 0; i < isimler.Count; i++)
            {
                checkedListBox1.Items.Add(isimler[i]);
                isimler.Remove(isimler[i]);
                break;
            }

            for (int i = 0; i < dersler.Count; i++)
            {
                listBox1.Items.Add(dersler[i]);
                dersler.Remove(dersler[i]);
                break;
            }
            for (int i = 0; i < dersogretmen.Count; i++)
            {
                textBox6.Text=(string)dersogretmen[i];
                dersogretmen.Remove(dersogretmen[i]);
                break;
            }
            for (int i = 0; i < dersogrenciler.Count; i++)
            {
                listBox2.Items.Add(dersogrenciler[i]);
                dersogrenciler.Remove(dersogrenciler[i]);
                break;
            }
            for (int i = 0; i < ogretmenler.Count; i++)
            {
                comboBox2.Items.Add(ogretmenler[i]);
                ogretmenler.Remove(ogretmenler[i]);
                break;
            }
            for (int i = 0; i < ogrenciler.Count; i++)
            {
                comboBox3.Items.Add(ogrenciler[i]);
                ogrenciler.Remove(ogrenciler[i]);
                break;
            }
            for (int i = 0; i < ogrenciderslerim.Count; i++)
            {
                listBox3.Items.Add(ogrenciderslerim[i]);
                ogrenciderslerim.Remove(ogrenciderslerim[i]);
                break;
            }
            for (int i = 0; i < ogretmenderslerim.Count; i++)
            {
                listBox8.Items.Add(ogretmenderslerim[i]);
                ogretmenderslerim.Remove(ogretmenderslerim[i]);
                break;
            }
            
            for (int i = 0; i < ogrenciogretmenim.Count; i++)
            {
                textBox8.Text=(string)ogrenciogretmenim[i];
                ogrenciogretmenim.Remove(ogrenciogretmenim[i]);
                break;
            }
            for (int i = 0; i < ogrenciogrenciler.Count; i++)
            {
                listBox4.Items.Add(ogrenciogrenciler[i]);
                ogrenciogrenciler.Remove(ogrenciogrenciler[i]);
                break;
            }
            for (int i = 0; i < gelenkutusuogr.Count; i++)
            {
                listBox5.Items.Add(gelenkutusuogr[i]);
                gelenkutusuogr.Remove(gelenkutusuogr[i]);
                break;
            }
            for (int i = 0; i < gelenkutusuogrt.Count; i++)
            {
                listBox6.Items.Add(gelenkutusuogrt[i]);
                gelenkutusuogrt.Remove(gelenkutusuogrt[i]);
                break;
            }

            for (int i = 0; i < ogretmenogretmenim.Count; i++)
            {
                textBox9.Text = (string)ogretmenogretmenim[i];
                ogretmenogretmenim.Remove(ogretmenogretmenim[i]);
                break;
            }
            for (int i = 0; i < ogretmenogrenciler.Count; i++)
            {
                listBox7.Items.Add(ogretmenogrenciler[i]);
                ogretmenogrenciler.Remove(ogretmenogrenciler[i]);
                break;
            }

            if (ogrmesajlar != "")
            {
                if (Global.rol == "Öğrenci")
                {
                    richTextBox2.AppendText(ogrmesajlar);
                    ogrmesajlar = "";
                }
                if (Global.rol == "Öğretmen")
                {
                    richTextBox5.AppendText(ogrmesajlar);
                    ogrmesajlar = "";
                }
            }
            if (sonogrencichatonceki != sonogrencichat)
            {
                if (Global.rol == "Öğrenci") { label17.Text = sonogrencichat; }

                if (Global.rol == "Öğretmen") { label21.Text = sonogrencichat; }
                sonogrencichatonceki = sonogrencichat;
            }

            if (ogretmenpuan != ogretmenpuanonceki)
            {
                if (Global.rol == "Öğrenci") {

                    Global.form1.progressBar2.Value = ogretmenpuan;
                    textBox12.Text = ogretmenpuan.ToString();
                }

                if (Global.rol == "Öğretmen") {
                   Global.form1.progressBar1.Value = ogretmenpuan;
                    textBox11.Text = ogretmenpuan.ToString();
                }
                ogretmenpuanonceki = ogretmenpuan;
            }


        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
              
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            YenileFunc();
            button2.Enabled = true;
        }
        public void Listele()
        {

            checkedListBox1.Items.Clear();

            DataSender.Listele("AdminKullaniciListeYenile");

        }
        public void ListeleDers()
        {

           listBox1.Items.Clear();

            DataSender.Listele("DersListeYenile");

        }
        public void ListeleOgretmen()
        {

            comboBox2.Items.Clear();

            DataSender.Listele("OgretmenListeYenile");

        }
        public void ListeleOgrenci()
        {

            comboBox3.Items.Clear();

            DataSender.Listele("OgrenciListeYenile");

        }
        public void TemizleDerslik()
        {
            listBox2.Items.Clear();
            textBox6.Text = "";
        }
            public void YenileFunc()
        {
            Listele();
            ListeleDers();
            ListeleOgretmen();
            ListeleOgrenci();
        }

        public void ListeleDersOgretmen(string derskodu)
        {

            textBox6.Text = "";

            DataSender.Listele2("DersOgretmenYenile", derskodu);

        }
        public void ListeleDersOgrenciler(string derskodu)
        {

            listBox2.Items.Clear();

            DataSender.Listele2("DersOgrenciYenile", derskodu);

        }
        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {



            int i;

            for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {


                    DataSender.KullaniciSil(checkedListBox1.Items[i].ToString());
                    

                }
            }


            Global.form1.YenileFunc();
            Global.form1.TemizleDerslik();


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            acmaonay = 1;
            acmaonayhata = "";
            if (degistirildi == 0)
            {
                acmaonay = 0;
                acmaonayhata += "Lütfen Bir Rol Seçin.";
            }
            else if (textBox5.Text == null || textBox5.Text == "" || textBox4.Text == null || textBox4.Text == "" || textBox3.Text == null || textBox3.Text == "")
            {
                acmaonay = 0;
                acmaonayhata += "Lütfen Boş Alanları Doldurun.";
            }
            else if (textBox4.Text != textBox3.Text)
            {
                acmaonay = 0;
                acmaonayhata += "Lütfen Eşleşen Bir Parola Girin.";
            }

            if (acmaonay == 1)
            {
                //datasender
                DataSender.KullaniciEkle(textBox5.Text, textBox4.Text, comboBox1.SelectedItem.ToString());
                textBox5.Text = "";
                textBox4.Text = "";
                textBox3.Text = "";
                Global.form1.YenileFunc();
            }

            if (acmaonay == 0)
            {
                //Console.WriteLine(acmaonayhata);
                //label6.Visible = true;
                //label6.Text = acmaonayhata;

                Yazi(acmaonayhata, "Kırmızı");

            }
            
            button1.Enabled = true;
        }


       

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            degistirildi = 1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dersismi = "";
            if (listBox1.SelectedItem != null)
            {
                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }


                ListeleDersOgrenciler(dersismi);
                ListeleDersOgretmen(dersismi);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            degistirildi2 = 1;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            degistirildi3 = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Öğretmen Atama
            string dersismi="";
            int asama = 0;
            if (listBox1.SelectedItem != null)
            {
                dersismi = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }
                asama = 1;
            }
            else
            {
                Global.form1.Yazi("Ders Kodu Seçimi Yapınız","Kırmızı");
            }
            if (asama == 1) {
               
                    if (comboBox2.SelectedItem != null)
                    {
                        DataSender.AtamaCikarma(comboBox2.SelectedItem.ToString(), dersismi, "Öğretmen", "Atama");
                        Global.form1.YenileFunc();
                        Global.form1.TemizleDerslik();
                    }
                    else { Global.form1.Yazi("Öğretmen Seçimi Yapınız", "Kırmızı"); }
                
                
            }

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Öğrenci Atama

            string dersismi = "";
            int asama = 0;
            if (listBox1.SelectedItem != null)
            {
                dersismi = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }
                asama = 1;
            }
            else
            {
                Global.form1.Yazi("Ders Kodu Seçimi Yapınız", "Kırmızı");
            }
            if (asama == 1)
            {

                if (comboBox3.SelectedItem != null)
                {
                    DataSender.AtamaCikarma(comboBox3.SelectedItem.ToString(), dersismi, "Öğrenci", "Atama");
                    Global.form1.YenileFunc();
                    Global.form1.TemizleDerslik();
                }
                else { Global.form1.Yazi("Öğrenci Seçimi Yapınız", "Kırmızı"); }


            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            //öğretmen geri çekme
            string dersismi = "";
            int asama = 0;
            if (listBox1.SelectedItem != null)
            {
                dersismi = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }
                asama = 1;
            }
            else
            {
                Global.form1.Yazi("Ders Kodu Seçimi Yapınız", "Kırmızı");
            }
            if (asama == 1)
            {

               
                    DataSender.AtamaCikarma(" ", dersismi, "Öğretmen", "Geri Çek");
                    Global.form1.YenileFunc();
                    Global.form1.TemizleDerslik();
                
             


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Öğrenci Atama

            string dersismi = "";
            int asama = 0;
            if (listBox1.SelectedItem != null)
            {
                dersismi = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }
                asama = 1;
            }
            else
            {
                Global.form1.Yazi("Ders Kodu Seçimi Yapınız", "Kırmızı");
            }
            if (asama == 1)
            {

                if (listBox2.SelectedItem != null)
                {
                    DataSender.AtamaCikarma(listBox2.SelectedItem.ToString(), dersismi, "Öğrenci", "Geri Çek");
                    Global.form1.YenileFunc();
                    Global.form1.TemizleDerslik();
                }
                else { Global.form1.Yazi("Öğrenci Seçimi Yapınız", "Kırmızı"); }


            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //ders sil
            string dersismi = "";
           
            if (listBox1.SelectedItem != null)
            {
                dersismi = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedItem.ToString().Substring(0, 1) == "❌") { dersismi = listBox1.SelectedItem.ToString().Substring(2); }

                if (listBox1.SelectedItem.ToString().Substring(0, 2) == "✔️") { dersismi = listBox1.SelectedItem.ToString().Substring(3); }

                //datasender
                DataSender.AtamaCikarma(" ", dersismi, "Ders Sil", " ");

            }
            else
            {
                Global.form1.Yazi("Ders Kodu Seçimi Yapınız", "Kırmızı");
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(textBox7.Text!=""|| textBox7.Text != null)
            {
                DataSender.AtamaCikarma(" ", textBox7.Text, "Ders Ekle", " ");
                textBox7.Text = "";
                Global.form1.YenileFunc();
            }
            else { Global.form1.Yazi("Ders Kodu Giriniz", "Kırmızı"); }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Global.form1.YenileFuncOgrenci();
        }

        public void YenileFuncOgrenci()
        {
            ListeleDerslerim();
            TemizleOgrenciListeleri();

            ListeleGelenKutusuOgr();

        }

        public void YenileFuncOgretmen()
        {
            ListeleDerslerimOGRT();
            TemizleOgretmenListeleri();

            ListeleGelenKutusuOgrt();

        }
        public void TemizleOgrenciListeleri()
        {
            textBox8.Text = "";
            listBox4.Items.Clear();
        }
            public void ListeleDerslerim()
        {

            listBox3.Items.Clear();

            DataSender.Listele2("OgrenciDersListesiYenile",Global.kullaniciadi);

        }

        public void TemizleOgretmenListeleri()
        {
            textBox9.Text = "";
            listBox7.Items.Clear();
        }
      

        public void ListeleDerslerimOGRT()
        {

            listBox8.Items.Clear();

            DataSender.Listele2("OgretmenDersListesiYenile", Global.kullaniciadi);

        }

        public void ListeleOgretmenimOGR(string dersismi)
        {
            //öğrenci için öğretmen listesi

            textBox8.Text = "";

         

                DataSender.Listele3("OgrenciOgretmenListesiYenile", Global.kullaniciadi, dersismi);
           

               

        }
        public void ListeleOgrenciOGR(string dersismi)
        {
            //öğrenci için öğretmen listesi

            listBox4.Items.Clear();

          

                DataSender.Listele3("OgrenciOgrenciListesiYenile", Global.kullaniciadi, dersismi);
          

        }

        public void ListeleOgretmenimOGRT(string dersismi)
        {
            //Öğretmen için öğretmen listesi

            textBox9.Text = "";



            DataSender.Listele3("OgretmenOgretmenListesiYenile", Global.kullaniciadi, dersismi);




        }
        public void ListeleOgrenciOGRT(string dersismi)
        {
            //Öğretmen için öğretmen listesi

            listBox7.Items.Clear();



            DataSender.Listele3("OgretmenOgrenciListesiYenile", Global.kullaniciadi, dersismi);


        }


        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            

                string dersismi = "";
                if (listBox3.SelectedItem != null)
                {
                dersismi = listBox3.SelectedItem.ToString();

                sonderskodu = dersismi;


                ListeleOgretmenimOGR(dersismi);
                ListeleOgrenciOGR(dersismi);
                DataSender.Listele2("DersKoduPuanOgrenci", dersismi);
            }

            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            if (textBox8.Text != "")
            {
                if (textBox8.Text != "Tanımsız")
                {
                    mesajpenceresiac(textBox8.Text);
                }
                else {
                    Global.form1.Yazi("Öğretmen Tanımsız", "Kırmızı");
                }
                //öğretmene mesaj
            }
            else
            {
                Global.form1.Yazi("Ders Seçimi Yapınız", "Kırmızı");
            }
        }
        private void mesajpenceresiac(string alici)
        {
            if (alici != Global.kullaniciadi)
            {
                Global.form1.mesajpenceresitemizleogr();
                button13.Show();
                sonogrencichat = alici;
                ListeleGelenKutusuOgr();
                DataSender.Listele3("MesajlarıYenile",Global.kullaniciadi,alici);

            }
            else {
                Global.form1.Yazi("Kendinize Mesaj Gönderemezsiniz","Kırmızı");
            }
            //eskimesajları yükle

        }

        private void mesajpenceresiacogrt(string alici)
        {
            if (alici != Global.kullaniciadi)
            {
             
                Global.form1.mesajpenceresitemizleogrt();
                button13.Show();
                sonogrencichat = alici;
                ListeleGelenKutusuOgrt();
                DataSender.Listele3("MesajlarıYenile", Global.kullaniciadi, alici);

            }
            else
            {
                Global.form1.Yazi("Kendinize Mesaj Gönderemezsiniz", "Kırmızı");
            }
            //eskimesajları yükle

        }

        public void mesajpenceresitemizleogr()
        {
            gizlilikdegistiogr6 = true;
            

        }

        public void mesajpenceresitemizleogrt()
        {
           
            gizlilikdegistiogr9 = true;


        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                if (listBox4.SelectedItem != null)
                {
                    mesajpenceresiac(listBox4.SelectedItem.ToString());
                }
                else
                {
                    Global.form1.Yazi("Öğrenci Seçimi Yapınız", "Kırmızı");
                }
            }
            else {
                Global.form1.Yazi("Ders Seçimi Yapınız","Kırmızı");
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //mesaj Gonder
            DataSender.SendChatMesaj(richTextBox3.Text,sonogrencichat,Global.kullaniciadi);
            richTextBox3.Clear();

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        public void ListeleGelenKutusuOgr()
        {
            GelenKutusuTemizleOgr();
            DataSender.Listele2("GelenListesiYenileOgr", Global.kullaniciadi);
        }

        public void ListeleGelenKutusuOgrt()
        {
            GelenKutusuTemizleOgrt();
            DataSender.Listele2("GelenListesiYenileOgrt", Global.kullaniciadi);
        }

        private void GelenKutusuTemizleOgr()
        {
            gizlilikdegistiogr7 = true;
        }
        private void GelenKutusuTemizleOgrt()
        {
            gizlilikdegistiogr8 = true;
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.SelectedItem!=null)
            {
                mesajpenceresiac(listBox5.SelectedItem.ToString());
            }
        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

            string dersismi = "";
            if (listBox8.SelectedItem != null)
            {
                dersismi = listBox8.SelectedItem.ToString();

                sonderskodu = dersismi;


                ListeleOgretmenimOGRT(dersismi);
                ListeleOgrenciOGRT(dersismi);
                DataSender.Listele2("DersKoduPuanOgretmen", dersismi);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
           Global.form1.YenileFuncOgretmen();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (listBox8.SelectedItem != null)
            {
                if (listBox7.SelectedItem != null)
                {
                    mesajpenceresiacogrt(listBox7.SelectedItem.ToString());
                }
                else
                {
                    Global.form1.Yazi("Öğrenci Seçimi Yapınız", "Kırmızı");
                }
            }
            else
            {
                Global.form1.Yazi("Ders Seçimi Yapınız", "Kırmızı");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //mesaj Gonder
            DataSender.SendChatMesaj(richTextBox4.Text, sonogrencichat, Global.kullaniciadi);
            richTextBox4.Clear();
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox6.SelectedItem != null)
            {
                mesajpenceresiacogrt(listBox6.SelectedItem.ToString());
            }
        }



        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        public void progressbarpuan(int sayi) {
            ogretmenpuan = sayi;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                if (textBox8.Text != "Tanımsız" && textBox8.Text != "")
                {
                    DataSender.SendOgretmenPuan(trackBar1.Value, textBox8.Text, Global.kullaniciadi, listBox3.SelectedItem.ToString());
                }
                else
                {
                    Global.form1.Yazi("Öğretmen Tanımsız","Kırmızı");
                }
            }
            else {
                Global.form1.Yazi("Ders Seçimi Yapınız","Kırmızı");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox10.Text = trackBar1.Value.ToString();
        }

        private void textBox10_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
