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
    public partial class Admin_Dashboard : Form
    {
        string email = "";

        public Admin_Dashboard()
        {
            InitializeComponent();
        }
        public Admin_Dashboard(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            load_occupied_rooms();
            load_booking_history();
            load_dashboard_counts();
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        private Label CreateLabel(string text, int fontSize, FontStyle fontStyle, int left, int top)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, fontStyle),
                AutoSize = true,
                Left = left,
                Top = top
            };
        }
        //load Occupied Rooms
        void load_occupied_rooms()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT RA.ROOM_ID, SA.NAME 
                         FROM BOOKINGS_TABLE RA 
                         JOIN USER_TABLE SA ON RA.EMAIL = SA.EMAIL 
                         WHERE RA.BOOKING_STATUS = 'CHECK-IN'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            flowLayoutPanel1.Controls.Clear();

            Panel titlePanel = new Panel 
            { 
                Width = 240,
                Height = 40, 
                Margin = new Padding(2) 
            };
            titlePanel.Controls.Add(CreateLabel("Occupied Rooms", 18, FontStyle.Bold, 25, 0));
            flowLayoutPanel1.Controls.Add(titlePanel);

            Panel headerPanel = new Panel 
            { 
                Width = 240,
                Height = 30, 
                Margin = new Padding(2) 
            };
            headerPanel.Controls.Add(CreateLabel("NAME", 14, FontStyle.Bold, 20, 0));
            headerPanel.Controls.Add(CreateLabel("ROOM NO", 14, FontStyle.Bold, 125, 0));
            headerPanel.Controls.Add(new Panel
            {
                Height = 2,
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray
            });
            flowLayoutPanel1.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string roomId = rdr["ROOM_ID"].ToString();
                string name = rdr["NAME"].ToString();

                Panel taskPanel = new Panel
                {
                    Width = 240,
                    Height = 30,
                    Margin = new Padding(3)
                };
                taskPanel.Controls.Add(CreateLabel(name, 12, FontStyle.Bold, 0, 0));
                taskPanel.Controls.Add(CreateLabel(roomId, 12, FontStyle.Bold, 145, 0));
                flowLayoutPanel1.Controls.Add(taskPanel);
            }
            con.Close();
        }

        void load_booking_history()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT RA.ROOM_ID, RA.BOOKING_DATE, SA.NAME 
                         FROM BOOKINGS_TABLE RA 
                         JOIN USER_TABLE SA ON RA.EMAIL = SA.EMAIL";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            flowLayoutPanel2.Controls.Clear();

            Panel titlePanel = new Panel 
            { 
                Width = 530,
                Height = 40, 
                Margin = new Padding(2) 
            };
            titlePanel.Controls.Add(CreateLabel("Booking History", 18, FontStyle.Bold, 120, 0));
            flowLayoutPanel2.Controls.Add(titlePanel);

            Panel headerPanel = new Panel
            { 
                Width = 530, 
                Height = 30, 
                Margin = new Padding(5)
            };
            headerPanel.Controls.Add(CreateLabel("NAME", 14, FontStyle.Bold, 10, 0));
            headerPanel.Controls.Add(CreateLabel("Booking Date", 14, FontStyle.Bold, 181, 0));
            headerPanel.Controls.Add(CreateLabel("ROOM NO", 14, FontStyle.Bold, 395, 0));
            headerPanel.Controls.Add(new Panel
            {
                Height = 2,
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray
            });
            flowLayoutPanel2.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string name = rdr["NAME"].ToString();
                string bookingDate = rdr["BOOKING_DATE"].ToString();
                string roomId = rdr["ROOM_ID"].ToString();

                Panel taskPanel = new Panel { Width = 500, Height = 30, Margin = new Padding(10) };
                taskPanel.Controls.Add(CreateLabel(name, 12, FontStyle.Bold, 0, 0));
                taskPanel.Controls.Add(CreateLabel(bookingDate, 14, FontStyle.Bold, 145, 0));
                taskPanel.Controls.Add(CreateLabel(roomId, 12, FontStyle.Bold, 425, 0));
                flowLayoutPanel2.Controls.Add(taskPanel);
            }
            con.Close();
        }
        void load_dashboard_counts()
        {
            SqlConnection con = GetConnection();

            // 1. Total Bookings
            SqlCommand totalBookingsCmd = new SqlCommand("SELECT COUNT(*) FROM BOOKINGS_TABLE", con);
            int totalBookings = (int)totalBookingsCmd.ExecuteScalar();
            counttb.Text = totalBookings.ToString();

            // 2. Total Occupied Rooms (CHECK-IN status)
            SqlCommand occupiedRoomsCmd = new SqlCommand("SELECT COUNT(*) FROM BOOKINGS_TABLE WHERE BOOKING_STATUS = 'CHECK-IN'", con);
            int occupiedRooms = (int)occupiedRoomsCmd.ExecuteScalar();
            counttor.Text = occupiedRooms.ToString();

            // 3. Today's Check-Ins (Check current date)
            SqlCommand todaysCheckInCmd = new SqlCommand("SELECT COUNT(*) FROM BOOKINGS_TABLE WHERE CAST(CHECK_IN AS DATE) = CAST(GETDATE() AS DATE)", con);
            int todaysCheckIn = (int)todaysCheckInCmd.ExecuteScalar();
            counttci.Text = todaysCheckIn.ToString();

            con.Close();
        }
        //Leave Request button
        private void button1_Click(object sender, EventArgs e)
        {
            Handle_Leave_RequestcsAdmin handle_Leave_Requestcs = new Handle_Leave_RequestcsAdmin();
            handle_Leave_Requestcs.Show();
            this.Visible = false;
        }
        //Manage Rooms button
        private void button2_Click(object sender, EventArgs e)
        {
            Handle_Rooms_Admin handle_Maintainence = new Handle_Rooms_Admin();
            handle_Maintainence.Show();
            this.Visible = false;
        }
        //log out button
        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Visible = false;
        }
        //Manage User button
        private void button4_Click(object sender, EventArgs e)
        {
            Manage_Users_Admin manage_Users_Admin = new Manage_Users_Admin();
            manage_Users_Admin.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Assign_Staff_Task assign_Staff_Task = new Assign_Staff_Task();
            assign_Staff_Task.Show();
            this.Visible = false;
        }
    }
    
}
