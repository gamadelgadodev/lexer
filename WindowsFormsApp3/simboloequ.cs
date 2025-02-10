using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class simboloequ
    {
        string simbolon;
        string direccion;
        string tipo;
        string signo;
        string error;

        public simboloequ()
        {
            Simbolon = "";
            Direccion = "";
            Tipo = "";
            Signo = "";
            Error = "";
        }
        public string Simbolon { get => simbolon; set => simbolon = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Signo { get => signo; set => signo = value; }
        public string Error { get => error; set => error = value; }
    }
}
