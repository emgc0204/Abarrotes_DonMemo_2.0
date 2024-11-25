using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abarrotes_DonMemo_2._0
{
    public partial class Principal : Form
    {

        public Principal()
        {
            InitializeComponent();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroProductos ventana = Application.OpenForms.OfType<RegistroProductos>().FirstOrDefault();
            if (ventana == null)
            {
                ventana = new RegistroProductos
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    Size = new Size(733, 489),
                    FormBorderStyle = FormBorderStyle.Sizable
                };

                ventana.Show();
            }
            else
            {
                ventana.BringToFront();
            }
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?", "ATENCIÓN", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroCategorias ventana = Application.OpenForms.OfType<RegistroCategorias>().FirstOrDefault();
            if (ventana == null)
            {
                ventana = new RegistroCategorias
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    Size = new Size(733, 489),
                    FormBorderStyle = FormBorderStyle.Sizable
                };

                ventana.Show();
            }
            else
            {
                ventana.BringToFront();
            }

        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Marcas ventana = Application.OpenForms.OfType<Marcas>().FirstOrDefault();
            if (ventana == null)
            {
                ventana = new Marcas
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    Size = new Size(733, 489),
                    FormBorderStyle = FormBorderStyle.Sizable
                };

                ventana.Show();
            }
            else
            {
                ventana.BringToFront();
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();
        }
    }
}