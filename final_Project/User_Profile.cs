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
    public partial class User_Profile : Form
    {
        string email,type="",roomId,rID;
        public User_Profile()
        {
            InitializeComponent();
        }
        public User_Profile(string email,string type)
        {
            this.email = email;
            this.type = type;
            InitializeComponent();
        }
        public User_Profile(string email,string roomId, string type)
        {
            this.roomId=roomId; 
            this.email = email;
            this.type = type;
            InitializeComponent();
        }
        SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        private Label CreateDataLabel(string text, int left)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe Print", 11, FontStyle.Bold),
                AutoSize = true,
                Left = left,
                Top = 5
            };
        }

        //cencel your booking [can't cencel bookings that's already been check-out]
        private void CencelBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedRoomId = clickedButton.Tag.ToString();
            SqlConnection conn = GetConnection();
            string s = "DELETE FROM BOOKINGS_TABLE WHERE BOOKING_ID LIKE '" + selectedRoomId + "' AND BOOKING_STATUS != 'CHECK-OUT' ";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            load_current_booking();


        }
        //load the booking information from database and show
        void load_current_booking()
        {
            flowLayoutPanel1.Controls.Clear();
            SqlConnection conn = GetConnection();

            SqlCommand cmd = new SqlCommand("SELECT BOOKING_ID,ROOM_ID,CHECK_IN,CHECK_OUT,BOOKING_DATE,BOOKING_STATUS FROM BOOKINGS_TABLE WHERE EMAIL LIKE '" + email + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["BOOKING_ID"].ToString();
                    rID = reader["ROOM_ID"].ToString();
                    string checkIn = Convert.ToDateTime(reader["CHECK_IN"]).ToString("yyyy-MM-dd");
                    string checkOut = Convert.ToDateTime(reader["CHECK_OUT"]).ToString("yyyy-MM-dd");
                    string booked = Convert.ToDateTime(reader["BOOKING_DATE"]).ToString("yyyy-MM-dd");
                    string status = reader["BOOKING_STATUS"].ToString();

                // Create panel UI (same as before, no need to change here)
                Panel roomPanel = new Panel
                    {
                        Width = 1130,
                        Height = 40,
                        ForeColor = Color.Black,
                        BorderStyle = BorderStyle.None,
                        Margin = new Padding(2)
                    };

                    Button cencelBtn = new Button
                    {
                        Text = "Cencel",
                        Font = new Font("Segoe Print", 10, FontStyle.Bold),
                        BackColor = Color.FromArgb(85, 88, 121),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat, // Make button flat
                        Width = 80,
                        Height = 30,
                        Top = 5,
                        Left = 1040,
                        Tag=id
                    };
                    cencelBtn.Click += CencelBtn_Click;
                    cencelBtn.FlatAppearance.BorderSize = 0;

                    roomPanel.Controls.Add(CreateDataLabel($"Booked in - {booked}" , 20));
                    roomPanel.Controls.Add(CreateDataLabel($"From - {checkIn}", 255));
                    roomPanel.Controls.Add(CreateDataLabel($"To - {checkOut}", 465));
                    roomPanel.Controls.Add(CreateDataLabel($"Status - {status}", 665));
                    roomPanel.Controls.Add(CreateDataLabel($"Room No - {rID}", 860));    
                    roomPanel.Controls.Add(cencelBtn);

                    flowLayoutPanel1.BackColor = Color.FromArgb(234, 239, 239);
                    flowLayoutPanel1.Controls.Add(roomPanel);
                }
            
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

        //Back button [as you can go profile from any form .So you have go back those form that you came from.]

        private void button1_Click(object sender, EventArgs e)
        {
            if (type == "room")
            {
                Rooms rooms = new Rooms(email);
                rooms.Show();
                this.Visible = false;
            }
            else if (type == "room_details")
            {
                Room_Details room_Details = new Room_Details(roomId, email);
                room_Details.Show();
                this.Visible = false;
            }
            else if (type == "confirm_bookings")
            {
                Confirm_Bookings confirm_Bookings = new Confirm_Bookings(roomId, email);
                confirm_Bookings.Show();
                this.Visible = false;
            }
            else if (type == "home")
            {
                Home form1 = new Home(email);
                form1.Show();
                this.Visible = false;
            }
        }
        //Edit Info Button [passing the current state also for coming back on the same form]
        private void button2_Click(object sender, EventArgs e)
        {
            if (type == "room")
            {
                Edit_Info edit_Info = new Edit_Info(email,type);
                edit_Info.Show();
                this.Visible = false;
            }
            else if (type == "room_details")
            {
                Edit_Info edit_Info = new Edit_Info( email, roomId, type);
                edit_Info.Show();
                this.Visible = false;
            }
            else if (type == "confirm_bookings")
            {
                Edit_Info edit_Info = new Edit_Info(email, roomId, type);
                edit_Info.Show();
                this.Visible = false;
            }
            else if (type == "home")
            {
                Edit_Info edit_Info = new Edit_Info(email, type);
                edit_Info.Show();
                this.Visible = false;
            }
        }

        //Unpaid Rooms button
        private void button3_Click(object sender, EventArgs e)
        {
            Pending_Payment pending_Payment = new Pending_Payment(email,roomId);
            pending_Payment.Show();
            this.Visible = false;
        }
        //log out button
        private void btnlogout_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Visible=false;
        }

        private void User_Profile_Load(object sender, EventArgs e)
        {
            load_current_booking();
            getUser();
        }

        
    }
}
