namespace sms
{
    partial class frmSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btMaximize = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btMinimize = new System.Windows.Forms.Button();
            this.chkfee = new System.Windows.Forms.CheckBox();
            this.chkSale = new System.Windows.Forms.CheckBox();
            this.chkreport = new System.Windows.Forms.CheckBox();
            this.chksms = new System.Windows.Forms.CheckBox();
            this.chkfeesms = new System.Windows.Forms.CheckBox();
            this.chkreminder = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkhindiformt = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-6, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(487, 29);
            this.label1.TabIndex = 111;
            this.label1.Text = "Setting";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(274, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 40);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Blue;
            this.btnCancel.Location = new System.Drawing.Point(175, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(-1, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(483, 6);
            this.label5.TabIndex = 117;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label4.Location = new System.Drawing.Point(-2, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(483, 2);
            this.label4.TabIndex = 116;
            this.label4.Text = "label4";
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.Blue;
            this.btnCreate.Location = new System.Drawing.Point(71, 2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(102, 40);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Update";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label3.Location = new System.Drawing.Point(-2, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(483, 2);
            this.label3.TabIndex = 115;
            this.label3.Text = "label3";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnCreate);
            this.panel2.Location = new System.Drawing.Point(-1, 361);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 46);
            this.panel2.TabIndex = 114;
            // 
            // btMaximize
            // 
            this.btMaximize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMaximize.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMaximize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btMaximize.Location = new System.Drawing.Point(420, 2);
            this.btMaximize.Name = "btMaximize";
            this.btMaximize.Size = new System.Drawing.Size(30, 24);
            this.btMaximize.TabIndex = 124;
            this.btMaximize.Text = "⁬";
            this.btMaximize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btMaximize.UseVisualStyleBackColor = true;
            this.btMaximize.Visible = false;
            // 
            // btExit
            // 
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btExit.Location = new System.Drawing.Point(450, 2);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(30, 24);
            this.btExit.TabIndex = 122;
            this.btExit.Text = "X";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Visible = false;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btMinimize
            // 
            this.btMinimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMinimize.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btMinimize.Location = new System.Drawing.Point(390, 2);
            this.btMinimize.Name = "btMinimize";
            this.btMinimize.Size = new System.Drawing.Size(30, 24);
            this.btMinimize.TabIndex = 123;
            this.btMinimize.Text = "−";
            this.btMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btMinimize.UseVisualStyleBackColor = true;
            this.btMinimize.Visible = false;
            // 
            // chkfee
            // 
            this.chkfee.AutoSize = true;
            this.chkfee.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkfee.ForeColor = System.Drawing.Color.Navy;
            this.chkfee.Location = new System.Drawing.Point(25, 76);
            this.chkfee.Name = "chkfee";
            this.chkfee.Size = new System.Drawing.Size(233, 18);
            this.chkfee.TabIndex = 126;
            this.chkfee.Text = "Fee Reciept (Single Or Double)";
            this.chkfee.UseVisualStyleBackColor = true;
            // 
            // chkSale
            // 
            this.chkSale.AutoSize = true;
            this.chkSale.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkSale.ForeColor = System.Drawing.Color.Navy;
            this.chkSale.Location = new System.Drawing.Point(25, 111);
            this.chkSale.Name = "chkSale";
            this.chkSale.Size = new System.Drawing.Size(238, 18);
            this.chkSale.TabIndex = 127;
            this.chkSale.Text = "Sale Reciept (Single Or Double)";
            this.chkSale.UseVisualStyleBackColor = true;
            // 
            // chkreport
            // 
            this.chkreport.AutoSize = true;
            this.chkreport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkreport.ForeColor = System.Drawing.Color.Navy;
            this.chkreport.Location = new System.Drawing.Point(25, 144);
            this.chkreport.Name = "chkreport";
            this.chkreport.Size = new System.Drawing.Size(294, 18);
            this.chkreport.TabIndex = 128;
            this.chkreport.Text = "Report Card (MarksWise Or GradeWise)";
            this.chkreport.UseVisualStyleBackColor = true;
            // 
            // chksms
            // 
            this.chksms.AutoSize = true;
            this.chksms.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chksms.ForeColor = System.Drawing.Color.Navy;
            this.chksms.Location = new System.Drawing.Point(25, 181);
            this.chksms.Name = "chksms";
            this.chksms.Size = new System.Drawing.Size(207, 18);
            this.chksms.TabIndex = 129;
            this.chksms.Text = "SMS Enable For Attendance";
            this.chksms.UseVisualStyleBackColor = true;
            // 
            // chkfeesms
            // 
            this.chkfeesms.AutoSize = true;
            this.chkfeesms.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkfeesms.ForeColor = System.Drawing.Color.Navy;
            this.chkfeesms.Location = new System.Drawing.Point(25, 216);
            this.chkfeesms.Name = "chkfeesms";
            this.chkfeesms.Size = new System.Drawing.Size(226, 18);
            this.chkfeesms.TabIndex = 130;
            this.chkfeesms.Text = "SMS Enable For Fee Collection";
            this.chkfeesms.UseVisualStyleBackColor = true;
            // 
            // chkreminder
            // 
            this.chkreminder.AutoSize = true;
            this.chkreminder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkreminder.ForeColor = System.Drawing.Color.Navy;
            this.chkreminder.Location = new System.Drawing.Point(25, 250);
            this.chkreminder.Name = "chkreminder";
            this.chkreminder.Size = new System.Drawing.Size(224, 18);
            this.chkreminder.TabIndex = 131;
            this.chkreminder.Text = "SMS Enable For Fee Reminder";
            this.chkreminder.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(46, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 52);
            this.button1.TabIndex = 132;
            this.button1.Text = "ICARD SETTING";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkhindiformt
            // 
            this.chkhindiformt.AutoSize = true;
            this.chkhindiformt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkhindiformt.ForeColor = System.Drawing.Color.Navy;
            this.chkhindiformt.Location = new System.Drawing.Point(25, 275);
            this.chkhindiformt.Name = "chkhindiformt";
            this.chkhindiformt.Size = new System.Drawing.Size(173, 18);
            this.chkhindiformt.TabIndex = 133;
            this.chkhindiformt.Text = "TC  FORMAT IN HINDI";
            this.chkhindiformt.UseVisualStyleBackColor = true;
            // 
            // frmSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(481, 413);
            this.Controls.Add(this.chkhindiformt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkreminder);
            this.Controls.Add(this.chkfeesms);
            this.Controls.Add(this.chksms);
            this.Controls.Add(this.chkreport);
            this.Controls.Add(this.chkSale);
            this.Controls.Add(this.chkfee);
            this.Controls.Add(this.btMaximize);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btMinimize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btMaximize;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btMinimize;
        private System.Windows.Forms.CheckBox chkfee;
        private System.Windows.Forms.CheckBox chkSale;
        private System.Windows.Forms.CheckBox chkreport;
        private System.Windows.Forms.CheckBox chksms;
        private System.Windows.Forms.CheckBox chkfeesms;
        private System.Windows.Forms.CheckBox chkreminder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkhindiformt;
    }
}