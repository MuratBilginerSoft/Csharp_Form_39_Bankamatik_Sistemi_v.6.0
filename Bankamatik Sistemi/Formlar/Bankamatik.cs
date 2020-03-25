using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bankamatik_Sistemi
{
    public partial class Bankamatik : Form
    {
        #region Değişkenler
        int i = 1;
        #endregion

        public Bankamatik()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "İşleminiz Gerçekleştiriliyor...";
            button1.Enabled = false;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             if (i < 13)
             {
                 pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + i + ".jpg");
                 i++;
             }

             else
             {
                 timer1.Stop();
                 Giriş grfrm = new Giriş();
                 this.Hide();
                 grfrm.ShowDialog();
                 this.Show();
             }
        }

        private void Bankamatik_Load(object sender, EventArgs e)
        {

        }
    }
}
