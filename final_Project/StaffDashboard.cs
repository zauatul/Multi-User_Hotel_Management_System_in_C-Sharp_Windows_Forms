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
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace final_Project
{
    public partial class StaffDashboard : Form
    {
        string Uemail, roomId, name;

        FlowLayoutPanel flowLayoutPanel2 = new FlowLayoutPanel();

        public StaffDashboard()
        {
            InitializeComponent();
        }
        public StaffDashboard(string email)
        {
            this.Uemail = email;
            InitializeComponent();
        }
        //Apply Leave button
        private void button3_Click(object sender, EventArgs e)
        {
            Staff_LeaveApplication leaveApplication = new Staff_LeaveApplication(Uemail);
            leaveApplication.Show();
            this.Visible = false;

        }
        //button Show Profile
        private void button4_Click(object sender, EventArgs e)
        {
            Employee_Profile profile = new Employee_Profile(Uemail,"staff");
            profile.Show();
            this.Visible=false;
        }
        //My Task button
        private void button2_Click_1(object sender, EventArgs e)
        {
            showMYTask showMYTask = new showMYTask(Uemail);
            showMYTask.Show();
            this.Visible = false;
            //MessageBox.Show(Uemail);
        }
        //search button
        private void btnsearch_Click(object sender, EventArgs e)
        {
            string searchedName = txtsearch.Text;
            load_room_assignments(searchedName);
        }
        //Dashboard button
        private void button1_Click_1(object sender, EventArgs e)
        {
            load_room_assignments();
        }

        private void StaffDashboard_Load(object sender, EventArgs e)
        {
            load_room_assignments();
            getUser();
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
            SqlCommand cmd = new SqlCommand($"SELECT NAME FROM USER_TABLE WHERE EMAIL LIKE '" + Uemail + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtuser.Text = reader["NAME"].ToString();
            }
        }
        //load all task for all from DB
        void load_room_assignments(string nameFilter = "")
        {
            flowLayoutPanel1.Controls.Clear();
            string source = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string q = @"SELECT RA.ASSIGNMENT_ID, RA.ROOM_ID, RA.ASSIGNED_DATE, RA.EMAIL, RA.ROOM_STATUS, UT.NAME 
             FROM ROOM_ASSIGNMENTS RA
             JOIN USER_TABLE UT ON RA.EMAIL = UT.EMAIL
             WHERE UT.NAME LIKE @nameFilter";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@nameFilter", "%" + nameFilter + "%");
            SqlDataReader rdr = cmd.ExecuteReader();

            Panel headerPanel = new Panel
            {
                Width = 650,
                Height = 50,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10)
            };
            Label NameLabel = new Label
            {
                Text = "NAME",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 20
            };
            Label EmailLabel = new Label
            {
                Text = "EMAIL",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 170
            };
            Label RoomLabel = new Label
            {
                Text = "ROOM NO",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 300
            };
            Label StatusLabel = new Label
            {
                Text = "ROOM STATUS",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 470
            };
            Panel bottomBorder = new Panel
            {
                Height = 2,                   // Border thickness
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray        // Border color
            };
            headerPanel.Controls.Add(NameLabel);
            headerPanel.Controls.Add(EmailLabel);
            headerPanel.Controls.Add(RoomLabel);
            headerPanel.Controls.Add(StatusLabel);
            headerPanel.Controls.Add(bottomBorder);

            flowLayoutPanel1.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                roomId = rdr["ROOM_ID"].ToString();
                string email = rdr["EMAIL"].ToString();
                string status= rdr["ROOM_STATUS"].ToString();
                 name = rdr["NAME"].ToString();

                

                Panel taskPanel = new Panel
                {
                    Width = 650,
                    Height = 50,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(10)
                };
                Label nameLabel = new Label
                {
                    Text = name,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 30
                };
                Label emailLabel = new Label
                {
                    Text = email,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 130
                };
                Label roomLabel = new Label
                {
                    Text = roomId,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 350
                };
                Label statusLabel = new Label
                {
                    Text = status,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 470
                };
                taskPanel.Controls.Add(nameLabel);
                taskPanel.Controls.Add(emailLabel);
                taskPanel.Controls.Add(roomLabel);
                taskPanel.Controls.Add(statusLabel);

                flowLayoutPanel1.Controls.Add(taskPanel);
            }
        }
        
        
    }
}
