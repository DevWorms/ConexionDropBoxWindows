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
using Newtonsoft.Json;
using System.IO;

namespace Scanda.AppTray
{
    public partial class RecuperarForm : MetroForm
    {
        public List<Control> controls = new List<Control>() { };
        // Mockup Respaldo
        List<string> anio_respaldos = new List<string>() { "2016", "2015", "2014"};
        private Config config;
        private string json;
        public RecuperarForm(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);

            tableLayoutPanel_Main.Visible = false;
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
            tab.Name = "Tab_" + obj.Text; 
            tab.Text = obj.Text;

            
            int y = 20;
            //int start_x = 0; int start_y = 20;
            for (int i = 0; i < 12; i++)
            {
                /*var groupbox = new GroupBox();
                groupbox.Text = "Mes " + i.ToString();
                groupbox.Location = new Point(start_x, start_y);
                for (int z = 0; z < 3; z++)
                {*/
                    var checkbox = new MetroCheckBox();
                    checkbox.Text = string.Format("archivo_{0}.zip", i);
                    checkbox.Location = new Point(0, y);
                    y += 20;
                    //groupbox.Controls.Add(checkbox);
               // }
                //start_y = start_y + y + 20;
                tab.Controls.Add(checkbox);
            }

            // Agregamos los botones
            // Boton Cerrar
            //var btnClose = new MetroButton();
            //btnClose.Text = "Cerrar";
            //btnClose.Location = new Point(566, 209);
            //btnClose.Size = new Size(86, 30);
            //btnClose.Click += btnClose_Click;

            //// Boton Descargar
            //var btnDownload = new MetroButton();
            //btnDownload.Text = "Descargar";
            //btnDownload.Location = new Point(463, 209);
            //btnDownload.Size = new Size(88, 30);
            //btnDownload.Click += btnClose_Click;

            //tab.Controls.Add(btnClose);
            //tab.Controls.Add(btnDownload);
            bool found = false;
            foreach (MetroTabPage page in metroTabControlPrincipal.TabPages)
            {
                if (tab.Name == page.Name)
                {
                    metroTabControlPrincipal.SelectedTab = page;
                    found = true;
                }
            }
            if (!found)
                metroTabControlPrincipal.TabPages.Add(tab);
        }

        void btnClose_Click(object sender, EventArgs e) {
            var btnClose = (MetroButton)sender;
            var parent = (MetroTabPage)btnClose.Parent;
            metroTabControlPrincipal.TabPages.Remove(parent);
        }

        private void mbtnDownload_Click(object sender, EventArgs e)
        {
            List<Control> _controls = new List<Control>() { };
            // Navegamos en todos los Tabs
            foreach (MetroTabPage page in metroTabControlPrincipal.TabPages)
            {
                //foreach(Control control in page.Controls)
                //{
                //    contro
                //}
                var c = GetAll(page, typeof(MetroCheckBox));
                foreach(MetroCheckBox chk in c)
                {
                    if(chk.Checked && chk.Enabled)
                    {
                        _controls.Add(chk);
                    }
                }
            }

            // cerramos y comenzamos a descargar
            controls = _controls;
            Close();
        }
        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void RecuperarForm_Load(object sender, EventArgs e)
        {
            // Obtenemos el listado de Folders
            var years = ScandaConector.getYears("1");
        }
    }
}
