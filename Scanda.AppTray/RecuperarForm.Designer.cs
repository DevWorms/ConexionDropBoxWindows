namespace Scanda.AppTray
{
    partial class RecuperarForm
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
            this.metroTabControlPrincipal = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPageRespaldos = new MetroFramework.Controls.MetroTabPage();
            this.metroTabControlPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControlPrincipal
            // 
            this.metroTabControlPrincipal.Controls.Add(this.metroTabPageRespaldos);
            this.metroTabControlPrincipal.Location = new System.Drawing.Point(24, 54);
            this.metroTabControlPrincipal.Name = "metroTabControlPrincipal";
            this.metroTabControlPrincipal.SelectedIndex = 0;
            this.metroTabControlPrincipal.Size = new System.Drawing.Size(663, 281);
            this.metroTabControlPrincipal.TabIndex = 0;
            // 
            // metroTabPageRespaldos
            // 
            this.metroTabPageRespaldos.HorizontalScrollbarBarColor = true;
            this.metroTabPageRespaldos.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageRespaldos.Name = "metroTabPageRespaldos";
            this.metroTabPageRespaldos.Size = new System.Drawing.Size(655, 242);
            this.metroTabPageRespaldos.TabIndex = 0;
            this.metroTabPageRespaldos.Text = "Respaldos";
            this.metroTabPageRespaldos.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPageRespaldos.VerticalScrollbarBarColor = true;
            // 
            // RecuperarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 374);
            this.Controls.Add(this.metroTabControlPrincipal);
            this.Name = "RecuperarForm";
            this.Text = "Respaldos";
            this.metroTabControlPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControlPrincipal;
        private MetroFramework.Controls.MetroTabPage metroTabPageRespaldos;
    }
}