namespace PangYaItensManagement
{
    partial class Form_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_choose_login = new System.Windows.Forms.Label();
            this.listBox_available_logins = new System.Windows.Forms.ListBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_connect_database = new System.Windows.Forms.Button();
            this.lbl_server_database_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = global::PangYaItensManagement.Properties.Resources.bg_pink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_choose_login, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox_available_logins, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_login, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_connect_database, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_server_database_status, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(276, 161);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_choose_login
            // 
            this.lbl_choose_login.AutoSize = true;
            this.lbl_choose_login.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_choose_login, 3);
            this.lbl_choose_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_choose_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_choose_login.Location = new System.Drawing.Point(3, 0);
            this.lbl_choose_login.Name = "lbl_choose_login";
            this.lbl_choose_login.Size = new System.Drawing.Size(270, 20);
            this.lbl_choose_login.TabIndex = 0;
            this.lbl_choose_login.Text = "Choose your login";
            this.lbl_choose_login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox_available_logins
            // 
            this.listBox_available_logins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_available_logins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_available_logins.FormattingEnabled = true;
            this.listBox_available_logins.Location = new System.Drawing.Point(3, 23);
            this.listBox_available_logins.Name = "listBox_available_logins";
            this.tableLayoutPanel1.SetRowSpan(this.listBox_available_logins, 3);
            this.listBox_available_logins.Size = new System.Drawing.Size(159, 135);
            this.listBox_available_logins.TabIndex = 1;
            // 
            // btn_login
            // 
            this.btn_login.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_login.Location = new System.Drawing.Point(168, 23);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(49, 40);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.Btn_login_Click);
            // 
            // btn_connect_database
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btn_connect_database, 2);
            this.btn_connect_database.Location = new System.Drawing.Point(168, 69);
            this.btn_connect_database.Name = "btn_connect_database";
            this.btn_connect_database.Size = new System.Drawing.Size(80, 40);
            this.btn_connect_database.TabIndex = 3;
            this.btn_connect_database.Text = "Connect to database server";
            this.btn_connect_database.UseVisualStyleBackColor = true;
            this.btn_connect_database.Click += new System.EventHandler(this.Btn_connect_database_Click);
            // 
            // lbl_server_database_status
            // 
            this.lbl_server_database_status.AutoSize = true;
            this.lbl_server_database_status.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_server_database_status, 2);
            this.lbl_server_database_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_server_database_status.ForeColor = System.Drawing.Color.Red;
            this.lbl_server_database_status.Location = new System.Drawing.Point(168, 141);
            this.lbl_server_database_status.Name = "lbl_server_database_status";
            this.lbl_server_database_status.Size = new System.Drawing.Size(80, 13);
            this.lbl_server_database_status.TabIndex = 4;
            this.lbl_server_database_status.Text = "Database off";
            // 
            // Form_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 161);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Login";
            this.Text = "PangYa IMGT Login";
            this.Shown += new System.EventHandler(this.Form_Login_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_choose_login;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.ListBox listBox_available_logins;
        private System.Windows.Forms.Button btn_connect_database;
        private System.Windows.Forms.Label lbl_server_database_status;
    }
}