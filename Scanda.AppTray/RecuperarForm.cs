using MetroFramework.Forms;
using MetroFramework.Controls;
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
    public partial class RecuperarForm : MetroForm
    {
        // Mockup Respaldo
        List<string> anio_respaldos = new List<string>() { "2016", "2015", "2014"};
        public RecuperarForm()
        {
            InitializeComponent();
            int x = 0;
            foreach (string anio in anio_respaldos)
            {
                var tile = new MetroTile();
                tile.Text = anio;
                tile.TextAlign = ContentAlignment.MiddleCenter;
                tile.Location = new Point(x, 19);
                tile.Width = 150;
                tile.Height = 150;
                tile.Click += MetroTile_Click;
                metroTabPageRespaldos.Controls.Add(tile);
                x += 160; 
            }
        }

        void MetroTile_Click(object sender, EventArgs e)
        {
            var obj = (MetroTile)sender;
            var tab = new MetroTabPage();
            tab.Text = obj.Text;

            int y = 20;
            for (int i = 0; i < 12; i++)
            {
                var checkbox = new MetroCheckBox();
                checkbox.Text = string.Format("archivo_{0}.zip", i);
                checkbox.Location = new Point(0, y);
                y += 20;
                tab.Controls.Add(checkbox);
            }

            // Agregamos los botones
            // Boton Cerrar
            var btnClose = new MetroButton();
            btnClose.Text = "Cerrar";
            btnClose.Location = new Point(566, 209);
            btnClose.Size = new Size(86, 30);
            btnClose.Click += btnClose_Click;

            // Boton Descargar
            var btnDownload = new MetroButton();
            btnDownload.Text = "Descargar";
            btnDownload.Location = new Point(463, 209);
            btnDownload.Size = new Size(88, 30);
            btnDownload.Click += btnClose_Click;

            tab.Controls.Add(btnClose);
            tab.Controls.Add(btnDownload);
            metroTabControlPrincipal.TabPages.Add(tab);
        }

        void btnClose_Click(object sender, EventArgs e) {
            var btnClose = (MetroButton)sender;
            var parent = (MetroTabPage)btnClose.Parent;
            metroTabControlPrincipal.TabPages.Remove(parent);
        }
    }
}
