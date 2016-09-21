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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.mTabControlConfiguracion = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPageConfiguration = new MetroFramework.Controls.MetroTabPage();
            this.gpbHistorycal = new System.Windows.Forms.GroupBox();
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
            this.mTabControlConfiguracion.SuspendLayout();
            this.metroTabPageConfiguration.SuspendLayout();
            this.gpbHistorycal.SuspendLayout();
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
            this.btnUserFolder.Location = new System.Drawing.Point(291, 71);
            this.btnUserFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUserFolder.Name = "btnUserFolder";
            this.btnUserFolder.Size = new System.Drawing.Size(193, 28);
            this.btnUserFolder.TabIndex = 15;
            this.btnUserFolder.Text = "Elegir carpeta";
            this.btnUserFolder.UseVisualStyleBackColor = true;
            this.btnUserFolder.Click += new System.EventHandler(this.btnUserFolder_Click);
            // 
            // mtxt_userfolder
            // 
            this.mtxt_userfolder.Enabled = false;
            this.mtxt_userfolder.Location = new System.Drawing.Point(12, 34);
            this.mtxt_userfolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_userfolder.Name = "mtxt_userfolder";
            this.mtxt_userfolder.ReadOnly = true;
            this.mtxt_userfolder.Size = new System.Drawing.Size(477, 28);
            this.mtxt_userfolder.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(436, 658);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 28);
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
            this.mTabControlConfiguracion.Location = new System.Drawing.Point(19, 79);
            this.mTabControlConfiguracion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mTabControlConfiguracion.Name = "mTabControlConfiguracion";
            this.mTabControlConfiguracion.SelectedIndex = 1;
            this.mTabControlConfiguracion.Size = new System.Drawing.Size(527, 572);
            this.mTabControlConfiguracion.TabIndex = 10;
            // 
            // metroTabPageConfiguration
            // 
            this.metroTabPageConfiguration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.metroTabPageConfiguration.Controls.Add(this.gpbHistorycal);
            this.metroTabPageConfiguration.Controls.Add(this.gpbCredenciales);
            this.metroTabPageConfiguration.Controls.Add(this.gpbIntervalo);
            this.metroTabPageConfiguration.Controls.Add(this.gpbConfRuta);
            this.metroTabPageConfiguration.HorizontalScrollbarBarColor = true;
            this.metroTabPageConfiguration.HorizontalScrollbarSize = 12;
            this.metroTabPageConfiguration.Location = new System.Drawing.Point(4, 39);
            this.metroTabPageConfiguration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroTabPageConfiguration.Name = "metroTabPageConfiguration";
            this.metroTabPageConfiguration.Size = new System.Drawing.Size(519, 529);
            this.metroTabPageConfiguration.TabIndex = 0;
            this.metroTabPageConfiguration.Text = "Configuración";
            this.metroTabPageConfiguration.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPageConfiguration.VerticalScrollbarBarColor = true;
            this.metroTabPageConfiguration.VerticalScrollbarSize = 13;
            // 
            // gpbHistorycal
            // 
            this.gpbHistorycal.Controls.Add(this.btnUserFolder);
            this.gpbHistorycal.Controls.Add(this.mtxt_userfolder);
            this.gpbHistorycal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gpbHistorycal.Location = new System.Drawing.Point(4, 150);
            this.gpbHistorycal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbHistorycal.Name = "gpbHistorycal";
            this.gpbHistorycal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbHistorycal.Size = new System.Drawing.Size(512, 116);
            this.gpbHistorycal.TabIndex = 0;
            this.gpbHistorycal.TabStop = false;
            this.gpbHistorycal.Text = "Ruta de historicos";
            this.gpbHistorycal.Visible = false;
            this.gpbHistorycal.Enter += new System.EventHandler(this.gpbHistorycal_Enter);
            // 
            // gpbCredenciales
            // 
            this.gpbCredenciales.Controls.Add(this.btnDesvincular);
            this.gpbCredenciales.Controls.Add(this.lblInfoCuenta);
            this.gpbCredenciales.Controls.Add(this.btnLogin);
            this.gpbCredenciales.Controls.Add(this.lblAlert);
            this.gpbCredenciales.Location = new System.Drawing.Point(4, 389);
            this.gpbCredenciales.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbCredenciales.Name = "gpbCredenciales";
            this.gpbCredenciales.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbCredenciales.Size = new System.Drawing.Size(509, 117);
            this.gpbCredenciales.TabIndex = 13;
            this.gpbCredenciales.TabStop = false;
            this.gpbCredenciales.Text = "Configuración de cuenta";
            // 
            // btnDesvincular
            // 
            this.btnDesvincular.Enabled = false;
            this.btnDesvincular.Location = new System.Drawing.Point(219, 58);
            this.btnDesvincular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDesvincular.Name = "btnDesvincular";
            this.btnDesvincular.Size = new System.Drawing.Size(193, 28);
            this.btnDesvincular.TabIndex = 13;
            this.btnDesvincular.Text = "Desvincular cuenta";
            this.btnDesvincular.UseVisualStyleBackColor = true;
            this.btnDesvincular.Click += new System.EventHandler(this.btnDesvincular_Click);
            // 
            // lblInfoCuenta
            // 
            this.lblInfoCuenta.AutoSize = true;
            this.lblInfoCuenta.Location = new System.Drawing.Point(9, 25);
            this.lblInfoCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfoCuenta.Name = "lblInfoCuenta";
            this.lblInfoCuenta.Size = new System.Drawing.Size(197, 17);
            this.lblInfoCuenta.TabIndex = 12;
            this.lblInfoCuenta.Text = "Obtén acceso a tus respaldos";
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnLogin.Location = new System.Drawing.Point(13, 59);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(193, 28);
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
            this.lblAlert.Location = new System.Drawing.Point(91, 129);
            this.lblAlert.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(0, 17);
            this.lblAlert.TabIndex = 10;
            // 
            // gpbIntervalo
            // 
            this.gpbIntervalo.Controls.Add(this.mtxt_time);
            this.gpbIntervalo.Controls.Add(this.lblTiempo);
            this.gpbIntervalo.Location = new System.Drawing.Point(1, 273);
            this.gpbIntervalo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbIntervalo.Name = "gpbIntervalo";
            this.gpbIntervalo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbIntervalo.Size = new System.Drawing.Size(512, 98);
            this.gpbIntervalo.TabIndex = 0;
            this.gpbIntervalo.TabStop = false;
            this.gpbIntervalo.Text = "Frecuencia de respaldos";
            // 
            // mtxt_time
            // 
            this.mtxt_time.Enabled = false;
            this.mtxt_time.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxt_time.Location = new System.Drawing.Point(301, 41);
            this.mtxt_time.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_time.Name = "mtxt_time";
            this.mtxt_time.Size = new System.Drawing.Size(188, 28);
            this.mtxt_time.TabIndex = 0;
            this.mtxt_time.Text = "0 Horas";
            this.mtxt_time.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(12, 49);
            this.lblTiempo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(120, 20);
            this.lblTiempo.TabIndex = 3;
            this.lblTiempo.Text = "Respaldo cada";
            // 
            // gpbConfRuta
            // 
            this.gpbConfRuta.Controls.Add(this.mtxt_folder);
            this.gpbConfRuta.Controls.Add(this.lblCarpeta);
            this.gpbConfRuta.Controls.Add(this.btnElegir);
            this.gpbConfRuta.Location = new System.Drawing.Point(4, 11);
            this.gpbConfRuta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbConfRuta.Name = "gpbConfRuta";
            this.gpbConfRuta.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbConfRuta.Size = new System.Drawing.Size(512, 132);
            this.gpbConfRuta.TabIndex = 6;
            this.gpbConfRuta.TabStop = false;
            this.gpbConfRuta.Text = "Configuración de ruta de respaldos";
            // 
            // mtxt_folder
            // 
            this.mtxt_folder.Enabled = false;
            this.mtxt_folder.Location = new System.Drawing.Point(9, 50);
            this.mtxt_folder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_folder.Name = "mtxt_folder";
            this.mtxt_folder.ReadOnly = true;
            this.mtxt_folder.Size = new System.Drawing.Size(477, 28);
            this.mtxt_folder.TabIndex = 1;
            // 
            // lblCarpeta
            // 
            this.lblCarpeta.AutoSize = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpeta.Location = new System.Drawing.Point(8, 20);
            this.lblCarpeta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(145, 20);
            this.lblCarpeta.TabIndex = 0;
            this.lblCarpeta.Text = "Ruta de respaldos";
            // 
            // btnElegir
            // 
            this.btnElegir.Location = new System.Drawing.Point(288, 86);
            this.btnElegir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnElegir.Name = "btnElegir";
            this.btnElegir.Size = new System.Drawing.Size(193, 28);
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
            this.metroTabPageAccount.HorizontalScrollbarSize = 12;
            this.metroTabPageAccount.Location = new System.Drawing.Point(4, 39);
            this.metroTabPageAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroTabPageAccount.Name = "metroTabPageAccount";
            this.metroTabPageAccount.Size = new System.Drawing.Size(519, 529);
            this.metroTabPageAccount.TabIndex = 1;
            this.metroTabPageAccount.Text = "Mi cuenta";
            this.metroTabPageAccount.VerticalScrollbarBarColor = true;
            this.metroTabPageAccount.VerticalScrollbarSize = 13;
            this.metroTabPageAccount.Click += new System.EventHandler(this.metroTabPageAccount_Click);
            // 
            // mtxt_totalspace
            // 
            this.mtxt_totalspace.AutoSize = true;
            this.mtxt_totalspace.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mtxt_totalspace.Location = new System.Drawing.Point(25, 209);
            this.mtxt_totalspace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mtxt_totalspace.Name = "mtxt_totalspace";
            this.mtxt_totalspace.Size = new System.Drawing.Size(0, 0);
            this.mtxt_totalspace.TabIndex = 15;
            // 
            // mtxt_cloudHist
            // 
            this.mtxt_cloudHist.Enabled = false;
            this.mtxt_cloudHist.Location = new System.Drawing.Point(25, 383);
            this.mtxt_cloudHist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_cloudHist.Name = "mtxt_cloudHist";
            this.mtxt_cloudHist.Size = new System.Drawing.Size(465, 28);
            this.mtxt_cloudHist.TabIndex = 14;
            // 
            // mlbl_cloudHist
            // 
            this.mlbl_cloudHist.AutoSize = true;
            this.mlbl_cloudHist.Location = new System.Drawing.Point(21, 343);
            this.mlbl_cloudHist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlbl_cloudHist.Name = "mlbl_cloudHist";
            this.mlbl_cloudHist.Size = new System.Drawing.Size(139, 20);
            this.mlbl_cloudHist.TabIndex = 13;
            this.mlbl_cloudHist.Text = "Historicos en la nube";
            this.mlbl_cloudHist.UseMnemonic = false;
            // 
            // mtxt_localHist
            // 
            this.mtxt_localHist.Enabled = false;
            this.mtxt_localHist.Location = new System.Drawing.Point(25, 295);
            this.mtxt_localHist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_localHist.Name = "mtxt_localHist";
            this.mtxt_localHist.Size = new System.Drawing.Size(465, 28);
            this.mtxt_localHist.TabIndex = 12;
            // 
            // mlbl_localHist
            // 
            this.mlbl_localHist.AutoSize = true;
            this.mlbl_localHist.Location = new System.Drawing.Point(21, 256);
            this.mlbl_localHist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlbl_localHist.Name = "mlbl_localHist";
            this.mlbl_localHist.Size = new System.Drawing.Size(101, 20);
            this.mlbl_localHist.TabIndex = 11;
            this.mlbl_localHist.Text = "Historicos local";
            // 
            // mlbl_usespace
            // 
            this.mlbl_usespace.AutoSize = true;
            this.mlbl_usespace.Location = new System.Drawing.Point(21, 130);
            this.mlbl_usespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlbl_usespace.Name = "mlbl_usespace";
            this.mlbl_usespace.Size = new System.Drawing.Size(186, 20);
            this.mlbl_usespace.TabIndex = 8;
            this.mlbl_usespace.Text = "Espacio utilizado en mi nube";
            // 
            // mtxt_user
            // 
            this.mtxt_user.Enabled = false;
            this.mtxt_user.Location = new System.Drawing.Point(21, 70);
            this.mtxt_user.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxt_user.Name = "mtxt_user";
            this.mtxt_user.Size = new System.Drawing.Size(465, 28);
            this.mtxt_user.TabIndex = 3;
            // 
            // mlbl_user
            // 
            this.mlbl_user.AutoSize = true;
            this.mlbl_user.Location = new System.Drawing.Point(21, 32);
            this.mlbl_user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlbl_user.Name = "mlbl_user";
            this.mlbl_user.Size = new System.Drawing.Size(56, 20);
            this.mlbl_user.TabIndex = 2;
            this.mlbl_user.Text = "Usuario";
            // 
            // metroPB_CloudSpace
            // 
            this.metroPB_CloudSpace.FontWeight = MetroFramework.MetroProgressBarWeight.Regular;
            this.metroPB_CloudSpace.Location = new System.Drawing.Point(25, 158);
            this.metroPB_CloudSpace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroPB_CloudSpace.Name = "metroPB_CloudSpace";
            this.metroPB_CloudSpace.RightToLeftLayout = true;
            this.metroPB_CloudSpace.Size = new System.Drawing.Size(465, 36);
            this.metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Green;
            this.metroPB_CloudSpace.TabIndex = 10;
            this.metroPB_CloudSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroPB_CloudSpace.Click += new System.EventHandler(this.metroPB_CloudSpace_Click);
            // 
            // metroTabPageHistorico
            // 
            this.metroTabPageHistorico.Controls.Add(this.dataGridViewHistoricos);
            this.metroTabPageHistorico.HorizontalScrollbarBarColor = true;
            this.metroTabPageHistorico.HorizontalScrollbarSize = 12;
            this.metroTabPageHistorico.Location = new System.Drawing.Point(4, 39);
            this.metroTabPageHistorico.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroTabPageHistorico.Name = "metroTabPageHistorico";
            this.metroTabPageHistorico.Size = new System.Drawing.Size(519, 529);
            this.metroTabPageHistorico.TabIndex = 2;
            this.metroTabPageHistorico.Text = "Último respaldo exitoso";
            this.metroTabPageHistorico.VerticalScrollbarBarColor = true;
            this.metroTabPageHistorico.VerticalScrollbarSize = 13;
            // 
            // dataGridViewHistoricos
            // 
            this.dataGridViewHistoricos.AllowUserToAddRows = false;
            this.dataGridViewHistoricos.AllowUserToDeleteRows = false;
            this.dataGridViewHistoricos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoricos.Location = new System.Drawing.Point(0, 16);
            this.dataGridViewHistoricos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewHistoricos.MultiSelect = false;
            this.dataGridViewHistoricos.Name = "dataGridViewHistoricos";
            this.dataGridViewHistoricos.ReadOnly = true;
            this.dataGridViewHistoricos.RowHeadersWidth = 61;
            this.dataGridViewHistoricos.Size = new System.Drawing.Size(521, 185);
            this.dataGridViewHistoricos.TabIndex = 2;
            this.dataGridViewHistoricos.SelectionChanged += new System.EventHandler(this.dataGridViewHistoricos_SelectionChanged);
            // 
            // ConfiguracionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 711);
            this.Controls.Add(this.mTabControlConfiguracion);
            this.Controls.Add(this.btnAceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "ConfiguracionForm";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.ConfiguracionForm_Load);
            this.mTabControlConfiguracion.ResumeLayout(false);
            this.metroTabPageConfiguration.ResumeLayout(false);
            this.gpbHistorycal.ResumeLayout(false);
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