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
    public partial class Register : Form
    {
        
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //for Name
            if (Regex.IsMatch(txtName.Text, "^[a-zA-Z]", RegexOptions.IgnoreCase) != true)
            {
                MessageBox.Show("Name must be alphabet characters!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }
            
            //for password
            if (Regex.IsMatch(txtPass.Text, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", RegexOptions.IgnoreCase) != true)
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
            }
            if (txtPass.Text != txtRepass.Text)
            {
                MessageBox.Show("Password not match!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SqlConnection conn = new SqlConnection(@"Data Source = DESKTOP-7K1R4E8; Initial Catalog = Accessories; Integrated Security = true");
            conn.Open();
            /*MessageBox.Show("Connected!");*/
            SqlCommand cmd = new SqlCommand("select * from Users where email='" + txtEmail.Text + "'", conn);
            /*SqlDataReader dataReader;*/
            cmd.CommandType = CommandType.Text;
            /*cmd.CommandText = "select * from Users where email=" + txtEmail.Text;*/
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                MessageBox.Show("Email has existed!");
                conn.Close();
            }
            else
            {
                conn.Close();
                cmd.CommandText = "Insert into Users(Fullname, Email, Pass) values(@fullname,@email,@pass)";
                cmd.Parameters.Add("@fullname", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
               
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = txtPass.Text;
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    MessageBox.Show("User created!");
                }
                else
                {
                    MessageBox.Show("Invalid Login please check username and password");
                }

                conn.Close();
            }

        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtRepass.Text = "";
        }
    }
}
