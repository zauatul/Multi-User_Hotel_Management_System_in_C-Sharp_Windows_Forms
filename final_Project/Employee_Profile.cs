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
    public partial class Employee_Profile : Form
    {
        string email,type;
        public Employee_Profile()
        {
            InitializeComponent();
        }
        public Employee_Profile(string email,string type)
        {
            this.type = type;
            this.email = email;
            InitializeComponent();
        }

        private void Staff_Profile_Load(object sender, EventArgs e)
        {
            getUser();

            if (type =="staff")
            { labelType.Text = "STAFF"; }
            else if(type == "receptionist")
            { labelType.Text = "RECEPTIONIST"; }
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        void getUser()

        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand($"SELECT NAME,PHONE_NUMBER,EMAIL FROM USER_TABLE WHERE EMAIL LIKE '" + email + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                txtname.Text = reader["NAME"].ToString();

                txtnumber.Text = reader["PHONE_NUMBER"].ToString();

                txtemail.Text = reader["EMAIL"].ToString();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(type == "staff")
            {
                StaffDashboard staffDashboard = new StaffDashboard(email);
                staffDashboard.Show();
                this.Visible = false;
            }
            else if(type == "receptionist")
            {
                Reciptionist reciptionist = new Reciptionist(email);
                reciptionist.Show();
                this.Visible = false;
            }
            
        }
        //Log out button
        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Edit_Info edit_Profilr = new Edit_Info(email,type);
            edit_Profilr.Show();
            this.Visible = false;
        }
    }
}
