using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String username, user_password;

            username = txt_username.Text;
            user_password = txt_password.Text;

            try
            {

                String querry = "SELECT * FROM teachers WHERE username = '" + txt_username.Text + "'AND password= '" + txt_password.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    username = txt_username.Text;
                    user_password = txt_password.Text;

                    
                     
                    //page that needed to be load
                    Form2 form1 = new Form2();
                    form1.Show();
                    this.Hide();
                    
                     
                }
                else
                {
                    MessageBox.Show("Invalid login details", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_password.Clear();
                    txt_username.Clear();

                    txt_username.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally { conn.Close(); }

        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void txt_username_Enter(object sender, EventArgs e)
        {
            if(txt_username.Text == "Username")

            {
                txt_username.Text = "";
                txt_username.ForeColor = Color.Black;
            }
        }

        private void txt_username_Leave(object sender, EventArgs e)
        {
            if(txt_username.Text == "")
            {
                txt_username.Text = "Username";
                txt_username.ForeColor = Color.Black;
            }
        }

        private void txt_password_Enter(object sender, EventArgs e)
        {
            if (txt_password.Text == "Password")

            {
                txt_password.Text = "";
                txt_password.ForeColor = Color.Black;
                txt_password.PasswordChar = '*';
            }
        }

        char? none = null;
        private void txt_password_Leave(object sender, EventArgs e)
        {
            if (txt_password.Text == "")
            {
                txt_password.Text = "Password";
                txt_password.ForeColor = Color.Black;
                txt_password.PasswordChar =Convert.ToChar(none);
            }
        }
    }
}
