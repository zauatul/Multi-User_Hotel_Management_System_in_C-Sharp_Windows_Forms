using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_Project
{
    public partial class Forgot_Password : Form
    {
        public Forgot_Password()
        {
            InitializeComponent();
        }

        private void Forgot_Password_Load(object sender, EventArgs e)
        {
            AddBottomBorder(txtemail);
            AddBottomBorder(txtnumber);
            AddBottomBorder(txtpassword);
        }
        private void AddBottomBorder(TextBox textBox)
        {

            Label bottomBorder = new Label();
            bottomBorder.Height = 3;
            bottomBorder.Dock = DockStyle.Bottom;
            bottomBorder.BackColor = Color.Black; 

            textBox.Controls.Add(bottomBorder);
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        //Confirm button
        private void button1_Click(object sender, EventArgs e)
        {
            string number="";
            SqlConnection conn= GetConnection();
                SqlCommand cmd = new SqlCommand("SELECT PHONE_NUMBER FROM USER_TABLE  WHERE EMAIL LIKE '" + txtemail.Text + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    number = reader["PHONE_NUMBER"].ToString();
                }
            
            if(number == txtnumber.Text)
            {
                updatePassword();
            }
            else
            {
                MessageBox.Show("Invalid Information");
            }
        }
        void updatePassword()
        {
            SqlConnection conn = GetConnection();
            string q = "UPDATE USER_TABLE SET PASSWORD='"+txtpassword.Text+"' WHERE EMAIL LIKE '"+txtemail.Text+"'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
            login();
        }
        void login()
        {
            Login login = new Login();
            login.Show();
            this.Visible = false;
        }
        //Back button
        private void button2_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}
