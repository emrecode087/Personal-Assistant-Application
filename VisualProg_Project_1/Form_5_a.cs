using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form_5_a : Form
    {
        public Form_5_a()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Önce sorgunuzu oluşturun
                    string insertQuery = "INSERT INTO tch_note (subject, NoteText) VALUES (@subject, @noteText)";

                    // Sonra SqlCommand nesnesi oluşturun ve parametreleri ekleyin
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@subject", new_reminder_textBox.Text);
                        command.Parameters.AddWithValue("@noteText", new_reminder_richBox.Text);

                        // Sorguyu çalıştırın
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Note added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("There is a problem ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"HATA: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool move;
        int mouse_x;
        int mouse_y;

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }
    }
}
