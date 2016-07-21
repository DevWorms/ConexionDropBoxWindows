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
            this.mcmbTime = new MetroFramework.Controls.MetroComboBox();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.gpbConfRuta = new System.Windows.Forms.GroupBox();
            this.mtxt_folder = new MetroFramework.Controls.MetroTextBox();
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.btnElegir = new System.Windows.Forms.Button();
            this.metroTabPageAccount = new MetroFramework.Controls.MetroTabPage();
            this.mtxt_usespace = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_usespace = new MetroFramework.Controls.MetroLabel();
            this.mtxt_avalaiblespace = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_avalaiblespace = new MetroFramework.Controls.MetroLabel();
            this.mtxt_totalspace = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_totalspace = new MetroFramework.Controls.MetroLabel();
            this.mtxt_user = new MetroFramework.Controls.MetroTextBox();
            this.mlbl_user = new MetroFramework.Controls.MetroLabel();
            this.mTabControlConfiguracion.SuspendLayout();
            this.metroTabPageConfiguration.SuspendLayout();
            this.gpbCredenciales.SuspendLayout();
            this.gpbIntervalo.SuspendLayout();
            this.gpbConfRuta.SuspendLayout();
            this.metroTabPageAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(328, 515);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(238, 515);
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
            this.mTabControlConfiguracion.Location = new System.Drawing.Point(14, 64);
            this.mTabControlConfiguracion.Name = "mTabControlConfiguracion";
            this.mTabControlConfiguracion.SelectedIndex = 0;
            this.mTabControlConfiguracion.Size = new System.Drawing.Size(395, 449);
            this.mTabControlConfiguracion.TabIndex = 10;
            // 
            // metroTabPageConfiguration
            // 
            this.metroTabPageConfiguration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.metroTabPageConfiguration.Controls.Add(this.gpbCredenciales);
            this.metroTabPageConfiguration.Controls.Add(this.gpbIntervalo);
            this.metroTabPageConfiguration.Controls.Add(this.gpbConfRuta);
            this.metroTabPageConfiguration.HorizontalScrollbarBarColor = true;
            this.metroTabPageConfiguration.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageConfiguration.Name = "metroTabPageConfiguration";
            this.metroTabPageConfiguration.Size = new System.Drawing.Size(387, 410);
            this.metroTabPageConfiguration.TabIndex = 0;
            this.metroTabPageConfiguration.Text = "Configuracion";
            this.metroTabPageConfiguration.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPageConfiguration.VerticalScrollbarBarColor = true;
            // 
            // gpbCredenciales
            // 
            this.gpbCredenciales.Controls.Add(this.btnDesvincular);
            this.gpbCredenciales.Controls.Add(this.lblInfoCuenta);
            this.gpbCredenciales.Controls.Add(this.btnLogin);
            this.gpbCredenciales.Controls.Add(this.lblAlert);
            this.gpbCredenciales.Location = new System.Drawing.Point(3, 271);
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
            this.btnDesvincular.Text = "Desvincular Cuenta";
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
            this.gpbIntervalo.Controls.Add(this.mcmbTime);
            this.gpbIntervalo.Controls.Add(this.lblTiempo);
            this.gpbIntervalo.Location = new System.Drawing.Point(1, 146);
            this.gpbIntervalo.Name = "gpbIntervalo";
            this.gpbIntervalo.Size = new System.Drawing.Size(384, 100);
            this.gpbIntervalo.TabIndex = 12;
            this.gpbIntervalo.TabStop = false;
            this.gpbIntervalo.Text = "Configuración frecuencía de respaldos";
            // 
            // mtxt_time
            // 
            this.mtxt_time.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxt_time.Location = new System.Drawing.Point(12, 59);
            this.mtxt_time.Name = "mtxt_time";
            this.mtxt_time.Size = new System.Drawing.Size(177, 23);
            this.mtxt_time.TabIndex = 13;
            this.mtxt_time.Text = "0";
            // 
            // mcmbTime
            // 
            this.mcmbTime.FormattingEnabled = true;
            this.mcmbTime.ItemHeight = 23;
            this.mcmbTime.Location = new System.Drawing.Point(207, 54);
            this.mcmbTime.Name = "mcmbTime";
            this.mcmbTime.Size = new System.Drawing.Size(160, 29);
            this.mcmbTime.TabIndex = 12;
            this.mcmbTime.SelectedIndexChanged += new System.EventHandler(this.mcmbTime_SelectedIndexChanged);
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(6, 35);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(213, 16);
            this.lblTiempo.TabIndex = 3;
            this.lblTiempo.Text = "Configure los tiempos de respaldo";
            // 
            // gpbConfRuta
            // 
            this.gpbConfRuta.Controls.Add(this.mtxt_folder);
            this.gpbConfRuta.Controls.Add(this.lblCarpeta);
            this.gpbConfRuta.Controls.Add(this.btnElegir);
            this.gpbConfRuta.Location = new System.Drawing.Point(3, 9);
            this.gpbConfRuta.Name = "gpbConfRuta";
            this.gpbConfRuta.Size = new System.Drawing.Size(384, 123);
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
            this.lblCarpeta.Size = new System.Drawing.Size(303, 16);
            this.lblCarpeta.TabIndex = 0;
            this.lblCarpeta.Text = "Elija la carpeta que sincroniza en este Dispositivo";
            // 
            // btnElegir
            // 
            this.btnElegir.Location = new System.Drawing.Point(216, 70);
            this.btnElegir.Name = "btnElegir";
            this.btnElegir.Size = new System.Drawing.Size(145, 23);
            this.btnElegir.TabIndex = 1;
            this.btnElegir.Text = "Elegir Carpeta";
            this.btnElegir.UseVisualStyleBackColor = true;
            this.btnElegir.Click += new System.EventHandler(this.btnElegir_Click);
            // 
            // metroTabPageAccount
            // 
            this.metroTabPageAccount.Controls.Add(this.mtxt_usespace);
            this.metroTabPageAccount.Controls.Add(this.mlbl_usespace);
            this.metroTabPageAccount.Controls.Add(this.mtxt_avalaiblespace);
            this.metroTabPageAccount.Controls.Add(this.mlbl_avalaiblespace);
            this.metroTabPageAccount.Controls.Add(this.mtxt_totalspace);
            this.metroTabPageAccount.Controls.Add(this.mlbl_totalspace);
            this.metroTabPageAccount.Controls.Add(this.mtxt_user);
            this.metroTabPageAccount.Controls.Add(this.mlbl_user);
            this.metroTabPageAccount.HorizontalScrollbarBarColor = true;
            this.metroTabPageAccount.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageAccount.Name = "metroTabPageAccount";
            this.metroTabPageAccount.Size = new System.Drawing.Size(387, 410);
            this.metroTabPageAccount.TabIndex = 1;
            this.metroTabPageAccount.Text = "Mi Cuenta";
            this.metroTabPageAccount.VerticalScrollbarBarColor = true;
            // 
            // mtxt_usespace
            // 
            this.mtxt_usespace.Enabled = false;
            this.mtxt_usespace.Location = new System.Drawing.Point(19, 286);
            this.mtxt_usespace.Name = "mtxt_usespace";
            this.mtxt_usespace.Size = new System.Drawing.Size(349, 23);
            this.mtxt_usespace.TabIndex = 9;
            // 
            // mlbl_usespace
            // 
            this.mlbl_usespace.AutoSize = true;
            this.mlbl_usespace.Location = new System.Drawing.Point(19, 253);
            this.mlbl_usespace.Name = "mlbl_usespace";
            this.mlbl_usespace.Size = new System.Drawing.Size(94, 19);
            this.mlbl_usespace.TabIndex = 8;
            this.mlbl_usespace.Text = "Espacio Usado";
            // 
            // mtxt_avalaiblespace
            // 
            this.mtxt_avalaiblespace.Enabled = false;
            this.mtxt_avalaiblespace.Location = new System.Drawing.Point(19, 210);
            this.mtxt_avalaiblespace.Name = "mtxt_avalaiblespace";
            this.mtxt_avalaiblespace.Size = new System.Drawing.Size(349, 23);
            this.mtxt_avalaiblespace.TabIndex = 7;
            // 
            // mlbl_avalaiblespace
            // 
            this.mlbl_avalaiblespace.AutoSize = true;
            this.mlbl_avalaiblespace.Location = new System.Drawing.Point(19, 177);
            this.mlbl_avalaiblespace.Name = "mlbl_avalaiblespace";
            this.mlbl_avalaiblespace.Size = new System.Drawing.Size(118, 19);
            this.mlbl_avalaiblespace.TabIndex = 6;
            this.mlbl_avalaiblespace.Text = "Espacio Disponible";
            // 
            // mtxt_totalspace
            // 
            this.mtxt_totalspace.Enabled = false;
            this.mtxt_totalspace.Location = new System.Drawing.Point(16, 132);
            this.mtxt_totalspace.Name = "mtxt_totalspace";
            this.mtxt_totalspace.Size = new System.Drawing.Size(349, 23);
            this.mtxt_totalspace.TabIndex = 5;
            // 
            // mlbl_totalspace
            // 
            this.mlbl_totalspace.AutoSize = true;
            this.mlbl_totalspace.Location = new System.Drawing.Point(16, 99);
            this.mlbl_totalspace.Name = "mlbl_totalspace";
            this.mlbl_totalspace.Size = new System.Drawing.Size(83, 19);
            this.mlbl_totalspace.TabIndex = 4;
            this.mlbl_totalspace.Text = "Espacio total";
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
            this.mlbl_user.Location = new System.Drawing.Point(12, 25);
            this.mlbl_user.Name = "mlbl_user";
            this.mlbl_user.Size = new System.Drawing.Size(53, 19);
            this.mlbl_user.TabIndex = 2;
            this.mlbl_user.Text = "Usuario";
            // 
            // ConfiguracionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 557);
            this.Controls.Add(this.mTabControlConfiguracion);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfiguracionForm";
            this.Resizable = false;
            this.Text = "Configuracion";
            this.Load += new System.EventHandler(this.ConfiguracionForm_Load);
            this.mTabControlConfiguracion.ResumeLayout(false);
            this.metroTabPageConfiguration.ResumeLayout(false);
            this.gpbCredenciales.ResumeLayout(false);
            this.gpbCredenciales.PerformLayout();
            this.gpbIntervalo.ResumeLayout(false);
            this.gpbIntervalo.PerformLayout();
            this.gpbConfRuta.ResumeLayout(false);
            this.gpbConfRuta.PerformLayout();
            this.metroTabPageAccount.ResumeLayout(false);
            this.metroTabPageAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private MetroFramework.Controls.MetroComboBox mcmbTime;
        private System.Windows.Forms.Label lblTiempo;
        private MetroFramework.Controls.MetroTextBox mtxt_folder;
        private MetroFramework.Controls.MetroTextBox mtxt_user;
        private MetroFramework.Controls.MetroLabel mlbl_user;
        private MetroFramework.Controls.MetroTextBox mtxt_usespace;
        private MetroFramework.Controls.MetroLabel mlbl_usespace;
        private MetroFramework.Controls.MetroTextBox mtxt_avalaiblespace;
        private MetroFramework.Controls.MetroLabel mlbl_avalaiblespace;
        private MetroFramework.Controls.MetroTextBox mtxt_totalspace;
        private MetroFramework.Controls.MetroLabel mlbl_totalspace;
        private MetroFramework.Controls.MetroTextBox mtxt_time;
    }
}