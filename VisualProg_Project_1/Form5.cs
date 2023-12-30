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

                    // Öğrenci notları verilerini çeken sorgu
                    string query = "SELECT subject, noteText FROM tch_note";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Her bir notu ListView'a ekleyin
                                ListViewItem item = new ListViewItem(reader["subject"].ToString());
                                item.SubItems.Add(reader["noteText"].ToString());

                                listView1.Items.Add(item);
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
            // Eğer ListView'da seçili bir öğe varsa devam et
            if (listView1.SelectedItems.Count > 0)
            {
                // Seçili öğeyi al
                ListViewItem selectedNote = listView1.SelectedItems[0];

                try
                {
                    string connectionString = @"Data Source=EXCALIBUR\SQLEXPRESS;Initial Catalog=projectDataBase;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Seçili notu veritabanından silen sorgu
                        string query = "DELETE FROM tch_note WHERE subject = @subject AND noteText = @noteText";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Parametreleri ekleyin
                            command.Parameters.AddWithValue("@subject", selectedNote.SubItems[0].Text);
                            command.Parameters.AddWithValue("@noteText", selectedNote.SubItems[1].Text);

                            // Sorguyu çalıştırın
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Not deleted", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // ListView'ı güncelleyin
                                PopulateListView();
                            }
                            else
                            {
                                MessageBox.Show("Error", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please choose a note", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
