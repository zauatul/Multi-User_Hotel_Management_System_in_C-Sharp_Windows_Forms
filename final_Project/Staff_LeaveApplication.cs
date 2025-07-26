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
    public partial class Staff_LeaveApplication : Form
    {
        string email="";
        public Staff_LeaveApplication()
        {
            InitializeComponent();
        }
        public Staff_LeaveApplication(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void LeaveApplication_Load(object sender, EventArgs e)
        {
            load_history();
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        //submit button (detail about leave)
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetConnection();
            string Id = " ";
            string IdQuerey = "SELECT ISNULL(MAX(LEAVE_ID), 0) + 1 FROM LEAVE_REQUEST";
            SqlCommand cmd1 = new SqlCommand(IdQuerey, con);
            Id = ((int)cmd1.ExecuteScalar()).ToString();
            Id = "000" + Id;

            string q = "INSERT INTO LEAVE_REQUEST VALUES ('" + Id + "','" + email + "','" + txtfromdate.Value.ToString("yyyy-MM-dd") + "','" + txttodate.Value.ToString("yyyy-MM-dd") + "','" + cmreason.Text + "','Pending')";
            SqlCommand cmd2 = new SqlCommand(q, con);
            cmd2.ExecuteNonQuery();
            load_history();
        }
        private Label CreateLabel(string text, int left, int fontSize = 10, bool bold = false)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, bold ? FontStyle.Bold : FontStyle.Regular),
                AutoSize = true,
                Top = 0,
                Left = left
            };
        }

        //load previous booking history
        void load_history()
        {
            SqlConnection con = GetConnection();
            string q = @"SELECT 
                        RA.FROM_DATE,
                        RA.TO_DATE,
                        RA.REASON,
                        RA.STATUS,
                        UT.NAME 
                    FROM LEAVE_REQUEST RA
                    JOIN USER_TABLE UT ON RA.EMAIL = UT.EMAIL
                    WHERE RA.EMAIL LIKE '" + email + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            flowLayoutPanel2.Controls.Clear();

            Panel headerPanel = new Panel
            {
                Width = 650,
                Height = 50,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(1)
            };
            Label nameLabel = CreateLabel("NAME", 0, 12, true);
            Label fromLabel = CreateLabel("FROM", 85, 12, true);
            Label toLabel = CreateLabel("TO", 190, 12, true);
            Label reasonLabel = CreateLabel("REASON", 320, 12, true);
            Label statusLabel = CreateLabel("STATUS", 493, 12, true);

            Panel bottomBorder = new Panel
            {
                Height = 2,                   // Border thickness
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray        // Border color
            };
            headerPanel.Controls.Add(nameLabel);
            headerPanel.Controls.Add(fromLabel);
            headerPanel.Controls.Add(toLabel);
            headerPanel.Controls.Add(reasonLabel);
            headerPanel.Controls.Add(statusLabel);
            headerPanel.Controls.Add(bottomBorder);

            flowLayoutPanel2.Controls.Add(headerPanel);

            while (rdr.Read())
            {
                string name = rdr["NAME"].ToString();
                string from = rdr["FROM_DATE"].ToString();
                string to = rdr["TO_DATE"].ToString();
                string reason = rdr["REASON"].ToString();
                string status = rdr["STATUS"].ToString();
                
                Panel taskPanel = new Panel
                {
                    Width = 650,
                    Height = 50,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(5)
                };
                Label nameLabelnew = CreateLabel(name, 0);
                Label fromLabelnew = CreateLabel(from, 80);
                Label toLabelnew = CreateLabel(to, 190);
                Label reasonLabelnew = CreateLabel(reason, 320);
                Label statusLabelnew = CreateLabel(status, 483);

                taskPanel.Controls.Add(nameLabelnew);
                taskPanel.Controls.Add(fromLabelnew);
                taskPanel.Controls.Add(toLabelnew);
                taskPanel.Controls.Add(reasonLabelnew);
                taskPanel.Controls.Add(statusLabelnew);

                flowLayoutPanel2.Controls.Add(taskPanel);
            }
        }
        //Dashboard Button
        private void button1_Click(object sender, EventArgs e)
        {
            StaffDashboard staffDashboard = new StaffDashboard(email);
            staffDashboard.Show();
            this.Visible = false;
        }
        //My Task Button 
        private void button2_Click(object sender, EventArgs e)
        {
            showMYTask showMYTask = new showMYTask(email);
            showMYTask.Show();
            this.Visible=false;
        }
    }
}
