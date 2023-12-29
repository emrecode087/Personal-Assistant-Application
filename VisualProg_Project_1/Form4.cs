using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace VisualProg_Project_1
{
    public partial class Form4 : Form
    {
        private string selectedClass;

        public Form4()
        {
            InitializeComponent();
            FillComboBoxWithCourseNames(); // ComboBox'ı doldur
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        void tabloGetir()
        {
            try
            {
                if (selectedClass != null)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                    {
                        connection.Open();

                        // İhtiyacınıza göre sütunları belirleyin
                        string selectedColumns = "id, name, surname, midterm1, midterm2, final, plusPoint";
                        string query = $"SELECT {selectedColumns} FROM {selectedClass}";

                        SqlDataAdapter sda = new SqlDataAdapter(query, connection);

                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboBoxWithCourseNames()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT class_1, class_2, class_3, class_4, class_5 FROM teachers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 1; i <= 5; i++)
                                {
                                    string className = reader[$"class_{i}"].ToString();
                                    if (!string.IsNullOrEmpty(className))
                                        comboBox1.Items.Add(className);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComboBox doldurulurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form4 = new Form2();
            form4.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox'tan seçilen sınıfı güncelle
            selectedClass = comboBox1.SelectedItem?.ToString();

            // Seçilen sınıfa göre DataGridView'ı güncelle
            tabloGetir();
        }

        private void list_button_Click(object sender, EventArgs e)
        {
            try
            {
                // ComboBox'tan seçilen sınıfı güncelle
                selectedClass = comboBox1.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedClass))
                {
                    // Seçilen sınıfa göre DataGridView'ı güncelle
                    tabloGetir();
                }
                else
                {
                    MessageBox.Show("Lütfen bir sınıf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler getirilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                {
                    connection.Open();

                    // DataGridView'da yapılan değişiklikleri al
                    DataTable changedDataTable = ((DataTable)dataGridView1.DataSource).GetChanges();

                    if (changedDataTable != null)
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT id, name, surname, midterm1, midterm2, final, plusPoint FROM {selectedClass}", connection))
                        {
                            // UPDATE sorgusu oluştur
                            string updateCommand = $"UPDATE {selectedClass} SET name = @name, surname = @surname, midterm1 = @midterm1, midterm2 = @midterm2, final = @final, plusPoint = @plusPoint WHERE id = @id";

                            adapter.UpdateCommand = new SqlCommand(updateCommand, connection);
                            adapter.UpdateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
                            adapter.UpdateCommand.Parameters.Add("@surname", SqlDbType.NVarChar, 50, "surname");
                            adapter.UpdateCommand.Parameters.Add("@midterm1", SqlDbType.Int, 0, "midterm1");
                            adapter.UpdateCommand.Parameters.Add("@midterm2", SqlDbType.Int, 0, "midterm2");
                            adapter.UpdateCommand.Parameters.Add("@final", SqlDbType.Int, 0, "final");
                            adapter.UpdateCommand.Parameters.Add("@plusPoint", SqlDbType.Int, 0, "plusPoint");
                            adapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original; // Anahtar sütunu belirt

                            // DataGridView'daki değişiklikleri veritabanına uygula
                            adapter.Update(changedDataTable);

                            // Değişiklikleri onayla
                            ((DataTable)dataGridView1.DataSource).AcceptChanges();
                        }

                        MessageBox.Show("Değişiklikler başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Herhangi bir değişiklik yapılmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Hücre tıklama olayını kontrol et
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // DataGridView'da seçilen satırdaki bilgileri TextBox'lara aktar
                txtName.Text = row.Cells["name"].Value.ToString();
                txtSurname.Text = row.Cells["surname"].Value.ToString();
                dataGridMidterm1.Text = row.Cells["midterm1"].Value.ToString();
                dataGridMidterm2.Text = row.Cells["midterm2"].Value.ToString();
                dataGridFinal.Text = row.Cells["final"].Value.ToString();

                // Eğer midterm1, midterm2 ve final textbox'ları doluysa
                if (!string.IsNullOrEmpty(dataGridMidterm1.Text) && !string.IsNullOrEmpty(dataGridMidterm2.Text) && !string.IsNullOrEmpty(dataGridFinal.Text))
                {
                    // Notları al
                    double midterm1 = Convert.ToDouble(dataGridMidterm1.Text);
                    double midterm2 = Convert.ToDouble(dataGridMidterm2.Text);
                    double final = Convert.ToDouble(dataGridFinal.Text);

                    // Yıl sonu notunu hesapla (%30 midterm1, %30 midterm2, %40 final)
                    double yearEndGrade = (midterm1 * 0.3) + (midterm2 * 0.3) + (final * 0.4);

                    // Yıl sonu notunu TextBox'a yaz
                    txt_year_and_average.Text = yearEndGrade.ToString();

                    // Öğrencinin devamsızlık durumunu al
                    string studentName = txtName.Text; // veya farklı bir TextBox kullanarak alabilirsiniz
                    string className = selectedClass;
                    string situation = GetSituationForStudent(studentName, className);

                    // Devamsızlık durumunu kontrol et ve gerekirse "FAIL" olarak güncelle
                    if (situation == "FAIL")
                    {
                        txt_absenceStatus.Text = "FAIL";
                        txt_absenceStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        txt_absenceStatus.Text = "NORMAL";
                        txt_absenceStatus.ForeColor = Color.Black;
                    }

                    // Yıl sonu notuna göre harf notunu hesapla ve Label'a yazdır
                    string letterGrade = GetLetterGrade(yearEndGrade, situation);
                    label_result.Text = $"Letter Grade: {letterGrade}";
                }
                else
                {
                    // Herhangi bir not eksikse TextBox'ları ve Label'ı temizle
                    txt_year_and_average.Clear();
                    txt_absenceStatus.Clear();
                    label_result.Text = "";
                }
            }
        }

        private string GetLetterGrade(double yearEndGrade, string situation)
        {
            if (situation == "FAIL")
            {
                // Devamsızlık durumu "FAIL" ise harf notu gösterme
                return "-";
            }
            else if (yearEndGrade < 50) return "FF - succeeded";
            else if (yearEndGrade < 55) return "DC - succeeded";
            else if (yearEndGrade < 60) return "DD - succeeded";
            else if (yearEndGrade < 70) return "CC - succeeded";
            else if (yearEndGrade < 75) return "CB - succeeded";
            else if (yearEndGrade < 80) return "BB - succeeded";
            else if (yearEndGrade < 85) return "BA - succeeded";
            else if (yearEndGrade <= 100) return "AA - succeeded";
            else return "Geçersiz";
        }


        private string GetSituationForStudent(string studentName, string className)
        {
            string situation = "NORMAL"; // Varsayılan değer

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                {
                    connection.Open();

                    // Öğrencinin devamsızlık sayısını sorgula
                    string query = $"SELECT situation FROM {className} WHERE name = @studentName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentName", studentName);
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Veritabanında situation değeri varsa onu al
                            situation = result.ToString();

                            // Devamsızlık durumunu kontrol et ve gerekirse "FAIL" olarak güncelle
                            int absenceCount = Convert.ToInt32(situation);
                            if (absenceCount > 4)
                            {
                                situation = "FAIL";
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return situation;
        }
        bool move;
        int mouse_x;
        int mouse_y;

        private void Form4_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form4_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form4_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }
    }
}
