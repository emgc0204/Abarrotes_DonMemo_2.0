using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abarrotes_DonMemo_2._0
{
    public partial class Marcas : Form
    {
        public Marcas()
        {
            InitializeComponent();
        }
        private string path = "C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Marcas.txt";
        private void fmrMarcas_Load(object sender, EventArgs e)
        {
            this.lstvListado.Columns.Add("No.", 50, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("MARCAS", 100, HorizontalAlignment.Center);

            this.lstvListado.View = View.Details;
            this.lstvListado.FullRowSelect = true;
            this.lstvListado.GridLines = true;
            this.lstvListado.Font = new Font("Garamond", 10, FontStyle.Regular); // Cambia la fuente del ListView

            this.lstvListado.CheckBoxes = true;

            leerArchivo();
        }
        private void leerArchivo()
        {
            this.lstvListado.Items.Clear();

            int contador = 0;

            if(File.Exists(path))
            {
                StreamReader archivo = new StreamReader(path);
                string linea = archivo.ReadLine();

                while (linea != null)
                {
                    string[] datos = new string[2];
                    datos[0] = (contador + 1).ToString();
                    datos[1] = linea;

                    ListViewItem item = new ListViewItem(datos);
                    this.lstvListado.Items.Add(item);

                    contador++;
                    linea = archivo.ReadLine();
                }
                archivo.Close();
            }
            else
            {
                MessageBox.Show("El archivo no existe.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.tlpCantidad.Text = contador.ToString();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            StreamWriter archivo = new StreamWriter(path, true);
            archivo.WriteLine(this.txtNombre.Text);
            archivo.Flush();
            archivo.Close();

            leerArchivo();

            this.txtNombre.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstvListado.CheckedIndices.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "¿Desea eliminar los elementos seleccionados?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    List<string> lineas = new List<string>();
                    if (File.Exists(path))
                    {
                        lineas = File.ReadAllLines(path).ToList();
                    }

                    for (int i = lstvListado.CheckedIndices.Count - 1; i >= 0; i--)
                    {
                        int index = lstvListado.CheckedIndices[i];

                        if (index < lineas.Count)
                        {
                            lineas.RemoveAt(index);
                        }

                        lstvListado.Items.RemoveAt(index);
                    }

                    File.WriteAllLines(path, lineas);

                    MessageBox.Show(
                        "Elementos eliminados correctamente.",
                        "Eliminación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Seleccione al menos un elemento para eliminar.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            leerArchivo();
        }
    }
}
