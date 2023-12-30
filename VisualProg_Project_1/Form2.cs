using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form2 : Form
    {
        
        private Timer timer;
        public Form2()
        {
            InitializeComponent();
            LoadTeacherData(); // Sayfa yüklendiğinde öğretmen verilerini yükle
            

            // Timer'ı oluştur
            timer = new Timer();
            timer.Interval = 1000; // 1000 milisaniye (1 saniye) interval
            timer.Tick += Timer_Tick;

            // Timer'ı başlat
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Anlık saat ve tarih bilgisini al
            DateTime currentTime = DateTime.Now;

            // Saati label'a ekle
            time_label.Text = currentTime.ToString("HH:mm:ss");

            // Tarihi label'a ekle
            date_label.Text = currentTime.ToString("dd.MM.yyyy");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadTeacherData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                {
                    connection.Open();

                    // Öğretmen verilerini almak için bir sorgu yap
                    string query = "SELECT TOP 1 name, surname, e_mail FROM teachers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        // Eğer veri varsa, label'lara aktar
                        if (reader.Read())
                        {
                            name_label.Text = reader["name"].ToString();
                            surname_label.Text = reader["surname"].ToString();
                            e_mail_label.Text = reader["e_mail"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool firstClick_1 = true;
        private DateTime lastClickTime;
        private void button2_Click(object sender, EventArgs e)
        {
            if (firstClick_1)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick_1 = false;
                lastClickTime = DateTime.Now;
            }
            else
            {
                // İkinci tıklamada, bir saniye içinde olmalı
                TimeSpan timeSinceLastClick = DateTime.Now - lastClickTime;

                if (timeSinceLastClick.TotalSeconds <= 1)
                {
                    // Sayfayı geçiş yap
                    Form3 form2 = new Form3();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    // Reset for the next first click
                    firstClick_1 = true;
                }
            }
        }
        private bool firstClick_2 = true;
        private void button3_Click(object sender, EventArgs e)
        {
            if (firstClick_2)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick_2 = false;
                lastClickTime = DateTime.Now;
            }
            else
            {
                // İkinci tıklamada, bir saniye içinde olmalı
                TimeSpan timeSinceLastClick = DateTime.Now - lastClickTime;

                if (timeSinceLastClick.TotalSeconds <= 1)
                {
                    // Sayfayı geçiş yap
                    Form4 form = new Form4();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    // Reset for the next first click
                    firstClick_2 = true;
                }
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (firstClick_2)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick_2 = false;
                lastClickTime = DateTime.Now;
            }
            else
            {
                // İkinci tıklamada, bir saniye içinde olmalı
                TimeSpan timeSinceLastClick = DateTime.Now - lastClickTime;

                if (timeSinceLastClick.TotalSeconds <= 1)
                {
                    // Sayfayı geçiş yap
                    Form5 form2 = new Form5();
                    form2.Show();
                    //this.Hide();
                }
                else
                {
                    // Reset for the next first click
                    firstClick_2 = true;
                }
            }
        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (firstClick_2)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick_2 = false;
                lastClickTime = DateTime.Now;
            }
            else
            {
                // İkinci tıklamada, bir saniye içinde olmalı
                TimeSpan timeSinceLastClick = DateTime.Now - lastClickTime;

                if (timeSinceLastClick.TotalSeconds <= 1)
                {
                    // Bir saniye içinde ikinci tıklama oldu, kullanıcıya sor
                    DialogResult result = MessageBox.Show("Do you want to quit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Kullanıcı "Yes" dedi, sayfayı geçiş yap
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Close();
                    }
                    else
                    {
                        // Kullanıcı "No" dedi, reset for the next first click
                        firstClick_2 = true;
                    }
                }
                else
                {
                    // Reset for the next first click
                    firstClick_2 = true;
                }
            }


        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            if (firstClick_2)
            {
                // İlk tıklamada sadece değişkeni güncelle, sayfayı geçiş yapma
                firstClick_2 = false;
                lastClickTime = DateTime.Now;
            }
            else
            {
                // İkinci tıklamada, bir saniye içinde olmalı
                TimeSpan timeSinceLastClick = DateTime.Now - lastClickTime;

                if (timeSinceLastClick.TotalSeconds <= 1)
                {

                    // Sayfayı geçiş yap
                    Form6 form = new Form6();
                    form.Show();
                    //this.Hide();
                }
                else
                {
                    // Reset for the next first click
                    firstClick_2 = true;
                }
            }

        }
    }
}
