namespace Scanda.Service
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceScandaProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceScandaInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceScandaProcessInstaller
            // 
            this.serviceScandaProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceScandaProcessInstaller.Password = null;
            this.serviceScandaProcessInstaller.Username = null;
            // 
            // serviceScandaInstaller
            // 
            this.serviceScandaInstaller.Description = "Scanda Service";
            this.serviceScandaInstaller.DisplayName = "Scanda";
            this.serviceScandaInstaller.ServiceName = "ServiceScanda";
            this.serviceScandaInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceScandaProcessInstaller,
            this.serviceScandaInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceScandaProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceScandaInstaller;
    }
}