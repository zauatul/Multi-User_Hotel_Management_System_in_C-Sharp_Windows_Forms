namespace final_Project
{
    partial class Payment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtexpdate = new System.Windows.Forms.DateTimePicker();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcardnumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnpayment = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel1.Controls.Add(this.txtexpdate);
            this.panel1.Controls.Add(this.txtcode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtcardnumber);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(122, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 410);
            this.panel1.TabIndex = 0;
            // 
            // txtexpdate
            // 
            this.txtexpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtexpdate.Location = new System.Drawing.Point(45, 282);
            this.txtexpdate.Name = "txtexpdate";
            this.txtexpdate.Size = new System.Drawing.Size(200, 30);
            this.txtexpdate.TabIndex = 6;
            // 
            // txtcode
            // 
            this.txtcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(317, 282);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(220, 30);
            this.txtcode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(312, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "Card Security Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(40, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Expiration ";
            // 
            // txtcardnumber
            // 
            this.txtcardnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcardnumber.Location = new System.Drawing.Point(45, 122);
            this.txtcardnumber.Name = "txtcardnumber";
            this.txtcardnumber.Size = new System.Drawing.Size(492, 30);
            this.txtcardnumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(40, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Card Number";
            // 
            // btnpayment
            // 
            this.btnpayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(92)))), ((int)(((byte)(122)))));
            this.btnpayment.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpayment.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnpayment.Location = new System.Drawing.Point(356, 554);
            this.btnpayment.Name = "btnpayment";
            this.btnpayment.Size = new System.Drawing.Size(169, 47);
            this.btnpayment.TabIndex = 1;
            this.btnpayment.Text = "Payment";
            this.btnpayment.UseVisualStyleBackColor = false;
            this.btnpayment.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(92)))), ((int)(((byte)(122)))));
            this.button2.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(687, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 42);
            this.button2.TabIndex = 2;
            this.button2.Text = "Print Recipt";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnback
            // 
            this.btnback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(92)))), ((int)(((byte)(122)))));
            this.btnback.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnback.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnback.Location = new System.Drawing.Point(12, 24);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(136, 42);
            this.btnback.TabIndex = 3;
            this.btnback.Text = "Back";
            this.btnback.UseVisualStyleBackColor = false;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 630);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnpayment);
            this.Controls.Add(this.panel1);
            this.Name = "Payment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.Payment_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcardnumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtexpdate;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Button btnpayment;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnback;
    }
}