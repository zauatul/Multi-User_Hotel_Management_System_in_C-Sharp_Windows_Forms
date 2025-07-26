using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_Project
{
    public partial class Manage_Users_Admin : Form
    {
        string email;
        public Manage_Users_Admin()
        {
            InitializeComponent();
        }

        private void Manage_Employee_Admin_Load(object sender, EventArgs e)
        {
            load_Users();
        }

        private Label CreateHeaderLabel(string text, int left, int fontSize)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Left = left,
                Top = 10
            };
        }
        private TextBox CreateStyledTextBox(string text, string name, int left, int top, int width)
        {
            return new TextBox
            {
                Text = text,
                Name = name,
                Font = new Font("Segoe Print", 15, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Height = 40,
                Width = width,
                Left = left,
                Top = top
            };
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        //Update button
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Panel panel = btn.Tag as Panel;

            string name = panel.Controls["txtName"].Text.Trim();
            string phone = panel.Controls["txtPhone"].Text.Trim();
            string email = panel.Controls["txtEmail"].Text.Trim(); // Key
            string password = panel.Controls["txtPassword"].Text.Trim();

            using (SqlConnection con = GetConnection())
            {
                string q = "UPDATE USER_TABLE SET NAME = @name, PHONE_NUMBER = @phone, PASSWORD = @password WHERE EMAIL = @email";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                int rowsAffected = cmd.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "User updated successfully." : "Update failed.");
            }

            load_Users();
        }

        void load_Users(string userType = "")
        {
            SqlConnection con = GetConnection();

            string q = "SELECT NAME, PHONE_NUMBER, EMAIL, PASSWORD FROM USER_TABLE";
            if (!string.IsNullOrEmpty(userType))
            {
                q += " WHERE USER_TYPE = @type";
            }

            SqlCommand cmd = new SqlCommand(q, con);
            if (!string.IsNullOrEmpty(userType))
            {
                cmd.Parameters.AddWithValue("@type", userType);
            }

            SqlDataReader rdr = cmd.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();

            Panel headerPanel = new Panel
            {
                Width = 940,
                Height = 40,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10)
            };

            int fontSize = 18;
            headerPanel.Controls.Add(CreateHeaderLabel("Name", 26, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Phone Number", 185, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Email", 450, fontSize));
            headerPanel.Controls.Add(CreateHeaderLabel("Password", 660, fontSize));
            flowLayoutPanel1.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string name = rdr["NAME"].ToString();
                string phone = rdr["PHONE_NUMBER"].ToString();
                string email = rdr["EMAIL"].ToString();
                string password = rdr["PASSWORD"].ToString();

                Panel taskPanel = new Panel
                {
                    Width = 940,
                    Height = 60,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(5)
                };

                // Create named TextBoxes
                TextBox txtName = CreateStyledTextBox(name, "txtName", 10, 15, 150);
                TextBox txtPhone = CreateStyledTextBox(phone, "txtPhone", 200, 15, 170);
                TextBox txtEmail = CreateStyledTextBox(email, "txtEmail", 400, 15, 240);
                TextBox txtPassword = CreateStyledTextBox(password, "txtPassword", 670, 15, 130);

                // Buttons with parent Panel as Tag
                Button updateBtn = new Button
                {
                    Text = "Update",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.FromArgb(43, 92, 122),
                    ForeColor = Color.White,
                    Width = 100,
                    Height = 40,
                    Top = 15,
                    Left = 825,
                    Tag = taskPanel
                };
                updateBtn.Click += UpdateBtn_Click;


                // Add controls to panel
                taskPanel.Controls.Add(txtName);
                taskPanel.Controls.Add(txtPhone);
                taskPanel.Controls.Add(txtEmail);
                taskPanel.Controls.Add(txtPassword);
                taskPanel.Controls.Add(updateBtn);

                flowLayoutPanel1.Controls.Add(taskPanel);
            }

            rdr.Close();
            con.Close();
        }
        //Sorting combo-box
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = comboBox1.SelectedItem.ToString();

            if (selectedType == "Default Sorting")
                load_Users(); 
            else
                load_Users(selectedType);
        }
        //Back button
        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
            this.Visible=false;
        }
    }
}
