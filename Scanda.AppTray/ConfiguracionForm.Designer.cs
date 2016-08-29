namespace Scanda.AppTray
{
    partial class ConfiguracionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguracionForm));
            this.btnUserFolder = new System.Windows.Forms.Button();
            this.mtxt_userfolder = new MetroFramework.Controls.MetroTextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.mTabControlConfiguracion = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPageConfiguration = new MetroFramework.Controls.MetroTabPage();
            this.gpbCredenciales = new System.Windows.Forms.GroupBox();
            this.btnDesvincular = new System.Windows.Forms.Button();
            this.lblInfoCuenta = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblAlert = new System.Windows.Forms.Label();
            this.gpbIntervalo = new System.Windows.Forms.GroupBox();
            this.mtxt_time = new MetroFramework.Controls.MetroTextBox();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.gpbConfRuta = new System.Windows.Forms.GroupBox();
            this.mtxt_folder = new MetroFramework.Controls.MetroTextBox();
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.btnElegir = new System.Windows.Forms.Button();
            this.metroTabPageAccount = new MetroFramework.Controls.MetroTabPage();
            this.mtxt_totalspace = new MetroFramework.Controls.MetroLabel();
            this.mtxt_cloudHist = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_cloudHist = new MetroFramework.Controls.MetroLabel();
            this.mtxt_localHist = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_localHist = new MetroFramework.Controls.MetroLabel();
            this.mlbl_usespace = new MetroFramework.Controls.MetroLabel();
            this.mtxt_user = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_user = new MetroFramework.Controls.MetroLabel();
            this.metroPB_CloudSpace = new MetroFramework.Controls.MetroProgressBar();
            this.metroTabPageHistorico = new MetroFramework.Controls.MetroTabPage();
            this.dataGridViewHistoricos = new System.Windows.Forms.DataGridView();
            gpbHistorycal = new System.Windows.Forms.GroupBox();
            this.mTabControlConfiguracion.SuspendLayout();
            this.metroTabPageConfiguration.SuspendLayout();
            gpbHistorycal.SuspendLayout();
            this.gpbCredenciales.SuspendLayout();
            this.gpbIntervalo.SuspendLayout();
            this.gpbConfRuta.SuspendLayout();
            this.metroTabPageAccount.SuspendLayout();
            this.metroTabPageHistorico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoricos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUserFolder
            // 
            this.btnUserFolder.Location = new System.Drawing.Point(218, 58);
            this.btnUserFolder.Name = "btnUserFolder";
            this.btnUserFolder.Size = new System.Drawing.Size(145, 23);
            this.btnUserFolder.TabIndex = 15;
            this.btnUserFolder.Text = "Elegir carpeta";
            this.btnUserFolder.UseVisualStyleBackColor = true;
            this.btnUserFolder.Click += new System.EventHandler(this.btnUserFolder_Click);
            // 
            // mtxt_userfolder
            // 
            this.mtxt_userfolder.Enabled = false;
            this.mtxt_userfolder.Location = new System.Drawing.Point(9, 28);
            this.mtxt_userfolder.Name = "mtxt_userfolder";
            this.mtxt_userfolder.ReadOnly = true;
            this.mtxt_userfolder.Size = new System.Drawing.Size(358, 23);
            this.mtxt_userfolder.TabIndex = 14;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(328, 535);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(238, 535);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // mTabControlConfiguracion
            // 
            this.mTabControlConfiguracion.Controls.Add(this.metroTabPageConfiguration);
            this.mTabControlConfiguracion.Controls.Add(this.metroTabPageAccount);
            this.mTabControlConfiguracion.Controls.Add(this.metroTabPageHistorico);
            this.mTabControlConfiguracion.Location = new System.Drawing.Point(14, 64);
            this.mTabControlConfiguracion.Name = "mTabControlConfiguracion";
            this.mTabControlConfiguracion.SelectedIndex = 0;
            this.mTabControlConfiguracion.Size = new System.Drawing.Size(395, 465);
            this.mTabControlConfiguracion.TabIndex = 10;
            // 
            // metroTabPageConfiguration
            // 
            this.metroTabPageConfiguration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.metroTabPageConfiguration.Controls.Add(gpbHistorycal);
            this.metroTabPageConfiguration.Controls.Add(this.gpbCredenciales);
            this.metroTabPageConfiguration.Controls.Add(this.gpbIntervalo);
            this.metroTabPageConfiguration.Controls.Add(this.gpbConfRuta);
            this.metroTabPageConfiguration.HorizontalScrollbarBarColor = true;
            this.metroTabPageConfiguration.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageConfiguration.Name = "metroTabPageConfiguration";
            this.metroTabPageConfiguration.Size = new System.Drawing.Size(387, 426);
            this.metroTabPageConfiguration.TabIndex = 0;
            this.metroTabPageConfiguration.Text = "Configuración";
            this.metroTabPageConfiguration.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPageConfiguration.VerticalScrollbarBarColor = true;
            // 
            // gpbHistorycal
            // 
            gpbHistorycal.Controls.Add(this.btnUserFolder);
            gpbHistorycal.Controls.Add(this.mtxt_userfolder);
            gpbHistorycal.ForeColor = System.Drawing.SystemColors.ControlText;
            gpbHistorycal.Location = new System.Drawing.Point(3, 122);
            gpbHistorycal.Name = "gpbHistorycal";
            gpbHistorycal.Size = new System.Drawing.Size(384, 94);
            gpbHistorycal.TabIndex = 14;
            gpbHistorycal.TabStop = false;
            gpbHistorycal.Text = "Ruta de historicos";
            gpbHistorycal.Visible = false;
            gpbHistorycal.Enter += new System.EventHandler(this.gpbHistorycal_Enter);
            // 
            // gpbCredenciales
            // 
            this.gpbCredenciales.Controls.Add(this.btnDesvincular);
            this.gpbCredenciales.Controls.Add(this.lblInfoCuenta);
            this.gpbCredenciales.Controls.Add(this.btnLogin);
            this.gpbCredenciales.Controls.Add(this.lblAlert);
            this.gpbCredenciales.Location = new System.Drawing.Point(3, 316);
            this.gpbCredenciales.Name = "gpbCredenciales";
            this.gpbCredenciales.Size = new System.Drawing.Size(382, 95);
            this.gpbCredenciales.TabIndex = 13;
            this.gpbCredenciales.TabStop = false;
            this.gpbCredenciales.Text = "Configuración de cuenta";
            // 
            // btnDesvincular
            // 
            this.btnDesvincular.Enabled = false;
            this.btnDesvincular.Location = new System.Drawing.Point(164, 47);
            this.btnDesvincular.Name = "btnDesvincular";
            this.btnDesvincular.Size = new System.Drawing.Size(145, 23);
            this.btnDesvincular.TabIndex = 13;
            this.btnDesvincular.Text = "Desvincular cuenta";
            this.btnDesvincular.UseVisualStyleBackColor = true;
            this.btnDesvincular.Click += new System.EventHandler(this.btnDesvincular_Click);
            // 
            // lblInfoCuenta
            // 
            this.lblInfoCuenta.AutoSize = true;
            this.lblInfoCuenta.Location = new System.Drawing.Point(7, 20);
            this.lblInfoCuenta.Name = "lblInfoCuenta";
            this.lblInfoCuenta.Size = new System.Drawing.Size(148, 13);
            this.lblInfoCuenta.TabIndex = 12;
            this.lblInfoCuenta.Text = "Obtén acceso a tus respaldos";
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnLogin.Location = new System.Drawing.Point(10, 48);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(145, 23);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "Agregar cuenta";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblAlert.ForeColor = System.Drawing.Color.Red;
            this.lblAlert.Location = new System.Drawing.Point(68, 105);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(0, 13);
            this.lblAlert.TabIndex = 10;
            // 
            // gpbIntervalo
            // 
            this.gpbIntervalo.Controls.Add(this.mtxt_time);
            this.gpbIntervalo.Controls.Add(this.lblTiempo);
            this.gpbIntervalo.Location = new System.Drawing.Point(1, 222);
            this.gpbIntervalo.Name = "gpbIntervalo";
            this.gpbIntervalo.Size = new System.Drawing.Size(384, 80);
            this.gpbIntervalo.TabIndex = 12;
            this.gpbIntervalo.TabStop = false;
            this.gpbIntervalo.Text = "Frecuencia de respaldos";
            // 
            // mtxt_time
            // 
            this.mtxt_time.Enabled = false;
            this.mtxt_time.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxt_time.Location = new System.Drawing.Point(226, 33);
            this.mtxt_time.Name = "mtxt_time";
            this.mtxt_time.Size = new System.Drawing.Size(141, 23);
            this.mtxt_time.TabIndex = 13;
            this.mtxt_time.Text = "0 Horas";
            this.mtxt_time.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(9, 40);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(102, 16);
            this.lblTiempo.TabIndex = 3;
            this.lblTiempo.Text = "Respaldo cada";
            // 
            // gpbConfRuta
            // 
            this.gpbConfRuta.Controls.Add(this.mtxt_folder);
            this.gpbConfRuta.Controls.Add(this.lblCarpeta);
            this.gpbConfRuta.Controls.Add(this.btnElegir);
            this.gpbConfRuta.Location = new System.Drawing.Point(3, 9);
            this.gpbConfRuta.Name = "gpbConfRuta";
            this.gpbConfRuta.Size = new System.Drawing.Size(384, 107);
            this.gpbConfRuta.TabIndex = 6;
            this.gpbConfRuta.TabStop = false;
            this.gpbConfRuta.Text = "Configuración de Ruta de respaldos";
            // 
            // mtxt_folder
            // 
            this.mtxt_folder.Enabled = false;
            this.mtxt_folder.Location = new System.Drawing.Point(7, 41);
            this.mtxt_folder.Name = "mtxt_folder";
            this.mtxt_folder.ReadOnly = true;
            this.mtxt_folder.Size = new System.Drawing.Size(358, 23);
            this.mtxt_folder.TabIndex = 11;
            // 
            // lblCarpeta
            // 
            this.lblCarpeta.AutoSize = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpeta.Location = new System.Drawing.Point(6, 16);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(119, 16);
            this.lblCarpeta.TabIndex = 0;
            this.lblCarpeta.Text = "Ruta de respaldos";
            // 
            // btnElegir
            // 
            this.btnElegir.Location = new System.Drawing.Point(216, 70);
            this.btnElegir.Name = "btnElegir";
            this.btnElegir.Size = new System.Drawing.Size(145, 23);
            this.btnElegir.TabIndex = 1;
            this.btnElegir.Text = "Elegir carpeta";
            this.btnElegir.UseVisualStyleBackColor = true;
            this.btnElegir.Click += new System.EventHandler(this.btnElegir_Click);
            // 
            // metroTabPageAccount
            // 
            this.metroTabPageAccount.Controls.Add(this.mtxt_totalspace);
            this.metroTabPageAccount.Controls.Add(this.mtxt_cloudHist);
            this.metroTabPageAccount.Controls.Add(this.mlbl_cloudHist);
            this.metroTabPageAccount.Controls.Add(this.mtxt_localHist);
            this.metroTabPageAccount.Controls.Add(this.mlbl_localHist);
            this.metroTabPageAccount.Controls.Add(this.mlbl_usespace);
            this.metroTabPageAccount.Controls.Add(this.mtxt_user);
            this.metroTabPageAccount.Controls.Add(this.mlbl_user);
            this.metroTabPageAccount.Controls.Add(this.metroPB_CloudSpace);
            this.metroTabPageAccount.HorizontalScrollbarBarColor = true;
            this.metroTabPageAccount.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageAccount.Name = "metroTabPageAccount";
            this.metroTabPageAccount.Size = new System.Drawing.Size(387, 426);
            this.metroTabPageAccount.TabIndex = 1;
            this.metroTabPageAccount.Text = "Mi cuenta";
            this.metroTabPageAccount.VerticalScrollbarBarColor = true;
            this.metroTabPageAccount.Click += new System.EventHandler(this.metroTabPageAccount_Click);
            // 
            // mtxt_totalspace
            // 
            this.mtxt_totalspace.AutoSize = true;
            this.mtxt_totalspace.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mtxt_totalspace.Location = new System.Drawing.Point(19, 170);
            this.mtxt_totalspace.Name = "mtxt_totalspace";
            this.mtxt_totalspace.Size = new System.Drawing.Size(0, 0);
            this.mtxt_totalspace.TabIndex = 15;
            // 
            // mtxt_cloudHist
            // 
            this.mtxt_cloudHist.Enabled = false;
            this.mtxt_cloudHist.Location = new System.Drawing.Point(14, 311);
            this.mtxt_cloudHist.Name = "mtxt_cloudHist";
            this.mtxt_cloudHist.Size = new System.Drawing.Size(349, 23);
            this.mtxt_cloudHist.TabIndex = 14;
            // 
            // mlbl_cloudHist
            // 
            this.mlbl_cloudHist.AutoSize = true;
            this.mlbl_cloudHist.Location = new System.Drawing.Point(16, 279);
            this.mlbl_cloudHist.Name = "mlbl_cloudHist";
            this.mlbl_cloudHist.Size = new System.Drawing.Size(130, 19);
            this.mlbl_cloudHist.TabIndex = 13;
            this.mlbl_cloudHist.Text = "Historicos en la nube";
            this.mlbl_cloudHist.UseMnemonic = false;
            // 
            // mtxt_localHist
            // 
            this.mtxt_localHist.Enabled = false;
            this.mtxt_localHist.Location = new System.Drawing.Point(14, 238);
            this.mtxt_localHist.Name = "mtxt_localHist";
            this.mtxt_localHist.Size = new System.Drawing.Size(349, 23);
            this.mtxt_localHist.TabIndex = 12;
            // 
            // mlbl_localHist
            // 
            this.mlbl_localHist.AutoSize = true;
            this.mlbl_localHist.Location = new System.Drawing.Point(16, 208);
            this.mlbl_localHist.Name = "mlbl_localHist";
            this.mlbl_localHist.Size = new System.Drawing.Size(96, 19);
            this.mlbl_localHist.TabIndex = 11;
            this.mlbl_localHist.Text = "Historicos local";
            // 
            // mlbl_usespace
            // 
            this.mlbl_usespace.AutoSize = true;
            this.mlbl_usespace.Location = new System.Drawing.Point(16, 106);
            this.mlbl_usespace.Name = "mlbl_usespace";
            this.mlbl_usespace.Size = new System.Drawing.Size(123, 19);
            this.mlbl_usespace.TabIndex = 8;
            this.mlbl_usespace.Text = "Espacio en mi nube";
            // 
            // mtxt_user
            // 
            this.mtxt_user.Enabled = false;
            this.mtxt_user.Location = new System.Drawing.Point(16, 57);
            this.mtxt_user.Name = "mtxt_user";
            this.mtxt_user.Size = new System.Drawing.Size(349, 23);
            this.mtxt_user.TabIndex = 3;
            // 
            // mlbl_user
            // 
            this.mlbl_user.AutoSize = true;
            this.mlbl_user.Location = new System.Drawing.Point(16, 26);
            this.mlbl_user.Name = "mlbl_user";
            this.mlbl_user.Size = new System.Drawing.Size(53, 19);
            this.mlbl_user.TabIndex = 2;
            this.mlbl_user.Text = "Usuario";
            // 
            // metroPB_CloudSpace
            // 
            this.metroPB_CloudSpace.FontWeight = MetroFramework.MetroProgressBarWeight.Regular;
            this.metroPB_CloudSpace.HideProgressText = false;
            this.metroPB_CloudSpace.Location = new System.Drawing.Point(16, 128);
            this.metroPB_CloudSpace.Name = "metroPB_CloudSpace";
            this.metroPB_CloudSpace.RightToLeftLayout = true;
            this.metroPB_CloudSpace.Size = new System.Drawing.Size(349, 29);
            this.metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Green;
            this.metroPB_CloudSpace.TabIndex = 10;
            this.metroPB_CloudSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroPB_CloudSpace.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPB_CloudSpace.Click += new System.EventHandler(this.metroPB_CloudSpace_Click);
            // 
            // metroTabPageHistorico
            // 
            this.metroTabPageHistorico.Controls.Add(this.dataGridViewHistoricos);
            this.metroTabPageHistorico.HorizontalScrollbarBarColor = true;
            this.metroTabPageHistorico.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageHistorico.Name = "metroTabPageHistorico";
            this.metroTabPageHistorico.Size = new System.Drawing.Size(387, 426);
            this.metroTabPageHistorico.TabIndex = 2;
            this.metroTabPageHistorico.Text = "Último respaldo exitoso";
            this.metroTabPageHistorico.VerticalScrollbarBarColor = true;
            // 
            // dataGridViewHistoricos
            // 
            this.dataGridViewHistoricos.AllowUserToAddRows = false;
            this.dataGridViewHistoricos.AllowUserToDeleteRows = false;
            this.dataGridViewHistoricos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoricos.Location = new System.Drawing.Point(0, 13);
            this.dataGridViewHistoricos.MultiSelect = false;
            this.dataGridViewHistoricos.Name = "dataGridViewHistoricos";
            this.dataGridViewHistoricos.ReadOnly = true;
            this.dataGridViewHistoricos.RowHeadersWidth = 61;
            this.dataGridViewHistoricos.Size = new System.Drawing.Size(391, 150);
            this.dataGridViewHistoricos.TabIndex = 2;
            this.dataGridViewHistoricos.SelectionChanged += new System.EventHandler(this.dataGridViewHistoricos_SelectionChanged);
            // 
            // ConfiguracionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 578);
            this.Controls.Add(this.mTabControlConfiguracion);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfiguracionForm";
            this.Resizable = false;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.ConfiguracionForm_Load);
            this.mTabControlConfiguracion.ResumeLayout(false);
            this.metroTabPageConfiguration.ResumeLayout(false);
            gpbHistorycal.ResumeLayout(false);
            this.gpbCredenciales.ResumeLayout(false);
            this.gpbCredenciales.PerformLayout();
            this.gpbIntervalo.ResumeLayout(false);
            this.gpbIntervalo.PerformLayout();
            this.gpbConfRuta.ResumeLayout(false);
            this.gpbConfRuta.PerformLayout();
            this.metroTabPageAccount.ResumeLayout(false);
            this.metroTabPageAccount.PerformLayout();
            this.metroTabPageHistorico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoricos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gpbHistorycal;

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private MetroFramework.Controls.MetroTabControl mTabControlConfiguracion;
        private MetroFramework.Controls.MetroTabPage metroTabPageConfiguration;
        private System.Windows.Forms.GroupBox gpbConfRuta;
        private System.Windows.Forms.Label lblCarpeta;
        private System.Windows.Forms.Button btnElegir;
        private MetroFramework.Controls.MetroTabPage metroTabPageAccount;
        private System.Windows.Forms.GroupBox gpbCredenciales;
        private System.Windows.Forms.Button btnDesvincular;
        private System.Windows.Forms.Label lblInfoCuenta;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.GroupBox gpbIntervalo;
        private System.Windows.Forms.Label lblTiempo;
        private MetroFramework.Controls.MetroTextBox mtxt_folder;
        private MetroFramework.Controls.MetroTextBox mtxt_user;
        private MetroFramework.Controls.MetroLabel mlbl_user;
        private MetroFramework.Controls.MetroLabel mlbl_usespace;
        private MetroFramework.Controls.MetroTextBox mtxt_time;
        private MetroFramework.Controls.MetroProgressBar metroPB_CloudSpace;
        private MetroFramework.Controls.MetroLabel mlbl_cloudHist;
        private MetroFramework.Controls.MetroTextBox mtxt_localHist;
        private MetroFramework.Controls.MetroLabel mlbl_localHist;
        private System.Windows.Forms.Button btnUserFolder;
        private MetroFramework.Controls.MetroTextBox mtxt_userfolder;
        private MetroFramework.Controls.MetroTabPage metroTabPageHistorico;
        private System.Windows.Forms.DataGridView dataGridViewHistoricos;
        private MetroFramework.Controls.MetroTextBox mtxt_cloudHist;
        private MetroFramework.Controls.MetroLabel mtxt_totalspace;
    }
}