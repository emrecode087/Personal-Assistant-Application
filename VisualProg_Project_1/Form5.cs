using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            PopulateListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateListView()
        {
            listView1.Items.Clear();

            try
            {
                string connectionString = @"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Öğretmen adı ve soyadı burada bir değerle değiştirilmelidir.
                    string teacherName = "Emre";
                    string teacherSurname = "Baş";

                    string query = $"SELECT note_1, note_2, note_3, note_4, note_5, note_6, note_7, note_8, note_9, note_10, note_11, note_12, note_13, note_14 FROM teachers WHERE name = @TeacherName AND surname = @TeacherSurname";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TeacherName", teacherName);
                        command.Parameters.AddWithValue("@TeacherSurname", teacherSurname);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Sütun adlarını ve değerlerini bir koleksiyona ekle
                            List<Tuple<int, string>> columnData = new List<Tuple<int, string>>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                string columnValue = Convert.ToString(reader[columnName]);

                                // Sayıyı ayıkla
                                int noteNumber = int.Parse(columnName.Split('_')[1]);

                                columnData.Add(new Tuple<int, string>(noteNumber, columnValue));
                            }

                            // Koleksiyonu sırala
                            columnData.Sort((x, y) => x.Item1.CompareTo(y.Item1));

                            // Listview'e sıralı sütun adlarını ve değerlerini ekle
                            foreach (var data in columnData)
                            {
                                string columnName = $"note_{data.Item1}";
                                ListViewItem item = new ListViewItem(columnName);
                                item.SubItems.Add(data.Item2);
                                listView1.Items.Add(item);
                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"HATA: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool move;
        int mouse_x;
        int mouse_y;
        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form5_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form5_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_5_a form = new Form_5_a();
            form.ShowDialog();
            PopulateListView(); // Form_5_a kapatıldıktan sonra listeyi güncelle
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];
                string columnName = selected.Text;

                try
                {
                    string connectionString = @"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Öğretmen adı ve soyadı burada bir değerle değiştirilmelidir.
                        string teacherName = "Emre";
                        string teacherSurname = "Baş";

                        string query = $"UPDATE teachers SET {columnName} = NULL WHERE name = @TeacherName AND surname = @TeacherSurname";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@TeacherName", teacherName);
                            command.Parameters.AddWithValue("@TeacherSurname", teacherSurname);

                            int affectedRows = command.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                MessageBox.Show($"Not başarıyla silindi: {columnName}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PopulateListView(); // Listeyi güncelle
                            }
                            else
                            {
                                MessageBox.Show($"Not silinirken bir hata oluştu: {columnName}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"HATA: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz notu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
