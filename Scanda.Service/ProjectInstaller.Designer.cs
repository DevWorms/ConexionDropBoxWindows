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
            this.serviceDBProtectorProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceDBProtectorInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceDBProtectorProcessInstaller
            // 
            this.serviceDBProtectorProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.serviceDBProtectorProcessInstaller.Password = null;
            this.serviceDBProtectorProcessInstaller.Username = null;
            // 
            // serviceDBProtectorInstaller
            // 
            this.serviceDBProtectorInstaller.Description = "DBProtector Service";
            this.serviceDBProtectorInstaller.DisplayName = "DBProtector Service";
            this.serviceDBProtectorInstaller.ServiceName = "DBProtector Service";
            this.serviceDBProtectorInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceDBProtectorProcessInstaller,
            this.serviceDBProtectorInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceDBProtectorProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceDBProtectorInstaller;
    }
}