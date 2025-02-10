using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class mapacont
    {
        string direccion;
        string valor;

        public mapacont()
        {
            Direccion = "";
            Valor = "";
        }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Valor { get => valor; set => valor = value; }
    }
}
