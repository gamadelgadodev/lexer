using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class operandos
    {
        string simbolon;
        string direccion;
        string tipo;
        string signo;

        public operandos()
        {
            Simbolon = "";
            Direccion = "";
            Tipo = "";
            Signo = "";
        }
        public string Simbolon { get => simbolon; set => simbolon = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Signo { get => signo; set => signo = value; }
    }
}
