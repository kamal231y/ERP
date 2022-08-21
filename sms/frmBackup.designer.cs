namespace sms
{
    partial class frmBackup
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
            this.btBrowse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btBackup = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackUp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btBrowse
            // 
            this.btBrowse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btBrowse.Location = new System.Drawing.Point(324, 135);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(104, 30);
            this.btBrowse.TabIndex = 48;
            this.btBrowse.Text = "&Browse...";
            this.btBrowse.UseVisualStyleBackColor = false;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(172, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 16);
            this.label7.TabIndex = 47;
            this.label7.Text = ":";
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btClose.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.ForeColor = System.Drawing.Color.Blue;
            this.btClose.Location = new System.Drawing.Point(342, 241);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(168, 35);
            this.btClose.TabIndex = 49;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(50, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 18);
            this.label8.TabIndex = 46;
            this.label8.Text = "BackUp- Path";
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btCancel.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.ForeColor = System.Drawing.Color.Blue;
            this.btCancel.Location = new System.Drawing.Point(175, 241);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(167, 35);
            this.btCancel.TabIndex = 44;
            this.btCancel.Text = "&Cancel";
            this.btCancel.UseVisualStyleBackColor = false;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click_1);
            // 
            // btBackup
            // 
            this.btBackup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btBackup.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBackup.ForeColor = System.Drawing.Color.Blue;
            this.btBackup.Location = new System.Drawing.Point(2, 241);
            this.btBackup.Name = "btBackup";
            this.btBackup.Size = new System.Drawing.Size(173, 35);
            this.btBackup.TabIndex = 43;
            this.btBackup.Text = "&Backup";
            this.btBackup.UseVisualStyleBackColor = false;
            this.btBackup.Click += new System.EventHandler(this.btBackup_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(-3, 279);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(515, 7);
            this.label9.TabIndex = 42;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightBlue;
            this.label10.Location = new System.Drawing.Point(-6, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(515, 2);
            this.label10.TabIndex = 41;
            this.label10.Text = "label10";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(-2, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(514, 7);
            this.label4.TabIndex = 34;
            // 
            // txtBackUp
            // 
            this.txtBackUp.Location = new System.Drawing.Point(202, 109);
            this.txtBackUp.Name = "txtBackUp";
            this.txtBackUp.Size = new System.Drawing.Size(226, 20);
            this.txtBackUp.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Font = new System.Drawing.Font("Verdana", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(-5, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(517, 26);
            this.label12.TabIndex = 39;
            this.label12.Text = "Back-Up";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightBlue;
            this.label11.Location = new System.Drawing.Point(1, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(513, 2);
            this.label11.TabIndex = 40;
            this.label11.Text = "label11";
            // 
            // frmBackup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(512, 288);
            this.ControlBox = false;
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btBackup);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBackUp);
            this.Controls.Add(this.label12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBackup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btBackup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackUp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}