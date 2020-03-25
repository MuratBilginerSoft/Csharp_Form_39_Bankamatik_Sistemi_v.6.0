using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Bankamatik_Sistemi
{
    public partial class Form1 : Form
    {
        #region Değişkenler
        string yol = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=İşBankası.accdb";
        string sorgu,sorgu2,sorgu3;
        OleDbConnection bağlantı;
        OleDbCommand komut,komut2,komut3;
        OleDbDataAdapter kayıt;
        DataTable tablo,tablo2,tablo3;
        int t;
        int alınanpara;
        int beş, on, yirmi, elli, yüz, ikiyüz, beş2, on2, yirmi2, elli2, yüz2, ikiyüz2;
        int para,para1;
        DateTime zaman;
        #endregion

        #region Metodlar

        public void parayıver(int x)

        {

            if (para<alınanpara)
            {
                MessageBox.Show("Çekmek istediğiniz miktar hesabınızda bulunmamaktadır","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            else
            {
                parasayısı[0] = x / 200;
                x = x % 200;
                parasayısı[1] = x / 100;
                x = x % 100;
                parasayısı[2] = x / 50;
                x = x % 50;
                parasayısı[3] = x / 20;
                x = x % 20;
                parasayısı[4] = x / 10;
                x = x % 10;
                parasayısı[5] = x / 5;
                x = x % 5;


                if (x != 0)
                {
                    MessageBox.Show("Şu anda bankamatikte bu işleminizi gerçekleştirecek banknot bulunmamaktadır", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                int z = 5;
                int e = 0;

                foreach (int parax in parasayısı)
                {
                    if (parax > parasayısı2[z])
                    {
                        e++;

                    }
                    z--;
                }

                if (e != 0)
                {
                    MessageBox.Show("Şu anda bu isteğinizi karşılayacak banknot bulunmamaktadır ATM de.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else if (e == 0)
                {
                    groupBox1.Visible = true;
                   
                    label7.Text = parasayısı[0].ToString() + " Adet";
                    label6.Text = parasayısı[1].ToString() + " Adet";
                    label5.Text = parasayısı[2].ToString() + " Adet";
                    label4.Text = parasayısı[3].ToString() + " Adet";
                    label3.Text = parasayısı[4].ToString() + " Adet";
                    label2.Text = parasayısı[5].ToString() + " Adet";
                }
            }
            
        }

        public void bağlan()
        {

            try
            {
                bağlantı = new OleDbConnection(yol);
            }
            catch (OleDbException ex)
            {

                MessageBox.Show(" " + ex, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        
        }

        public void kişibilgileri()
        {
            sorgu = "Select * from Kişiler";
            komut = new OleDbCommand(sorgu,bağlantı);
            kayıt = new OleDbDataAdapter(komut);
            tablo = new DataTable();

            bağlantı.Open();
            kayıt.Fill(tablo);
            bağlantı.Close();
        
        }

        public void butondurumu()
        {
            if (para>=200)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
            }

            else if (para>=100)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }

            else if (para>=50)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }

            else if (para>=20)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else if (para>=10)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }

            else if (para>=5)
            {
                button1.Enabled = true;
            }
        }

        public void butonfalse()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        
        
        }

        public void paragüncelle()
        {
            zaman = DateTime.Now;
            sorgu2 = "Update Kişiler set Karttaki_Miktar='"+para+"', Son_Islem='"+zaman+"' where Kimlik="+(t+1)+"";
            komut2 = new OleDbCommand(sorgu2,bağlantı);
            bağlantı.Open();
            komut2.ExecuteNonQuery();
            bağlantı.Close();
        }

        public void banknotgüncelle()
        {
            int y = 5,k=0, kimlik=1;

            for (k = 0; k < 6; k++)
            {
                parasayısı2[y] = parasayısı2[y] - parasayısı[k];
                y--;
            }


            sorgu3 = sorgu2 = "update Para set ikiyüzlük='" + parasayısı2[5] + "', yüzlük='" + parasayısı2[4] + "', ellilik='" + parasayısı2[3] + "', yirmilik='" + parasayısı2[2] + "', onluk='" + parasayısı2[1] + "', beşlik='" + parasayısı2[0] + "' where Kimlik=" + kimlik + " ";
            komut3 = new OleDbCommand(sorgu3,bağlantı);

            bağlantı.Open();
            komut3.ExecuteNonQuery();
            bağlantı.Close();
        }
        public void labeltemizle()
        {
            label2.Text = "?";
            label3.Text = "?";
            label4.Text = "?";
            label5.Text = "?";
            label6.Text = "?";
            label7.Text = "?";
            textBox1.Clear();
        }

        public void paradurumu()
        {
            sorgu = "Select * from Para";
            komut = new OleDbCommand(sorgu, bağlantı);
            kayıt = new OleDbDataAdapter(komut);

            tablo2 = new DataTable();

            bağlantı.Open();
            kayıt.Fill(tablo2);
            bağlantı.Close();

            

            parasayısı2[0] = Convert.ToInt32(tablo2.Rows[0]["beşlik"]);
            parasayısı2[1] = Convert.ToInt32(tablo2.Rows[0]["onluk"]);
            parasayısı2[2] = Convert.ToInt32(tablo2.Rows[0]["yirmilik"]);
            parasayısı2[3] = Convert.ToInt32(tablo2.Rows[0]["ellilik"]);
            parasayısı2[4] = Convert.ToInt32(tablo2.Rows[0]["yüzlük"]);
            parasayısı2[5] = Convert.ToInt32(tablo2.Rows[0]["ikiyüzlük"]);

        }

        #endregion

        # region Tanımlamalar

        int[] parasayısı=new int[6];
        int[] parasayısı2 = new int[6];

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            butonfalse();
            bağlan();
            paradurumu();
            kişibilgileri();
            t = Giriş.i;
            para = Convert.ToInt32(tablo.Rows[t]["Karttaki_Miktar"]);
            label11.Text = tablo.Rows[t]["Karttaki_Miktar"].ToString()+" TL";
            label13.Text = tablo.Rows[t]["Son_Islem"].ToString().Substring(0, 10);
            timer1.Start();
            groupBox1.Visible = false;
            
            label9.Text = Giriş.ad + " " + Giriş.soyad;
            butondurumu();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label15.Text = DateTime.Now.ToLongTimeString();
            label14.Text = DateTime.Now.ToLongDateString();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                alınanpara = Convert.ToInt32(textBox1.Text);
                parayıver(alınanpara);

            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Geçerli Bir Değer Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            alınanpara = int.Parse(button1.Text.Substring(0, 1));
            parayıver(alınanpara);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            parayıver(int.Parse(button4.Text.Substring(0, 3)));
            alınanpara = int.Parse(button4.Text.Substring(0, 2));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            parayıver(int.Parse(button2.Text.Substring(0, 2)));
            alınanpara = int.Parse(button2.Text.Substring(0, 2));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            alınanpara = int.Parse(button5.Text.Substring(0, 3));
            parayıver(alınanpara);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            parayıver(int.Parse(button3.Text.Substring(0, 2)));
            alınanpara = int.Parse(button3.Text.Substring(0, 2));
        }

        private void button6_Click(object sender, EventArgs e)
        {

            parayıver(int.Parse(button6.Text.Substring(0, 3)));
            alınanpara = int.Parse(button6.Text.Substring(0, 3));
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            para = para - alınanpara;
            bağlan();
            paradurumu();
            paragüncelle();
            kişibilgileri();
            para = Convert.ToInt32(tablo.Rows[t]["Karttaki_Miktar"]);
            label11.Text = tablo.Rows[t]["Karttaki_Miktar"].ToString() + " TL";
            label13.Text = tablo.Rows[t]["Son_Islem"].ToString();
            butonfalse();
            butondurumu();
            labeltemizle();

            banknotgüncelle();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
