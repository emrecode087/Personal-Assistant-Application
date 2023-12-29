using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form3 : Form
    {
        private string selectedClass;

        public Form3()
        {
            InitializeComponent();
            FillComboBoxWithCourseNames(); // ComboBox'ı doldur
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form3 = new Form2();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void UpdateStudentStatus(string status, bool incrementPlusPoint)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedStudent = listView1.SelectedItems[0];
                string selectedClass = comboBox1.SelectedItem?.ToString();

                if (selectedClass != null)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                        {
                            connection.Open();

                            string query;

                            if (incrementPlusPoint)
                                query = $"UPDATE {selectedClass} SET plusPoint = plusPoint + 1 WHERE id = @studentID";
                            else
                                query = $"UPDATE {selectedClass} SET situation = situation + 1 WHERE id = @studentID";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@studentID", selectedStudent.SubItems[0].Text);

                                int affectedRows = command.ExecuteNonQuery();

                                if (affectedRows > 0)
                                {
                                    MessageBox.Show("Öğrenci durumu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    // Durumu güncellendikten sonra ListView'ı yenile
                                    insertListView(selectedClass);
                                }
                                else
                                {
                                    MessageBox.Show("Öğrenci durumu güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Öğrenci durumu güncellenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir sınıf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void student_not_here_Click(object sender, EventArgs e)
        {
            UpdateStudentStatus("Not Here", false);
        }

        private void student_plus_Point_Click(object sender, EventArgs e)
        {
            UpdateStudentStatus("", true);
        }

        private void list_button_Click(object sender, EventArgs e)
        {
            // ComboBox'tan seçilen sınıfa ait öğrencileri listele
            selectedClass = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedClass))
            {
                MessageBox.Show("Lütfen bir sınıf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ListView'ı temizle ve seçilen sınıfa ait öğrencileri ekle
            insertListView(selectedClass);
        }

        private void insertListView(string selectedClass)
        {
            listView1.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {selectedClass}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["id"].ToString());
                                item.SubItems.Add(reader["name"].ToString());
                                item.SubItems.Add(reader["surname"].ToString());
                                item.SubItems.Add(reader["situation"].ToString());
                                item.SubItems.Add(reader["plusPoint"].ToString());

                                // Eğer "number of absences" (4. kolon) 4'ten büyükse "FAIL" yaz, değilse "NORMAL" yaz
                                int numberOfAbsences = Convert.ToInt32(reader["situation"]);
                                string absenceStatus = (numberOfAbsences > 4) ? "FAIL" : "NORMAL";
                                item.SubItems.Add(absenceStatus);

                                listView1.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Öğrenciler çekilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void search_Student_Click(object sender, EventArgs e)
        {
            string searchName = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchName))
            {
                MessageBox.Show("Lütfen bir isim girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the selected class is not null
            if (comboBox1.SelectedItem != null)
            {
                string selectedClass = comboBox1.SelectedItem.ToString();

                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True"))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM {selectedClass} WHERE name LIKE @searchName";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@searchName", $"%{searchName}%");

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                listView1.Items.Clear();

                                while (reader.Read())
                                {
                                    ListViewItem item = new ListViewItem(reader["id"].ToString());
                                    item.SubItems.Add(reader["name"].ToString());
                                    item.SubItems.Add(reader["surname"].ToString());
                                    item.SubItems.Add(reader["situation"].ToString());
                                    item.SubItems.Add(reader["plusPoint"].ToString());

                                    listView1.Items.Add(item);
                                }

                                if (listView1.Items.Count == 0)
                                {
                                    MessageBox.Show("Belirtilen isme sahip öğrenci bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Öğrenci aranırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir sınıf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Check if the text in textBox1 is empty, if so, display all students again
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                // Display all students for the selected class
                list_button_Click(sender, e);
            }
        }

        private bool firstClick = true;
        private void finish_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
            }
            else 
            {
                if(comboBox1.SelectedItem == null || comboBox2.SelectedItem == null) 
                {
                    MessageBox.Show("You did not start the class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Week is completed ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 form3 = new Form2();
                    form3.Show();
                    this.Hide();
                }
                
            }
            
        }
    }
}
