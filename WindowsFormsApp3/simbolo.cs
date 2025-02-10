using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class simbolo
    {
        string simbolon;
        string direccion;
        string tipo;

        public simbolo()
        {
            Simbolon = "";
            Direccion = "";
            Tipo = "";
        }
        public string Simbolon { get => simbolon; set => simbolon = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
