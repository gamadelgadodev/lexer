using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Linea
    {
        string nolinea;
        string cp;
        string etiquieta;
        string codop;
        string op1;
        string codobj;

        public Linea()
        {
            nolinea = "";
            cp = "";
            Etiquieta = "";
            Codop = "";
            Op1 = "";
            codobj = "";
        }

        public string NumLinea { get => nolinea; set => nolinea = value; }
        public string Cp { get => cp; set => cp = value; }
        public string Etiquieta { get => etiquieta; set => etiquieta = value; }
        public string Codop { get => codop; set => codop = value; }
        public string Op1 { get => op1; set => op1 = value; }
        public string CodObj { get => codobj; set => codobj = value; }
    }
}
