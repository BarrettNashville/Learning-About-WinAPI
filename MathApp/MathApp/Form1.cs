using System;
using System.Windows.Forms;

namespace MathApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cbxOperation.SelectedIndex = 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int num1 = Int32.Parse(txtNum1.Text);
                int num2 = Int32.Parse(txtNum2.Text);

                int operation = cbxOperation.SelectedIndex;
                switch (operation)
                {
                    case 0:
                        lblResult.Text = (num1 + num2).ToString();
                        break;
                    case 1:
                        lblResult.Text = (num1 - num2).ToString();
                        break;
                    case 2:
                        if (num2 == 0)
                        {
                            lblResult.Text = "Cannot divide by zero";
                            break;
                        }

                        lblResult.Text = (num1 / num2).ToString();
                        break;
                    case 3:
                        lblResult.Text = (num1 * num2).ToString();
                        break;
                }

            }
            catch (Exception ex)
            {
                lblResult.Text = "Please enter an integer into each text box";
            }
        }


        private void txtNum1_MouseDown(object sender, MouseEventArgs e)
        {
            txtNum1.Text = "";
        }

        private void txtNum2_MouseDown(object sender, MouseEventArgs e)
        {
            txtNum2.Text = "";
        }
    }
}
