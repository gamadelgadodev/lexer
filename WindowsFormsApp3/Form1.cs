using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Antlr4;
using Antlr4.Runtime;
using NCalc;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        string RutaArchivo = "";
        public static List<string> ParseErrorList = new List<string>();
        string basse = "";
        string dirF = "";
        public Form1()
        {
            InitializeComponent();
        }
        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // directorio inicial donde se abrira
            //openFileDialog1.InitialDirectory = "C:\\";
            // filtro de archivos.
            //openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";

            // codigo para abrir el cuadro de dialogo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RutaArchivo = openFileDialog1.FileName;
                    //textBox1.Text = str_RutaArchivo;
                    string str_textoArchivo = leerArchivo(RutaArchivo);
                    richTextBox1.Text = str_textoArchivo;
                    //textBox1.Text = RutaArchivo;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            AddLineNumbers();
        }
        public string leerArchivo(string str_ruta)
        {
            string str_textoArchivo = System.IO.File.ReadAllText(@"" + str_ruta);
            return str_textoArchivo;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            RutaArchivo = "";
            //textBox1.Text = RutaArchivo;
            richTextBox2.Clear();
            dataGridView1.DataSource = "";
            dataGridView2.DataSource = "";
            richTextBox3.Text = "";
            dirF = "";
            basse = "";
            label4.Text = "0";
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RutaArchivo=="")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string txt = saveFileDialog1.FileName;
                    StreamWriter textoguardar = new StreamWriter(txt);
                    textoguardar.Write(richTextBox1.Text);
                    RutaArchivo = txt;
                    textoguardar.Close();
                }
            }
            else
            {
                StreamWriter textoguardar = new StreamWriter(RutaArchivo);
                textoguardar.Write(richTextBox1.Text);
                textoguardar.Close();
            }
            
        }

        // codigo para analizar
        public void analiza(List<int> listaErr)
        {
            
            string line = RutaArchivo;
            //line = Console.ReadLine();
            //SE ALMACENA LA CADENA DE ENTRADA
           
                //SI DETECTA EXIT SALE DEL PROGRAMA
            string[] lines = File.ReadAllLines(line);
            for (int x = 0; x < lines.Length; x++)
            {
                //Console.WriteLine(lines[x]);
                calcLexer lex = new calcLexer(new AntlrInputStream(lines[x] + Environment.NewLine));
                //CREAMOS UN LEXER CON LA CADENA QUE ESCRIBIO EL USUARIO
                lex.RemoveErrorListeners();
                lex.AddErrorListener(new ThrowExceptionErrorListener(ParseErrorList, x));
                CommonTokenStream tokens = new CommonTokenStream(lex);
                //CREAMOS LOS TOKENS SEGUN EL LEXER CREADO
                calcParser parser = new calcParser(tokens);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ThrowExceptionErrorListener(ParseErrorList, x));
                //CREAMOS EL PARSER CON LOS TOKENS CREADOS
                try
                {
                    parser.programa();
                    //SE VERIFICA QUE EL ANALIZADOR EMPIECE CON LA EXPRESION

                }
                catch (RecognitionException e)
                {

                    Console.WriteLine(e.StackTrace);
                }
            }
            StreamWriter sw = new StreamWriter( line+"err" );
            foreach (string e in ParseErrorList)
            {
                char[] op = e.ToCharArray();
                string opS = "";
                bool band = false;
                foreach (char lx in op)
                {
                    if (lx == ':')
                        break;
                    if (band)
                    {
                        opS = opS + lx;
                        // Console.WriteLine(opS);
                    }
                    if (lx == 'a')
                        band = !band;


                }
                //Console.WriteLine(opS);

                listaErr.Add(int.Parse(opS));
                sw.WriteLine(e);
                
            }
            sw.Close();
            richTextBox2.Text = System.IO.File.ReadAllText(@"" + RutaArchivo + "err");
            //paso(lines, line, listaErr);
            ParseErrorList.Clear();
            
        }
        public void paso(string[] prog, string nombreArch, List<int> err,List<Linea> intermedio,List<simbolo> tabsim)
        {
            //List<simbolo> tabsim = new List<simbolo>();
            int i = 1;
            string nombreArchin = "Intermedio" + nombreArch;
            string direccionini = "";
            string CONTLOC = "";
            //string basse = "";
            int cont = 0;
            Linea linea = LeerLinea(prog[cont]);
            //Console.WriteLine(linea.Etiquieta);
            //Console.WriteLine(linea.Codop);
            //Console.WriteLine(linea.Op1);
            if (linea.Codop == "START")
            {
                direccionini = ConvH(linea.Op1);
                CONTLOC = direccionini;
                linea.NumLinea = i++.ToString();
                linea.Cp = CONTLOC.ToUpper();
                intermedio.Add(linea);
                //EscribirArchIn(nombreArchin, (cont + 1) + " " + CONTLOC.ToUpper() + " " + prog[cont]);
                cont++;
                linea = LeerLinea(prog[cont]);
            }
            else
            {
                CONTLOC = "0";
            }
            //Console.WriteLine("Hago start");

            while (linea.Codop != "END")
            {
                linea.NumLinea = i++.ToString();
                linea.Cp = CONTLOC.ToUpper();
                intermedio.Add(linea);
                //EscribirArchIn(nombreArchin, (cont + 1) + " " + CONTLOC.ToUpper() + " " + prog[cont]);
                bool simDuplicado = false;
                if (!err.Contains(cont + 1))
                {
                    if (linea.Etiquieta != "")
                    {
                        simbolo simb = new simbolo();
                        if (linea.Codop == "EQU")
                        {
                            if (linea.Op1 == "*")
                            {
                                simb.Simbolon = linea.Etiquieta;
                                simb.Direccion = CONTLOC.ToUpper();
                                simb.Tipo = "A";
                                if (contiene(tabsim, simb))
                                {
                                    //linea.Cp = CONTLOC.ToUpper();
                                    //intermedio.Add(linea);
                                    EscribirArchIn(nombreArch + "err", "Linea " + cont + ":Simbolo duplicado");
                                }
                                else
                                    tabsim.Add(simb);
                            }
                            else if (constaovar(linea.Op1))
                                {
                                    //simbolo simb = new simbolo();
                                    //
                                    simb.Simbolon = linea.Etiquieta;
                                    simb.Direccion = ConvH(linea.Op1).ToUpper();
                                    simb.Tipo = "A";
                                    if (contiene(tabsim, simb))
                                    {
                                        //linea.Cp = CONTLOC.ToUpper();
                                        //intermedio.Add(linea);
                                        EscribirArchIn(nombreArch + "err", "Linea " + cont + ":Simbolo duplicado");
                                    }
                                    else
                                        tabsim.Add(simb);
                            }else
                            {
                                simbolo valor = analizaExpreEQU(tabsim, linea.Op1);
                                if(valor.Direccion=="ERROR SNE" || valor.Direccion == "ERROR")
                                {
                                    linea.CodObj = valor.Direccion;
                                    EscribirArchIn(nombreArch + "err", "Linea " + (cont+1) + ":Error en la expresion");
                                    simb.Simbolon = linea.Etiquieta;
                                    simb.Direccion = "FFFFFF";
                                    simb.Tipo = valor.Tipo;
                                    if (contiene(tabsim, simb))
                                    {
                                        //linea.Cp = CONTLOC.ToUpper();
                                        //intermedio.Add(linea);
                                        EscribirArchIn(nombreArch + "err", "Linea " + cont + ":Simbolo duplicado");
                                    }
                                    else
                                        tabsim.Add(simb);
                                    
                                }
                                else
                                {
                                    //string[] res = valor.Direccion;
                                    simb.Simbolon = linea.Etiquieta;
                                    simb.Direccion =  valor.Direccion.ToUpper();
                                    simb.Tipo = valor.Tipo;
                                    if (contiene(tabsim, simb))
                                    {
                                        //linea.Cp = CONTLOC.ToUpper();
                                        //intermedio.Add(linea);
                                        EscribirArchIn(nombreArch + "err", "Linea " + cont + ":Simbolo duplicado");
                                    }
                                    else
                                        tabsim.Add(simb);
                                }
                            }
                        }else if (linea.Etiquieta != "")
                        {
                            //simbolo simb = new simbolo();
                            //simb = analizaExpre(tabsim, linea.Op1);
                            simb.Simbolon = linea.Etiquieta;
                            simb.Direccion = CONTLOC.ToUpper();
                            simb.Tipo = "R";
                            if (contiene(tabsim, simb))
                            {
                                //linea.Cp = CONTLOC.ToUpper();
                                //intermedio.Add(linea);
                                EscribirArchIn(nombreArch + "err", "Linea " + cont + ":Simbolo duplicado");
                            }
                            else
                                tabsim.Add(simb);
                        }
                    }
                    int sum = TabOp(linea.Codop);
                    if (sum > 0)
                        CONTLOC = SumaHex(CONTLOC, sum.ToString());
                    else if (linea.Codop == "WORD")
                        CONTLOC = SumaHex(CONTLOC, 3.ToString());
                    else if (linea.Codop == "RESW")
                        CONTLOC = SumaHex(CONTLOC, ConvH((3 * Convert.ToInt32(ConvH(linea.Op1), 16)).ToString()));
                    else if (linea.Codop == "RESB")
                    {
                        //Console.WriteLine(linea.Op1);
                        CONTLOC = SumaHex(CONTLOC, ConvH(linea.Op1));

                    }
                    else if (linea.Codop == "BASE")
                        basse = linea.Op1;
                    else if (linea.Codop == "BYTE")
                    {
                        char[] op = linea.Op1.ToCharArray();
                        string opS = "";
                        bool band = false;
                        foreach (char e in op)
                        {
                            if (band)
                                opS = opS + e;
                            if (e == '\'')
                                band = !band;
                        }
                        opS = opS.TrimEnd('\'');
                        // Console.WriteLine(opS);
                        //Console.WriteLine(opS.Length/2);
                        if (op[0] == 'C')
                            CONTLOC = SumaHex(CONTLOC, opS.Count().ToString());
                        else if (op[0] == 'X')
                            CONTLOC = SumaHex(CONTLOC, ConvH(((opS.Count() + 1) / 2).ToString()));
                        // Console.WriteLine(CONTLOC);
                    }
                }
                //EscribirArchIn(nombreArchin, CONTLOC + " " + prog[cont]);
                cont++;
                linea = LeerLinea(prog[cont]);
            }
            linea.NumLinea = i++.ToString();
            linea.Cp = CONTLOC.ToUpper();
            intermedio.Add(linea);
            //EscribirArchIn(nombreArchin, (cont + 1) + " " + CONTLOC.ToUpper() + " " + prog[cont]);
            StreamWriter sw = new StreamWriter(nombreArch + "Tabsim" );
            sw.WriteLine("Simbolo " + "Direccion");
            foreach (simbolo s in tabsim)
            {
                sw.WriteLine(s.Simbolon + " " + s.Direccion);
            }
            sw.WriteLine("Tamano del programa:" + CONTLOC);
            sw.Close();
            escribirArch(intermedio, RutaArchivo + "OBJ1");
            //Console.WriteLine("Archivos generados correctamente");
            richTextBox2.Text = "";
            richTextBox2.Text = System.IO.File.ReadAllText(@"" + RutaArchivo + "err");
            dataGridView1.DataSource = intermedio;
            dataGridView2.DataSource = tabsim;
            dirF = CONTLOC;
            //paso2(nombreArch, err, tabsim, basse, CONTLOC);
        }
        public void paso2(string nombreArch, List<int> err, List<simbolo> tabsim, string bass, string dirF,List<Linea> intermedio)
        {
            File.Delete(nombreArch+"REG");
            string basse = contieneDs(tabsim, bass);
            string[] prog = File.ReadAllLines( nombreArch + "OBJ1");
            //List<simbolo> tabsim = new List<simbolo>();
            string nombreArchin = nombreArch + "OBJF";
            string nombreArchinreg =  nombreArch + "REG" ;
            string direccionini = "";
            string CONTLOC = "";
            string nameP = "";
            int cont = 0;
            Linea linea = LeerLineaf(prog[cont]);
            //Console.WriteLine(linea.Etiquieta);
            //Console.WriteLine(linea.Codop);
            //Console.WriteLine(linea.Op1);
            if (linea.Codop == "START")
            {
                nameP = nombreP(linea.Etiquieta);
                direccionini = ConvH(linea.Op1);
               // MessageBox.Show(dirF);
                CONTLOC = "--";
                intermedio[cont].CodObj = CONTLOC;
                EscribirArchIn(nombreArchin, prog[cont] + " " + "--");
                EscribirArchIn(nombreArchinreg, "H" + nameP + completa(direccionini, 3) + completa(dirF, 3));
                cont++;
                linea = LeerLineaf(prog[cont]); ;
            }
            else
            {
                CONTLOC = "--";
            }
            //Console.WriteLine("Hago start");
            string Treg = "";
            string Taux = "";
            int conT = 0;
            List<string> reloc = new List<string>();
            while (linea.Codop != "END")
            {
                
                bool simDuplicado = false;
                if (!err.Contains(cont + 1))
                {
                    int sum = TabOp(linea.Codop);
                    if (sum > 0)
                    {
                        bool band = false;
                        if (linea.Codop == "RSUB")
                            conT = conT + 3;
                        else
                            conT = conT + TabOp(linea.Codop);
                        if (Treg == "")
                            Treg = "T" + completa(linea.Cp, 3);
                        if (TabOp(linea.Codop) == 4)
                            band = true;
                        CONTLOC = ensambla(linea, tabsim, basse);
                        if (band)
                        {
                            string regM = "M" + completa(SumaHex(linea.Cp, "1"), 3) + "05" + "+" + nameP;
                            reloc.Add(regM);
                        }
                        //EscribirArchIn(nombreArchin, prog[cont] + "\t" + CONTLOC);

                    }
                    else if (linea.Codop == "WORD")
                    {
                        if (constaovar(linea.Op1))
                        {
                            CONTLOC = completa(ConvH(linea.Op1), 3);
                            if (Treg == "")
                                Treg = "T" + completa(linea.Cp, 3);


                        }
                        else
                        {
                            simbolo s = analizaExpreEQU(tabsim, linea.Op1);
                            CONTLOC = completa(s.Direccion, 3).ToUpper();
                            if (Treg == "")
                                Treg = "T" + completa(linea.Cp, 3);

                        }
                        conT = conT + 3;
                    }

                    else if (linea.Codop == "BYTE")
                    {
                        if (Treg == "")
                            Treg = "T" + completa(linea.Cp, 3);
                        char[] op = linea.Op1.ToCharArray();
                        string opS = "";
                        bool band = false;
                        foreach (char e in op)
                        {
                            if (band)
                                opS = opS + e;
                            if (e == '\'')
                                band = !band;
                        }
                        opS = opS.TrimEnd('\'');
                       // Console.WriteLine("hola entro");
                        //Console.WriteLine(opS.Length/2);
                        if (op[0] == 'C')
                        {
                            CONTLOC = aschi(opS);
                            conT = conT + ((CONTLOC.Count() + 1) / 2);
                        }
                        else if (op[0] == 'X')
                        {
                            CONTLOC = opS;
                            conT = conT + ((CONTLOC.Count() + 1) / 2);
                        }

                    }

                    if (conT >= 29 || linea.Codop == "RESW" || linea.Codop == "RESB")
                    {
                        if (Taux != "")
                        {
                            EscribirArchIn(nombreArchinreg, Treg + completa(ConvH(conT.ToString()).ToUpper(), 2) + Taux);
                        }
                        Taux = "";
                        Treg = "";
                        conT = 0;
                    }
                    else
                    {
                        if (!CONTLOC.Contains("ERROR"))
                            Taux = Taux + CONTLOC;
                        else
                        {

                            string contlocaux = sephex(CONTLOC);
                            Taux = Taux + contlocaux;
                        }
                        //Console.WriteLine(linea.Codop);
                    }
                }
                intermedio[cont].CodObj = CONTLOC;
                EscribirArchIn(nombreArchin, prog[cont] + "\t" + CONTLOC);
                CONTLOC = "";
                //EscribirArchIn(nombreArchin, CONTLOC + " " + prog[cont]);
                cont++;
                linea = LeerLineaf(prog[cont]); 
            }
            if (Taux != "")
                EscribirArchIn(nombreArchinreg, Treg + completa(ConvH(conT.ToString()).ToUpper(), 2) + Taux);
            Taux = "";
            Treg = "";
            foreach (string s in reloc)
            {
                EscribirArchIn(nombreArchinreg, s.ToUpper());
            }
            intermedio[cont].CodObj = CONTLOC;
            //EscribirArchIn(nombreArchin, prog[cont] + "\t" + CONTLOC);
            if (contieneDs(tabsim, linea.Op1) != "")
                EscribirArchIn(nombreArchinreg, "E" + completa(contieneDs(tabsim, linea.Op1).ToUpper(), 3));
            else
                EscribirArchIn(nombreArchinreg, "E" + "FFFFFF" + "  ERROR SIMBOLO NO ENCONTRADO");
            Console.WriteLine("Archivos generados correctamente");
            escribirArch(intermedio, nombreArchin);
            dataGridView1.DataSource = intermedio;
            richTextBox3.Text = System.IO.File.ReadAllText(@"" + RutaArchivo + "REG");
        }
        public simbolo analizaExpreEQU(List<simbolo> tabsim, string expresion)
        {
            string expreori = expresion;
            //MessageBox.Show("expresion original: " + expresion);
            simbolo valor = new simbolo();
            List<simbolo> simboloSop = new List<simbolo>();
            char[] charSeparators = new char[] { '*', '(', ')', '+', '-' };
            bool bandSimE = true;
            string[] simbolos = expresion.Split(charSeparators);
            foreach (string simbol in simbolos)
            {
                if (!contieneS(tabsim, simbol) && !constaovar(simbol))
                {
                    valor.Direccion = "ERROR SNE";
                    bandSimE = false;
                }
                //MessageBox.Show("simbolos" + simbol);
            }
            if (bandSimE)
            {
                //MessageBox.Show("Hola entro a analizar el segundo caso "+ simbolos.Length);
                //analizaer simbolos 
                List<simboloequ> simboloSign = new List<simboloequ>();

                for (int i = 0; i < simbolos.Length; i++)
                {
                    simbolo exu = new simbolo();
                    simboloequ aux = new simboloequ();
                    if (expresion.Contains(simbolos[i]) && contieneS(tabsim, simbolos[i]))
                    {
                        exu = findSimbol(tabsim, simbolos[i]);
                        aux.Direccion = exu.Direccion;
                        aux.Tipo = exu.Tipo;
                        //MessageBox.Show(exu.Tipo + exu.Simbolon + exu.Tipo);
                        int ini = expresion.IndexOf(simbolos[i]);
                        if (ini != 0 && expresion[ini - 1] == '-')
                        {
                            aux.Simbolon = simbolos[i];
                            aux.Signo = "-";
                            simboloSign.Add(aux);
                        }
                        else if (ini != 0 && expresion[ini - 1] == '*' && aux.Tipo == "R")
                        {
                            valor.Tipo = "A";
                            valor.Direccion = "ERROR";

                        }
                        else
                        {
                            aux.Simbolon = simbolos[i];
                            simboloSign.Add(aux);
                        }
                    }
                    else if (simbolos[i] !="")
                    {
                        aux.Tipo = "A";
                        aux.Simbolon = simbolos[i];
                        aux.Direccion = simbolos[i];
                        //MessageBox.Show("termino Absolutos"+aux.Tipo + aux.Simbolon + aux.Tipo);
                        simboloSign.Add(aux);
                    }
                }
                if (valor.Direccion != "ERROR SNE" && valor.Direccion != "ERROR SNE")
                {
                    valor.Tipo = tipoexpre(simboloSign);
                   // MessageBox.Show("Tipo expresion" + valor.Tipo);
                    foreach (simboloequ s in simboloSign)
                    {
                       expreori = expreori.Replace(s.Simbolon, Convert.ToInt32(s.Direccion, 16).ToString());
                    }
                }
                //foreach (simboloequ s in simboloSign)
                //{
                //    MessageBox.Show(s.Tipo + s.Simbolon + s.Direccion);
                //}
                if(valor.Direccion!="ERROR")
                {
                   // MessageBox.Show("expresion susitituida: " + expreori + "direccion calculada: " + valor.Direccion);
                    valor.Direccion = ConvH(calcula(expreori));
                }
            }
            return valor;
        }
        public string calcula(string expre)
        {
            Expression myExpr = new Expression(expre);
            var val = myExpr.Evaluate();
            string res = val.ToString();
            return res;
        }
        public string tipoexpre(List<simboloequ> simbolos)
        {
            string tipo = "";
            int contA=0;
            int contRp=0;
            int contRn=0;
            foreach (simboloequ s in simbolos)
            {
                switch (s.Tipo)
                {
                    case ("A"):
                        contA++;
                        break;
                    case ("R"):
                        if (s.Signo == "-")
                            contRn++;
                        else
                            contRp++;
                        break;
                }
            }
            //MessageBox.Show(contA + "-" + contRn + "-" + contRp + "_A-RN-RP");
            if ((contRn - contRp) >= 1)
                tipo = "E";
            else if ((contRn - contRp) == 0)
                tipo = "A";
            else if ((contRp - contRn) == 1)
                tipo = "R";
            else if ((contRp - contRn) > 1)
                tipo = "E";
            else if (contRp == 0 && contRn == 0 && contA != 0)
                tipo = "A";
            else
                tipo = "E";
            return tipo;
        }
        public simbolo findSimbol(List<simbolo> tabsim,string simbol)
        {
            simbolo x = new simbolo();
            foreach (simbolo simbo in tabsim)
            {
                if (simbo.Simbolon.Equals(simbol))
                {
                    x = simbo;
                }
            }
            return x;
        }
        public string contieneDs(List<simbolo> tabsim, string sim)
        {
            string val = "";
            foreach (simbolo s in tabsim)
            {
                if (s.Simbolon == sim)
                    val = s.Direccion;
            }
            return val;
        }
        public simbolo contieneSimb(List<simbolo> tabsim, string sim)
        {
            simbolo val = new simbolo();
            foreach (simbolo s in tabsim)
            {
                if (s.Simbolon == sim)
                    val = s;
            }
            return val;
        }
        public string ensambla(Linea linea, List<simbolo> tabsim, string basse)
        {
            string codiObj = "";
            string op="";
            int formato = TabOp(linea.Codop);
            if (linea.Op1.Contains("(") && linea.Op1.Contains(")"))
            {
                if (linea.Op1.Contains("#"))
                {
                    op = linea.Op1.Replace("#", "");
                }else if (linea.Op1.Contains("@"))
                {
                    op = linea.Op1.Replace("@", "");
                }else
                {
                    op = linea.Op1;
                }
                simbolo s = analizaExpreEQU(tabsim, op);
                if (formato==3)
                {
                    codiObj = completa(s.Direccion, 3).ToUpper();
                }else if (formato == 4)
                {
                    codiObj = completa(s.Direccion, 4).ToUpper();
                }
            }else
            {
                switch (formato)
                {
                    case 1:
                        codiObj = formato1(linea.Codop);
                        break;
                    case 2:
                        codiObj = formato2(linea);
                        break;
                    case 3:
                        codiObj = completa(formato3(linea, tabsim, basse), 3);
                        break;
                    case 4:
                        codiObj = completa(formato4(linea, tabsim), 4);
                        break;
                    default:
                        codiObj = "";
                        break;
                }
            }
            
            return codiObj;
        }
        public string formato1(string cod)
        {
            string res = "";
            string[] codop = new string[] { "FIX", "FLOAT", "HIO", "NORM", "SIO", "TIO" };
            string[] vcod = new string[] { "C4", "C0", "F4", "C8", "F0", "F8" };
            for (int i = 0; i < codop.Count(); i++)
            {
                if (cod == codop[i])
                    res = vcod[i];
            }
            return res;
        }
        public string formato2(Linea linea)
        {
            string[] nreg = new string[] { "A", "X", "L", "B", "S", "T", "F" };
            string[] vreg = new string[] { "0", "1", "2", "3", "4", "5", "6" };
            string res = "";
            string[] words = linea.Op1.Split(',');
            switch (linea.Codop)
            {
                case "SHIFTL":
                    res = "A4";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    res = res + ConvH(words[1]);
                    break;
                case "SHIFTR":
                    res = "A8";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    res = res + ConvH(words[1]);
                    break;
                case "SVC":
                    res = "B0" + ConvH(words[0]);
                    res = res + "0";
                    break;
                case "CLEAR":
                    res = "B4";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    res = res + "0";
                    break;
                case "TIXR":
                    res = "B8";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    res = res + "0";
                    break;
                case "ADDR":
                    res = "90";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                case "COMPR":
                    res = "A0";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                case "DIVR":
                    res = "9C";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                case "MULR":
                    res = "98";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                case "RMO":
                    res = "AC";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                case "SUBR":
                    res = "94";
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[0] == nreg[i])
                            res = res + vreg[i];
                    }
                    for (int i = 0; i < nreg.Count(); i++)
                    {
                        if (words[1] == nreg[i])
                            res = res + vreg[i];
                    }
                    break;
                default:
                    break;
            }
            return res;
        }
        public string formato3(Linea linea, List<simbolo> tabsim, string basse)
        {
            string res = "";
            string[] codop = new string[] { "ADD", "ADDF", "AND", "COMP", "COMPF", "DIV",
                "DIVF", "J", "JEQ", "JGT", "JLT", "JSUB", "LDA", "LDB", "LDCH", "LDF",
                "LDL", "LDS", "LDT", "LDX", "LPS", "MUL", "MULF", "OR", "RD", "SSK",
                "STA", "STB", "STCH", "STF", "STI", "STL", "STS", "STSW", "STT", "STX", "SUB", "SUBF", "TD", "TIX", "WD","RSUB" };
            string[] vcod = new string[] { "18", "58", "40", "28", "88", "24",
                "64","3C","30","34","38","48","00","68","50","70",
                "08","6C","74","04","DO","20","60","44","D8","EC",
                "0C","78","54","80","D4","14","7C","E8","84","10","1C","5C","E0","2C","DC","4C"};
            for (int i = 0; i < codop.Count(); i++)
            {
                if (linea.Codop == codop[i])
                    res = vcod[i];
            }
            Console.WriteLine(res);
            if (res == "4C")
            {
                res = "4F0000";
                return res;
            }
            string bin = ConvertirB(res);
            string binaux = "00000000";
            if (bin.Count() < 8)
            {

                int tam = bin.Count();

                binaux = binaux.Remove(binaux.Length - tam);
                binaux = binaux + bin;
            }
            binaux = binaux.Remove(binaux.Length - 2);
            if (linea.Op1.Contains("@"))
            {
                binaux = binaux + "10";
                linea.Op1 = linea.Op1.Replace("@", "");
                if (constaovar(linea.Op1))
                {
                    string bindesp = ConvertirB(ConvH(linea.Op1));
                    binaux = binaux + "0000";
                    string desp = "000000000000";
                    int tam = bindesp.Count();
                    desp = desp.Remove(desp.Length - tam);
                    desp = desp + bindesp;
                    binaux = binaux + desp;
                    res = convertBtH(binaux);
                }
                else
                {
                    if (contieneDs(tabsim, linea.Op1) != "")
                    {
                        string Ta = contieneDs(tabsim, linea.Op1);
                        if (compHex(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))), "800"))
                        {
                            string bindesp = ConvertirB(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))));
                            int tam = 0;
                            if (bindesp.Count() > 12)
                            {
                                bindesp = bindesp.Remove(0, 20);
                                tam = 12;
                            }
                            else
                                tam = bindesp.Count();


                            binaux = binaux + "0010";
                            string desp = "000000000000";

                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);
                        }
                        else if (hexnong(Ta, basse))
                        {
                            string bindesp = ConvertirB(RestaHex(Ta, basse));
                            int tam = 0;
                            if (bindesp.Count() > 12)
                            {
                                bindesp = bindesp.Remove(0, 20);
                                tam = 12;
                            }
                            else
                                tam = bindesp.Count();
                            binaux = binaux + "0100";
                            string desp = "000000000000";
                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);
                        }
                        else
                        {
                            binaux = binaux + "0110111111111111";
                            res = convertBtH(binaux) + "  Error no relativo al contador ni a la base";
                        }
                    }
                    else
                    {
                        binaux = binaux + "0110111111111111";
                        res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                    }
                }
            }
            else if (linea.Op1.Contains("#"))
            {
                binaux = binaux + "01";
                linea.Op1 = linea.Op1.Replace("#", "");
                if (constaovar(linea.Op1))
                {
                    string bindesp = ConvertirB(ConvH(linea.Op1));
                    binaux = binaux + "0000";
                    string desp = "000000000000";
                    int tam = bindesp.Count();
                    desp = desp.Remove(desp.Length - tam);
                    desp = desp + bindesp;
                    binaux = binaux + desp;
                    res = convertBtH(binaux);
                }
                else
                {
                    if (contieneDs(tabsim, linea.Op1) != "")
                    {
                        string Ta = contieneDs(tabsim, linea.Op1);
                        if (compHex(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))), "800"))
                        {

                            string bindesp = ConvertirB(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))));
                            int tam = 0;
                            if (bindesp.Count() > 12)
                            {
                                bindesp = bindesp.Remove(0, 20);
                                tam = 12;
                            }
                            else
                                tam = bindesp.Count();
                            binaux = binaux + "0010";
                            string desp = "000000000000";
                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);
                        }
                        else if (hexnong(Ta, basse))
                        {
                            string bindesp = ConvertirB(RestaHex(Ta, basse));
                            int tam = 0;
                            if (bindesp.Count() > 12)
                            {
                                bindesp = bindesp.Remove(0, 20);
                                tam = 12;
                            }
                            else
                                tam = bindesp.Count();
                            binaux = binaux + "0100";
                            string desp = "000000000000";
                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);

                        }
                        else
                        {
                            binaux = binaux + "0110111111111111";
                            res = convertBtH(binaux) + "  Error no relativo al contador ni a la base";
                        }
                    }
                    else
                    {
                        binaux = binaux + "0110111111111111";
                        res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                    }
                }
            }
            else
            {
                binaux = binaux + "11";

                if (linea.Op1.Contains(","))
                {
                    string[] words = linea.Op1.Split(',');
                    if (constaovar(words[0]))
                    {
                        if (words[1] == "X")
                        {
                            string bindesp = ConvertirB(ConvH(words[0]));
                            binaux = binaux + "1000";
                            string desp = "000000000000";
                            int tam = bindesp.Count();
                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);
                        }
                    }
                    else
                    {
                        if (contieneDs(tabsim, words[0]) != "")
                        {
                            string Ta = contieneDs(tabsim, words[0]);
                            if (compHex(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))), "800"))
                            {
                                string bindesp = ConvertirB(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))));
                                int tam = 0;
                                if (bindesp.Count() > 12)
                                {
                                    bindesp = bindesp.Remove(0, 20);
                                    tam = 12;
                                }
                                else
                                    tam = bindesp.Count();
                                binaux = binaux + "1010";
                                string desp = "000000000000";
                                desp = desp.Remove(desp.Length - tam);
                                desp = desp + bindesp;
                                binaux = binaux + desp;
                                res = convertBtH(binaux);
                            }
                            else if (hexnong(Ta, basse))
                            {
                                string bindesp = ConvertirB(RestaHex(Ta, basse));
                                int tam = 0;
                                if (bindesp.Count() > 12)
                                {
                                    bindesp = bindesp.Remove(0, 20);
                                    tam = 12;
                                }
                                else
                                    tam = bindesp.Count();
                                binaux = binaux + "1100";
                                string desp = "000000000000";
                                desp = desp.Remove(desp.Length - tam);
                                desp = desp + bindesp;
                                binaux = binaux + desp;
                                res = convertBtH(binaux);

                            }
                            else
                            {
                                binaux = binaux + "1110111111111111 ";
                                res = convertBtH(binaux) + "  Error no relativo al contador ni a la base";
                            }
                        }
                        else
                        {
                            binaux = binaux + "0110111111111111";
                            res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                        }
                    }
                }
                else
                {
                    if (constaovar(linea.Op1))
                    {
                        string bindesp = ConvertirB(ConvH(linea.Op1));
                        binaux = binaux + "0000";
                        string desp = "000000000000";
                        int tam = bindesp.Count();
                        desp = desp.Remove(desp.Length - tam);
                        desp = desp + bindesp;
                        binaux = binaux + desp;
                        res = convertBtH(binaux);
                    }
                    else
                    {
                        if (contieneDs(tabsim, linea.Op1) != "")
                        {
                            string Ta = contieneDs(tabsim, linea.Op1);
                            if (compHex(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))), "800"))
                            {
                                string bindesp = ConvertirB(RestaHex(Ta, SumaHex(linea.Cp, ConvH(TabOp(linea.Codop).ToString()))));
                                int tam = 0;
                                if (bindesp.Count() > 12)
                                {
                                    bindesp = bindesp.Remove(0, 20);
                                    tam = 12;
                                }
                                else
                                    tam = bindesp.Count();
                                binaux = binaux + "0010";
                                string desp = "000000000000";
                                desp = desp.Remove(desp.Length - tam);
                                desp = desp + bindesp;
                                binaux = binaux + desp;
                                res = convertBtH(binaux);
                            }
                            else if (hexnong(Ta, basse))
                            {
                                string bindesp = ConvertirB(RestaHex(Ta, basse));
                                int tam = 0;
                                if (bindesp.Count() > 12)
                                {
                                    bindesp = bindesp.Remove(0, 20);
                                    tam = 12;
                                }
                                else
                                    tam = bindesp.Count();
                                binaux = binaux + "0100";
                                string desp = "000000000000";
                                desp = desp.Remove(desp.Length - tam);
                                desp = desp + bindesp;
                                binaux = binaux + desp;
                                res = convertBtH(binaux);

                            }
                            else
                            {
                                binaux = binaux + "0110111111111111";
                                res = convertBtH(binaux) + "  Error no relativo al contador ni a la base";
                            }
                        }
                        else
                        {
                            binaux = binaux + "0110111111111111";
                            res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                        }
                    }
                }

            }
            Console.WriteLine(linea.Codop + "operando a eliminar" + linea.Op1);
            Console.WriteLine(binaux);
            Console.WriteLine(res);
            res = res.ToUpper();
            return res;
        }
        public string formato4(Linea linea, List<simbolo> tabsim)
        {
            string res = "";
            linea.Codop = linea.Codop.Replace("+", "");
            string[] codop = new string[] { "ADD", "ADDF", "AND", "COMP", "COMPF", "DIV",
                "DIVF", "J", "JEQ", "JGT", "JLT", "JSUB", "LDA", "LDB", "LDCH", "LDF",
                "LDL", "LDS", "LDT", "LDX", "LPS", "MUL", "MULF", "OR", "RD", "SSK",
                "STA", "STB", "STCH", "STF", "STI", "STL", "STS", "STSW", "STT", "STX", "SUB", "SUBF", "TD", "TIX", "WD","RSUB" };
            string[] vcod = new string[] { "18", "58", "40", "28", "88", "24",
                "64","3C","30","34","38","48","00","68","50","70",
                "08","6C","74","04","D0","20","60","44","D8","EC",
                "0C","78","54","80","D4","14","7C","E8","84","10","1C","5C","E0","2C","DC","4C"};
            for (int i = 0; i < codop.Count(); i++)
            {
                if (linea.Codop == codop[i])
                    res = vcod[i];
            }
            Console.WriteLine(res);
            if (res == "4C")
            {
                res = "4F0000";
                return res;
            }
            string bin = ConvertirB(res);
            string binaux = "00000000";
            if (bin.Count() < 8)
            {

                int tam = bin.Count();

                binaux = binaux.Remove(binaux.Length - tam);
                binaux = binaux + bin;
            }
            binaux = binaux.Remove(binaux.Length - 2);
            if (linea.Op1.Contains("@"))
            {
                linea.Op1 = linea.Op1.Replace("@", "");
                binaux = binaux + "10";
                if (contieneDs(tabsim, linea.Op1) != "")
                {
                    string Ta = contieneDs(tabsim, linea.Op1);
                    string bindesp = ConvertirB(Ta);
                    binaux = binaux + "0001";
                    string desp = "0000000000000000";
                    int tam = bindesp.Count();
                    desp = desp.Remove(desp.Length - tam);
                    desp = desp + bindesp;
                    binaux = binaux + desp;
                    res = convertBtH(binaux);
                }
                else
                {
                    binaux = binaux + "011111111111111111111111";
                    res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                }
            }
            if (linea.Op1.Contains("#"))
            {
                linea.Op1 = linea.Op1.Replace("#", "");
                binaux = binaux + "01";
                if (contieneDs(tabsim, linea.Op1) != "")
                {
                    string Ta = contieneDs(tabsim, linea.Op1);
                    string bindesp = ConvertirB(Ta);
                    binaux = binaux + "0001";
                    string desp = "00000000000000000000";
                    int tam = bindesp.Count();
                    desp = desp.Remove(desp.Length - tam);
                    desp = desp + bindesp;
                    binaux = binaux + desp;
                    res = convertBtH(binaux);
                }
                else
                {
                    binaux = binaux + "011111111111111111111111";
                    res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                }
            }
            else
            {
                binaux = binaux + "11";
                if (linea.Op1.Contains(","))
                {
                    string[] words = linea.Op1.Split(',');
                    if (constaovar(words[0]))
                    {
                        if (words[1] == "X")
                        {
                            string Ta = contieneDs(tabsim, words[0]);
                            string bindesp = ConvertirB(Ta);
                            binaux = binaux + "0001";
                            string desp = "00000000000000000000";
                            int tam = bindesp.Count();
                            desp = desp.Remove(desp.Length - tam);
                            desp = desp + bindesp;
                            binaux = binaux + desp;
                            res = convertBtH(binaux);
                        }
                    }
                    else
                    {
                        binaux = binaux + "011111111111111111111111";
                        res = convertBtH(binaux) + "  Error: Operando incorrecto";
                    }
                }
                else
                {
                    binaux = binaux + "10";
                    if (contieneDs(tabsim, linea.Op1) != "")
                    {
                        string Ta = contieneDs(tabsim, linea.Op1);
                        string bindesp = ConvertirB(Ta);
                        binaux = binaux + "0001";
                        string desp = "00000000000000000000";
                        int tam = bindesp.Count();
                        desp = desp.Remove(desp.Length - tam);
                        desp = desp + bindesp;
                        binaux = binaux + desp;
                        res = convertBtH(binaux);
                    }
                    else
                    {
                        binaux = binaux + "011111111111111111111111";
                        res = convertBtH(binaux) + "  Error: Simbolo no encontrado";
                    }
                }
            }
            res = res.ToUpper();
            return res;
        }
        public string aschi(string carac)
        {
            string res = "";
            char[] values = carac.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the integer value to a hexadecimal value in string form.
                Console.WriteLine($"Hexadecimal value of {letter} is {value:X}");
                res = res + $"{ value:X}";
            }

            return res;
        }
        public string sephex(string op)
        {
            string cadN = "";
            char[] chars = op.ToCharArray();
            foreach (var c in chars)
            {
                if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                    cadN = cadN + c;
                if (c == ' ')
                    break;
            }
            return cadN;
        }
        public string ConvertirB(string Cadena)
        {
            string aBinario = "";
            aBinario = Convert.ToString(Convert.ToInt32(Cadena, 16), 2);
            return aBinario;
        }
        public bool constaovar(string op)
        {
            bool isHex;
            char[] chars = op.ToCharArray();
            foreach (var c in chars)
            {
                isHex = ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
                if (!isHex)
                    return false;
            }
            return true;
        }
        public string convertBtH(string bin)
        {

            int n = 0;
            for (int x = bin.Length - 1, y = 0; x >= 0; x--, y++)
            {
                if (bin[x] == '0' || bin[x] == '1')
                {
                    n += (int)(int.Parse(bin[x].ToString()) * Math.Pow(2, y));
                }
            }
            string Hex = ConvH(n.ToString());
            return Hex;
        }
        public bool compHex(string s1, string s2)
        {
            bool band = false;
            int numero1 = Convert.ToInt32(s1, 16);
            int numero2 = Convert.ToInt32(s2, 16);
            if (numero1 < numero2 && numero1 > -numero2)
                band = true;
            else
                band = false;
            return band;
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
        public bool hexnong(string s1, string s2)
        {
            bool band = false;
            int numero1 = Convert.ToInt32(s1, 16);
            int numero2 = Convert.ToInt32(s2, 16);
            int suma = numero1 - numero2;
            string sumH = suma.ToString("x");
            if (suma >= 0)
                band = true;
            if (suma < 0)
                band = false;
            return band;
        }
        public Linea LeerLineaf(string lineComp)
        {
            string[] codops = new string[] {"EQU","FIX" , "FLOAT" , "HIO" , "NORM" , "SIO" , "TIO" , "RSUB","SHIFTL", "SHIFTR" , "SVC" , "CLEAR" , "TIXR" , "ADDR" ,"COMPR" , "DIVR" , "MULR" , "RMO"  , "SUBR",
                                            "ADD" , "ADDF" , "AND" , "COMP" , "COMPF" , "DIV" , "DIVF" , "J" , "JEQ" , "JGT" , "JLT" , "JSUB" , "LDA" , "LDB" , "LDCH" , "LDF" , "LDL" , "LDS" , "LDT" , "LDX" , "LPS" , "MUL" , "MULF" , "OR" , "RD" , "SSK" , "STA" , "STB" , "STCH" , "STF" , "STI" , "STL" , "STS" , "STSW" , "STT" , "STX" , "SUB" , "SUBF" , "TD" , "TIX" , "WD"
                                            ,"START" , "END" , "BYTE" , "WORD", "RESB" , "BASE" , "RESW","+ADD" , "+ADDF" , "+AND" , "+COMP" , "+COMPF" , "+DIV" , "+DIVF" , "+J" , "+JEQ", "+JGT" , "+JLT" , "+JSUB" , "+LDA" , "+LDB" , "+LDCH" , "+LDF" , "+LDL" , "+LDS" , "+LDT" , "+LDX" , "+LPS" , "+MUL" , "+MULF" , "+OR" , "+RD" , "+SSK" , "+STA" , "+STB" , "+STCH" , "+STF" , "+STI" , "+STL" , "+STS" , "+STSW" , "+STT" , "+STX" , "+SUB" , "+SUBF" , "+TD" , "+TIX" , "+WD"};
            Linea linea = new Linea();
            int count = 0;
            int top = 2;
            string[] words = lineComp.Split('\t', ' ');
            linea.Cp = words[1];
            if (codops.Contains(words[2]))
                linea.Codop = words[2];
            else
            {
                linea.Etiquieta = words[2];
                linea.Codop = words[3];
                top++;
            }
            //MessageBox.Show(words[1]);
            foreach (string e in words)
            {
                if (count > top)
                {
                    linea.Op1 = linea.Op1 + words[count];
                }
                count++;
            }
            return linea;
        }
        public string completa(string num, int f)
        {
            string nuevo = "";
            string binaux = "";
            switch (f)
            {
                case 2:
                    binaux = "00";
                    if (num.Count() < 2)
                    {

                        int tam = num.Count();

                        binaux = binaux.Remove(binaux.Length - tam);
                        binaux = binaux + num;
                        num = binaux;
                    }
                    break;
                case 3:
                    binaux = "000000";
                    if (num.Count() < 6)
                    {

                        int tam = num.Count();

                        binaux = binaux.Remove(binaux.Count() - tam);
                        binaux = binaux + num;
                        num = binaux;
                    }
                    break;
                case 4:
                    binaux = "00000000";
                    if (num.Count() < 8)
                    {

                        int tam = num.Count();
                        binaux = binaux.Remove(binaux.Length - tam);
                        binaux = binaux + num;
                        num = binaux;
                    }
                    break;
                default:
                    break;
            }
            return num;
        }
        public string nombreP(string name)
        {
            if (name.Count() < 6)
            {
                for (int i = name.Count(); i < 6; i++)
                {
                    name = name + " ";
                }
            }
            else if (name.Count() > 6)
            {
                for (int i = name.Count(); i > 6; i--)
                {
                    name = name.Remove(name.Length - 1);
                }
            }
            return name;
        }
        public string SumaHex(string s1, string s2)
        {
            int numero1 = Convert.ToInt32(s1, 16);
            int numero2 = Convert.ToInt32(s2, 16);
            int suma = numero1 + numero2;
            string sumH = suma.ToString("x");
            return sumH;
        }
        public void escribirArch(List<Linea> inter,string ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            foreach (Linea s in inter)
            {
                sw.WriteLine(s.NumLinea + "\t"+ s.Cp + "\t" + s.Etiquieta + "\t" + s.Codop + "\t" + s.Op1 + "\t" + s.CodObj);
            }
            sw.Close();
        }
        public bool contiene(List<simbolo> tabsim, simbolo sim)
        {
            bool band = false;
            foreach (simbolo s in tabsim)
            {
                if (s.Simbolon == sim.Simbolon)
                    band = true;
            }
            return band;
        }
        public bool contieneS(List<simbolo> tabsim, string sim)
        {
            bool band = false;
            foreach (simbolo s in tabsim)
            {
                if (s.Simbolon == sim)
                    band = true;
            }
            return band;
        }
        public int TabOp(string pcop)
        {
            string[] codops1 = new string[] { "FIX", "FLOAT", "HIO", "NORM", "SIO", "TIO" };
            string[] codops2 = new string[] { "SHIFTL", "SHIFTR", "SVC", "CLEAR", "TIXR", "ADDR", "COMPR", "DIVR", "MULR", "RMO", "SUBR" };
            string[] codops3 = new string[] { "ADD", "ADDF", "AND", "COMP", "COMPF", "DIV", "DIVF", "J", "JEQ", "JGT", "JLT", "JSUB", "LDA", "LDB", "LDCH", "LDF", "LDL", "LDS", "LDT", "LDX", "LPS", "MUL", "MULF", "OR", "RD", "SSK", "STA", "STB", "STCH", "STF", "STI", "STL", "STS", "STSW", "STT", "STX", "SUB", "SUBF", "TD", "TIX", "WD", "RSUB" };
            string[] codops4 = new string[] { "+ADD", "+ADDF", "+AND", "+COMP", "+COMPF", "+DIV", "+DIVF", "+J", "+JEQ", "+JGT", "+JLT", "+JSUB", "+LDA", "+LDB", "+LDCH", "+LDF", "+LDL", "+LDS", "+LDT", "+LDX", "+LPS", "+MUL", "+MULF", "+OR", "+RD", "+SSK", "+STA", "+STB", "+STCH", "+STF", "+STI", "+STL", "+STS", "+STSW", "+STT", "+STX", "+SUB", "+SUBF", "+TD", "+TIX", "+WD", "+RSUB" };

            int sum = 0;
            if (codops1.Contains(pcop))
                sum = 1;
            else if (codops2.Contains(pcop))
                sum = 2;
            else if (codops3.Contains(pcop))
                sum = 3;
            else if (codops4.Contains(pcop))
                sum = 4;
            return sum;
        }
        public void EscribirArchIn(string nombreArch, string linea)
        {
            File.AppendAllLines(nombreArch, new String[] { linea });
        }
        public Linea LeerLinea(string lineComp)
        {
            string[] codops = new string[] {"FIX" , "FLOAT" , "HIO" , "NORM" , "SIO" , "TIO" , "RSUB","SHIFTL", "SHIFTR" , "SVC" , "CLEAR" , "TIXR" , "ADDR" ,"COMPR" , "DIVR" , "MULR" , "RMO"  , "SUBR",
                                            "ADD" , "ADDF" , "AND" , "COMP" , "COMPF" , "DIV" , "DIVF" , "J" , "JEQ" , "JGT" , "JLT" , "JSUB" , "LDA" , "LDB" , "LDCH" , "LDF" , "LDL" , "LDS" , "LDT" , "LDX" , "LPS" , "MUL" , "MULF" , "OR" , "RD" , "SSK" , "STA" , "STB" , "STCH" , "STF" , "STI" , "STL" , "STS" , "STSW" , "STT" , "STX" , "SUB" , "SUBF" , "TD" , "TIX" , "WD"
                                            ,"START" , "END" , "BYTE" , "WORD", "RESB" , "BASE" , "RESW","+ADD" , "+ADDF" , "+AND" , "+COMP" , "+COMPF" , "+DIV" , "+DIVF" , "+J" , "+JEQ", "+JGT" , "+JLT" , "+JSUB" , "+LDA" , "+LDB" , "+LDCH" , "+LDF" , "+LDL" , "+LDS" , "+LDT" , "+LDX" , "+LPS" , "+MUL" , "+MULF" , "+OR" , "+RD" , "+SSK" , "+STA" , "+STB" , "+STCH" , "+STF" , "+STI" , "+STL" , "+STS" , "+STSW" , "+STT" , "+STX" , "+SUB" , "+SUBF" , "+TD" , "+TIX" , "+WD"};
            Linea linea = new Linea();
            int count = 0;
            int top = 0;
            string[] words = lineComp.Split('\t', ' ');
            if (codops.Contains(words[0]))
                linea.Codop = words[0];
            else
            {
                linea.Etiquieta = words[0];
                linea.Codop = words[1];
                top++;
            }

            foreach (string e in words)
            {
                if (count > top)
                {
                    linea.Op1 = linea.Op1 + words[count];
                }
                count++;
            }
            return linea;
        }
        public string ConvH(string numH)
        {
            // Console.WriteLine(numH);
            if (numH.Contains("h") | numH.Contains("H"))
                numH = numH.TrimEnd('H');
            else
            {
                int dec = int.Parse(numH);
                numH = dec.ToString("x");
                // Console.WriteLine(numH);
            }
            return numH;
        }
        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RutaArchivo != "")
            {
                List<int> listaErr = new List<int>();
                //string[] lines= new string[];
                analiza(listaErr);
            }
            else
            {
                MessageBox.Show("Guarda el archivo para poder continuar");
            }
            
        }

        private void paso1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RutaArchivo!="")
            {
                List<int> listaErr = new List<int>();
                List<Linea> intermedio = new List<Linea>();
                List<simbolo> tabsim = new List<simbolo>();
                analiza(listaErr);
                string[] lines = File.ReadAllLines(RutaArchivo);
                paso(lines, RutaArchivo, listaErr, intermedio, tabsim);
                label4.Text = dirF;
            }else
            {
                MessageBox.Show("Guarda el archivo para poder continuar");
            }
        }

        private void paso2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RutaArchivo != "")
            {
                List<int> listaErr = new List<int>();
                List<Linea> intermedio = new List<Linea>();
                List<simbolo> tabsim = new List<simbolo>();
                analiza(listaErr);
                string[] lines = File.ReadAllLines(RutaArchivo);
                paso(lines, RutaArchivo, listaErr, intermedio, tabsim);
                //MessageBox.Show(dirF);
                paso2(RutaArchivo, listaErr, tabsim, basse, dirF, intermedio);
                label4.Text = dirF;
            }
            else
            {
                MessageBox.Show("Guarda el archivo para poder continuar");
            }
            
        }

        private void ensamblarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (RutaArchivo != "")
            {
                List<int> listaErr = new List<int>();
                List<Linea> intermedio = new List<Linea>();
                List<simbolo> tabsim = new List<simbolo>();
                analiza(listaErr);
                string[] lines = File.ReadAllLines(RutaArchivo);
                paso(lines, RutaArchivo, listaErr, intermedio, tabsim);
                //MessageBox.Show(dirF);
                paso2(RutaArchivo, listaErr, tabsim, basse, dirF, intermedio);
                label4.Text = dirF;
            }
            else
            {
                MessageBox.Show("Guarda el archivo para poder continuar");
            }

        }

        private void crearUnArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para crear un archivo basta con comenzar a escribir en la parte del codigo o en el menu de archivo " +
                "dar a la opcion de nuevo");
        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para guardar un archivo una vez que se escribio el codigo dentro del campo correspondiente basta con presionar la opccion de guardar el menu de archivo si el archivo se va a guardar por primera vez saldra un" +
                "una ventana emergente en la cual podremos elegir la ruta donde se guardara asignamos un nombre y una extension, en esa ruta se guardaran todos los archivo" +
                "generados por el porgrama");
        }

        private void ensamblarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para ensamblar tenemos el menu con las opciones correspondientes para poder ensamblar antes tuvimos que " +
                "guardar nuestro archivo una vez que tengamos el codigo y el archivo guardado basta con presionar la opcion deseada " +
                "y los campos se actualizaran segun la opcion presionada");
        }

        private void abrirUnArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para poder abrir un archivo basta con buscar en el direccitorio donde se guardo para identificar a este sera el que solo tenga el nombre del archivo" +
                "si tiene una extension como: REG,OBJ1,OBJF,TABSIM no se debe de abrir ya que estos son archivos generados por la misma app, en la ruta donde se encuntra la solucion van dos archivos los cuales ya se trabajaron en entregas anteriores del proyecto");
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formulario = new Cargador();
            formulario.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formulario = new Cargador();
            formulario.Show();
        }
    }
    class ThrowExceptionErrorListener : BaseErrorListener, IAntlrErrorListener<int>
    {
        public List<string> ErrorList { get; private set; }
        public List<string> Errorlist { get; }
        public int x { get; private set; }

        //BaseErrorListener implementation
        public ThrowExceptionErrorListener(List<string> errorList, int L)
        {
            x = L + 1;
            Errorlist = errorList;
        }
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            //throw new ArgumentException("Invalid Expression: {0}", msg, e);
            var errmsg = "Linea" + x + ": " + msg;
            //Console.WriteLine("Error en la linea {0}", line, "con posision {0}", charPositionInLine);
            //Console.WriteLine("Invalid Expression: {0}", msg, e);
            Errorlist.Add(errmsg);
        }

        //IAntlrErrorListener<int> implementation
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            //throw new ArgumentException("Invalid Expression: {0}", msg, e);
            var errmsg = "Linea" + x + ": " + msg;
            //Console.WriteLine("Error en la linea {0}", line, "con posision {0}", charPositionInLine);
            //Console.WriteLine("Error en la expresion: {0}", msg, e);
            Errorlist.Add(errmsg);
        }
    }
}
