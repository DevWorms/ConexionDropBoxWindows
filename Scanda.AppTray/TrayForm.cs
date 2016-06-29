using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanda.AppTray
{
    public partial class FormTray : Form
    {
        public FormTray()
        {
            InitializeComponent();
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void FormTray_Move(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIconScanda.ShowBalloonTip(1000, "Important notice", "Scanda Service is Running", ToolTipIcon.Info);
            }
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Folder");
        }

        private void autenticarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
