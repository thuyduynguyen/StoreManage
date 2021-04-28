using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace StoreManage
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            //for password
            /*if (Regex.IsMatch(txtPass.Text, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", RegexOptions.IgnoreCase) != true)
            {
                MessageBox.Show("Password must be atleast 8 to 15 characters. It contains atleast one Upper case and numbers.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPass.Focus();
                return;
            }
            //for Email Address
            if (Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase) != true)
            {
                MessageBox.Show("Invalid Email Address!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
                return;
            }*/

            SqlConnection conn = new SqlConnection(@"Data Source = DESKTOP-7K1R4E8; Initial Catalog = Accessories; Integrated Security = true");
            conn.Open();
            /*SqlCommand cmd = new SqlCommand("select * from Users where email='" + txtMail.Text +"' and pass ='"+ txtPw +"'", conn);*/
            SqlCommand cmd = new SqlCommand("select * from Users where email='" + txtEmail.Text + "' and pass ='" + txtPass.Text + "' ", conn);
            cmd.CommandType = CommandType.Text;
            
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                MessageBox.Show("Login sucessfully!");
                conn.Close();
                this.Hide();
                Home home = new Home();
                home.Show();
            }
            else
            {
                MessageBox.Show("Account has not exist!Please sign up!");
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtPass.Text = "";
        }
    }
}
