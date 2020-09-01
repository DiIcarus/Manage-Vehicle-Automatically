namespace windowsApp
{
    partial class frmInsertKey
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
            this.btnGenKey = new System.Windows.Forms.Button();
            this.rdCheckOut = new System.Windows.Forms.RadioButton();
            this.rdCheckIn = new System.Windows.Forms.RadioButton();
            this.txtKeyCode = new System.Windows.Forms.TextBox();
            this.txtShareCode = new System.Windows.Forms.TextBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.btnSummit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGenKey);
            this.panel1.Controls.Add(this.rdCheckOut);
            this.panel1.Controls.Add(this.rdCheckIn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 76);
            this.panel1.TabIndex = 0;
            // 
            // btnGenKey
            // 
            this.btnGenKey.Location = new System.Drawing.Point(171, 18);
            this.btnGenKey.Name = "btnGenKey";
            this.btnGenKey.Size = new System.Drawing.Size(117, 23);
            this.btnGenKey.TabIndex = 2;
            this.btnGenKey.Text = "Gen Share Code";
            this.btnGenKey.UseVisualStyleBackColor = true;
            this.btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);
            // 
            // rdCheckOut
            // 
            this.rdCheckOut.AutoSize = true;
            this.rdCheckOut.Location = new System.Drawing.Point(34, 41);
            this.rdCheckOut.Name = "rdCheckOut";
            this.rdCheckOut.Size = new System.Drawing.Size(75, 17);
            this.rdCheckOut.TabIndex = 1;
            this.rdCheckOut.TabStop = true;
            this.rdCheckOut.Text = "Check Out";
            this.rdCheckOut.UseVisualStyleBackColor = true;
            this.rdCheckOut.CheckedChanged += new System.EventHandler(this.rdCheckOut_CheckedChanged);
            // 
            // rdCheckIn
            // 
            this.rdCheckIn.AutoSize = true;
            this.rdCheckIn.Location = new System.Drawing.Point(34, 18);
            this.rdCheckIn.Name = "rdCheckIn";
            this.rdCheckIn.Size = new System.Drawing.Size(67, 17);
            this.rdCheckIn.TabIndex = 0;
            this.rdCheckIn.TabStop = true;
            this.rdCheckIn.Text = "Check In";
            this.rdCheckIn.UseVisualStyleBackColor = true;
            this.rdCheckIn.CheckedChanged += new System.EventHandler(this.rdCheckIn_CheckedChanged);
            // 
            // txtKeyCode
            // 
            this.txtKeyCode.Location = new System.Drawing.Point(114, 116);
            this.txtKeyCode.Name = "txtKeyCode";
            this.txtKeyCode.Size = new System.Drawing.Size(100, 21);
            this.txtKeyCode.TabIndex = 1;
            // 
            // txtShareCode
            // 
            this.txtShareCode.Location = new System.Drawing.Point(114, 154);
            this.txtShareCode.Name = "txtShareCode";
            this.txtShareCode.Size = new System.Drawing.Size(100, 21);
            this.txtShareCode.TabIndex = 2;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(46, 119);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(55, 13);
            this.lb1.TabIndex = 3;
            this.lb1.Text = "Key code:";
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(36, 157);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(65, 13);
            this.lb2.TabIndex = 4;
            this.lb2.Text = "Share code:";
            // 
            // btnSummit
            // 
            this.btnSummit.Location = new System.Drawing.Point(139, 196);
            this.btnSummit.Name = "btnSummit";
            this.btnSummit.Size = new System.Drawing.Size(75, 23);
            this.btnSummit.TabIndex = 5;
            this.btnSummit.Text = "Submit";
            this.btnSummit.UseVisualStyleBackColor = true;
            this.btnSummit.Click += new System.EventHandler(this.btnSummit_Click);
            // 
            // frmInsertKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 247);
            this.Controls.Add(this.btnSummit);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.txtShareCode);
            this.Controls.Add(this.txtKeyCode);
            this.Controls.Add(this.panel1);
            this.Name = "frmInsertKey";
            this.Text = "frmInsertKey";
            this.Load += new System.EventHandler(this.frmInsertKey_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenKey;
        private System.Windows.Forms.RadioButton rdCheckOut;
        private System.Windows.Forms.RadioButton rdCheckIn;
        private System.Windows.Forms.TextBox txtKeyCode;
        private System.Windows.Forms.TextBox txtShareCode;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Button btnSummit;
    }
}