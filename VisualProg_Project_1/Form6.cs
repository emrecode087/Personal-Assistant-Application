using System;
using System.Windows.Forms;

namespace VisualProg_Project_1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            
        }

        float num1;
        float num2;
        string option;
        float result;

        private void button20_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + btn_0.Text;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtTotal.Clear();
            num1 = 0;
            num2 = 0;
            option = string.Empty;
        }

        private void button_1_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_1.Text;
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_2.Text;
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_3.Text;
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_4.Text;
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_5.Text;
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_6.Text;
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_7.Text;
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_8.Text;
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text + button_9.Text;
        }

        private void btn_equals_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                num2 = float.Parse(txtTotal.Text);               
                float result = 0;

                switch (option)
                {
                    case "+":
                        result = num1 + num2;
                        
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method without performing the calculation
                        }
                        break;
                    
                }

                txtTotal.Text = result.ToString();
            }
        }


        private void btn_divide_Click(object sender, EventArgs e)
        {
            option = "/";
            num1 = float.Parse(txtTotal.Text);

            txtTotal.Clear();
        }

        private void btn_multiply_Click(object sender, EventArgs e)
        {
            option = "*";
            num1 = float.Parse(txtTotal.Text);

            txtTotal.Clear();
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            option = "-";
            num1 = float.Parse(txtTotal.Text);

            txtTotal.Clear();
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            option = "+";
            num1 = float.Parse(txtTotal.Text);

            txtTotal.Clear();
        }

        private void btn_percent_Click(object sender, EventArgs e)
        {
            option = "%";
            num1 = float.Parse(txtTotal.Text); // Convert to a percentage and store
            result = (float)(num1 / 100); // Calculate percentage (e.g., 10% of 50)
            txtTotal.Text = result.ToString();
        }


        private void btn_decimal_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text == "0" )
            {
                txtTotal.Text = "0";
            }
            else if (!txtTotal.Text.Contains(","))
            {
                txtTotal.Text += ",";
            }
        }
    }
}
