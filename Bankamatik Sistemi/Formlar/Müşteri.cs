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
    public partial class Müşteri : Form
    {
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

        #endregion

        #region Değişkenler

        string yol = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=İşBankası.accdb";
        string sorgu;
        OleDbConnection bağlantı;
        OleDbCommand komut;
        OleDbDataAdapter kayıt;
        DataTable tablo;

        #endregion

        #region Tanımlamalar
        Form1 nfrm2 = new Form1();
        Parayatır nfrm3 = new Parayatır();

        #endregion

      

        public Müşteri()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            nfrm3.ShowDialog();
            this.Show();
        }

        private void Müşteri_Load(object sender, EventArgs e)
        {
            bağlan();
            kişibilgileri();
            int t = Giriş.i;
            timer1.Start();
            label9.Text = Giriş.ad + " " + Giriş.soyad;
            label11.Text = tablo.Rows[t]["Karttaki_Miktar"].ToString() + " TL";
            label13.Text = tablo.Rows[t]["Son_Islem"].ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label15.Text = DateTime.Now.ToLongTimeString();
            label1.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            nfrm2.ShowDialog();
            this.Show();
        }
    }
}
