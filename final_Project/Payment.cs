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
using iTextSharp.text.pdf;

namespace final_Project
{
    public partial class Payment : Form
    {
        string bookingId,roomId,checkIn,checkOut,email;
        int totalBill;
        public Payment()
        {
            InitializeComponent();
        }
        public Payment(string roomId,string email,string bookingId,string checkIn,string checkOut)
        {
            this.email = email;
            this.bookingId = bookingId;
            this.checkIn = checkIn;
            this.checkOut = checkOut;
            InitializeComponent();
        }
        SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=mydb;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            find_room();
        }
        //change booking status of a user
        void change_status()
        {
            SqlConnection sqlConnection = GetConnection();
            string q = "UPDATE  BOOKINGS_TABLE SET BOOKING_STATUS='PAID' WHERE BOOKING_ID LIKE '"+bookingId+"'";
            SqlCommand cmd = new SqlCommand(q, sqlConnection);
            cmd.ExecuteNonQuery();
        }

        void find_room()
        {
            SqlConnection sqlConnection = GetConnection();
            string q = "SELECT ROOM_ID FROM BOOKINGS_TABLE WHERE BOOKING_ID LIKE '" + bookingId + "' ";
            SqlCommand cmd = new SqlCommand(q, sqlConnection);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                roomId = rdr["ROOM_ID"].ToString();
            }
        }
        //store the payment data
        void payment()
        {
            int bill = total_Bill(roomId);
            SqlConnection sqlConnection = GetConnection();
            string Id = " ";
            string IdQuerey = "SELECT ISNULL(MAX(P_ID), 0) + 1 FROM PAYMENT";
            SqlCommand cmd1 = new SqlCommand(IdQuerey, sqlConnection);
            Id = ((int)cmd1.ExecuteScalar()).ToString();
            string q = "INSERT INTO PAYMENT VALUES ('"+Id+ "','"+bookingId+"','"+txtcardnumber.Text+"','"+txtexpdate.Value.ToString("yyyy-MM-dd") + "','"+txtcode.Text+"','"+bill+"')";
            SqlCommand cmd = new SqlCommand(q, sqlConnection);
            cmd.ExecuteNonQuery();
        }
        //back button
        private void btnback_Click(object sender, EventArgs e)
        {
            Confirm_Bookings confirm_Bookings = new Confirm_Bookings(roomId,email);
            confirm_Bookings.Show();
            this.Visible = false;
        }
        //print recipt button
        private void button2_Click(object sender, EventArgs e)
        {
            string cardNumber = txtcardnumber.Text;
            int billAmount = total_Bill(roomId);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = "Receipt_" + bookingId + ".pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Add content
                    pdfDoc.Add(new iTextSharp.text.Paragraph("HOTEL BOOKING RECEIPT"));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Booking ID: " + bookingId));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Room ID: " + roomId));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Check In: " + checkIn));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Check Out: " + checkOut));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Card Number: " + txtcardnumber.Text));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Total Bill: " + total_Bill(roomId) + " BDT"));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                    pdfDoc.Close();
                    stream.Close();
                }
               
            }
        }
        //payment button
        private void button1_Click(object sender, EventArgs e)
        {
            payment();
            change_status();
            MessageBox.Show("Payemnt successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //calculate total bill
        int total_Bill(string roomId)
        {
            int count = make_bill();
            int bill = 1;
            string sBill="";
            SqlConnection sqlConnection = GetConnection();
            string q = "SELECT PPN FROM ROOM_TABLE WHERE ROOM_ID LIKE '"+roomId+"' ";
            SqlCommand cmd = new SqlCommand(q, sqlConnection);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                sBill = rdr["PPN"].ToString();
            }
            bill = Convert.ToInt32(sBill);
            bill *= count;
            return bill;
        }
        //count total day
        int make_bill()
        {
            int count = 0;
            string sCount = "";
            SqlConnection sqlConnection = GetConnection();
            string q = "SELECT DATEDIFF(day, '" + checkIn + "', '" + checkOut + "') AS DaysBetween";
            SqlCommand cmd = new SqlCommand(q, sqlConnection);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                sCount = rdr["DaysBetween"].ToString();
            }
            count = Convert.ToInt32(sCount);
            return count;
        }


    }
}
