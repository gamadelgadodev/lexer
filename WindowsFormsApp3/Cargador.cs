using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Cargador : Form
    {
        string Archi1 = "";
        string Archi2 = "";
        string dirProg = "0";
        string dirSC = "0";
        public Cargador()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string RutaArchivo = openFileDialog1.FileName;
                    //textBox1.Text = str_RutaArchivo;
                    string str_textoArchivo = leerArchivo(RutaArchivo);
                    richTextBox1.Text = str_textoArchivo;
                    Archi1 = str_textoArchivo;
                    //rellenaDta(str_textoArchivo);
                    //textBox1.Text = RutaArchivo;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }
        public string leerArchivo(string str_ruta)
        {
            string str_textoArchivo = System.IO.File.ReadAllText(@"" + str_ruta);
            return str_textoArchivo;
        }
        public void rellenaDta(string txt)
        {
            List<string> reg = new List<string>();
            bool bandT = false;
            string[] renglones = txt.Split('\n');
            string r = "";
            foreach (string ren in renglones)
            {
                if (ren.First() == 'T')
                {
                    //reg.Add(ren);
                    richTextBox1.Text = richTextBox1.Text + "\n" + ren;
                }
            }
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string RutaArchivo = openFileDialog1.FileName;
                    //textBox1.Text = str_RutaArchivo;
                    string str_textoArchivo = leerArchivo(RutaArchivo);
                    richTextBox1.Text = richTextBox1.Text + str_textoArchivo;
                    Archi2 = str_textoArchivo;
                    //rellenaDta(str_textoArchivo);
                    //textBox1.Text = RutaArchivo;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dirProg = textBox1.Text;
            dirSC = dirProg;
            List<TABSE> ts = new List<TABSE>();
            List<mapacont> mapas = new List<mapacont>();
            List<Mapa> ma = new List<Mapa>();
            if (Archi1!="")
            {
                paso1(ts, Archi1);
                paso1(ts, Archi2);
                dataGridView2.DataSource = ts;
                dirProg = textBox1.Text;
                dirSC = dirProg;
                paso2(ts,ma, Archi1,mapas);
                paso2(ts,ma, Archi2,mapas);
                llenaMap(mapas);
                

            }
        }
        public void paso1(List<TABSE> ts,string arch)
        {
            string[] lineas = arch.Split('\n');
            foreach (string l in lineas)
            {
                if (l[0]=='H')
                {
                    TABSE sim = new TABSE();
                    sim.Seccion = l.Substring(1, 6 - 1);
                    //MessageBox.Show(sim.Seccion);
                    sim.Direccion = dirProg;
                    //MessageBox.Show(sim.Direccion);
                    sim.Longitud = l.Substring(13, 19 - 13);
                    //MessageBox.Show(sim.Longitud);
                    ts.Add(sim);
                    dirSC = dirProg;
                    dirProg = SumaHex(sim.Direccion , sim.Longitud).ToUpper();
                }
                if (l[0]=='D')
                {
                    TABSE sim1 = new TABSE();
                    TABSE sim2 = new TABSE();
                    int rep = (l.Length - 1) / 6;
                    sim1.Simbolon = l.Substring(1, 6 - 1);
                    sim1.Direccion = SumaHex(dirSC,l.Substring(7, 13 - 7));
                    ts.Add(sim1);
                    sim2.Simbolon = l.Substring(13, 19 - 13);
                    sim2.Direccion = SumaHex(dirSC, l.Substring(19, 25 - 19));
                    ts.Add(sim2);
                }
                if(l[0]=='E')
                    break;
            }
        }
        public void paso2(List<TABSE> ts,List<Mapa> m, string arch, List<mapacont> mapas)
        {
            string dircarg="";
            string dirj = "";
            string[] lineas = arch.Split('\n');
           // MessageBox.Show(lineas.Length.ToString());
            
            foreach (string l in lineas)
            {
               // MessageBox.Show(l);
                if (l[0]=='T')
                {
                    //MessageBox.Show("direccion:"+l.Substring(1, 7 - 1));
                    dircarg = dirSC;
                    dirSC = SumaHex(dirSC, l.Substring(1, 7 - 1)).ToUpper();
                    int cont1 = 7;
                    int cont2 = 9;
                    dirj = dirSC;
                    int tam = hexadecimalDecimal(l.Substring(7, 9 - 7).ToUpper());
                    for (int i = 0; i <  tam; i++)
                    {
                        cont1 += 2;
                        cont2 += 2;
                        mapacont a = new mapacont();
                        a.Direccion = dirj;
                        a.Valor = l.Substring(cont1, cont2 - cont1).ToUpper();
                       // MessageBox.Show(a.Valor);
                        mapas.Add(a);
                        dirj = SumaHex(dirj, "1");
                    }
                    
                }
                if(l[0]=='M')
                {
                    string n= l.Substring(1, 7 - 1).ToUpper();
                    string b= l.Substring(7, 9 - 7).ToUpper();
                    string sig= l.Substring(9, 10 - 9).ToUpper();
                    string simb = l.Substring(10, (l.Length-1) - 10);
                    string sus = "";
                    //MessageBox.Show(simb);
                    simb =simb.Replace(" ","");
                    foreach (TABSE f in ts)
                    {
                        // MessageBox.Show(f.Simbolon+simb);
                       //MessageBox.Show(f.Simbolon + simb);
                        string comp = f.Simbolon.Replace(" ", "");
                        //MessageBox.Show(compcad(comp, simb).ToString());
                        if (compcad(comp,simb))
                        {
                            sus = f.Direccion;
                           // MessageBox.Show("susti");
                            
                        }
                        if(f.Seccion == simb)
                        {
                            sus = f.Direccion;
                        }
                            
                    }
                    
                  
                   bool band=false;
                    string dirmod = SumaHex(dircarg, n);
                    //MessageBox.Show(dirmod);
                    dirmod = SumaHex(dirmod, "1");
                    int cont = 0;
                    string sus1 = sus[0].ToString() + sus[1].ToString();
                    string sus2 = sus[2].ToString() + sus[3].ToString();
                    foreach (mapacont mp in mapas)
                    {
                        if (mp.Direccion ==  dirmod)
                        {
                            band = true;
                        }if(band)
                        {
                            if (cont == 1)
                            {
                                if (sig=="+")
                                {
                                    mp.Valor = SumaHex(mp.Valor, sus2);
                                    cont++;
                                }
                                else
                                {
                                    mp.Valor = RestaHex(mp.Valor, sus2);
                                    cont++;
                                }
                                
                            }
                            if (cont == 0)
                            {
                                if (sig == "+")
                                {
                                    mp.Valor = SumaHex(mp.Valor, sus1);
                                    cont++;
                                }
                                else
                                {
                                    mp.Valor = RestaHex(mp.Valor, sus1);
                                    cont++;
                                }
                            }
                            
                        }if (cont==2)
                            break;
                    }
                }
                if(l[0]=='H')
                {
                   // MessageBox.Show("holaH");
                    TABSE sim = new TABSE();
                    sim.Seccion = l.Substring(1, 6 - 1);
                    //MessageBox.Show(sim.Seccion);
                    sim.Direccion = dirProg;
                    //MessageBox.Show(sim.Direccion);
                    sim.Longitud = l.Substring(13, 19 - 13);
                    //MessageBox.Show(sim.Longitud);
                    //ts.Add(sim);
                    dirSC = dirProg;
                    dirProg = SumaHex(sim.Direccion, sim.Longitud).ToUpper();
                }
                if (l[0] == 'E')
                    break;
            }
            
        }
        public void llenaMap(List<mapacont> mapas)
        {
            bool bandex = true;
            List<Mapa> Datas = new List<Mapa>();
            //Mapa m = new Mapa();
            string aux="";
            foreach (mapacont x in mapas)
            {
                aux = x.Direccion.ToLower();
                aux = aux.Remove(aux.Length - 1);
                aux = aux + "0";
                if (Datas.Count == 0)
                {
                    Mapa m = new Mapa();
                    m.Direccion = aux;

                    switch (x.Direccion.Last())
                    {
                        case '0':
                           m.Cero = x.Valor;
                            break;
                        case '1':
                            m.Uno = x.Valor;
                            break;
                        case '2':
                            m.Dos = x.Valor;
                            break;
                        case '3':
                            m.Tres = x.Valor;
                            break;
                        case '4':
                            m.Cuatro = x.Valor;
                            break;
                        case '5':
                            m.Cinco = x.Valor;
                            break;
                        case '6':
                            m.Seis = x.Valor;
                            break;
                        case '7':
                            m.Siete = x.Valor;
                            break;
                        case '8':
                            m.Ocho = x.Valor;
                            break;
                        case '9':
                            m.Nueve = x.Valor;
                            break;
                        case 'a':
                            m.A = x.Valor;
                            break;
                        case 'b':
                            m.B = x.Valor;
                            break;
                        case 'c':
                            m.C = x.Valor;
                            break;
                        case 'd':
                            m.D = x.Valor;
                            break;
                        case 'e':
                            m.E = x.Valor;
                            break;
                        case 'f':
                            m.Seis = x.Valor;
                            break;
                        default:
                            break;
                    }
                    Datas.Add(m);
                }
                else
                {
                    foreach (Mapa y in Datas)
                    {
                        if (y.Direccion == aux)
                        {
                            switch (x.Direccion.Last())
                            {
                                case '0':
                                    Datas.ElementAt(Datas.IndexOf(y)).Cero = x.Valor;
                                    break;
                                case '1':
                                   Datas.ElementAt(Datas.IndexOf(y)).Uno = x.Valor;
                                    break;
                                case '2':
                                    Datas.ElementAt(Datas.IndexOf(y)).Dos = x.Valor;
                                    break;
                                case '3':
                                    Datas.ElementAt(Datas.IndexOf(y)).Tres = x.Valor;
                                    break;
                                case '4':
                                    Datas.ElementAt(Datas.IndexOf(y)).Cuatro = x.Valor;
                                    break;
                                case '5':
                                    Datas.ElementAt(Datas.IndexOf(y)).Cinco = x.Valor;
                                    break;
                                case '6':
                                    Datas.ElementAt(Datas.IndexOf(y)).Seis = x.Valor;
                                    break;
                                case '7':
                                    Datas.ElementAt(Datas.IndexOf(y)).Siete = x.Valor;
                                    break;
                                case '8':
                                    Datas.ElementAt(Datas.IndexOf(y)).Ocho = x.Valor;
                                    break;
                                case '9':
                                    Datas.ElementAt(Datas.IndexOf(y)).Nueve = x.Valor;
                                    break;
                                case 'a':
                                    Datas.ElementAt(Datas.IndexOf(y)).A = x.Valor;
                                    break;
                                case 'b':
                                    Datas.ElementAt(Datas.IndexOf(y)).B = x.Valor;
                                    break;
                                case 'c':
                                    Datas.ElementAt(Datas.IndexOf(y)).C = x.Valor;
                                    break;
                                case 'd':
                                    Datas.ElementAt(Datas.IndexOf(y)).D = x.Valor;
                                    break;
                                case 'e':
                                    Datas.ElementAt(Datas.IndexOf(y)).E = x.Valor;
                                    break;
                                case 'f':
                                    Datas.ElementAt(Datas.IndexOf(y)).F = x.Valor;
                                    break;
                                default:
                                    break;
                            }bandex = true;
                        }
                        else
                        {
                            bandex = false;
                        }

                    }
                    if ( bandex == false)
                    {
                        Mapa m = new Mapa();
                        m.Direccion = aux;

                        switch (x.Direccion.Last())
                        {
                            case '0':
                                m.Cero = x.Valor;
                                break;
                            case '1':
                                m.Uno = x.Valor;
                                break;
                            case '2':
                                m.Dos = x.Valor;
                                break;
                            case '3':
                                m.Tres = x.Valor;
                                break;
                            case '4':
                                m.Cuatro = x.Valor;
                                break;
                            case '5':
                                m.Cinco = x.Valor;
                                break;
                            case '6':
                                m.Seis = x.Valor;
                                break;
                            case '7':
                                m.Siete = x.Valor;
                                break;
                            case '8':
                                m.Ocho = x.Valor;
                                break;
                            case '9':
                                m.Nueve = x.Valor;
                                break;
                            case 'a':
                                m.A = x.Valor;
                                break;
                            case 'A':
                                m.A = x.Valor;
                                break;
                            case 'b':
                                m.B = x.Valor;
                                break;
                            case 'c':
                                m.C = x.Valor;
                                break;
                            case 'd':
                                m.D = x.Valor;
                                break;
                            case 'e':
                                m.E = x.Valor;
                                break;
                            case 'f':
                                m.F = x.Valor;
                                break;
                            default:
                                break;
                        }
                        Datas.Add(m);
                    }
                }
            }
            dataGridView1.DataSource = Datas;
        }
        public string RestaHex(string s1, string s2)
        {
            int numero1 = Convert.ToInt32(s1, 16);
            int numero2 = Convert.ToInt32(s2, 16);
            int suma = numero1 - numero2;
            string sumH = suma.ToString("x");
            if (sumH.Count() > 12)
                sumH = sumH.Remove(1, 20);
            return sumH;
        }
        public bool compcad(string s1, string s2)
        {
            bool band = true;
            //MessageBox.Show("Compara"+s1.Length.ToString()+s2.Length.ToString());
            if (s1.Length == s2.Length)
            {
                //MessageBox.Show("Compara" + s1.Equals(s2).ToString());
                for (int i=0;i<s1.Length;i++)
                {
                    if (s1[i] != s2[i])
                    {
                        band = false;
                        break;
                    }
                }
            }else
            {
                band = false;
            }
            return band;
        }
        public static int hexadecimalDecimal(String hexadecimal)
        {

            int numero = 0;

            const int DIVISOR = 16;

            for (int i = 0, j = hexadecimal.Length - 1; i < hexadecimal.Length; i++, j--)
            {

                if (hexadecimal[i] >= '0' && hexadecimal[i] <= '9')
                {
                    numero += (int)Math.Pow(DIVISOR, j) * Convert.ToInt32(hexadecimal[i] + "");
                }
                else if (hexadecimal[i] >= 'A' && hexadecimal[i] <= 'F')
                {
                    numero += (int)Math.Pow(DIVISOR, j) * Convert.ToInt32((hexadecimal[i] - 'A' + 10) + "");
                }
                else
                {
                    return -1;
                }

            }

            return numero;

        }
        
        public string SumaHex(string s1, string s2)
        {
            int numero1 = Convert.ToInt32(s1, 16);
            int numero2 = Convert.ToInt32(s2, 16);
            int suma = numero1 + numero2;
            string sumH = suma.ToString("x");
            return sumH;
        }
    }
}
