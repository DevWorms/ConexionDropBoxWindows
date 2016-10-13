namespace Scanda.AppTray
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblAlert = new System.Windows.Forms.Label();
            this.pbx_logo = new System.Windows.Forms.PictureBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblMessages = new System.Windows.Forms.Label();
            this.mlbl_info = new MetroFramework.Controls.MetroLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(117, 282);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(115, 30);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Iniciar Sesion";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(117, 188);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(261, 26);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(117, 231);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(261, 26);
            this.txtPassword.TabIndex = 2;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblAlert.ForeColor = System.Drawing.Color.Red;
            this.lblAlert.Location = new System.Drawing.Point(126, 238);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(0, 13);
            this.lblAlert.TabIndex = 5;
            // 
            // pbx_logo
            // 
            this.pbx_logo.Image = global::Scanda.AppTray.Properties.Resources.DB_Protector_64X64;
            this.pbx_logo.Location = new System.Drawing.Point(181, 28);
            this.pbx_logo.Name = "pbx_logo";
            this.pbx_logo.Size = new System.Drawing.Size(117, 73);
            this.pbx_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_logo.TabIndex = 6;
            this.pbx_logo.TabStop = false;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Tai Le", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.Gray;
            this.lblCompany.Location = new System.Drawing.Point(114, 349);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(70, 13);
            this.lblCompany.TabIndex = 7;
            this.lblCompany.Text = "@ 2016 Xamal";
            this.lblCompany.Visible = false;
            // 
            // lblMessages
            // 
            this.lblMessages.AutoSize = true;
            this.lblMessages.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessages.ForeColor = System.Drawing.Color.Red;
            this.lblMessages.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblMessages.Location = new System.Drawing.Point(114, 152);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(0, 19);
            this.lblMessages.TabIndex = 9;
            // 
            // mlbl_info
            // 
            this.mlbl_info.AutoSize = true;
            this.mlbl_info.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlbl_info.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlbl_info.Location = new System.Drawing.Point(191, 117);
            this.mlbl_info.Name = "mlbl_info";
            this.mlbl_info.Size = new System.Drawing.Size(107, 25);
            this.mlbl_info.TabIndex = 10;
            this.mlbl_info.Text = "Inicia sesión";
            this.mlbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(354, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "v1.11";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 395);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mlbl_info);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.pbx_logo);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnLogin);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Resizable = false;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.PictureBox pbx_logo;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblMessages;
        private MetroFramework.Controls.MetroLabel mlbl_info;
        private System.Windows.Forms.Label label1;
    }
}