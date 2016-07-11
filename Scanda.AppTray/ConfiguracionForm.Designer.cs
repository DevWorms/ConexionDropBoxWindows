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
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.btnElegir = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gpbConfRuta = new System.Windows.Forms.GroupBox();
            this.gpbIntervalo = new System.Windows.Forms.GroupBox();
            this.gpbCredenciales = new System.Windows.Forms.GroupBox();
            this.btnDesvincular = new System.Windows.Forms.Button();
            this.lblInfoCuenta = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblAlert = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gpbConfRuta.SuspendLayout();
            this.gpbIntervalo.SuspendLayout();
            this.gpbCredenciales.SuspendLayout();
            this.SuspendLayout();
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
            this.btnElegir.Location = new System.Drawing.Point(164, 70);
            this.btnElegir.Name = "btnElegir";
            this.btnElegir.Size = new System.Drawing.Size(145, 23);
            this.btnElegir.TabIndex = 1;
            this.btnElegir.Text = "Elegir Carpeta";
            this.btnElegir.UseVisualStyleBackColor = true;
            this.btnElegir.Click += new System.EventHandler(this.btnElegir_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Enabled = false;
            this.txtRuta.Location = new System.Drawing.Point(9, 44);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(300, 20);
            this.txtRuta.TabIndex = 2;
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Cada 1 Hora",
            "Cada 6 Horas",
            "Cada 12 Horas",
            "Cada 1 Día",
            "Cada 3 Días",
            "Cada 7 Días",
            "Cada 10 Días",
            "Cada 15 Días",
            "Cada 20 Días",
            "Cada 30 Días"});
            this.comboBox1.Location = new System.Drawing.Point(10, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // gpbConfRuta
            // 
            this.gpbConfRuta.Controls.Add(this.lblCarpeta);
            this.gpbConfRuta.Controls.Add(this.txtRuta);
            this.gpbConfRuta.Controls.Add(this.btnElegir);
            this.gpbConfRuta.Location = new System.Drawing.Point(15, 12);
            this.gpbConfRuta.Name = "gpbConfRuta";
            this.gpbConfRuta.Size = new System.Drawing.Size(328, 123);
            this.gpbConfRuta.TabIndex = 5;
            this.gpbConfRuta.TabStop = false;
            this.gpbConfRuta.Text = "Configuración de Ruta de respaldos";
            // 
            // gpbIntervalo
            // 
            this.gpbIntervalo.Controls.Add(this.comboBox1);
            this.gpbIntervalo.Controls.Add(this.lblTiempo);
            this.gpbIntervalo.Location = new System.Drawing.Point(15, 147);
            this.gpbIntervalo.Name = "gpbIntervalo";
            this.gpbIntervalo.Size = new System.Drawing.Size(324, 100);
            this.gpbIntervalo.TabIndex = 6;
            this.gpbIntervalo.TabStop = false;
            this.gpbIntervalo.Text = "Configuración frecuencía de respaldos";
            // 
            // gpbCredenciales
            // 
            this.gpbCredenciales.Controls.Add(this.btnDesvincular);
            this.gpbCredenciales.Controls.Add(this.lblInfoCuenta);
            this.gpbCredenciales.Controls.Add(this.btnLogin);
            this.gpbCredenciales.Controls.Add(this.lblAlert);
            this.gpbCredenciales.Location = new System.Drawing.Point(15, 268);
            this.gpbCredenciales.Name = "gpbCredenciales";
            this.gpbCredenciales.Size = new System.Drawing.Size(324, 95);
            this.gpbCredenciales.TabIndex = 7;
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
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(269, 421);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(179, 421);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // ConfiguracionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 458);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gpbCredenciales);
            this.Controls.Add(this.gpbIntervalo);
            this.Controls.Add(this.gpbConfRuta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfiguracionForm";
            this.Text = "Configuracion";
            this.gpbConfRuta.ResumeLayout(false);
            this.gpbConfRuta.PerformLayout();
            this.gpbIntervalo.ResumeLayout(false);
            this.gpbIntervalo.PerformLayout();
            this.gpbCredenciales.ResumeLayout(false);
            this.gpbCredenciales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCarpeta;
        private System.Windows.Forms.Button btnElegir;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox gpbConfRuta;
        private System.Windows.Forms.GroupBox gpbIntervalo;
        private System.Windows.Forms.GroupBox gpbCredenciales;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblInfoCuenta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnDesvincular;
    }
}