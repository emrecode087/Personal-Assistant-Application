using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool firstClick = true;

        private void button2_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick = false;
            }
            else
            {
                // İkinci tıklamada sayfayı geçiş yap
                Form3 form2 = new Form3();
                form2.Show();
                this.Hide();
            }
        }
    }
}
