﻿namespace Scanda.AppTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecuperarForm));
            this.metroTabControlPrincipal = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPageRespaldos = new MetroFramework.Controls.MetroTabPage();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnCancel = new MetroFramework.Controls.MetroButton();
            this.mbtnDownload = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.metroTabControlPrincipal.SuspendLayout();
            this.metroTabPageRespaldos.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControlPrincipal
            // 
            this.metroTabControlPrincipal.Controls.Add(this.metroTabPageRespaldos);
            this.metroTabControlPrincipal.Location = new System.Drawing.Point(24, 54);
            this.metroTabControlPrincipal.Name = "metroTabControlPrincipal";
            this.metroTabControlPrincipal.SelectedIndex = 0;
            this.metroTabControlPrincipal.Size = new System.Drawing.Size(663, 362);
            this.metroTabControlPrincipal.TabIndex = 0;
            // 
            // metroTabPageRespaldos
            // 
            this.metroTabPageRespaldos.Controls.Add(this.tableLayoutPanel_Main);
            this.metroTabPageRespaldos.HorizontalScrollbarBarColor = true;
            this.metroTabPageRespaldos.Location = new System.Drawing.Point(4, 35);
            this.metroTabPageRespaldos.Name = "metroTabPageRespaldos";
            this.metroTabPageRespaldos.Size = new System.Drawing.Size(655, 323);
            this.metroTabPageRespaldos.TabIndex = 0;
            this.metroTabPageRespaldos.Text = "Respaldos";
            this.metroTabPageRespaldos.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPageRespaldos.VerticalScrollbarBarColor = true;
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_Main.ColumnCount = 1;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 1;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel_Main.TabIndex = 2;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(583, 428);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(104, 23);
            this.mbtnCancel.TabIndex = 1;
            this.mbtnCancel.Text = "Cancelar";
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnDownload
            // 
            this.mbtnDownload.Location = new System.Drawing.Point(405, 428);
            this.mbtnDownload.Name = "mbtnDownload";
            this.mbtnDownload.Size = new System.Drawing.Size(163, 23);
            this.mbtnDownload.TabIndex = 2;
            this.mbtnDownload.Text = "Descargar Seleccionados";
            this.mbtnDownload.Click += new System.EventHandler(this.mbtnDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(268, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "v1.15";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Tai Le", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.Gray;
            this.lblCompany.Location = new System.Drawing.Point(28, 428);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(70, 13);
            this.lblCompany.TabIndex = 12;
            this.lblCompany.Text = "@ 2016 Xamal";
            this.lblCompany.Visible = false;
            // 
            // RecuperarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(715, 474);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.mbtnDownload);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.metroTabControlPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RecuperarForm";
            this.Text = "Respaldos";
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Load += new System.EventHandler(this.RecuperarForm_Load);
            this.metroTabControlPrincipal.ResumeLayout(false);
            this.metroTabPageRespaldos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControlPrincipal;
        private MetroFramework.Controls.MetroTabPage metroTabPageRespaldos;
        private MetroFramework.Controls.MetroButton mbtnCancel;
        private MetroFramework.Controls.MetroButton mbtnDownload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCompany;
    }
}