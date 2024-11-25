using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abarrotes_DonMemo_2._0
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            CargarUsuarios();
        }

        private string path = "C:\\Users\\emili\\source\\repos\\Abarrotes_DonMemo_2.0\\project\\Usuarios.txt";
        private List<Usuarios> listaUsuarios = new List<Usuarios>();
        private void CargarUsuarios()
        {

            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string linea = reader.ReadLine();
                while (linea != null)
                {
                    string palabras = linea.Trim();
                    string[] datos = palabras.Split(',');
                    if (datos.Length == 3)
                    {
                        Usuarios user = new Usuarios();
                        user.pUsername = datos[0];
                        user.pPassword = datos[1];
                        user.pPrivilegio = datos[2];
                        listaUsuarios.Add(user);
                    }
                    linea = reader.ReadLine();
                }
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario_ingresado = txtUser.Text.Trim();
            string password_ingresado = txtPassword.Text.Trim();
            Usuarios usuario_correcto = new Usuarios { pUsername = usuario_ingresado, pPassword = password_ingresado };
            Usuarios usuario = null;
            foreach (var u in listaUsuarios)
            {
                if (u.pUsername == usuario_correcto.pUsername && u.pPassword == usuario_correcto.pPassword)
                {
                    usuario = u;
                    break;
                }
            }

            if (usuario != null)
            {
                if (usuario.pPrivilegio == "admin")
                {
                    MessageBox.Show("¡Inicio de sesión correcto!\nIngresando como: Administrador","LOGIN EXITOSO!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    
                }
                else if (usuario.pPrivilegio == "cajero")
                {
                    MessageBox.Show("¡Inicio de sesión correcto!\nIngresando como: Cajero", "LOGIN EXITOSO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Tag = "cajero";
                }
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.","ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
    public class Usuarios
    {
        private string Username;
        private string Password;
        private string Privilegio;

        public string pUsername
        {
            get { return Username; }
            set { Username = value; }
        }

        public string pPassword
        {
            get { return Password; }
            set { Password = value; }
        }

        public string pPrivilegio
        {
            get { return Privilegio; }
            set { Privilegio = value; }
        }
    }
}
