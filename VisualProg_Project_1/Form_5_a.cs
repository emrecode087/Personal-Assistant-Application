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

                    // NoteIndex değerini al
                    int noteIndex = GetNoteIndex(connection);

                    // Eğer noteIndex 11'e ulaştıysa, sıfırla
                    if (noteIndex >= 14)
                    {
                        noteIndex = 1;
                    }

                    string columnName = $"note_{noteIndex}";
                    string textBoxValue = new_reminder_textBox.Text;
                    string richTextBoxValue = new_reminder_richBox.Text;

                    // Önce var olan veriyi kontrol et
                    if (CheckNoteExistence(connection, columnName))
                    {
                        MessageBox.Show($"Lütfen hatırlatma kutunuzda yer açınız: {columnName}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Notu ekleyin
                        string query = $"UPDATE teachers SET {columnName} = @Note, noteIndex = @NewNoteIndex WHERE name = @TeacherName AND surname = @TeacherSurname";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@TeacherName", "Emre"); // Öğretmen adını burada bir değerle değiştirin
                            command.Parameters.AddWithValue("@TeacherSurname", "Baş"); // Öğretmen soyadını burada bir değerle değiştirin
                            command.Parameters.AddWithValue("@Note", $"{textBoxValue} - {richTextBoxValue}");
                            command.Parameters.AddWithValue("@NewNoteIndex", noteIndex + 1);

                            int affectedRows = command.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                MessageBox.Show($"Not başarıyla eklendi: {columnName}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Not eklenirken bir hata oluştu: {columnName}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"HATA: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckNoteExistence(SqlConnection connection, string columnName)
        {
            // Veritabanında belirli bir sütunun değerinin NULL olup olmadığını kontrol et
            string query = $"SELECT {columnName} FROM teachers WHERE name = @TeacherName AND surname = @TeacherSurname";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TeacherName", "Emre"); // Öğretmen adını burada bir değerle değiştirin
                command.Parameters.AddWithValue("@TeacherSurname", "Baş"); // Öğretmen soyadını burada bir değerle değiştirin

                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value;
            }
        }

        private int GetNoteIndex(SqlConnection connection)
        {
            // Veritabanındaki noteIndex değerini al
            string query = $"SELECT noteIndex FROM teachers WHERE name = @TeacherName AND surname = @TeacherSurname";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TeacherName", "Emre"); // Öğretmen adını burada bir değerle değiştirin
                command.Parameters.AddWithValue("@TeacherSurname", "Baş"); // Öğretmen soyadını burada bir değerle değiştirin

                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 1;
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
