namespace windowsApp
{
    partial class frmCapture
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdCheckOut = new System.Windows.Forms.RadioButton();
            this.cbxCamera = new System.Windows.Forms.ComboBox();
            this.rdCheckIn = new System.Windows.Forms.RadioButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.txtCurrentTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInsertKey = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerRequest = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lb3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtVehicleId = new System.Windows.Forms.TextBox();
            this.txtOwnerName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtTicketAvailable = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdCheckOut);
            this.panel1.Controls.Add(this.cbxCamera);
            this.panel1.Controls.Add(this.rdCheckIn);
            this.panel1.Controls.Add(this.radioGroup1);
            this.panel1.Controls.Add(this.txtCurrentTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnInsertKey);
            this.panel1.Controls.Add(this.btnCapture);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 71);
            this.panel1.TabIndex = 0;
            // 
            // rdCheckOut
            // 
            this.rdCheckOut.AutoSize = true;
            this.rdCheckOut.Location = new System.Drawing.Point(428, 41);
            this.rdCheckOut.Name = "rdCheckOut";
            this.rdCheckOut.Size = new System.Drawing.Size(75, 17);
            this.rdCheckOut.TabIndex = 17;
            this.rdCheckOut.TabStop = true;
            this.rdCheckOut.Text = "Check Out";
            this.rdCheckOut.UseVisualStyleBackColor = true;
            this.rdCheckOut.CheckedChanged += new System.EventHandler(this.rdCheckOut_CheckedChanged);
            // 
            // cbxCamera
            // 
            this.cbxCamera.FormattingEnabled = true;
            this.cbxCamera.Location = new System.Drawing.Point(642, 39);
            this.cbxCamera.Name = "cbxCamera";
            this.cbxCamera.Size = new System.Drawing.Size(121, 21);
            this.cbxCamera.TabIndex = 7;
            // 
            // rdCheckIn
            // 
            this.rdCheckIn.AutoSize = true;
            this.rdCheckIn.Location = new System.Drawing.Point(428, 19);
            this.rdCheckIn.Name = "rdCheckIn";
            this.rdCheckIn.Size = new System.Drawing.Size(67, 17);
            this.rdCheckIn.TabIndex = 14;
            this.rdCheckIn.TabStop = true;
            this.rdCheckIn.Text = "Check In";
            this.rdCheckIn.UseVisualStyleBackColor = true;
            this.rdCheckIn.CheckedChanged += new System.EventHandler(this.rdCheckIn_CheckedChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(415, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Size = new System.Drawing.Size(104, 53);
            this.radioGroup1.TabIndex = 13;
            // 
            // txtCurrentTime
            // 
            this.txtCurrentTime.Location = new System.Drawing.Point(642, 12);
            this.txtCurrentTime.Name = "txtCurrentTime";
            this.txtCurrentTime.Size = new System.Drawing.Size(121, 21);
            this.txtCurrentTime.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(600, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // btnInsertKey
            // 
            this.btnInsertKey.Location = new System.Drawing.Point(255, 23);
            this.btnInsertKey.Name = "btnInsertKey";
            this.btnInsertKey.Size = new System.Drawing.Size(75, 23);
            this.btnInsertKey.TabIndex = 3;
            this.btnInsertKey.Text = "Insert Key";
            this.btnInsertKey.UseVisualStyleBackColor = true;
            this.btnInsertKey.Click += new System.EventHandler(this.btnInsertKey_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(174, 23);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 2;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(93, 23);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Tag = "Start";
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerRequest
            // 
            this.timerRequest.Interval = 1000;
            this.timerRequest.Tick += new System.EventHandler(this.timerRequest_Tick);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox.Location = new System.Drawing.Point(0, 71);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(509, 366);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Location = new System.Drawing.Point(529, 89);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(34, 13);
            this.lb3.TabIndex = 2;
            this.lb3.Text = "Detail";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(550, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vehicle Id:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(534, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Owner Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(572, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Email:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(526, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Phone Number:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(522, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Ticket Available:";
            // 
            // txtVehicleId
            // 
            this.txtVehicleId.Location = new System.Drawing.Point(609, 133);
            this.txtVehicleId.Name = "txtVehicleId";
            this.txtVehicleId.Size = new System.Drawing.Size(100, 21);
            this.txtVehicleId.TabIndex = 8;
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Location = new System.Drawing.Point(609, 158);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(100, 21);
            this.txtOwnerName.TabIndex = 9;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(609, 185);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 21);
            this.txtEmail.TabIndex = 10;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(609, 208);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 21);
            this.txtPhoneNumber.TabIndex = 11;
            // 
            // txtTicketAvailable
            // 
            this.txtTicketAvailable.Location = new System.Drawing.Point(609, 231);
            this.txtTicketAvailable.Name = "txtTicketAvailable";
            this.txtTicketAvailable.Size = new System.Drawing.Size(100, 21);
            this.txtTicketAvailable.TabIndex = 12;
            // 
            // frmCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 437);
            this.Controls.Add(this.txtTicketAvailable);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.txtVehicleId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb3);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.Name = "frmCapture";
            this.Text = "frmCapture";
            this.Load += new System.EventHandler(this.frmCapture_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxCamera;
        private System.Windows.Forms.TextBox txtCurrentTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInsertKey;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerRequest;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtVehicleId;
        private System.Windows.Forms.TextBox txtOwnerName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtTicketAvailable;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.RadioButton rdCheckIn;
        private System.Windows.Forms.RadioButton rdCheckOut;
    }
}