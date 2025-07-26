using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace final_Project
{
    public partial class Reciptionist : Form
    {
        string email;
        public Reciptionist()
        {
            InitializeComponent();
        }
        public Reciptionist(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
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
                Top = 10
            };
        }
        //Update button
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedBookingId = clickedButton.Tag.ToString();
            Update_Booking_Status update_Booking_Status = new Update_Booking_Status(selectedBookingId,email);
            update_Booking_Status.Show();
            this.Visible = false;
        }
        //load all booking related information 
        void load_bookings()
        {
            SqlConnection conn = GetConnection();

            string selectedStatus = cmsort.SelectedItem?.ToString();

            // Base query
            string s = @"SELECT RA.BOOKING_ID, RA.EMAIL, RA.ROOM_ID, RA.CHECK_IN, RA.CHECK_OUT, RA.BOOKING_STATUS, UT.NAME 
                 FROM BOOKINGS_TABLE RA 
                 JOIN USER_TABLE UT ON RA.EMAIL = UT.EMAIL 
                 WHERE RA.BOOKING_STATUS != 'CHECK-OUT'";

            // Add filter if a specific status is selected
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "Default Sorting")
            {
                s += $" AND RA.BOOKING_STATUS = '{selectedStatus}'";
            }
            else
            {
                s = @"SELECT RA.BOOKING_ID, RA.EMAIL, RA.ROOM_ID, RA.CHECK_IN, RA.CHECK_OUT, RA.BOOKING_STATUS, UT.NAME 
                     FROM BOOKINGS_TABLE RA 
                     JOIN USER_TABLE UT ON RA.EMAIL = UT.EMAIL 
                     WHERE RA.BOOKING_STATUS != 'CHECK-OUT'";
            }
            SqlCommand cmd = new SqlCommand(s, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            // Clear previous controls
            flowLayoutPanel1.Controls.Clear();

            // HEADER PANEL
            Panel headerPanel = new Panel
            {
                Width = 1100,
                Height = 40,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10)
            };

            int fontSize = 12;
            headerPanel.Controls.Add(CreateHeaderLabel("Booking ID", 0, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Name", 130, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Email", 300, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Room No", 470, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Check-In", 575, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Check-Out", 710, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Status", 850, fontSize));

            flowLayoutPanel1.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string bookingId = rdr["BOOKING_ID"].ToString();
                string email = rdr["EMAIL"].ToString();
                string status = rdr["BOOKING_STATUS"].ToString();
                string name = rdr["NAME"].ToString();
                string roomId = rdr["ROOM_ID"].ToString();
                string checkIn = Convert.ToDateTime(rdr["CHECK_IN"]).ToString("yyyy-MM-dd");
                string checkOut = Convert.ToDateTime(rdr["CHECK_OUT"]).ToString("yyyy-MM-dd");

                Panel taskPanel = new Panel
                {
                    Width = 1100,
                    Height = 40,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(10)
                };
                Button bookBtn = new Button
                {
                    Text = "Update",
                    Font = new Font("Segoe Print", 11, FontStyle.Bold),
                    BackColor = Color.FromArgb(43, 92, 122),
                    ForeColor = Color.White,
                    Width = 120,
                    Height = 35,
                    Top = 10,
                    Left = 980,
                    Tag = bookingId
                };
                bookBtn.Click += UpdateBtn_Click;

                taskPanel.Controls.Add(CreateDataLabel(bookingId, 10, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(name, 115, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(email, 255, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(roomId, 485, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(checkIn, 575, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(checkOut, 710, fontSize));
                taskPanel.Controls.Add(CreateDataLabel(status, 850, fontSize));
                taskPanel.Controls.Add(bookBtn);

                flowLayoutPanel1.Controls.Add(taskPanel);
            }

            rdr.Close();
            conn.Close();
        }




        private void Reciptionist_Load(object sender, EventArgs e)
        {
            load_bookings();
        }

       
        //Apply Leave button
        private void button1_Click(object sender, EventArgs e)
        {
            Reciptionist_Leave reciptionist_Leave = new Reciptionist_Leave(email);
            reciptionist_Leave.Show();
            this.Visible = false;
        }

        //pictureBox1 for Reciptionist Profile
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Employee_Profile reciptionist_Profile = new Employee_Profile(email, "receptionist");
            reciptionist_Profile.Show();
            this.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_bookings();
        }

        
    }
}
