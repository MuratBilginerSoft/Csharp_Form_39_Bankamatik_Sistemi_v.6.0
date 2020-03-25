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
    public partial class Parayatır : Form
    {
        #region Değişkenler
        string yol = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=İşBankası.accdb";
        string sorgu,sorgu2;
        OleDbConnection bağlantı;
        OleDbCommand komut,komut2;
        OleDbDataAdapter kayıt;
        DataTable tablo;
        DateTime zaman;
        int t;
        int para;
        #endregion 

        #region Metodlar

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
            komut = new OleDbCommand(sorgu, bağlantı);
            kayıt = new OleDbDataAdapter(komut);
            tablo = new DataTable();

            bağlantı.Open();
            kayıt.Fill(tablo);
            bağlantı.Close();

        }

        public void paraekle()
        {
            zaman = DateTime.Now;
            para = int.Parse(tablo.Rows[t]["Karttaki_Miktar"].ToString())+int.Parse(textBox1.Text);
            sorgu2 = "Update Kişiler set Karttaki_Miktar='" + para + "', Son_Islem='" + zaman + "' where Kimlik=" + Giriş.kartno + "";
            komut2 = new OleDbCommand(sorgu2, bağlantı);
            bağlantı.Open();
            komut2.ExecuteNonQuery();
            bağlantı.Close();
            MessageBox.Show("Başarılı Bir Şekilde İşlem Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion

        public Parayatır()
        {
            InitializeComponent();
        }

        private void Parayatır_Load(object sender, EventArgs e)
        {
            t = Giriş.i;
            bağlan();
            kişibilgileri();
            para = int.Parse(tablo.Rows[t]["Karttaki_Miktar"].ToString());
            label11.Text = para + " TL";
            label13.Text = tablo.Rows[t]["Son_Islem"].ToString().Substring(0, 10);
            timer1.Start();
            label9.Text = Giriş.ad + " " + Giriş.soyad;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label15.Text = DateTime.Now.ToLongTimeString();
            label14.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bağlan();
            paraekle();
            kişibilgileri();
            label11.Text = tablo.Rows[t]["Karttaki_Miktar"].ToString() + " TL";
            label13.Text = tablo.Rows[t]["Son_Islem"].ToString().Substring(0, 10);
        }
    }
}
