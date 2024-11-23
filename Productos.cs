using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abarrotes_DonMemo_2._0
{
    class Productos
    {
        private string cadena;

        public string obtenerCodigo
        {
            get
            {
                Random rnd = new Random();
                string abc = "QWERTYUIOPLKJHGFDSAZXCVBNM0987654321";

                for (int x = 0; x < 5; x++)
                {
                    int Valor = rnd.Next(abc.Length);
                    cadena += abc.Substring(Valor, 1);
                }
                return cadena;
            }
        }
    }
}
