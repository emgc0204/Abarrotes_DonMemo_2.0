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
    public partial class RegistroProductos : Form
    {
        public RegistroProductos()
        {
            InitializeComponent();
        }

        private string path = "C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Productos.txt";
        private void RegistroProductos_Load(object sender, EventArgs e)
        {
            this.lstvListado.Columns.Add("CODIGO", 80, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("NOMBRE", 120, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("CATEGORIA", 120, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("MARCA", 120, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("CANTIDAD", 120, HorizontalAlignment.Center);
            this.lstvListado.Columns.Add("PRECIO", 120, HorizontalAlignment.Center);

            this.lstvListado.View = View.Details;
            this.lstvListado.FullRowSelect = true;
            this.lstvListado.GridLines = true;
            this.lstvListado.Font = new Font("Garamond", 10, FontStyle.Regular);

            lstvListado.CheckBoxes = true;

            leerArchivo();
            cargarCategorias();
            cargarMarcas();
        }
        private void cargarCategorias()
        {
            cboCategoria.Items.Clear();

            if (File.Exists("C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Categorias.txt"))
            {
                string[] categorias = File.ReadAllLines("C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Categorias.txt");
                cboCategoria.Items.AddRange(categorias);
            }
            else
            {
                MessageBox.Show("El archivo de categorías no existe.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cargarMarcas()
        {
            cboMarca.Items.Clear();

            if (File.Exists("C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Marcas.txt"))
            {
                string[] marcas = File.ReadAllLines("C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Marcas.txt");
                cboMarca.Items.AddRange(marcas);
            }
            else
            {
                MessageBox.Show("El archivo de marcas no existe.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefd = new SaveFileDialog();
            savefd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            savefd.FileName = this.txtCodigo.Text + ".png";

            if (savefd.ShowDialog() != DialogResult.Cancel)
            {
                string codBarras = savefd.FileName;
                Bitmap bitmap = new Bitmap(pcbCodigoBarra.Image);
                bitmap.Save(codBarras);
            }
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Productos p = new Productos();
            this.txtCodigo.Text = p.obtenerCodigo;
            Zen.Barcode.Code128BarcodeDraw mgenerador = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pcbCodigoBarra.Image = mgenerador.Draw(this.txtCodigo.Text, 60);
            pcbCodigoBarra.SizeMode = PictureBoxSizeMode.CenterImage;

        }
        private void leerArchivo()
        {
            this.lstvListado.Items.Clear();

            if (File.Exists(path))
            {
                using (StreamReader archivo = new StreamReader(path))
                {
                    string codigo;

                    while ((codigo = archivo.ReadLine()) != null) 
                    {
                        string nombre = archivo.ReadLine();     
                        string categoria = archivo.ReadLine();   
                        string marca = archivo.ReadLine();      
                        string cantidad = archivo.ReadLine();  
                        string precio = archivo.ReadLine();      

                        if (nombre != null && categoria != null && marca != null && cantidad != null && precio != null)
                        {
                            string[] datos = { codigo, nombre, categoria, marca, cantidad, precio };
                            ListViewItem item = new ListViewItem(datos);
                            this.lstvListado.Items.Add(item);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El archivo no existe.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Productos p = new Productos();
            string codigo = p.obtenerCodigo;

            StreamWriter archivo = new StreamWriter(path, true);
            archivo.WriteLine(codigo); 
            archivo.WriteLine(this.txtNombre.Text);
            archivo.WriteLine(this.cboCategoria.Text);
            archivo.WriteLine(this.cboMarca.Text);
            archivo.WriteLine(this.txtCantidad.Text);
            archivo.WriteLine(this.txtPrecio.Text);
            archivo.Flush();
            archivo.Close();

            leerArchivo();

            this.txtNombre.Clear();
            this.txtCantidad.Clear();
            this.txtPrecio.Clear();
            this.cboCategoria.SelectedIndex = -1;
            this.cboMarca.SelectedIndex = -1;
            this.txtCodigo.Clear();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstvListado.CheckedIndices.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "¿Desea eliminar los elementos seleccionados?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    if (File.Exists(path))
                    {
                        List<string> lineas = File.ReadAllLines(path).ToList();

                        List<string> lineasActualizadas = new List<string>();

                        int lineasPorProducto = 6;

                        List<int> indicesAEliminar = lstvListado.CheckedIndices.Cast<int>().OrderByDescending(i => i).ToList();

                        for (int i = 0; i < lineas.Count; i += lineasPorProducto)
                        {
                            int productoIndex = i / lineasPorProducto;

                            if (!indicesAEliminar.Contains(productoIndex))
                            {
                                for (int j = 0; j < lineasPorProducto; j++)
                                {
                                    if (i + j < lineas.Count)
                                    {
                                        lineasActualizadas.Add(lineas[i + j]);
                                    }
                                }
                            }
                        }

                        File.WriteAllLines(path, lineasActualizadas);

                        foreach (int index in indicesAEliminar)
                        {
                            lstvListado.Items.RemoveAt(index);
                        }

                        MessageBox.Show(
                            "Elementos eliminados correctamente.",
                            "Eliminación exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "El archivo no existe. No se pueden eliminar los productos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
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
