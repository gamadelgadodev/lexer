using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Mapa
    {
        string direccion;
        string cero;
        string uno;
        string dos;
        string tres;
        string cuatro;
        string cinco;
        string seis;
        string siete;
        string ocho;
        string nueve;
        string a;
        string b;
        string c;
        string d;
        string e;
        string f;

        public Mapa()
        {
            Direccion = "";
            Cero = "";
            Uno = "";
            Dos = "";
            Tres = "";
            Cuatro = "";
            Cinco = "";
            Seis = "";
            Siete = "";
            Ocho = "";
            Nueve = "";
            A = "";
            B = "";
            C = "";
            D = "";
            E = "";
            F = "";
        }
        public string Cero { get => cero; set => cero = value; }
        public string Uno { get => uno; set => uno = value; }
        public string Dos { get => dos; set => dos = value; }
        public string Tres { get => tres; set => tres = value; }
        public string Cuatro { get => cuatro; set => cuatro = value; }

        public string Cinco { get => cinco; set => cinco = value; }
        public string Seis { get => seis; set => seis = value; }
        public string Siete { get => siete; set => siete = value; }
        public string Ocho { get => ocho; set => ocho = value; }
        public string Nueve { get => nueve; set => nueve = value; }
        public string A { get => a; set => a = value; }
        public string B { get => b; set => b = value; }
        public string C { get => c; set => c = value; }
        public string D { get => d; set => d = value; }
        public string E { get => e; set => e = value; }
        public string F { get => f; set => f = value; }
        public string Direccion { get => direccion; set => direccion = value; }
    }
}
