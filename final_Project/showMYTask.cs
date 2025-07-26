using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_Project
{
    public partial class showMYTask : Form
    {
        string email;
        public showMYTask()
        {
            InitializeComponent();
        }
        public showMYTask(string email)
        {
            InitializeComponent();
            this.email=email;
        }

        private void showMYTask_Load(object sender, EventArgs e)
        {
            loadmytask(email);
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        //load own task
        void loadmytask(string myEmail)
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT ROOM_ID,ROOM_STATUS
                 FROM ROOM_ASSIGNMENTS  WHERE EMAIL LIKE '" + myEmail + "' AND ROOM_STATUS != 'CLEANED' ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            Panel headerPanel = new Panel
            {
                Width = 420,
                Height = 50,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10)
            };
            Label RoomLabel = new Label
            {
                Text = "ROOM NO",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 13
            };
            Label StatusLabel = new Label
            {
                Text = "ROOM STATUS",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 0,
                Left = 123
            };
            Panel bottomBorder = new Panel
            {
                Height = 2,                   // Border thickness
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray        // Border color
            };
            headerPanel.Controls.Add(RoomLabel);
            headerPanel.Controls.Add(StatusLabel);
            headerPanel.Controls.Add(bottomBorder);

            flowLayoutPanel1.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string roomId = rdr["ROOM_ID"].ToString();
                string status = rdr["ROOM_STATUS"].ToString();
                cmroomno.Items.Add(roomId);

                Panel taskPanel = new Panel
                {
                    Width = 420,
                    Height = 50,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(10)
                };
                Label roomLabel = new Label
                {
                    Text = roomId,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 30
                };
                Label statusLabel = new Label
                {
                    Text = status,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 0,
                    Left = 145
                };
                Button completeBtn = new Button
                {
                    Text = "Complete",
                    Font = new Font("Segoe Print", 11, FontStyle.Bold),
                    BackColor = Color.FromArgb(43, 92, 122),
                    ForeColor = Color.White,
                    Width = 120,
                    Height = 35,
                    Top = 0,
                    Left = 280,
                    Tag= roomId
                };
                completeBtn.Click += CompleteBtn_Click;
                taskPanel.Controls.Add(roomLabel);
                taskPanel.Controls.Add(statusLabel);
                taskPanel.Controls.Add(completeBtn);

                flowLayoutPanel1.Controls.Add(taskPanel);
            }
        }

        //Complete button for change status of room dirty or clean
        private void CompleteBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedRoomId = clickedButton.Tag.ToString();
            SqlConnection con = GetConnection();
            string q = "Update ROOM_ASSIGNMENTS SET ROOM_STATUS = 'CLEANED' WHERE ROOM_ID LIKE '" + selectedRoomId + "'";
            SqlCommand sqlCommand = new SqlCommand(q, con);
            sqlCommand.ExecuteNonQuery();
            loadmytask(email);

        }

        //Dashboard Button
        private void button1_Click(object sender, EventArgs e)
        {
            StaffDashboard staffDashboard = new StaffDashboard(email);
            staffDashboard.Show();
            this.Visible = false;
        }

        //Apply Leave Button
        private void button3_Click(object sender, EventArgs e)
        {
            Staff_LeaveApplication leaveApplication = new Staff_LeaveApplication(email);
            leaveApplication.Show();
            this.Visible=false;
        }

        //send maintenance request 
        private void button5_Click(object sender, EventArgs e)
        {
            DateTime today=DateTime.Now;
            SqlConnection con = GetConnection();
            string Id = " ";
            string IdQuerey = "SELECT ISNULL(MAX(M_ID), 0) + 1 FROM MaintenanceRequests";
            SqlCommand cmd1 = new SqlCommand(IdQuerey, con);
            Id = ((int)cmd1.ExecuteScalar()).ToString();
            Id = "00" + Id;

            string s = $"INSERT INTO MaintenanceRequests VALUES ('"+Id+"','"+cmroomno.Text+"','"+cmproblem.Text+"','Pending','"+today.ToString("yyyy-MM-dd")+"','"+email+"')";
            SqlCommand cmd2 = new SqlCommand(s, con);
            cmd2.ExecuteNonQuery();

        }
    }
}
