using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class TABSE
    {
        string seccion;
        string simbolo;
        string direccion;
        string longitud;

        public TABSE()
        {
            Seccion = "";
            Simbolon = "";
            Direccion = "";
            longitud = "";
        }
        public string Seccion { get => seccion; set => seccion = value; }
        public string Simbolon { get => simbolo; set => simbolo = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Longitud { get => longitud; set => longitud = value; }
    }
}
