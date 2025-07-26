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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace final_Project
{
    public partial class Assign_Staff_Task : Form
    {
        public Assign_Staff_Task()
        {
            InitializeComponent();
        }

        private void Assign_Staff_Task_Load(object sender, EventArgs e)
        {
            load_rooms();
            load_staff_email();
            load_room_assignment();
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        void load_staff_email()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT EMAIL FROM USER_TABLE  WHERE USER_TYPE LIKE 'STAFF'  ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                string email= rdr["EMAIL"].ToString();
                cmemail.Items.Add(email);
            }
        }

        void load_rooms()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT ROOM_ID FROM ROOM_TABLE ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string rId = rdr["ROOM_ID"].ToString();
                cmroom.Items.Add(rId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            SqlConnection con = GetConnection();
            string Id = " ";
            string IdQuerey = "SELECT ISNULL(MAX(ASSIGNMENT_ID), 0) + 1 FROM ROOM_ASSIGNMENTS";
            SqlCommand cmd1 = new SqlCommand(IdQuerey, con);
            Id = ((int)cmd1.ExecuteScalar()).ToString();
            Id = "00" + Id;


            string s = $"INSERT INTO ROOM_ASSIGNMENTS VALUES ('" + Id + "','" + cmroom.Text + "','" + today.ToString("yyyy-MM-dd") + "','" + cmemail.Text + "','DIRTY')";
            SqlCommand cmd2 = new SqlCommand(s, con);
            cmd2.ExecuteNonQuery();
            load_room_assignment();
        }
        private Label CreateHeaderLabel(string text, int left, int fontSize)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                AutoSize = true,
                Left = left,
                Top = 10
            };
        }

        private Label CreateDataLabel(string text, int left, int fontSize)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, FontStyle.Regular),
                AutoSize = true,
                Left = left,
                Top = 8
            };
        }


        void load_room_assignment()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT ASSIGNMENT_ID, ROOM_ID, ASSIGNED_DATE, EMAIL, ROOM_STATUS FROM ROOM_ASSIGNMENTS ORDER BY ASSIGNMENT_ID DESC";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            flowLayoutPanel1.Controls.Clear();

            // Create header
            Panel headerPanel = new Panel
            {
                Width = 780,
                Height = 40,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(5)
            };

            headerPanel.Controls.Add(CreateHeaderLabel("Assign ID", 0, 14));
            headerPanel.Controls.Add(CreateHeaderLabel("Room ID", 120, 14));
            headerPanel.Controls.Add(CreateHeaderLabel("Assigned Date", 240, 14));
            headerPanel.Controls.Add(CreateHeaderLabel("Email", 440, 14));
            headerPanel.Controls.Add(CreateHeaderLabel("Room Status", 615, 14));

            flowLayoutPanel1.Controls.Add(headerPanel);

            // Loop through records
            while (rdr.Read())
            {
                string aId = rdr["ASSIGNMENT_ID"].ToString();
                string rId = rdr["ROOM_ID"].ToString();
                string email = rdr["EMAIL"].ToString();
                string aDate = Convert.ToDateTime(rdr["ASSIGNED_DATE"]).ToString("yyyy-MM-dd");
                string rStatus = rdr["ROOM_STATUS"].ToString();

                Panel taskPanel = new Panel
                {
                    Width = 780,
                    Height = 35,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(5)
                };

                taskPanel.Controls.Add(CreateDataLabel(aId, 10, 12));
                taskPanel.Controls.Add(CreateDataLabel(rId, 130, 12));
                taskPanel.Controls.Add(CreateDataLabel(aDate, 250, 12));
                taskPanel.Controls.Add(CreateDataLabel(email, 415, 12));
                taskPanel.Controls.Add(CreateDataLabel(rStatus, 635, 12));

                flowLayoutPanel1.Controls.Add(taskPanel);
            }

            rdr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
            this.Visible = false;
        }
    }
}
