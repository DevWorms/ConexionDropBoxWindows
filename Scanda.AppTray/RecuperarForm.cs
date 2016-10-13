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
using Scanda.AppTray.Models;
using System.Text.RegularExpressions;

namespace Scanda.AppTray
{
    public partial class RecuperarForm : MetroForm
    {
        public bool btnCancelar = false;
        public List<Control> controls = new List<Control>() { };
        private Config config;
        private string json;
        private string RFCregexp = "([A-Zz-z]{3,4}\\d{6}(---|\\w{3})?)";
        public RecuperarForm(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);

            tableLayoutPanel_Main.Visible = false;
        }

        private async void MetroTile_Click(object sender, EventArgs e)
        {
            var obj = (MetroTile)sender;
            var tab = new MetroTabPage();
            tab.Name = "Tab_" + obj.Text; 
            tab.Text = obj.Text;
            tab.AutoScroll = true;
            List<FileDetail> storage_files = new List<FileDetail>() { };
            // Obtenemos el Listado de meses de ese año
            List<string> meses = await ScandaConector.getMonths(config.id_customer, obj.Text);
            foreach(string mes in meses)
            {
                List<string> files = await ScandaConector.getFiles(config.id_customer, obj.Text, mes);
                foreach(string file in files)
                {
                    var match = Regex.Match(file, RFCregexp);
                    storage_files.Add(new FileDetail() { file = file, mes = mes, rfc = match.ToString() });
                }
                // storage_files.AddRange(files);
            }
            int y = 20;
            var parents = storage_files.Where(ent => ent.rfc != "").Select(ent => ent.rfc).Distinct().ToArray();
            foreach (var parent in parents)
            {
                FileDetail[] files = storage_files.Where(ent => ent.rfc == parent).ToArray<FileDetail>();
                Label lblGroup = new Label();
                lblGroup.Text = parent + "\n______________________________________";
                lblGroup.BackColor = Color.White;
                lblGroup.Width = 200;
                lblGroup.TextAlign = ContentAlignment.MiddleCenter;
                lblGroup.Location = new Point(0, y);
                y += 25;
                tab.Controls.Add(lblGroup);
                foreach (FileDetail file in files)
                {
                    var checkbox = new MetroCheckBox();
                    checkbox.Text = file.file;// string.Format("archivo_{0}.zip", i);
                    checkbox.Name = obj.Text + "_" + file.mes + "_" + file.file;
                    checkbox.Width = 300;
                    checkbox.Location = new Point(0, y);
                    y += 20;
                    tab.Controls.Add(checkbox);
                }
                y += 30;
            }
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

        private async void RecuperarForm_Load(object sender, EventArgs e)
        {
            // Obtenemos el listado de Folders
            List<string> files = await ScandaConector.getFolders(config.id_customer);
            int x = 0;
            foreach (string anio in files)
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

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            btnCancelar = true;
        }
    }
}
