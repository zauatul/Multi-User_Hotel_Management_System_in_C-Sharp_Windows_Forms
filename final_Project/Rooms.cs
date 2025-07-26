using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_Project
{
    public partial class Rooms : Form
    {
        public string mail="nnnahid227@gmail.com";
        public Rooms()
        {
            string sortQuery = "ORDER BY ROOM_ID";
            InitializeComponent();
            load_rooms(sortQuery);
        }
        public Rooms(string email)
        {
            string sortQuery = "ORDER BY ROOM_ID";
            InitializeComponent();
            load_rooms(sortQuery);
            mail= email; 
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        //Book Now button (show after run the code)
        private void BookBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedRoomId = clickedButton.Tag.ToString();
            Room_Details bookingForm = new Room_Details(selectedRoomId, mail);
            bookingForm.Show();
            this.Visible = false;
        
        }
        
        private void Rooms_Load(object sender, EventArgs e)
        {
            getUser();

        }

        //load room details from database and showing dynamically 
        void load_rooms(string sortQuery)
        {
            flowLayoutPanel1.Controls.Clear(); 
            SqlConnection conn=GetConnection();

            SqlCommand cmd = new SqlCommand($"SELECT ROOM_ID, TYPE, PPN, DESCRIPTION, PICTURE FROM ROOM_TABLE {sortQuery}", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string imagePath = reader["PICTURE"].ToString();
                string price = reader["PPN"].ToString();
                string description = reader["DESCRIPTION"].ToString();
                string roomId = reader["ROOM_ID"].ToString();
                string type = reader["TYPE"].ToString();

                int panelWidth = flowLayoutPanel1.ClientSize.Width - 30;
                Panel roomPanel = new Panel
                {
                    Height = 200,
                    Width=panelWidth,
                    BorderStyle = BorderStyle.FixedSingle,
                };

                PictureBox pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 230,
                    Height = 170,
                    Top = 10,
                    Left = 10
                };

                if (File.Exists(imagePath))
                {
                    pictureBox.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBox.Image = SystemIcons.Warning.ToBitmap();
                }
                    
                
                Panel rightPanel = new Panel
                {
                    Left = 247,
                    Top = 10,
                    Width = 800,
                    Height = 160
                };

                Label typeLabel = new Label
                {
                    Text = type,
                    Font = new Font("Segoe Print", 12, FontStyle.Bold),
                    AutoSize = false,
                    Top = 10,
                    Left = 0,
                    Width = 230,
                    Height = 35
                };

                Label descLabel = new Label
                {
                    Text = description,
                    Font = new Font("Segoe Print", 10, FontStyle.Regular),
                    AutoSize = false,
                    Top = 50,
                    Left = 0,
                    Width = 800,
                    Height = 50
                };

                Label priceLabel = new Label
                {
                    Text = $"Price: {price} BDT/night",
                    Font = new Font("Segoe Print", 11, FontStyle.Bold),
                    AutoSize = true,
                    Top = 100,
                    Left = 0
                };

                Button bookBtn = new Button
                {
                    Text = "Book Now",
                    Font = new Font("Segoe Print", 11, FontStyle.Bold),
                    BackColor= Color.FromArgb(43, 92, 122),
                    ForeColor=Color.White,
                    Width = 120,
                    Height = 35,
                    Top = 125,
                    Left = 0,
                    Tag = roomId
                };
                bookBtn.Click += BookBtn_Click;

                rightPanel.Controls.Add(typeLabel);
                rightPanel.Controls.Add(descLabel);
                rightPanel.Controls.Add(priceLabel);
                rightPanel.Controls.Add(bookBtn);

                roomPanel.Controls.Add(pictureBox);
                roomPanel.Controls.Add(rightPanel);

                flowLayoutPanel1.Controls.Add(roomPanel);
            }
            
        }

        //combo-box for sorting 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSort = comboBox1.SelectedItem?.ToString();
            string sortQuery = "";

            if (selectedSort == "Alphabetically")
            {
                sortQuery = "ORDER BY TYPE";
            }
            else if (selectedSort == "Low to High")
            {
                sortQuery = "ORDER BY CAST(PPN AS INT) ASC";
            }
            else if (selectedSort == "High to Low")
            {
                sortQuery = "ORDER BY CAST(PPN AS INT) DESC";
            }
            else
            {
                sortQuery = "ORDER BY ROOM_ID";
            }
            load_rooms(sortQuery);
        }

        //search button
        private void button1_Click(object sender, EventArgs e)
        {
            string sortQuery = "WHERE TYPE LIKE '%"+txtsearch.Text+"%'";
            load_rooms(sortQuery);
        }

        //Back button
        private void button2_Click(object sender, EventArgs e)
        {
            Home form1 = new Home(mail);
            form1.Show();
            this.Visible = false;
        }

        //get the user name and show the name in the button 
        void getUser()
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand($"SELECT NAME FROM USER_TABLE WHERE EMAIL LIKE '"+mail+"'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtuser.Text = reader["NAME"].ToString();
            }
            
        }

        //Button for show own profile
        private void button3_Click(object sender, EventArgs e)
        {
            string type = "room";
            User_Profile user_Profile = new User_Profile(mail,type);
            user_Profile.Show();
            this.Visible = false;
        }

        
    }
}
