namespace WindowsFormsApp3
{
    partial class Cargador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cargador));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deshacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contenidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.índiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.acercadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.herramientasToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1098, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.toolStripSeparator,
            this.toolStripMenuItem1});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.abrirToolStripMenuItem.Text = "&Abrir primero";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "&Abrir segundo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deshacerToolStripMenuItem,
            this.toolStripSeparator3});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.editarToolStripMenuItem.Text = "&Cargador ligador";
            // 
            // deshacerToolStripMenuItem
            // 
            this.deshacerToolStripMenuItem.Name = "deshacerToolStripMenuItem";
            this.deshacerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.deshacerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deshacerToolStripMenuItem.Text = "&Cargar ligar";
            this.deshacerToolStripMenuItem.Click += new System.EventHandler(this.deshacerToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.personalizarToolStripMenuItem,
            this.opcionesToolStripMenuItem});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "&Herramientas";
            // 
            // personalizarToolStripMenuItem
            // 
            this.personalizarToolStripMenuItem.Name = "personalizarToolStripMenuItem";
            this.personalizarToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.personalizarToolStripMenuItem.Text = "&Personalizar";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.opcionesToolStripMenuItem.Text = "&Opciones";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contenidoToolStripMenuItem,
            this.índiceToolStripMenuItem,
            this.buscarToolStripMenuItem,
            this.toolStripSeparator5,
            this.acercadeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ay&uda";
            // 
            // contenidoToolStripMenuItem
            // 
            this.contenidoToolStripMenuItem.Name = "contenidoToolStripMenuItem";
            this.contenidoToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.contenidoToolStripMenuItem.Text = "&Contenido";
            // 
            // índiceToolStripMenuItem
            // 
            this.índiceToolStripMenuItem.Name = "índiceToolStripMenuItem";
            this.índiceToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.índiceToolStripMenuItem.Text = "Índic&e";
            // 
            // buscarToolStripMenuItem
            // 
            this.buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            this.buscarToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.buscarToolStripMenuItem.Text = "&Buscar";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(132, 6);
            // 
            // acercadeToolStripMenuItem
            // 
            this.acercadeToolStripMenuItem.Name = "acercadeToolStripMenuItem";
            this.acercadeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.acercadeToolStripMenuItem.Text = "&Acerca de...";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 247);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1011, 235);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Registo de texto";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(22, 120);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(513, 96);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Direccion de carga";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(575, 66);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(458, 150);
            this.dataGridView2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "TABSE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mapa de memoria";
            // 
            // Cargador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 531);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Cargador";
            this.Text = "Cargador";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deshacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contenidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem índiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem acercadeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}